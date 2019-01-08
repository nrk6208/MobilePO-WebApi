using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NB.MOBILEPO.BAL.Services
{
    public class ShipmentsService : CommonService, IShipmentsService
    {
        public ShipmentsService(
            MobilePoDbContext dbContext,
            IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~ShipmentsService()
        {
            GC.SuppressFinalize(this);
        }
        public ShipmentsRestModel GetShipmentByInvoiceNumber(string invoiceNumber)
        {
            return _dbContext.Shipments
                .Include(i => i.Gateentries)
                .Include(i => i.SpmtPror)
                .ThenInclude(t => t.Purchaseorderlines)
                .Include(i => i.Shipmentlines)
                .ThenInclude(l => l.SpmlProl)
                .Include(i => i.Shipmentlines)
                //.ThenInclude(sl => sl.SpmlSpmt.Gateentries.Receipts)
                .Where(e => !e.SpmtIsdeleted.Value &&
                string.Equals(e.SpmtInvoiceno, invoiceNumber, StringComparison.OrdinalIgnoreCase))
                .AsEnumerable()
                .Select(s => new ShipmentsRestModel
                {
                    GateEntryNumber = (s.Gateentries?.GetrNumber) ?? "",
                    InvoiceNumber = s.SpmtInvoiceno,
                    LorryNumber = s.SpmtLorryno,
                    OrderId = s.SpmtProrId,
                    OrderNumber = s.SpmtPror.ProrNumber,
                    OrderedQuantity = s.SpmtPror.Purchaseorderlines.Sum(m => m.ProlQuantity),
                    Quantity = s.Shipmentlines.Sum(m => m.SpmlQuantity),
                    Date = s.SpmtCreateddate,
                    ShipmentId = s.SpmtId,
                    Lines = GetAllShipmentLines(s.Shipmentlines)
                }).FirstOrDefault();
            //return _dbContext.Shipmentlines
            //    .Include(i => i.SpmlSpmt)
            //    .Include(i => i.SpmlProl)
            //    //.Include(i => i.SpmlCreatedbyNavigation)
            //    .Include(i => i.SpmlSpmt.SpmtPror)
            //    .Include(i => i.SpmlSpmt.Gateentries)
            //    .Where(e =>
            //        !e.SpmlSpmt.SpmtIsdeleted.Value &&
            //        //IsSameSupplierUser(e.SpmlCreatedbyNavigation.UserSplrId) &&
            //        string.Equals(e.SpmlSpmt.SpmtInvoiceno, invoiceNumber, StringComparison.OrdinalIgnoreCase))
            //    .AsEnumerable()
            //    .Select(s => new ShipmentsRestModel
            //    {
            //        InvoiceNumber = s.SpmlSpmt.SpmtInvoiceno,
            //        LorryNumber = s.SpmlSpmt.SpmtLorryno,
            //        OrderId = s.SpmlSpmt.SpmtProrId,
            //        OrderLineId = s.SpmlProlId,
            //        OrderLineNumber = s.SpmlProl.ProlNumber,
            //        OrderNumber = s.SpmlSpmt.SpmtPror.ProrNumber,
            //        Quantity = s.SpmlQuantity,
            //        ShipmentDate = s.SpmlSpmt.SpmtShipmentdate,
            //        ShipmentId = s.SpmlSpmtId,
            //        ShipmentLineId = s.SpmlId,
            //        GateEntryNumber = (s.SpmlSpmt.Gateentries?.GetrNumber)??"",
            //    });
        }
        public IEnumerable<ShipmentsRestModel> GetAllShipments()
        {
            return _dbContext.Shipments
                .Include(i => i.Gateentries)
                .Include(i => i.SpmtPror)
                .ThenInclude(t => t.Purchaseorderlines)
                .Include(i => i.Shipmentlines)
                .ThenInclude(l => l.SpmlProl)
                .Where(e => !e.SpmtIsdeleted.Value).AsEnumerable()
                .Select(s => new ShipmentsRestModel
                {
                    GateEntryNumber = (s.Gateentries?.GetrNumber) ?? "",
                    InvoiceNumber =s.SpmtInvoiceno,
                    LorryNumber=s.SpmtLorryno,
                    OrderId=s.SpmtProrId,
                    OrderNumber=s.SpmtPror.ProrNumber,
                    OrderedQuantity=s.SpmtPror.Purchaseorderlines.Sum(m=> m.ProlQuantity),
                    Quantity = s.Shipmentlines.Sum(m=> m.SpmlQuantity),
                    Date=s.SpmtCreateddate,
                    ShipmentId=s.SpmtId,
                    Lines = GetAllShipmentLines(s.Shipmentlines)
                }).OrderByDescending(o => o.ShipmentId);
        }
        public bool PostShipment(ShipmentModel model)
        {
            var shipment = _dbContext.Shipments.Add(new Shipments {
                SpmtCreateddate = DateTime.UtcNow,
                SpmtCreatedby = CurrentUserId,
                SpmtInvoiceno = model.InvoiceNumber?.ToUpper(),
                SpmtLorryno = model.LorryNumber?.ToUpper(),
                SpmtIsdeleted = false,
                SpmtShipmentdate = DateTime.UtcNow,
                SpmtProrId = model.OrderId
            });
            foreach (var line in model.Lines)
            {
                _dbContext.Shipmentlines.Add(new Shipmentlines
                {
                    SpmlCreatedby=CurrentUserId,
                    SpmlCreateddate=DateTime.UtcNow,
                    SpmlIsdeleted=false,
                    SpmlProlId=line.OrderLineId,
                    SpmlQuantity = line.ShippingQuantity,
                    SpmlSpmtId=shipment.Entity.SpmtId
                });
            }
            return SaveDbChanges();
        }
        public bool IsInvoiceExisted(string invoiceNumber)
        {
            return _dbContext.Shipments.Any(s => !s.SpmtIsdeleted.Value && string.Equals(s.SpmtInvoiceno, invoiceNumber, StringComparison.OrdinalIgnoreCase));
        }
        //public bool IsShipmentAllowed(IEnumerable<ShipmentLinesModel> model, out string message)
        //{
        //    message = "";
        //    foreach (var line in model)
        //    {
        //        var records = _dbContext.Shipmentlines
        //            .Include(i => i.SpmlProl)
        //            .Where(e => !e.SpmlIsdeleted.Value && e.SpmlProlId == line.OrderLineId);
        //        int totalDeliveredQuantity = records.Sum(s => s.SpmlQuantity);
        //        int totalOrderedQuantity = records.FirstOrDefault()?.SpmlProl.ProlQuantity ?? 0;
        //        if (totalDeliveredQuantity == 0)
        //        {
        //            continue;
        //        }
        //        else if (totalDeliveredQuantity > totalOrderedQuantity)
        //        {
        //            message = "Can't accept, All ordered quantity has shipped for line "+ records.FirstOrDefault()?.SpmlProl?.ProlNumber;
        //            return false;
        //        } else if (totalDeliveredQuantity + line.Quantity > totalOrderedQuantity)
        //        {
        //            message = String.Format("Can't accept {0} quantities, as {1} quantities has already delivered out of {2} ordered quantity for line {3}", line.DeliveredQuantity, totalDeliveredQuantity, totalOrderedQuantity, records.FirstOrDefault()?.SpmlProl?.ProlNumber);
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        private IEnumerable<ShipmentLinesRestModel> GetAllShipmentLines(IEnumerable<Shipmentlines> shipmentLines)
        {
            foreach (var line in shipmentLines)
            {
                Gateentries gateEntry = line.SpmlSpmt.Gateentries;
                var deliveredQuantity = 
                    gateEntry == null ? 0 : 
                    _dbContext.Receiptlines
                    .Include(i=>i.RctlRcpt.RcptGetr).SingleOrDefault(e => !e.RctlIsdeleted.Value && e.RctlProlId == line.SpmlProlId && e.RctlRcpt.RcptGetrId== gateEntry.GetrId)?.RctlQuantity ?? 0;
                yield return new ShipmentLinesRestModel
                {
                    OrderLineId = line.SpmlProl.ProlId,
                    OrderLineNumber = line.SpmlProl.ProlNumber,
                    OrderedQuantity = line.SpmlProl.ProlQuantity,
                    Quantity = line.SpmlQuantity,
                    DeliveredQuantity = deliveredQuantity,
                    ShipmentLineId = line.SpmlId,
                    Date = line.SpmlCreateddate,
                    Item = line.SpmlProl.ProlItem
                };
            }
        }
    }
}
