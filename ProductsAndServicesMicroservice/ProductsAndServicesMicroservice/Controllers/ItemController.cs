using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAndServicesMicroservice.Data;
using ProductsAndServicesMicroservice.DTOs;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAndServicesMicroservice.Controllers
{
    /// <summary>
    /// ItemForSale controller which shows all items for sale 
    /// </summary>
    [ApiController]
    [Route("api/items")]
    [Produces("application/json", "application/xml")]
    public class ItemController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IAccountMockRepository accountMockRepository;
        private readonly IMapper mapper;

        public ItemController(IProductRepository productRepository, IServiceRepository serviceRepository, IAccountMockRepository accountMockRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.accountMockRepository = accountMockRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all items for sale
        /// </summary>
        /// <param name="firstName">First name of the user who added item</param>
        /// <returns>List of items for sale</returns>
        /// <remarks>
        /// Example of request \
        /// GET 'https://localhost:44349/api/itemsForSale'
        /// </remarks>
        /// <response code="200">Success answer - return items</response>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [AllowAnonymous] //svi korisnici mogu da pristupe metodi (GET je bezbedna i idempotentna metoda), tj. mogu da izlistaju sve oglase
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ItemDto>> GetItemForSales(string firstName)
        {
            try
            {
                List<Product> products;
                List<Service> services;
                if (!string.IsNullOrEmpty(firstName))
                {
                    var user = accountMockRepository.GetAccountByFirstName(firstName);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "User does not exist! Please check first name.");
                    }
                    else
                    {
                        //vracamo items for sale koje je neki user dodao
                        products = productRepository.GetProductsByAccountId(user.AccountId);
                        services = serviceRepository.GetServicesByAccountId(user.AccountId);
                    }
                }
                else
                {
                    //vracamo sve items for sale
                    products = productRepository.GetProducts();
                    services = serviceRepository.GetServices();
                }
                List<ItemDto> itemForSale = new List<ItemDto>();
                itemForSale.AddRange(mapper.Map<List<ItemDto>>(products));
                itemForSale.AddRange(mapper.Map<List<ItemDto>>(services));
                if (itemForSale.Count == 0)
                {
                    return NoContent();
                }
                return Ok(itemForSale);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns options for working with items for sale
        /// </summary>
        /// <returns>Options for a given URL</returns>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44349/api/itemsForSale'
        /// </remarks>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetItemForSalesOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        /// <summary>
        /// Returns item for sale by id
        /// </summary>
        /// <param name="itemForSaleId">Id of item for sale</param>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44349/api/itemsForSale/' \
        ///     --param  'itemForSaleId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae'
        /// </remarks>
        ///<response code="200">Success answer - return item by id</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet("{itemForSaleId}")]
        public ActionResult<ItemDto> GetItemForSaleById(Guid itemForSaleId)
        {
            try
            {
                var product = productRepository.GetProductById(itemForSaleId);
                if (product != null)
                {
                    return Ok(mapper.Map<ItemDto>(product));
                }

                var service = serviceRepository.GetServiceById(itemForSaleId);
                if (service != null)
                {
                    return Ok(mapper.Map<ItemDto>(service));
                }
                return StatusCode(StatusCodes.Status404NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
