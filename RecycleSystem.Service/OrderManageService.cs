using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.OrderManageDTO;
using RecycleSystem.Data.Data.WareHouseDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using RecycleSystem.Ulitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecycleSystem.Service
{
    public class OrderManageService : IOrderManageService
    {
        private readonly DbContext _dbContext;
        public OrderManageService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AcceptOrder(string oid, string userId, out string message)
        {
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>();
            DemandOrderInfo orderInfo = orderInfos.Where(o => o.Oid == oid).FirstOrDefault();
            if (orderInfo != null)
            {

                orderInfo.UserId = userId;
                orderInfo.Status = (int?)TypeEnum.DemendOrderStatus.Accepted;
                _dbContext.Set<DemandOrderInfo>().Update(orderInfo);

                //改变需求订单状态的同时，要往订单表中也插入一条数据。方便后续内部管理者查看
                //查询是否已经有人接取订单（此功能类似抢购，可能同时并发，因现今技术问题，暂时使用此方法来判断）
                OrderInfo order = _dbContext.Set<OrderInfo>().Where(o => o.OriginalOrderId == orderInfo.Oid).FirstOrDefault();
                if (order != null)
                {
                    message = "该订单已被人接取！";
                    return false;
                }
                OrderInfo newOrder = new OrderInfo
                {
                    InstanceId = "O" + DateTime.Now.ToString("yyyyMMddHHmmssffff"),//要求要日期的时分秒以及毫秒
                    CategoryId = orderInfo.CategoryId,
                    Name = orderInfo.Name,
                    OriginalOrderId = orderInfo.Oid,
                    EnterpriseId = orderInfo.EnterpriseId,
                    Num = orderInfo.Num,
                    Unit = orderInfo.Unit,
                    ReceiverId = orderInfo.UserId,
                    Status = (int)TypeEnum.OrderStatus.Running,
                    AddTime = DateTime.Now,
                    Url = null
                };
                _dbContext.Set<OrderInfo>().Add(newOrder);
                if (_dbContext.SaveChanges() > 0)
                {
                    message = "接单成功！";
                    return true;
                }
                message = "失败！内部出现异常";
                return false;
            }
            message = "订单不存在！";
            return false;
        }

        public IEnumerable<DemandOrderOutput> GetFinishedOrder(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.DemendOrderStatus.Finished); // To Find The order which has been finished
            count = orderInfos.Count();
            IEnumerable<DemandOrderOutput> orderOutputs = (from a in orderInfos
                                                           where a.Oid.Contains(queryInfo) || queryInfo == null
                                                           select new DemandOrderOutput
                                                           {
                                                               Id = a.Id,
                                                               Oid = a.Oid,
                                                               Name = a.Name,
                                                               Num = a.Num,
                                                               Unit = a.Unit,
                                                               Enterpriser = (from u in userInfos where u.UserId == a.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               Status = a.Status,
                                                               AddTime = a.AddTime,
                                                               Category = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                               Receiver = (from n in userInfos where n.UserId == a.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               EnterpriseName = (from n in userInfos where n.UserId == a.EnterpriseId select new { n.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault(),
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }

        public IEnumerable<DemandOrderOutput> GetMyDemandOrders(int page, int limit, out int count, string queryInfo, string userId)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(d=>d.EnterpriseId==userId); // To Find The order which has been finished
            count = orderInfos.Count();
            IEnumerable<DemandOrderOutput> orderOutputs = (from a in orderInfos
                                                           where a.Oid.Contains(queryInfo) || queryInfo == null
                                                           select new DemandOrderOutput
                                                           {
                                                               Id = a.Id,
                                                               Oid = a.Oid,
                                                               Name = a.Name,
                                                               Num = a.Num,
                                                               Unit = a.Unit,
                                                               Enterpriser = (from u in userInfos where u.UserId == a.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               Status = a.Status,
                                                               AddTime = a.AddTime,
                                                               Category = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                               Receiver = (from n in userInfos where n.UserId == a.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               EnterpriseName = (from n in userInfos where n.UserId == a.EnterpriseId select new { n.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault(),
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }

        public DemandOrderOutput GetOrderByOID(string id)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            DemandOrderInfo info = _dbContext.Set<DemandOrderInfo>().Where(d => d.Oid == id).FirstOrDefault();
            if (info != null)
            {
                DemandOrderOutput output = new DemandOrderOutput
                {
                    Id = info.Id,
                    Oid = info.Oid,
                    Name = info.Name,
                    Num = info.Num,
                    Unit = info.Unit,
                    Enterpriser = (from u in userInfos where u.UserId == info.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                    Status = info.Status,
                    AddTime = info.AddTime,
                    Category = (from g in categorylnfos where g.CategoryId == info.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                    Receiver = (from n in userInfos where n.UserId == info.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault(),
                    EnterpriseName = (from e in userInfos where e.UserId == info.EnterpriseId select new { e.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault()
                };
                return output;
            }
            return null;
        }

        /// <summary>
        /// Get UnAceeptOrder List
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IEnumerable<DemandOrderOutput> GetOrders(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.DemendOrderStatus.unAccept); //未接受的订单
            count = orderInfos.Count();
            IEnumerable<DemandOrderOutput> orderOutputs = (from a in orderInfos
                                                           where a.Oid.Contains(queryInfo) || queryInfo == null
                                                           select new DemandOrderOutput
                                                           {
                                                               Id = a.Id,
                                                               Oid = a.Oid,
                                                               Name = a.Name,
                                                               Num = a.Num,
                                                               Unit = a.Unit,
                                                               Enterpriser = (from u in userInfos where u.UserId == a.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               Status = a.Status,
                                                               AddTime = a.AddTime,
                                                               Category = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                               EnterpriseName = (from n in userInfos where n.UserId == a.EnterpriseId select new { n.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault(),
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }

        public IEnumerable<DemandOrderOutput> GetRuningOrder(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.DemendOrderStatus.Accepted); // To query the order which has been accpeted
            count = orderInfos.Count();
            IEnumerable<DemandOrderOutput> orderOutputs = (from a in orderInfos
                                                           where a.Oid.Contains(queryInfo) || queryInfo == null
                                                           select new DemandOrderOutput
                                                           {
                                                               Id = a.Id,
                                                               Oid = a.Oid,
                                                               Name = a.Name,
                                                               Num = a.Num,
                                                               Unit = a.Unit,
                                                               Enterpriser = (from u in userInfos where u.UserId == a.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               Status = a.Status,
                                                               AddTime = a.AddTime,
                                                               Category = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                               Receiver = (from n in userInfos where n.UserId == a.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               EnterpriseName = (from n in userInfos where n.UserId == a.EnterpriseId select new { n.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault(),
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }

        public IEnumerable<OrderOutput> GetUnVerifyOrderList(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<OrderInfo> orderInfos = _dbContext.Set<OrderInfo>().Where(o => o.Status == (int)TypeEnum.OrderStatus.Running && !string.IsNullOrEmpty(o.Url)); // To query the order which has been accpeted
            count = orderInfos.Count();
            IEnumerable<OrderOutput> orderOutputs = (from a in orderInfos
                                                           where a.InstanceId.Contains(queryInfo) || queryInfo == null
                                                           select new OrderOutput
                                                           {
                                                               Id = a.Id,
                                                               InstanceId = a.InstanceId,
                                                               Name = a.Name,
                                                               Num = a.Num,
                                                               Unit = a.Unit,
                                                               EnterpriseID = (from u in userInfos where u.UserId == a.EnterpriseId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               Status = a.Status,
                                                               AddTime = a.AddTime,
                                                               CategoryName = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                               Receiver = (from n in userInfos where n.UserId == a.ReceiverId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                               EnterpriseName = (from n in userInfos where n.UserId == a.EnterpriseId select new { n.EnterpriseName }).Select(s => s.EnterpriseName).FirstOrDefault(),
                                                               OriginalOrder = a.OriginalOrderId
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }
    }
}
