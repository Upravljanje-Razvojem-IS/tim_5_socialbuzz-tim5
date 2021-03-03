using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheSocialBaz.Server.Data.Models;
using TheSocialBaz.Server.Models.Identity;

namespace TheSocialBaz.Server.Controllers
{
    public class IdentityController : ApiController
    {

        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Registering a Member user.
        /// </summary>
        /// <param name="model">RegisterUserRequest is a model needed for the registration.</param>
        /// <returns>Returns the created Member user.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Identity/Register
        ///     {
        ///        "username": "test",
        ///        "email": "test@test.com",
        ///        "password": "test123",
        ///        "name": "Sebastijan",
        ///        "surname": "Kokai",
        ///        "gender": "Male",
        ///        "phonenumber": "061-123-45-56"
        ///     }
        ///     --header Content-Type application/json
        ///
        /// </remarks>
        /// <response code="201">Returns the created Member user.</response>
        /// <response code="400">Returns bad request if some of required information is not sent or is not valid.</response>
        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Add claims to the user
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, model.Name.ToString()));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, model.Surname.ToString()));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, model.Email.ToString()));
                await _userManager.AddClaimAsync(user, new Claim("Claim.JoinedAt", user.CreatedAt.ToString()));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Member"));

                // Non required properties
                if (model.PhoneNumber != null)
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.MobilePhone, model.PhoneNumber.ToString()));
                if (model.Gender != null)
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Gender, model.Gender.ToString()));
                if (model.DoB != null)
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, model.DoB.ToString()));

                return Created("", user);
            }
            
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Registering a Corporate user. Member user token required in order to process the registration.
        /// </summary>
        /// <param name="model">RegisterCorporationRequestModel is a model needed for the registration.</param>
        /// <returns>Returns the created Corporation user.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Identity/RegisterCorporation
        ///     {
        ///        "username": "corpTest",
        ///        "email": "corp@test.com",
        ///        "password": "test123",
        ///        "nameofcorporation": "Test",
        ///        "foundedat": "03-08-1998"
        ///     }
        ///     --header Authorization: "Token of Member user"
        ///
        /// </remarks>
        /// <response code="201">Returns the created Corporation user.</response>
        /// <response code="400">Returns bad request if some of required information is not sent or is not valid.</response>
        [HttpPost]
        [Route(nameof(RegisterCorporation))]
        [Authorize(Roles = "Member, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterCorporation(RegisterCorporationRequestModel model)
        {
            var corporation = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            // User which created the corporate account
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userManager.CreateAsync(corporation, model.Password);

            if (result.Succeeded)
            {
                // Add claims to the corporation
                await _userManager.AddClaimAsync(corporation, new Claim(ClaimTypes.NameIdentifier, corporation.Id.ToString()));
                await _userManager.AddClaimAsync(corporation, new Claim(ClaimTypes.Name, model.NameOfCorporation.ToString()));
                await _userManager.AddClaimAsync(corporation, new Claim("Claim.FoundedAt", model.FoundedAt.ToString()));
                await _userManager.AddClaimAsync(corporation, new Claim("Claim.UserId", userId.ToString()));
                await _userManager.AddClaimAsync(corporation, new Claim(ClaimTypes.Role, "Corporate"));

                return Created("", corporation);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login for users.
        /// </summary>
        /// <param name="model">LoginRequestModel is a model needed for the registration.</param>
        /// <returns>Returns a JWT token.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Identity/Login
        ///     {
        ///        "username": "Username",
        ///        "password": "Password123"
        ///     }
        ///     --header Content-type application/json
        ///
        /// </remarks>
        /// <response code="200">Returns a token for a user if the user credentials are valid.</response>
        /// <response code="400">Returns bad request if user credentials are not valid.</response>
        [HttpPost]
        [Route(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> Login(LoginRequestModel model)
        {
            // Get the user
            var user = await _userManager.FindByNameAsync(model.Username);

            // Check if the user exists
            if(user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            // Check if the password is correct or if the account has been disabled
            if (!passwordValid)
            {
                return Unauthorized();
            }

            if(user.IsDisabled)
            {
                return Unauthorized("Your account has been disabled.");
            }

            // Get claims for the user
            var claims = await _userManager.GetClaimsAsync(user);

            var authClaims = new List<Claim>(claims);

            // Generating properties for token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generating a token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return new
            {
                Token = encryptedToken
            };
        }
    }
}
