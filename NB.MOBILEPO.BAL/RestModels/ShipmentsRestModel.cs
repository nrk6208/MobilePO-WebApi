using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class ShipmentsRestModel
    {
        public long ShipmentId { get; set; }
        //public long ShipmentLineId { get; set; }
        public long OrderId { get; set; }
        public string OrderNumber { get; set; }
        //public long OrderLineId { get; set; }
        //public short OrderLineNumber { get; set; }
        public int Quantity { get; set; }
        public int OrderedQuantity { get; set; }
        public string InvoiceNumber { get; set; }
        public string LorryNumber { get; set; }
        public DateTime? Date { get; set; } // Shipment Date
        public string GateEntryNumber { get; set; }
        public IEnumerable<ShipmentLinesRestModel> Lines { get; set; }
    }
    public class ShipmentLinesRestModel
    {
        public long ShipmentLineId { get; set; }
        public long OrderLineId { get; set; }
        public short OrderLineNumber { get; set; }
        public int Quantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public DateTime? Date { get; set; }
        public string Item { get; set; }
        public int OrderedQuantity { get; set; }
    }
}
