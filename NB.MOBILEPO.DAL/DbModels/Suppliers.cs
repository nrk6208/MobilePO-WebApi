using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Purchaseorders = new HashSet<Purchaseorders>();
            Users = new HashSet<Users>();
        }

        public long SplrId { get; set; }
        public string SplrCode { get; set; }
        public string SplrName { get; set; }
        public string SplrAddress { get; set; }
        public bool? SplrIsdeleted { get; set; }
        public long? SplrCreatedby { get; set; }
        public DateTime? SplrCreateddate { get; set; }
        public long? SplrModifiedby { get; set; }
        public DateTime? SplrModifieddate { get; set; }

        public Users SplrCreatedbyNavigation { get; set; }
        public Users SplrModifiedbyNavigation { get; set; }
        public ICollection<Purchaseorders> Purchaseorders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
