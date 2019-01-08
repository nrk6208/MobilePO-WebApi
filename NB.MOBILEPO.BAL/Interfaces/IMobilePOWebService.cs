using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using NB.MOBILEPO.BAL.Models.ServiceModels;

namespace NB.MOBILEPO.BAL.Interfaces
{
    [ServiceContract]
    public interface IMobilePOWebService
    {
        [OperationContract]
        bool PostBulkPurchaseOrders(IEnumerable<PurchaseOrderServiceModel> model);
        [OperationContract]
        bool PostOrUpdatePurchaseOrder(PurchaseOrderServiceModel model);
    }
}
