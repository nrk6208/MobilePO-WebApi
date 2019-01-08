using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class PurchaseOrdersRestModel
    {
        public long OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public IEnumerable<PurchaseOrderLinesRestModel> Lines { get; set; }
    }
    public class PurchaseOrderLinesRestModel
    {
        public long OrderLineId { get; set; }
        public short OrderLineNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Item { get; set; }
        public string Status { get; set; }
        public int ShippingQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int UnDeliveredQuantity { get; set; }
    }
}
