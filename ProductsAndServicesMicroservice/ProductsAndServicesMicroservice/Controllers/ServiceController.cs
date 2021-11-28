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
using ProductsAndServicesMicroservice.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAndServicesMicroservice.Controllers
{
    /// <summary>
    /// Service controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/services")]
    [Produces("application/json", "application/xml")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IAuthHelper auth;

        public ServiceController(IServiceRepository serviceRepository, IMapper mapper, ILoggerMockRepository logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor, IAuthHelper auth)
        {
            this.serviceRepository = serviceRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.auth = auth;
        }

        /// <summary>
        /// Returns list of all services
        /// </summary>
        /// <param name="sName">Name of the service</param>
        /// <returns>List of services</returns>
        /// <remarks> 
        /// Example of request \
        /// GET 'https://localhost:44395/api/services' \
        /// </remarks>
        /// <response code="200">Success answer - return all services</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Service>> GetServices(string sName)
        {

            var services = serviceRepository.GetServices(sName);

            if (services == null || services.Count == 0)
            {
                return NoContent();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all services", null);
            return Ok(mapper.Map<List<ServiceConfirmationDto>>(services));

        }

        /// <summary>
        /// Returns service with specific serviceId
        /// </summary>
        /// <param name="serviceId">Id of one service</param>
        /// <remarks>    
        /// Example of request \
        /// GET 'https://localhost:44395/api/services/' \
        ///     --param  'serviceId = 1f4aa5b3-a67f-45c5-b519-771a7c09a944'
        /// </remarks>
        /// <response code="200">Success answer - return service by id</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{serviceId}")]
        [AllowAnonymous] 
        public ActionResult<Service> GetServiceById(Guid serviceId)
        {

            var service = serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get service by id", null);
            return Ok(mapper.Map<ServiceConfirmationDto>(service));

        }

        /// <summary>
        /// Add new service
        /// </summary>
        /// <param name="serviceDto">Model of service</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// POST 'https://localhost:44395/api/services/'\
        ///     --header 'key: Bearer Verica' \
        /// Example: \
        /// { \
        ///        "name": "Test service", \
        ///        "description": "Test description", \
        ///        "price": "5000.00 RSD", \
        ///        "accountId": "b1d1e043-85c9-4ee1-9eb7-38314c109607", \
        ///         "startDate": "2021-11-28T14:36:25.883Z", \
        ///         "endDate": "2021-11-28T14:36:25.883Z"\
        ///}
        /// </remarks>
        /// <response code="201">Returns the created service</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">There was an error on the server</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<ServiceConfirmationDto> CreateService([FromBody] ServiceCreationDto serviceDto, [FromHeader] string key)
        {
            try
            {
                
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                Service service = mapper.Map<Service>(serviceDto);
                serviceRepository.CreateService(service);
                serviceRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "New service created", null);

                string location = linkGenerator.GetPathByAction("GetServiceById", "Service", new { serviceId = service.ItemId });

                return Created(location, mapper.Map<ServiceConfirmationDto>(service));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "There is error while creating service", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="serviceUpdateDto">Model of service</param>
        /// <param name="serviceId">Service id</param>
        /// <param name="accountId">ID of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of successful request \
        /// PUT 'https://localhost:44395/api/products/'\
        ///  --header 'key: Bearer Verica' \
        ///  --param  'serviceId = 2228e12e-9e5f-46cf-f59e-08d95c4b3916' -> change this for testing\
        ///  --header 'accountId = b1d1e043-85c9-4ee1-9eb7-38314c109607' \
        /// Example: \
        /// { \
        ///        "name": "Update Test service", \
        ///        "description": "Update Test description", \
        ///        "price": "5000.00 RSD", \
        ///        "accountId": "b1d1e043-85c9-4ee1-9eb7-38314c109607", \
        ///        "startDate": "2021-11-28T14:36:25.883Z", \
        ///        "endDate": "2021-11-28T14:36:25.883Z"\
        /// } \
        /// Example of bad request \
        /// PUT 'https://localhost:44395/api/products/'\
        ///  --header 'key: Bearer Verica ' \
        ///  --param  'serviceId = 2228e12e-9e5f-46cf-f59e-08d95c4b3916' -> change this for testing\
        ///  --header 'accountId = 9888cf22-b353-4162-aedc-734ca2dc26a4' \
        /// Example: \
        /// { \
        ///        "name": "Update Test service", \
        ///        "description": "Update Test description", \
        ///        "price": "5000.00 RSD", \
        ///        "accountId": "b1d1e043-85c9-4ee1-9eb7-38314c109607" \
        ///        "startDate": "2021-11-28T14:36:25.883Z", \
        ///        "endDate": "2021-11-28T14:36:25.883Z"\
        /// } \
        /// </remarks>
        /// <response code="200">Success answer - updated service</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{serviceId}")]
        public ActionResult<Product> UpdateService([FromBody] ServiceUpdateDto serviceUpdateDto, Guid serviceId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
             
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

               
                if (serviceUpdateDto.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Not allowed! User does not have permission!");
                }

                var oldService = serviceRepository.GetServiceById(serviceId);
                if (oldService == null)
                {
                    return NotFound();
                }
                Service newService = mapper.Map<Service>(serviceUpdateDto);

                serviceRepository.UpdateService(oldService, newService);

                serviceRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Product updated", null);

                return Ok(oldService);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete services
        /// </summary>
        /// <param name="serviceId">Id of service</param>
        /// <param name="accountId">ID of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44395/api/services/'\
        ///  --header 'key: Bearer Verica' \
        ///  --param  'serviceId = 2228e12e-9e5f-46cf-f59e-08d95c4b3916' -> change this for testing \
        ///  --header 'accountId = b1d1e043-85c9-4ee1-9eb7-38314c109607' \
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{serviceId}")]
        public IActionResult DeleteService(Guid serviceId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
                
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var service = serviceRepository.GetServiceById(serviceId);

              
                if (service.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Not allowed!");
                }

                if (service == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no service!");
                }

                serviceRepository.DeleteService(serviceId);
                serviceRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns implemented options for working with service
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request
        /// OPTIONS 'https://localhost:44395/api/services'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetServiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
