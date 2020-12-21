using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.LoginDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using Senkuu.MaterialSystem.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecycleSystem.Service
{
    public class AccountService : IAccountService
    {
        private readonly DbContext _dbContext;
        public AccountService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoginOutput Login(LoginInput loginInput)
        {
            IQueryable<UserInfo> infos = _dbContext.Set<UserInfo>();
            IQueryable<UserType> types = _dbContext.Set<UserType>();
            IQueryable<DepartmentInfo> departments = _dbContext.Set<DepartmentInfo>();
            IQueryable<RoleInfo> roleInfos = _dbContext.Set<RoleInfo>();
            IQueryable<RUserRoleInfo> userRoles = _dbContext.Set<RUserRoleInfo>();
            string password = MD5Helper.EncryptString(loginInput.Password);
            UserInfo user = infos.Where(u => u.UserId == loginInput.UserId && u.Password == password && u.DelFlag == false).FirstOrDefault();
            if (user != null)
            {
                IEnumerable<string> roleName = (from a in userRoles
                                                join b in roleInfos on a.RoleId equals b.RoleId into join_a
                                                from c in join_a.DefaultIfEmpty()
                                                where a.UserId == user.UserId
                                                select new
                                                {
                                                    c.RoleName
                                                }).Select(s => s.RoleName).ToList();
                IEnumerable<string> roleId = (from a in userRoles
                                              join b in roleInfos on a.RoleId equals b.RoleId into join_a
                                              from c in join_a.DefaultIfEmpty()
                                              where a.UserId == user.UserId
                                              select new
                                              {
                                                  c.RoleId
                                              }).Select(s => s.RoleId).ToList();
                LoginOutput outputs = (from a in types
                                       join b in infos on a.Id equals b.UserTypeId into join_a
                                       from c in join_a.DefaultIfEmpty()
                                       join d in departments on c.DepartmentId equals d.DepartmentId
                                       where c.UserId == user.UserId
                                       select new LoginOutput
                                       {
                                           Id = c.Id,
                                           UserTypeName = a.TypeName,
                                           DepartmentId = d.DepartmentId,
                                           DepartmentName = d.DepartmentName,
                                           Email = c.Email,
                                           Tel = c.Tel,
                                           AddTime = c.AddTime,
                                           DelFlag = c.DelFlag,
                                           EnterpriseName = c.EnterpriseName,
                                           Gender = c.Gender,
                                           Token = c.Token,
                                           UserId = c.UserId,
                                           UserName = c.UserName,
                                           //RoleName = roleName,
                                           //RoleId = roleId
                                       }).FirstOrDefault();
                return outputs;
            }
            return null;
        }
    }
}


