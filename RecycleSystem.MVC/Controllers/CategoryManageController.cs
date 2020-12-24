using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecycleSystem.Data.Data.CategoryDTO;
using RecycleSystem.IService;
using Senkuu.MaterialSystem.Model;
using Senkuu.MaterialSystem.Utility;

namespace RecycleSystem.MVC.Controllers
{
    public class CategoryManageController : Controller
    {
        private ICategoryManageService _categoryManageService;
        public CategoryManageController(ICategoryManageService categoryManageService)
        {
            _categoryManageService = categoryManageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string GetCategoriesList(int page,int limit,string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<CategoryOutput> categories = _categoryManageService.GetCategories(page, limit, out count, queryInfo);
            DataResult<IEnumerable<CategoryOutput>> data = new DataResult<IEnumerable<CategoryOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = categories
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        public JsonResult GetCategories()
        {
            return Json(_categoryManageService.GetCategories());
        }
    }
}
