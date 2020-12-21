using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecycleSystem.Data.Data.OrderManageDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using Senkuu.MaterialSystem.Model;
using Senkuu.MaterialSystem.Utility;

namespace RecycleSystem.MVC.Controllers
{
    public class OrderManageController : Controller
    {
        private readonly IOrderManageService _orderManageService;
        public OrderManageController(IOrderManageService orderManageService)
        {
            _orderManageService = orderManageService;
        }
        public IActionResult Index()
        {
            //未受理订单页面由员工可访问查看
            return View();
        }
        public string GetUnacceptOrders(int page,int limit,string queryinfo)
        {
            if (!string.IsNullOrEmpty(queryinfo))
            {
                queryinfo = queryinfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> demandOrders = _orderManageService.GetOrders(page, limit, out count, queryinfo);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = demandOrders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);

        }
        public IActionResult Runing()
        {
            return View();
        }
        public string GetRunningOrders(int page, int limit, string queryinfo)
        {
            if (!string.IsNullOrEmpty(queryinfo))
            {
                queryinfo = queryinfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> runningOrders = _orderManageService.GetRuningOrder(page, limit, out count, queryinfo);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = runningOrders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        public IActionResult Finished()
        {
            return View();
        }
        public string GetFinishedOrder(int page, int limit, string queryinfo)
        {
            if (!string.IsNullOrEmpty(queryinfo))
            {
                queryinfo = queryinfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> finishedOrder = _orderManageService.GetFinishedOrder(page, limit, out count, queryinfo);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = finishedOrder
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
    }
}
