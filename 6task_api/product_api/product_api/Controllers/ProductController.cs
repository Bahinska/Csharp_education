using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using product_api.Models;
using product_api.Repository.IRepository;
using System.Data;
using System.Net;
using Serilog;
using product_api.Log;

namespace product_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _db;
        private readonly ILog _logger;
        public ProductsController(IProductRepository db, ILog log)
        {
            _db = db;
            _response = new();
            _logger = log;
        }

        [HttpGet]
        [Authorize]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllProducts()
        {
            try
            {

                List<Product> productList;
                productList =  _db.GetAllProducts();
                var products = from pr in productList
                               select pr.ProductDto();
                _response.Result = new List<ProductDto>(products);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogWrite("SUCCESS Get all products");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
                _logger.LogWrite("FAILED Get all products");
            }
            return _response;
        }
        //[HttpGet("GetAllPublished")]
        //[Authorize]
        ////[ResponseCache(CacheProfileName = "Default30")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<APIResponse>> GetAllPublished()
        //{
        //    try
        //    {
        //        IEnumerable<Product> productList;
        //        productList = _db.GetPublished();
        //        _response.Result = new List<Product>(productList);
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _logger.LogWrite("SUCCESS Get all publiched");
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //        _logger.LogWrite("FAILED Get all products");
        //    }
        //    return _response;
        //}
        [HttpGet("{id:int}", Name = "GetProduct")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(VillaDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetProduct(int id)
        {
            try
            {
                var product = await _db.GetAsync(id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No product with such id.");
                    _logger.LogWrite($"FAILED Get product with id {id}");
                    return NotFound(_response);
                }
                _response.Result = product;
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogWrite($"SUCCESS Get product with id {id}");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _logger.LogWrite($"FAILED Get product with id {id}");
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductDto create)
        {
            try
            {
                if (await _db.GetAsync(create.Id) != null)
                {
                    _response.ErrorMessages.Add("Product with such id already exist");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWrite($"FAILED Create product");
                    return BadRequest(_response);
                }

                if (create == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No data.");
                    _logger.LogWrite($"FAILED Create product");
                    return BadRequest(_response);
                }

                await _db.CreateAsync(create.Product());
                _response.Result = create;
                _response.StatusCode = HttpStatusCode.Created;
                _logger.LogWrite($"SUCCESS Create product");
                return CreatedAtRoute("GetProduct", new { id = create.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _logger.LogWrite($"FAILED Create product");
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [Authorize(Roles = "Admin,Manager")]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                var prod = await _db.GetAsync(id);
                if (prod == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Product not found");
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _logger.LogWrite("FAILED Delete product");
                    return NotFound();
                }
                await _db.RemoveAsync(prod);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _logger.LogWrite("SUCCESS Delete product");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("FAILED Delete product");
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductDto update)
        {
            try
            {
                if (update == null || id != update.Id)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Update failed.");
                    _response.IsSuccess = false;
                    _logger.LogWrite($"FAILED Update product with id {id}");
                    return BadRequest(_response);
                }
                await _db.UpdateAsync(update.Product());
                _response.StatusCode = HttpStatusCode.NoContent;
                _logger.LogWrite($"SUCCESS Update product with id {id}");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogWrite($"FAILED Update product with id {id}");
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        //[HttpPut("{id:int}", Name = "Publish")]
        //public async Task<ActionResult<APIResponse>> Publish(int id)
        //{
        //    var prod = await _db.GetAsync(id);
        //    if (prod == null)
        //    {
        //        _response.IsSuccess=false;
        //        _response.StatusCode= HttpStatusCode.BadRequest;
        //        _response.ErrorMessages.Add("No published product with such id");
        //        _logger.LogWrite($"FAILED Publich with id {id}");
        //        return BadRequest(_response);
        //    }
        //    else
        //    {
        //        prod.State = "Published";
        //        await _db.UpdateAsync(prod);
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _logger.LogWrite($"SUCCESS Publich product with id {id}");
        //        return Ok(_response);
        //    }

        //}

    }
}
