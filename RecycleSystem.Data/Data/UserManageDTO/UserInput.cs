using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleSystem.Data.Data.UserManageDTO
{
    public class UserInput
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool? Gender { get; set; }
        public int UserTypeId { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string EnterpriseName { get; set; }
    }
}
