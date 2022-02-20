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
    [RoutePrefix("api/Category")]
    public class CategoryApiController : ApiController
    {
        public InteraktifCSModel db = new InteraktifCSModel();

     
        [HttpGet]
        [Route("GetAll")]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

     
        [HttpGet]
        [Route("Get")]
        public Category GetCategory(Guid id)
        {
            Category category = db.Categories.Where(c=>c.CategoryID==id).SingleOrDefault();            

            return category;
        }

        [HttpPost]
        [Route("Add")]
        public ResultState InsertCategory(Category _category)
        {
            _category.CategoryID = Guid.NewGuid();
            db.Categories.Add(_category);

            try
            {
                db.SaveChanges();
                return new ResultState { IsSuccess = true, Message = "Kayıt Ekleme Başarılı" };
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(_category.CategoryID))
                {
                    return new ResultState { IsSuccess = false, Message = "Kategori Zaten Mevcut" };
                }
                else
                {
                    return new ResultState { IsSuccess = false, Message = "Kayıt Ekleme Başarısız" };
                }
            }
        }

        [HttpPut]
        [Route("Update")]
        public ResultState UpdateCategory(Category _category)
        {
            var category = db.Categories.Where(c => c.CategoryID == _category.CategoryID).FirstOrDefault();
            category.CategoryID = _category.CategoryID;
            category.CategoryName = _category.CategoryName;
            category.IsActive = _category.IsActive;
            category.Order = _category.Order;
            db.SaveChanges();
            return _category.CategoryID == null ? new ResultState { IsSuccess = false, Message = "Id değeri gönderilmemiş" } : new ResultState { IsSuccess = true, Message = "Kayıt Güncelleme Başarılı" };
        }

        [HttpDelete]
        [Route("Delete")]
        public ResultState DeleteCategory(Guid id)
        {
            Category category = db.Categories.Find(id);           

            db.Categories.Remove(category);
            db.SaveChanges();
            
            return new ResultState { IsSuccess = true, Message = "Kayıt Silme Başarılı" };
        }
               
        private bool CategoryExists(Guid id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}