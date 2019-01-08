using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NB.MOBILEPO.BAL.Services
{
    public class GateEntriesService : CommonService, IGateEntriesService
    {   
        public GateEntriesService(
            MobilePoDbContext dbContext, 
            IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~GateEntriesService()
        {
            GC.SuppressFinalize(this);
        }
        public bool PostGateEntry(GateEntryModel model, out string NewGateEntryNumber)
        {
            NewGateEntryNumber = "";
            _dbContext.Gateentries
                .Add(new Gateentries
                {
                    GetrCreatedby = CurrentUserId,
                    GetrCreateddate = DateTime.UtcNow,
                    GetrNumber = GenerateNewGateEntryNumber(ref NewGateEntryNumber),
                    GetrSpmtId = model.ShipmentId,
                    GetrIsdeleted = false
                });
            return SaveDbChanges();
        }
        public GateEntriesRestModel GetGateEntry(string gateEntryNumber)
        {
            return _dbContext.Gateentries
                .Include(i => i.GetrSpmt.SpmtPror)
                .Include(i => i.Receipts)
                .Include(i => i.GetrSpmt.Shipmentlines)
                .ThenInclude(t => t.SpmlProl)
                .ThenInclude(ti => ti.Receiptlines)
                //.Include(i => i.GetrCreatedbyNavigation)
                .Where(e => !e.GetrIsdeleted.Value &&
                //&& IsSameSupplierUser(e.GetrCreatedbyNavigation.UserSplrId) &&
                string.Equals(e.GetrNumber, gateEntryNumber, StringComparison.OrdinalIgnoreCase)).AsEnumerable()
                .Select(s => new GateEntriesRestModel
                {
                    GateEntryNumber = s.GetrNumber,
                    GateEntryId = s.GetrId,
                    InvoiceNumber = s.GetrSpmt.SpmtInvoiceno,
                    ShipmentDate = s.GetrSpmt.SpmtShipmentdate,
                    LorryNumber = s.GetrSpmt.SpmtLorryno,
                    ShipmentId = s.GetrSpmt.SpmtId,
                    ReceiptId = s.Receipts?.RcptId,
                    Order = new OrderRestModel
                    {
                        OrderId = s.GetrSpmt.SpmtProrId,
                        OrderNumber = s.GetrSpmt.SpmtPror.ProrNumber,
                        Status = s.GetrSpmt.SpmtPror.ProrStatus,
                        Lines = s.GetrSpmt.Shipmentlines.Select(sl => new OrderLinesRestModel
                        {
                            OrderLineId = sl.SpmlProl.ProlId,
                            OrderLineNumber = sl.SpmlProl.ProlNumber,
                            Quantity = sl.SpmlProl.ProlQuantity,
                            Status = sl.SpmlProl.ProlStatus,
                            //ReceivedQuantity = sl.SpmlProl.Receiptlines.SingleOrDefault(w => w.RctlRcptId == s.Receipts?.RcptId)?.RctlQuantity ?? sl.SpmlProl.ProlQuantity
                            DeliveredQuantity = sl.SpmlProl.Receiptlines.SingleOrDefault(w => !w.RctlIsdeleted.Value && w.RctlRcptId == s.Receipts?.RcptId)?.RctlQuantity ?? 0,
                            ShippedQuantity = s.GetrSpmt.Shipmentlines.SingleOrDefault(e => !e.SpmlIsdeleted.Value && e.SpmlProlId == sl.SpmlProlId)?.SpmlQuantity ?? 0
                        })
                    }
                }).FirstOrDefault();
        }
        private string GenerateNewGateEntryNumber(ref string NewGateEntryNumber)
        {
            NewGateEntryNumber = "GE" + Regex.Replace(DateTime.UtcNow.ToString().Substring(0, 19), @"(\W)+", "", RegexOptions.IgnoreCase);
            return NewGateEntryNumber;
        }
    }
}
