using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ProductsAndServicesMicroservice.Auth;
using ProductsAndServicesMicroservice.Data;
using ProductsAndServicesMicroservice.DTOs;
using ProductsAndServicesMicroservice.Entities;
using ProductsAndServicesMicroservice.Exceptions;
using ProductsAndServicesMicroservice.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAndServicesMicroservice.Controllers
{
    /// <summary>
    /// PastPrice controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/pastPrices")]
    [Produces("application/json", "application/xml")]
    public class PastPriceController : ControllerBase
    {
        private readonly IPastPriceRepository pastPriceRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IProductRepository productRepository;
        private readonly IServiceRepository serviceRepository;

        private readonly IAuthHelper auth;

        public PastPriceController(IPastPriceRepository pastPriceRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerMockRepository logger, IHttpContextAccessor contextAccessor, IProductRepository productRepository, IServiceRepository serviceRepository, IAuthHelper auth)
        {
            this.pastPriceRepository = pastPriceRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.auth = auth;
        }

        /// <summary>
        /// Returns list of all past prices
        /// </summary>
        /// <returns>List of all past prices</returns>
        /// <remarks> 
        /// Example of request \
        /// GET 'https://localhost:44349/api/pastPrices' \
        /// --header 'key: Bearer Bojana'
        /// </remarks>
        /// <param name="key">Authorization Key Value</param>
        /// <response code="200">Return list of past prices</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PastPrice>> GetPastPrices([FromHeader] string key)
        {
            //pristup metodi imaju samo autorizovani korisnici
            if (!auth.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
            }

            var pastPrices = pastPriceRepository.GetPastPrices();

            if (pastPrices == null || pastPrices.Count == 0)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all past prices", null);
            return Ok(pastPrices);

        }

        /// <summary>
        /// Returns past price by itemForSaleId
        /// </summary>
        /// <param name="itemForSaleId">Id of item for sale</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44349/api/pastPrices/' \
        ///     --param  'itemForSaleId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae' \
        ///     --header 'key: Bearer Bojana' 
        /// </remarks>
        /// <response code="200">Success answer - return past price by id</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Error on the server</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{itemForSaleId}")]
        public ActionResult<List<PastPrice>> GetPastPriceById(Guid itemForSaleId, [FromHeader] string key)
        {

            //pristup metodi imaju samo autorizovani korisnici
            if (!auth.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
            }

            var pastPrice = pastPriceRepository.GetPastPriceByItemId(itemForSaleId);
            if (pastPrice == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get past price by id", null);
            return Ok(pastPrice);

        }

        /// <summary>
        /// Create past prices
        /// </summary>
        /// <param name="pastPriceCreationDto">Model of past price</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// POST 'https://localhost:44349/api/pastPrices/'\
        ///     --header 'key: Bearer Bojana' \
        /// Example: \
        /// {   
        ///  "ItemForSaleId": "2d53fc22-eac4-43bb-8f55-d2b8495603cc", \
        ///  "Price": "3000.00RSD" \
        /// } 
        /// </remarks>
        /// <response code="201">Created past price</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<PastPrice> CreatePastPrice([FromBody] PastPriceCreationDto pastPriceCreationDto, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                PastPrice pastPrice = mapper.Map<PastPrice>(pastPriceCreationDto);

                Product product = productRepository.GetProductById(pastPrice.ItemId);
                Service service = serviceRepository.GetServiceById(pastPrice.ItemId);
                //prosla cena mora da se odnosi na neki proizvod ili uslugu koji vec postoje
                if (product == null && service == null)
                {
                    throw new DatabaseException("Item for sale with that id does not exists!");
                }

                pastPriceRepository.CreatePastPrice(pastPrice);
                pastPriceRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "New past price created", null);

                var location = linkGenerator.GetPathByAction("GetPastPriceById", "PastPrice", new { ItemForSaleId = pastPrice.ItemId });
                return Created(location, pastPrice);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Create error", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update past price
        /// </summary>
        /// <param name="pastPriceDto">Model of past price</param>
        /// <param name="pastPriceId">Past price id</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// PUT 'https://localhost:44349/api/pastPrices/'\
        ///  --header 'key: Bearer Bojana' \
        ///  --param  'pastPriceId = 9' \
        /// Example: \
        /// {   
        ///  "ItemForSaleId": "4f29d0a1-a000-4b56-9005-1a40ffcea3ae", \
        ///  "Price": "40000.00RSD \
        /// } 
        /// </remarks>
        /// <response code="200">Success answer - updated price</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{pastPriceId}")]
        public ActionResult<PastPrice> UpdatePastPrice([FromBody] PastPriceDto pastPriceDto, int pastPriceId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var oldPastPrice = pastPriceRepository.GetPastPriceById(pastPriceId);
                if (oldPastPrice == null)
                {
                    return NotFound();
                }
                PastPrice newPastPrice = mapper.Map<PastPrice>(pastPriceDto);

                Product product = productRepository.GetProductById(newPastPrice.ItemId);
                Service service = serviceRepository.GetServiceById(newPastPrice.ItemId);

                //prosla cena mora da se odnosi na neki proizvod ili uslugu koji vec postoje
                if (product == null && service == null)
                {
                    throw new DatabaseException("Item for sale with that id does not exists!");
                }

                pastPriceRepository.UpdatePastPrice(oldPastPrice, newPastPrice);
                pastPriceRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Updated past price", null);

                return Ok(oldPastPrice);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete past price
        /// </summary>
        /// <param name="pastPriceId">Id of past price</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44349/api/pastPrices/'\
        ///  --param  'pastPriceId = 10'
        ///  --header 'key: Bearer Bojana' \
        /// </remarks>
        /// <response code="204">Success answer - deleted price</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{pastPriceId}")]
        public IActionResult DeletePastPrice(int pastPriceId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var pastPrice = pastPriceRepository.GetPastPriceById(pastPriceId);
                if (pastPrice == null)
                {
                    return NotFound();
                }
                pastPriceRepository.DeletePastPrice(pastPriceId);

                pastPriceRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns implemented options for working with past price
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44349/api/pastPrices'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous] //svi mogu da pristupe
        public IActionResult GetPastPriceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
