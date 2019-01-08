using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Shipments
    {
        public Shipments()
        {
            Shipmentlines = new HashSet<Shipmentlines>();
        }

        public long SpmtId { get; set; }
        public long SpmtProrId { get; set; }
        public string SpmtInvoiceno { get; set; }
        public string SpmtLorryno { get; set; }
        public DateTime SpmtShipmentdate { get; set; }
        public bool? SpmtIsdeleted { get; set; }
        public long? SpmtCreatedby { get; set; }
        public DateTime? SpmtCreateddate { get; set; }
        public long? SpmtModifiedby { get; set; }
        public DateTime? SpmtModifieddate { get; set; }

        public Users SpmtCreatedbyNavigation { get; set; }
        public Users SpmtModifiedbyNavigation { get; set; }
        public Purchaseorders SpmtPror { get; set; }
        public Gateentries Gateentries { get; set; }
        public ICollection<Shipmentlines> Shipmentlines { get; set; }
    }
}
