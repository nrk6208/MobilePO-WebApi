using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Receiptlines
    {
        public long RctlId { get; set; }
        public long RctlRcptId { get; set; }
        public long RctlProlId { get; set; }
        public int RctlQuantity { get; set; }
        public bool? RctlIsdeleted { get; set; }
        public long? RctlCreatedby { get; set; }
        public DateTime? RctlCreateddate { get; set; }
        public long? RctlModifiedby { get; set; }
        public DateTime? RctlModifieddate { get; set; }

        public Users RctlCreatedbyNavigation { get; set; }
        public Users RctlModifiedbyNavigation { get; set; }
        public Purchaseorderlines RctlProl { get; set; }
        public Receipts RctlRcpt { get; set; }
    }
}
