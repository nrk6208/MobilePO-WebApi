using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Purchaseorders
    {
        public Purchaseorders()
        {
            Purchaseorderlines = new HashSet<Purchaseorderlines>();
            Receipts = new HashSet<Receipts>();
            Shipments = new HashSet<Shipments>();
        }

        public long ProrId { get; set; }
        public string ProrNumber { get; set; }
        public DateTime ProrDate { get; set; }
        public string ProrStatus { get; set; }
        public long? ProrSplrId { get; set; }
        public bool? ProrIsdeleted { get; set; }
        public long? ProrCreatedby { get; set; }
        public DateTime? ProrCreateddate { get; set; }
        public long? ProrModifiedby { get; set; }
        public DateTime? ProrModifieddate { get; set; }

        public Users ProrModifiedbyNavigation { get; set; }
        public Suppliers ProrSplr { get; set; }
        public ICollection<Purchaseorderlines> Purchaseorderlines { get; set; }
        public ICollection<Receipts> Receipts { get; set; }
        public ICollection<Shipments> Shipments { get; set; }
    }
}
