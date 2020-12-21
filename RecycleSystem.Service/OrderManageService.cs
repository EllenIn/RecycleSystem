using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.OrderManageDTO;
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

        public IEnumerable<DemandOrderOutput> GetFinishedOrder(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.OrderStatus.Finished); // To Find The order which has been finished
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
                                                               Receiver = (from n in userInfos where n.UserId == a.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault()
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
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
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.OrderStatus.unAccept); //未接受的订单
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
                                                               Category = (from g in categorylnfos where g.CategoryId == a.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault()
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }

        public IEnumerable<DemandOrderOutput> GetRuningOrder(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<DemandOrderInfo> orderInfos = _dbContext.Set<DemandOrderInfo>().Where(o => o.Status == (int)TypeEnum.OrderStatus.Accepted); // To query the order which has been accpeted
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
                                                               Receiver = (from n in userInfos where n.UserId == a.UserId select new { n.UserName }).Select(s => s.UserName).FirstOrDefault()
                                                           }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return orderOutputs;
        }
    }
}
