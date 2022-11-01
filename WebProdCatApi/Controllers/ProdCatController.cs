using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFS.ProdCat.App.CategoryApp;
using PFS.ProdCat.App.ProductApp;
using PFS.ProdCat.Model.Dtos;

namespace PFS.ProdCat.Api.Controllers
{

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProdCatController : ControllerBase
    {
        private readonly IProductApp _productApp;
        private readonly ICategoryApp _categoryApp;

        public ProdCatController(IProductApp productApp, ICategoryApp categoryApp)
        {
            _productApp = productApp;
            _categoryApp = categoryApp;
        }

        /// <summary>
        /// Creates a category
        /// </summary>
        /// <returns></returns>
        [HttpPost("/category", Name = "CreateCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateCategoryDto category)
        {

            var response = await _categoryApp.CreateCategoryAsync(category);

            if (!response.IsSuccess) return NotFound(response);

            return Ok(response);

        } 
        
        /// <summary>
        /// Gets list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("/category", Name = "GetCategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategories()
        {

            var response = await _categoryApp.GetCategoriesAsync();

            if (!response.IsSuccess) return NotFound(response);

            return Ok(response);

        }  

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <returns></returns>
        [HttpPost("/product", Name = "CreateProduct")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
        {

            var response = await _productApp.CreateProductAsync(product);

            if (!response.IsSuccess) return NotFound(response);

            return Ok(response);

        }
        
        /// <summary>
        /// Gets a product by product code
        /// </summary>
        /// <returns></returns>
        [HttpGet("/product/{code}", Name = "GetProductByCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductByCode(string code)
        {

            var response = await _productApp.GetProductByCodeAsync(code);

            if (!response.IsSuccess) return NotFound(response);

            return Ok(response);

        } 
        
        /// <summary>
        /// Gets products by expiry date range
        /// </summary>
        /// <returns></returns>
        [HttpGet("/products", Name = "GetProducts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQueryParam productQuery)
        {

            var response = await _productApp.GetProductsAsync(productQuery);

            if (!response.IsSuccess) return NotFound(response);

            return Ok(response);

        }
        
    }
}