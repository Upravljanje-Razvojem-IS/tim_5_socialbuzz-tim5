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
    /// Product controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/products")]
    [Produces("application/json", "application/xml")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IAuthHelper auth;

        public ProductController(IProductRepository productRepository, IMapper mapper, ILoggerMockRepository logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor, IAuthHelper auth)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.auth = auth;
        }

        /// <summary>
        /// Returns list of all products
        /// </summary>
        /// <param name="pName">Name of the product</param>
        /// <returns>List of products</returns>
        /// <remarks> 
        /// Example of request \
        /// GET 'https://localhost:44395/api/products' \
        /// </remarks>
        /// <response code="200">Success answer - return all products</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Product>> GetProducts(string pName)
        {

            var products = productRepository.GetProducts(pName);
            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all products", null);
            return Ok(mapper.Map<List<ProductConfirmationDto>>(products));

        }

        /// <summary>
        /// Returns product with specific productId
        /// </summary>
        /// <param name="productId">Id of one product</param>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44395/api/products/' \
        ///     --param  'productId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae'
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{productId}")]
        [AllowAnonymous] 
        public ActionResult<Product> GetProductById(Guid productId)
        {

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get product by id", null);
            return Ok(mapper.Map<ProductConfirmationDto>(product));

        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Model of product</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// POST 'https://localhost:44395/api/products/'\
        /// --header 'key: Bearer Verica' \
        /// Example: \
        /// { \
        ///        "name": "Test", \
        ///        "description": "Test description", \
        ///        "price": "100000.00 RSD", \
        ///        "accountId": "f2d8362a-124f-41a9-a22b-6e35b3a2953c", \
        ///        "weight": "500g" \
        ///        "quantity": "3",\
        ///        "productionDate": "2021-11-28T16:52:52.828Z"\
        /// }
        /// </remarks>
        /// <response code="201">Returns the created product</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">There was an error on the server</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<ProductConfirmationDto> CreateProduct([FromBody] ProductCreationDto productDto, [FromHeader] string key)
        {
            try
            {
               
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                Product product = mapper.Map<Product>(productDto);
                productRepository.CreateProduct(product);
                productRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "New product created", null);

                string location = linkGenerator.GetPathByAction("GetProductById", "Product", new { productId = product.ItemId });

                return Created(location, mapper.Map<ProductConfirmationDto>(product));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "There is error while creating product", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="productUpdateDto">Model of product</param>
        /// <param name="productId">Product id</param>
        /// <param name="accountId">ID of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of successful request \
        /// PUT 'https://localhost:44395/api/products/'\
        ///  --header 'key: Bearer Verica' \
        ///  --param  'productId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae' \
        ///  --header 'accountId = f2d8362a-124f-41a9-a22b-6e35b3a2953c' \
        /// Example: \
        /// { \
        ///     "name": "Mobilni telefon Huawei P40 Pro",
        ///     "description": "Huawei P30 Pro, 16gb RAM, 64gb memorije, dual sim.",
        ///     "price": "150000.00 RSD",
        ///     "accountId": "f2d8362a-124f-41a9-a22b-6e35b3a2953c",
        ///     "weight": "165g" \
        ///     "quantity": "3",\
        ///     "productionDate": "2021-11-28T16:52:52.828Z"\
        /// } \
        /// Example of bad request \
        /// PUT 'https://localhost:44395/api/products/'\
        ///  --header 'key: Bearer Verica' \
        ///  --param  'productId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae' \
        ///  --header 'accountId = 1bc6929f-0e75-4bef-a835-7dbb50d9e41a' -> this user can not change product \
        /// Example: \
        /// { \
        ///     "name": "Mobilni telefon Huawei P40 Pro",
        ///     "description": "Huawei P30 Pro, 16gb RAM, 64gb memorije, dual sim.",
        ///     "price": "150000.00 RSD",
        ///     "accountId": "f2d8362a-124f-41a9-a22b-6e35b3a2953c",
        ///     "weight": "165g" \
        ///     "quantity": "3",\
        ///     "productionDate": "2021-11-28T16:52:52.828Z"\
        /// }
        /// </remarks>
        /// <response code="200">Success answer - updated product</response>
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
        [HttpPut("{productId}")]
        public ActionResult<Product> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto, Guid productId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
                
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

              
                if (productUpdateDto.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Not allowed! User does not have permission!");
                }

                var oldProduct = productRepository.GetProductById(productId);
                if (oldProduct == null)
                {
                    return NotFound();
                }
                Product newProduct = mapper.Map<Product>(productUpdateDto);

                productRepository.UpdateProduct(oldProduct, newProduct);

                productRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Product updated", null);

                return Ok(oldProduct);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="accountId">Id of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44395/api/products/'\
        ///  --header 'key: Bearer Verica' \
        ///  --param  'productId = 3ca21d04-26fd-494d-a1fc-08d95c4724a9' -> change this for testing\
        ///  --header 'accountId = f2d8362a-124f-41a9-a22b-6e35b3a2953c' \
        /// </remarks>
        /// <response code="204">Deleted product</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(Guid productId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
               
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var product = productRepository.GetProductById(productId);

                
                if (product.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Not allowed!");
                }

                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no product!");
                }

                productRepository.DeleteProduct(productId);
                productRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns implemented options for working with product
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44395/api/products'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetProductOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
