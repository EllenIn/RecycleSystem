using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public string GetUnacceptOrders(int page, int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> demandOrders = _orderManageService.GetOrders(page, limit, out count, queryInfo);
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
        public string GetRunningOrders(int page, int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> runningOrders = _orderManageService.GetRuningOrder(page, limit, out count, queryInfo);
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
        public string GetFinishedOrder(int page, int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> finishedOrder = _orderManageService.GetFinishedOrder(page, limit, out count, queryInfo);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = finishedOrder
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        public IActionResult ViewOrder(string oid)
        {
            ViewBag.Order = _orderManageService.GetOrderByOID(oid);
            return View();
        }
        public JsonResult AcceptOrder(string oid)
        {
            string message;
            string userId;
            try
            {
                userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    message = "未登录！或登录已失效！";
                    return Json(message);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message);
            }
            _orderManageService.AcceptOrder(oid, userId, out message);
            return Json(message);
        }
        public IActionResult VerifyOrders()
        {
            return View();
        }
        public string VerifyGetUnVerifyOrderList(int page, int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<OrderOutput> Orders = _orderManageService.GetUnVerifyOrderList(page, limit, out count, queryInfo);
            DataResult<IEnumerable<OrderOutput>> data = new DataResult<IEnumerable<OrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = Orders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        #region 发布需求订单、查看我的订单、撤销订单
        public IActionResult ViewReleasedOrders()
        {
            return View();
        }
        public string GetMyOrders(int page, int limit, string queryInfo)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return "未登录！或登录已失效";
            }
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> Orders = _orderManageService.GetMyDemandOrders(page, limit, out count, queryInfo, userId);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = Orders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }

        public IActionResult ReleaseOrder()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReleaseOrder(DemandOrderInput demandOrderInput)
        {
            string msg;
            demandOrderInput.EnterpriseId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(demandOrderInput.EnterpriseId))
            {
                msg = "未登录！或登录已失效！";
                return Json(msg);
            }
            if (string.IsNullOrEmpty(demandOrderInput.CategoryId) || string.IsNullOrEmpty(demandOrderInput.Unit))
            {
                msg = "必须选择类别与单位！";
                return Json(msg);
            }
            if ((demandOrderInput.Name.Contains("铁") && demandOrderInput.Unit != "G1001")|| (demandOrderInput.Name.Contains("纸") && demandOrderInput.Unit != "G1002") || (demandOrderInput.Name.Contains("塑料") && demandOrderInput.Unit != "G1003") || (demandOrderInput.Name.Contains("玻璃瓶") && demandOrderInput.Unit != "G1004"))
            {
                msg = "所选类目不正确，不与物品们相匹配！";
                return Json(msg);
            }
            _orderManageService.ReleaseOrder(demandOrderInput, out msg);
            return Json(msg);
        }
        [HttpPost]
        public JsonResult WithdrewMyApplication(string oid)
        {
            string msg;
            _orderManageService.WithdrewMyApplication(oid, out msg);
            return Json(msg);
        }
        #endregion
        public IActionResult ViewAllOrders()
        {
            return View();
        }
        public string GetAllOrdersList(int page, int limit, string queryInfo)
        {
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> demandOrders = _orderManageService.GetAllOrders(page, limit, out count, queryInfo);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = demandOrders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        #region 特殊请求撤销订单
        public IActionResult SpecialRequest()
        {
            return View();
        }
        public string GetMyRuningOrders(int page, int limit, string queryInfo)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return "未登录！或登录已失效";
            }
            if (!string.IsNullOrEmpty(queryInfo))
            {
                queryInfo = queryInfo.Trim();
            }
            int count;
            IEnumerable<DemandOrderOutput> demandOrders = _orderManageService.GetMyRunningDemandOrders(page, limit, out count, queryInfo,userId);
            DataResult<IEnumerable<DemandOrderOutput>> data = new DataResult<IEnumerable<DemandOrderOutput>>
            {
                msg = "获取成功！",
                code = 0,
                count = count,
                data = demandOrders
            };
            return JsonNetHelper.SerialzeoJsonForCamelCase(data);
        }
        public IActionResult ApplyingWithdrew(string oid)
        {
            HttpContext.Session.SetString("OID", oid);
            return View();
        }
        public JsonResult GetOrderInfoByOID()
        {
            string oid = HttpContext.Session.GetString("OID");
            DemandOrderOutput output = _orderManageService.GetOrderByOID(oid);
            HttpContext.Session.Remove("OID");
            return Json(output);
        }
        public JsonResult WithdrewMyApplicationBySpecial(DemandOrderInput demandOrderInput)
        {
            string msg;
            demandOrderInput.UserId = HttpContext.Session.GetString("UserId");
            _orderManageService.WithdrewMyApplicationBySpecial(demandOrderInput, out msg);
            return Json(msg);
        }
        public IActionResult ViewSpecialApplyingOrder(string oid)
        {
            ViewBag.Order = _orderManageService.GetOrderByOID(oid);
            return View();
        }
        #endregion
        public IActionResult ApprovalSpecialOrderWithdrew()
        {
            return View();
        }

    }
}
