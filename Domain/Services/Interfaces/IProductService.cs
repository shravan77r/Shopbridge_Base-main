using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Shopbridge_base.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shopbridge_base.Common;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ActionResult<IEnumerable<Product>>> GetProduct(Request Rq);
        public Task<ActionResult<Product>> GetProduct(int id);
        public Task<ActionResult<Product>> PutProduct(int id, Product product);
        public Task<int> PostProduct(Product product);
        public Task<bool> DeleteProduct(int id);

    }
}
