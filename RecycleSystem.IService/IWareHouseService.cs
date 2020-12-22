using RecycleSystem.Data.Data.WareHouseDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleSystem.IService
{
    public interface IWareHouseService
    {
        IEnumerable<GoodsOutput> GetGoodsInputInfo(int page, int limit, out int count, string queryInfo);
    }
}
