using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Common;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly Shopbridge_Context dbcontext;
        public ProductService(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(Request Rq)
        {
            if (Rq.StartPage < 1)
                Rq.StartPage = 1;

            int reccounts = await dbcontext.Products.CountAsync();
            int recskip = (Rq.StartPage - 1) * Rq.PageSize;

            return await dbcontext.Products.Where(o => o.Status == Rq.IsActive).Skip(recskip).Take(Rq.PageSize).ToListAsync();
            //return await dbcontext.Products.ToListAsync();
        }
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await dbcontext.Products.FindAsync(id);
        }
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            dbcontext.Entry(product).State = EntityState.Modified;
            await dbcontext.SaveChangesAsync();
            return product;
        }
        public async Task<int> PostProduct(Product product)
        {
            //int a = 0;
            //int b = 0;
            //int c = a / b;

            dbcontext.Products.Add(product);
            await dbcontext.SaveChangesAsync();
            return product.Product_Id;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var prd = await dbcontext.Products.FindAsync(id);
            if (prd == null)
            {
                return false;
            }
            else
            {
                dbcontext.Remove(prd);
                await dbcontext.SaveChangesAsync();
                return true;
            }
        }
    }
}
