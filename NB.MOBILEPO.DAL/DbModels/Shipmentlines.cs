using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Shipmentlines
    {
        public long SpmlId { get; set; }
        public long SpmlSpmtId { get; set; }
        public long SpmlProlId { get; set; }
        public int SpmlQuantity { get; set; }
        public bool? SpmlIsdeleted { get; set; }
        public long? SpmlCreatedby { get; set; }
        public DateTime? SpmlCreateddate { get; set; }
        public long? SpmlModifiedby { get; set; }
        public DateTime? SpmlModifieddate { get; set; }

        public Users SpmlCreatedbyNavigation { get; set; }
        public Users SpmlModifiedbyNavigation { get; set; }
        public Purchaseorderlines SpmlProl { get; set; }
        public Shipments SpmlSpmt { get; set; }
    }
}
