using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Soru2;
using Soru2.ViewModels;

namespace Soru2.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductApiController : ApiController
    {
        public InteraktifCSModel db = new InteraktifCSModel();

        [HttpGet]
        [Route("GetAll")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        [HttpGet]
        [Route("Get")]
        public Product GetProduct(Guid id)
        {
            Product product = db.Products.Where(c => c.ProductID == id).SingleOrDefault();

            return product;
        }

        [HttpPost]
        [Route("Add")]
        public ResultState InsertProduct(Product _product)
        {
            Product product = new Product()
            {
                ProductID = Guid.NewGuid(),
                CategoryID = _product.CategoryID,
                ProductName = _product.ProductName,
                UnitsInStock = _product.UnitsInStock,
                Price = _product.Price,
                PriceVat = (_product.Price == null ? 0 : _product.Price * (decimal)1.18)

            };

            db.Products.Add(product);

            try
            {
                db.SaveChanges();
                return new ResultState { IsSuccess = true, Message = "Kayıt Ekleme Başarılı" };
            }
            catch (DbUpdateException)
            {
                if (ProductExists(_product.ProductID))
                {
                    return new ResultState { IsSuccess = false, Message = "Ürün Zaten Mevcut" };
                }
                else
                {
                    return new ResultState { IsSuccess = false, Message = "Kayıt Ekleme Başarısız" };
                }
            }
        }

        [HttpPut]
        [Route("Update")]
        public ResultState UpdateProduct(Product _product)
        {
            var product = db.Products.Where(c => c.ProductID == _product.ProductID).FirstOrDefault();
            product.ProductID = _product.ProductID;
            product.CategoryID = _product.CategoryID;
            product.ProductName = _product.ProductName;
            product.UnitsInStock = _product.UnitsInStock;
            product.Price = _product.Price;
            product.PriceVat = (_product.Price == null ? 0 : _product.Price * (decimal)1.18);
            db.SaveChanges();

            return _product.ProductID == null ? new ResultState { IsSuccess = false, Message = "Id değeri gönderilmemiş" } : new ResultState { IsSuccess = true, Message = "Kayıt Güncelleme Başarılı" };

        }

        [HttpDelete]
        [Route("Delete")]
        public ResultState DeleteProduct(Guid id)
        {
            Product product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();

            return new ResultState { IsSuccess = true, Message = "Kayıt Silme Başarılı" };
        }

        private bool ProductExists(Guid id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }


    }
}