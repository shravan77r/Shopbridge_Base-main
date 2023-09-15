using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using Shopbridge_base.Common;
using Shopbridge_base.Filters;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[BasicAuthenticationAttribute]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> logger)
        {
            this.productService = _productService;
            _logger = logger;
        }

       
        [HttpPost]
        [Route("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(Request Rq)
        {
            try
            {
                var prd = await productService.GetProduct(Rq);
                if (prd == null)
                    return NotFound();

                return prd;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {            
            try
            {
                var prd = await productService.GetProduct(id);
                if (prd.Value == null)
                    return NotFound();

                return prd;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {            
            if (id != product.Product_Id || Convert.ToInt32(id) == 0)
                return BadRequest();

            try
            {                
                var prd = await productService.PutProduct(id,product);
                if (prd == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        
        [HttpPost]
        public async Task<Response<int?>> PostProduct(Product product)
        {
            var resp =new Response<int?>();
            resp.Status = 0;

            try
            {
                var prd = await productService.PostProduct(product);
                //var prd = await productService.PostProduct(product);
                //if (prd == null)
                //    return NotFound();

                resp.Data = prd;
                resp.Message = "Record Inserted successfully";
                resp.Count = 1;
                resp.Status = 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                resp.Data = null;
                resp.Message = ex.Message;
                resp.Count = 0;
            }
            return resp;
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (Convert.ToInt32(id) == 0)
                return BadRequest();

            try
            {
                var prd = await productService.DeleteProduct(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
    }
}
