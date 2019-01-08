using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class GateEntriesRestModel
    {
        public string GateEntryNumber { get; set; }
        public long GateEntryId { get; set; }
        public long ShipmentId { get; set; }
        public string InvoiceNumber { get; set; }
        public string LorryNumber { get; set; }
        public OrderRestModel Order { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public long? ReceiptId { get; set; }
    }
    public class OrderRestModel
    {
        public long OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderLinesRestModel> Lines { get; set; }
    }
    public class OrderLinesRestModel
    {
        public long OrderLineId { get; set; }
        public short OrderLineNumber { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        //public int ReceivedQuantity { get { return Quantity; } }
        public int DeliveredQuantity { get; set; }
        public int ShippedQuantity { get; set; }
    }
}
