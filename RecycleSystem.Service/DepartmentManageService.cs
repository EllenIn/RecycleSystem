using Microsoft.EntityFrameworkCore;
using RecycleSystem.Data.Data.DepartmentManageDTO;
using RecycleSystem.DataEntity.Entities;
using RecycleSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecycleSystem.Service
{
    public class DepartmentManageService : IDepartmentManageService
    {
        private readonly DbContext _dbContext;
        public DepartmentManageService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddDepartment(DepartmentInput departmentInput, out string message)
        {
            IQueryable<DepartmentInfo> departmentInfos = _dbContext.Set<DepartmentInfo>();
            DepartmentInfo department = departmentInfos.Where(d => d.DepartmentId == departmentInput.DepartmentId).FirstOrDefault();
            if (department == null)
            {
                DepartmentInfo department1 = departmentInfos.Where(d => d.DepartmentName == departmentInput.DepartmentName).FirstOrDefault(); //部门名是否已被使用
                if (department1 == null)
                {
                    DepartmentInfo info = departmentInfos.Where(d => d.LeaderId == departmentInput.LeaderId).FirstOrDefault(); //查询此人是否已是领导
                    if (info == null)
                    {
                        try
                        {
                            DepartmentInfo newDepartment = new DepartmentInfo
                            {
                                DepartmentId = departmentInput.DepartmentId,
                                DepartmentName = departmentInput.DepartmentName,
                                LeaderId = departmentInput.LeaderId,
                                ParentId = departmentInput.ParentId,
                                Description = departmentInput.Description,
                                AddTime = DateTime.Now
                            };
                            _dbContext.Set<DepartmentInfo>().Add(newDepartment);
                            if (_dbContext.SaveChanges() > 0)
                            {
                                message = "添加成功！";
                                return true;
                            }
                            else
                            {
                                message = "添加失败！内部出现异常！";
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            return false;
                        }

                    }
                    message = "此人已是" + info.DepartmentName + "的领导！";
                    return false;
                }
                message = "该部门名已被使用！";
                return false;
            }
            message = "已存在该部门ID，请检查部门ID！";
            return false;
        }

        public DepartmentOutput GetDepartmentById(string id)
        {
            IQueryable<DepartmentInfo> departmentInfos = _dbContext.Set<DepartmentInfo>();
            DepartmentOutput department = (from d in departmentInfos
                                           where d.DepartmentId == id
                                           select new DepartmentOutput
                                           {
                                               Id = d.Id,
                                               DepartmentId = d.DepartmentId,
                                               DepartmentName = d.DepartmentName,
                                               LeaderId = d.LeaderId,
                                               ParentId = d.ParentId,
                                               Description = d.Description
                                           }).OrderBy(o => o.Id).FirstOrDefault();
            return department;
        }

        /// <summary>
        /// 部门信息页的数据展示
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页限制条目数</param>
        /// <param name="count">返回的总数</param>
        /// <param name="queryInfo">查询信息</param>
        /// <returns></returns>
        public IEnumerable<DepartmentOutput> GetDepartments(int page, int limit, out int count, string queryInfo)
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<DepartmentInfo> departmentInfos = _dbContext.Set<DepartmentInfo>();

            count = departmentInfos.Count();
            IEnumerable<DepartmentOutput> departments = (from d in departmentInfos
                                                         where d.DepartmentName.Contains(queryInfo) || queryInfo == null
                                                         select new DepartmentOutput
                                                         {
                                                             Id = d.Id,
                                                             DepartmentId = d.DepartmentId,
                                                             DepartmentName = d.DepartmentName,
                                                             LeaderId = (from u in userInfos where u.UserId == d.LeaderId select new { u.UserName }).Select(s => s.UserName).FirstOrDefault(),
                                                             Description = d.Description,
                                                             AddTime = d.AddTime,
                                                             ParentId = (from k in departmentInfos where k.DepartmentId == d.ParentId select new { k.DepartmentName }).Select(s => s.DepartmentName).FirstOrDefault()
                                                         }
                                                         ).OrderBy(o => o.Id).Skip((page - 1) * limit).Take(limit).ToList();
            return departments;
        }
        /// <summary>
        /// 供应下拉框等选择部门信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentOutput> GetDepartments()
        {
            IQueryable<UserInfo> userInfos = _dbContext.Set<UserInfo>();
            IQueryable<DepartmentInfo> departmentInfos = _dbContext.Set<DepartmentInfo>();
            IEnumerable<DepartmentOutput> departments = (from d in departmentInfos
                                                         select new DepartmentOutput
                                                         {
                                                             Id = d.Id,
                                                             DepartmentId = d.DepartmentId,
                                                             DepartmentName = d.DepartmentName,
                                                         }
                                                         ).OrderBy(o => o.Id).ToList();
            return departments;
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="departmentInput">输入的信息</param>
        /// <param name="message">返回去的信息</param>
        /// <returns></returns>
        public bool UpdateDepartmentInfoById(DepartmentInput departmentInput, out string message)
        {
            IQueryable<DepartmentInfo> departmentInfos = _dbContext.Set<DepartmentInfo>();
            DepartmentInfo department = departmentInfos.Where(d => d.DepartmentId == departmentInput.DepartmentId).FirstOrDefault();//是否存在该部门
            if (department != null)
            {
                DepartmentInfo department1 = departmentInfos.Where(d => d.DepartmentName == departmentInput.DepartmentName).FirstOrDefault();
                if (department1 == null || department.DepartmentName == departmentInput.DepartmentName)
                {
                    DepartmentInfo info = departmentInfos.Where(l => l.LeaderId == departmentInput.LeaderId).FirstOrDefault(); //查询此人是否已是领导
                    if (info == null || info.LeaderId == department.LeaderId)
                    {
                        try
                        {
                            department.DepartmentName = departmentInput.DepartmentName;
                            department.Description = departmentInput.Description;
                            department.LeaderId = departmentInput.LeaderId;
                            department.ParentId = departmentInput.ParentId;
                            _dbContext.Set<DepartmentInfo>().Update(department);
                            if (_dbContext.SaveChanges() > 0)
                            {
                                message = "修改成功！";
                                return true;
                            }
                            message = "修改失败！内部出现异常！";
                            return false;
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            return false;
                        }
                    }
                    message = "此人已是" + info.DepartmentName + "的领导！";
                    return false;
                }
                message = "此部门名称已被使用！";
                return false;
            }
            message = "内部错误！数据可能被篡改！";
            return false;
        }
    }
}
