using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Purchaseorderlines
    {
        public Purchaseorderlines()
        {
            Receiptlines = new HashSet<Receiptlines>();
            Shipmentlines = new HashSet<Shipmentlines>();
        }

        public long ProlId { get; set; }
        public long ProlProrId { get; set; }
        public short ProlNumber { get; set; }
        public string ProlStatus { get; set; }
        public DateTime ProlDate { get; set; }
        public string ProlItem { get; set; }
        public string ProlItemdescription { get; set; }
        public int ProlQuantity { get; set; }
        public bool? ProlIsdeleted { get; set; }
        public long? ProlCreatedby { get; set; }
        public DateTime? ProlCreateddate { get; set; }
        public long? ProlModifiedby { get; set; }
        public DateTime? ProlModifieddate { get; set; }

        public Users ProlCreatedbyNavigation { get; set; }
        public Users ProlModifiedbyNavigation { get; set; }
        public Purchaseorders ProlPror { get; set; }
        public ICollection<Receiptlines> Receiptlines { get; set; }
        public ICollection<Shipmentlines> Shipmentlines { get; set; }
    }
}
