using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecycleSystem.Data.Data.WareHouseDTO;
using RecycleSystem.IService;
using Senkuu.MaterialSystem.Model;
using Senkuu.MaterialSystem.Utility;

namespace RecycleSystem.MVC.Controllers
{
    public class WareHouseController : Controller
    {
        private readonly IWareHouseService _wareHouseService;
        public WareHouseController(IWareHouseService wareHouseService)
        {
            _wareHouseService = wareHouseService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string GetInputInfo(int page,int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<GoodsOutput> inputInfo = _wareHouseService.GetGoodsInputInfo(page, limit, out count, queryInfo);
            DataResult<IEnumerable<GoodsOutput>> data = new DataResult<IEnumerable<GoodsOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = inputInfo
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        public IActionResult MultipleImport(IFormFile file)
        {
            string strMsg;
            string userId = HttpContext.Session.GetString("UserId");
            DataTable table = new DataTable();
            table = ExcelHelper.ExcelToDataTable(file.OpenReadStream(), Path.GetExtension(file.FileName), out strMsg);
            IEnumerable<GoodsInput> data = ExcelHelper.ConvertToList(table);
            return null;
        }
    }
}
