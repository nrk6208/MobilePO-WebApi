using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Gateentries
    {
        public long GetrId { get; set; }
        public string GetrNumber { get; set; }
        public long GetrSpmtId { get; set; }
        public bool? GetrIsdeleted { get; set; }
        public long? GetrCreatedby { get; set; }
        public DateTime? GetrCreateddate { get; set; }
        public long? GetrModifiedby { get; set; }
        public DateTime? GetrModifieddate { get; set; }

        public Users GetrCreatedbyNavigation { get; set; }
        public Users GetrModifiedbyNavigation { get; set; }
        public Shipments GetrSpmt { get; set; }
        public Receipts Receipts { get; set; }
    }
}
