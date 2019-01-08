using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NB.MOBILEPO.BAL.Models.ServiceModels
{
    [DataContract]
    public class PurchaseOrderServiceModel
    {
        [DataMember]
        [Required]
        public string OrderNumber { get; set; }
        [DataMember]
        public string OrderStatus { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public IEnumerable<PurchaseOrderLinesServiceModel> Lines { get; set; }
    }
    [DataContract]
    public class PurchaseOrderLinesServiceModel
    {
        [DataMember]
        [Required]
        public short LineNumber { get; set; }
        [DataMember]
        public string LineStatus { get; set; }
        [DataMember]
        public DateTime LineDate { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }
    }
}
