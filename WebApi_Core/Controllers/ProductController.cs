using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Core.Models;
using WebApi_Core.ViewModels;

namespace WebApi_Core.Controllers
{
    
    [ApiController]
    [Route(Constants.ProductCRUD_Route)]
    public class ProductController : ControllerBase
    {
        InteraktifCSContext _db;
        public ProductController(InteraktifCSContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            List<Products> productlist = await _db.Set<Products>().ToListAsync();
            return productlist;            
        }       

       
        [HttpGet("{id}")]       
        public async Task<ActionResult<Products>> GetProductsById(Guid id)
        {
            var product = await _db.Set<Products>().FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }


        [HttpPost]

        public ProductVM PostCreateProduct(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(new Products()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    UnitsInStock = product.UnitsInStock,
                    Price = product.Price,
                    PriceVat = (product.Price == null ? 0 : product.Price * (decimal)1.18)

                });
                _db.SaveChanges();
            }
               
            return product;
        }     

        [HttpPut]
        public ProductVM UpdateProduct(Guid id,ProductVM _product)
        {
            Products product=_db.Products.Find(id);
            product.ProductName = _product.ProductName;
            product.CategoryId = _product.CategoryId;
            product.UnitsInStock = _product.UnitsInStock;
            product.Price = _product.Price;
            product.PriceVat = (_product.Price == null ? 0 : _product.Price * (decimal)1.18);
                       
            _db.Entry(product).State=EntityState.Modified;
            _db.SaveChangesAsync();
            return _product;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(Guid id)
        {
            var silinecek = await _db.Products.FindAsync(id);
            if (silinecek == null)
            {
                return NotFound();
            }

            _db.Products.Remove(silinecek);
            await _db.SaveChangesAsync();

            return silinecek;

        }
    }
}
