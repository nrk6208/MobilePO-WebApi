using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Receipts
    {
        public Receipts()
        {
            Receiptlines = new HashSet<Receiptlines>();
        }

        public long RcptId { get; set; }
        public string RcptReceiptno { get; set; }
        public long RcptProrId { get; set; }
        public long RcptGetrId { get; set; }
        public bool? RcptIsdeleted { get; set; }
        public long? RcptCreatedby { get; set; }
        public DateTime? RcptCreateddate { get; set; }
        public long? RcptModifiedby { get; set; }
        public DateTime? RcptModifieddate { get; set; }

        public Users RcptCreatedbyNavigation { get; set; }
        public Gateentries RcptGetr { get; set; }
        public Users RcptModifiedbyNavigation { get; set; }
        public Purchaseorders RcptPror { get; set; }
        public ICollection<Receiptlines> Receiptlines { get; set; }
    }
}
