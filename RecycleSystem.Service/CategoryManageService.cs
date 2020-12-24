using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.CategoryDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RecycleSystem.Service
{
    public class CategoryManageService : ICategoryManageService
    {
        private readonly DbContext _dbContext;
        public CategoryManageService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddCategory(CategoryInput categoryInput, out string msg)
        {
            throw new NotImplementedException();
        }

        public bool BanCategory(CategoryInput categoryInput, out string msg)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 为页面展示数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IEnumerable<CategoryOutput> GetCategories(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<Categorylnfo> categorylnfos = _dbContext.Set<Categorylnfo>();
            count = categorylnfos.Count();
            IEnumerable<CategoryOutput> categories = (from c in categorylnfos
                                                      where c.CategoryName.Contains(queryInfo)||queryInfo==null
                                                      select new CategoryOutput
                                                      {
                                                          Id = c.Id,
                                                          CategoryId = c.CategoryId,
                                                          CategoryName = c.CategoryName,
                                                          CurrentPrice = c.CurrentPrice,
                                                          Unit = c.Unit,
                                                          DelFlag = c.DelFlag,
                                                          AddTime = c.AddTime
                                                      }).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return categories;
        }
        /// <summary>
        /// 给其它功能提供相应数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryOutput> GetCategories()
        {
            IEnumerable<CategoryOutput> categories = (from c in _dbContext.Set<Categorylnfo>()
                                                      select new CategoryOutput
                                                      {
                                                          Id = c.Id,
                                                          CategoryId = c.CategoryId,
                                                          CategoryName = c.CategoryName,
                                                          CurrentPrice = c.CurrentPrice
                                                      }).OrderBy(o => o.Id).ToList();
            return categories;
        }

        public bool UpdateCategoryById(CategoryInput categoryInput, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
