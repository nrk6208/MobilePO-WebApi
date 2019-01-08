using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NB.MOBILEPO.BAL.Services
{
    public class PurchaseOrdersService : CommonService, IPurchaseOrdersService
    {
        public PurchaseOrdersService(
               MobilePoDbContext dbContext,
               IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~PurchaseOrdersService()
        {
            GC.SuppressFinalize(this);
        }
        public IEnumerable<PurchaseOrdersRestModel> GetAllPurchaseOrders()
        {
            //return new List<PurchaseOrdersRestModel>();
            return _dbContext.Purchaseorders
                .Include(i => i.Purchaseorderlines)
                //.ThenInclude(t => t.Shipmentlines)
                //.Include(i => i.Purchaseorderlines)
                .ThenInclude(te=>te.Receiptlines)
                .Include(i => i.Purchaseorderlines)
                .ThenInclude(tet => tet.Shipmentlines)
                .Where(e => !e.ProrIsdeleted.Value).AsEnumerable()
                .Select(s => new PurchaseOrdersRestModel
                {
                    OrderId = s.ProrId,
                    OrderNumber = s.ProrNumber,
                    Date = s.ProrDate,
                    Status = s.ProrStatus,
                    Quantity = s.Purchaseorderlines.Sum(c => c.ProlQuantity),
                    Lines = GetAllPurchaseOrderLines(s.Purchaseorderlines.Where(e => e.Receiptlines.Count() == 0 ? !e.ProlIsdeleted.Value : (e.Receiptlines.Sum(su => su.RctlQuantity) < e.ProlQuantity) && !e.ProlIsdeleted.Value))
                }).OrderByDescending(o => o.OrderId);
            //}).Where(e => e.Lines.Count(c => c.ShippingQuantity > 0) > 0).OrderByDescending(o => o.OrderId);
        }
        private IEnumerable<PurchaseOrderLinesRestModel> GetAllPurchaseOrderLines(IEnumerable<Purchaseorderlines> purchaseorderLines)
        {
            foreach (var line in purchaseorderLines)
            {
                var deliveredQuantity = line.Receiptlines.Where(e => !e.RctlIsdeleted.Value).Sum(s => s.RctlQuantity);
                var unDeliveredQuantity = line.Shipmentlines.Where(e => !e.SpmlIsdeleted.Value).Sum(s => s.SpmlQuantity) - deliveredQuantity;
                yield return new PurchaseOrderLinesRestModel
                {
                    OrderLineId = line.ProlId,
                    OrderLineNumber = line.ProlNumber,
                    Date = line.ProlDate,
                    Item = line.ProlItem,
                    Status = line.ProlStatus,
                    Quantity = line.ProlQuantity,
                    ShippingQuantity = line.ProlQuantity - deliveredQuantity - unDeliveredQuantity,
                    DeliveredQuantity = deliveredQuantity,
                    UnDeliveredQuantity = unDeliveredQuantity
                };
            }
        }
    }
}
