using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Users
    {
        public Users()
        {
            GateentriesGetrCreatedbyNavigation = new HashSet<Gateentries>();
            GateentriesGetrModifiedbyNavigation = new HashSet<Gateentries>();
            InverseUserCreatedbyNavigation = new HashSet<Users>();
            InverseUserModifiedbyNavigation = new HashSet<Users>();
            PurchaseorderlinesProlCreatedbyNavigation = new HashSet<Purchaseorderlines>();
            PurchaseorderlinesProlModifiedbyNavigation = new HashSet<Purchaseorderlines>();
            Purchaseorders = new HashSet<Purchaseorders>();
            ReceiptlinesRctlCreatedbyNavigation = new HashSet<Receiptlines>();
            ReceiptlinesRctlModifiedbyNavigation = new HashSet<Receiptlines>();
            ReceiptsRcptCreatedbyNavigation = new HashSet<Receipts>();
            ReceiptsRcptModifiedbyNavigation = new HashSet<Receipts>();
            ShipmentlinesSpmlCreatedbyNavigation = new HashSet<Shipmentlines>();
            ShipmentlinesSpmlModifiedbyNavigation = new HashSet<Shipmentlines>();
            ShipmentsSpmtCreatedbyNavigation = new HashSet<Shipments>();
            ShipmentsSpmtModifiedbyNavigation = new HashSet<Shipments>();
            SuppliersSplrCreatedbyNavigation = new HashSet<Suppliers>();
            SuppliersSplrModifiedbyNavigation = new HashSet<Suppliers>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserSecretkey { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string UserEmail { get; set; }
        public long? UserPhonenumber { get; set; }
        public string UserAddress { get; set; }
        public short UserRoleId { get; set; }
        public long? UserSplrId { get; set; }
        public bool? UserIsdeleted { get; set; }
        public long? UserCreatedby { get; set; }
        public DateTime? UserCreateddate { get; set; }
        public long? UserModifiedby { get; set; }
        public DateTime? UserModifieddate { get; set; }

        public Users UserCreatedbyNavigation { get; set; }
        public Users UserModifiedbyNavigation { get; set; }
        public Roles UserRole { get; set; }
        public Suppliers UserSplr { get; set; }
        public ICollection<Gateentries> GateentriesGetrCreatedbyNavigation { get; set; }
        public ICollection<Gateentries> GateentriesGetrModifiedbyNavigation { get; set; }
        public ICollection<Users> InverseUserCreatedbyNavigation { get; set; }
        public ICollection<Users> InverseUserModifiedbyNavigation { get; set; }
        public ICollection<Purchaseorderlines> PurchaseorderlinesProlCreatedbyNavigation { get; set; }
        public ICollection<Purchaseorderlines> PurchaseorderlinesProlModifiedbyNavigation { get; set; }
        public ICollection<Purchaseorders> Purchaseorders { get; set; }
        public ICollection<Receiptlines> ReceiptlinesRctlCreatedbyNavigation { get; set; }
        public ICollection<Receiptlines> ReceiptlinesRctlModifiedbyNavigation { get; set; }
        public ICollection<Receipts> ReceiptsRcptCreatedbyNavigation { get; set; }
        public ICollection<Receipts> ReceiptsRcptModifiedbyNavigation { get; set; }
        public ICollection<Shipmentlines> ShipmentlinesSpmlCreatedbyNavigation { get; set; }
        public ICollection<Shipmentlines> ShipmentlinesSpmlModifiedbyNavigation { get; set; }
        public ICollection<Shipments> ShipmentsSpmtCreatedbyNavigation { get; set; }
        public ICollection<Shipments> ShipmentsSpmtModifiedbyNavigation { get; set; }
        public ICollection<Suppliers> SuppliersSplrCreatedbyNavigation { get; set; }
        public ICollection<Suppliers> SuppliersSplrModifiedbyNavigation { get; set; }
    }
}
