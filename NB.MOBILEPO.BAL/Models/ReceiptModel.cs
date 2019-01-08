using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Models
{
    public class ReceiptModel
    {
        public string ReceiptNumber { get; set; }
        public long OrderId { get; set; }
        public long GateEntryId { get; set; }
        public IEnumerable<ReceiptLinesModel> ReceiptLines { get; set; }
    }
    public class ReceiptLinesModel
    {
        public long ReceiptId { get; set; }
        public long OrderLineId { get; set; }
        public int Quantity { get; set; }
    }
}
