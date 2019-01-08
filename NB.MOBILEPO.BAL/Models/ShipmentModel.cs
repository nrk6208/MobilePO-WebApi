using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NB.MOBILEPO.BAL.Models
{
    public class ShipmentModel
    {
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public string LorryNumber { get; set; }
        [Required]
        public long OrderId { get; set; }
        public IEnumerable<ShipmentLinesModel> Lines { get; set; }
    }
    public class ShipmentLinesModel
    {
        [Required]
        public long OrderLineId { get; set; }
        [Required]
        public int ShippingQuantity { get; set; }
    }
}
