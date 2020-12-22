using RecycleSystem.Data.Data.OrderManageDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleSystem.IService
{
    public interface IOrderManageService
    {
        IEnumerable<DemandOrderOutput> GetOrders(int page, int limit, out int count, string queryInfo);
        IEnumerable<DemandOrderOutput> GetRuningOrder(int page, int limit, out int count, string queryInfo);
        IEnumerable<DemandOrderOutput> GetFinishedOrder(int page, int limit, out int count, string queryInfo);
        DemandOrderOutput GetOrderByOID(string id);
        bool AcceptOrder(string oid,string userId,out string message);
    }
}
