using System;
using System.Collections.Generic;

#nullable disable

namespace RecycleSystem.DataEntity.Entities
{
    public partial class WorkFlow
    {
        public int Id { get; set; }
        public string InstanceId { get; set; }
        public string TypeId { get; set; }
        public string UserId { get; set; }
        public int? Status { get; set; }
        public string Reason { get; set; }
        public string CurrentReviewer { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
