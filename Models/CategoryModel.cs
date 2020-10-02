using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{
    public interface ICategoryModel
    {
        List<CategoryVM> GetCategoryVMs(watchContext context);
        CategoryVM GetCategoryVMByID(watchContext context, int idCate);
    }

    public class CategoryModel : ICategoryModel
    {
        public List<CategoryVM> GetCategoryVMs(watchContext context)
        {
            List<Category> asset = null;
            using (CategoryDao db = new CategoryDao())
                asset = db.GetList(context);
            return Helper.LsObjectToLsVM<CategoryVM, Category>(asset);
        }

        public CategoryVM GetCategoryVMByID(watchContext context, int idCate)
        {
            Category asset = null;
            using (CategoryDao db = new CategoryDao())
                asset = db.Get(context, idCate);
            return Helper.ObjectToVM<CategoryVM, Category>(asset);
        }
    }

    public class CategoryVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //SEO
        public string SeoImage { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }
}