using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IPurchaseOrdersService
    {
        IEnumerable<PurchaseOrdersRestModel> GetAllPurchaseOrders();
    }
}
