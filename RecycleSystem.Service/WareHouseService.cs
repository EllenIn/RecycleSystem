using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.WareHouseDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecycleSystem.Service
{
    public class WareHouseService : IWareHouseService
    {
        private readonly DbContext _dbContext;
        public WareHouseService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<GoodsOutput> GetGoodsInputInfo(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            IQueryable<InputInfo> inputInfos = _dbContext.Set<InputInfo>();
            count = inputInfos.Count();
            IEnumerable<GoodsOutput> goodsOutputs = (from i in inputInfos
                                                     where i.InstanceId.Contains(queryInfo) || queryInfo == null
                                                     select new GoodsOutput
                                                     {
                                                         Id = i.Id,
                                                         InstanceId = i.InstanceId,
                                                         Category = (from g in categorylnfos where g.CategoryId == i.CategoryId select new { g.CategoryName }).Select(s => s.CategoryName).FirstOrDefault(),
                                                         Name = i.Name,
                                                         Unit = i.Unit,
                                                         Num = i.Num,
                                                         InputUser = (from u in userInfos where u.UserId == i.InputUser select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                         AddTime=i.AddTime
                                                     }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return goodsOutputs;
        }
    }
}
