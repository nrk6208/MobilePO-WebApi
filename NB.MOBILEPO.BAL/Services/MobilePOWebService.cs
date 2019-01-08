using Microsoft.AspNetCore.Http;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models.ServiceModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NB.MOBILEPO.BAL.Services
{
    public class MobilePOWebService : CommonService, IMobilePOWebService
    {
        public MobilePOWebService(
               MobilePoDbContext dbContext,
               IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~MobilePOWebService()
        {
            GC.SuppressFinalize(this);
        }
        public bool PostBulkPurchaseOrders(IEnumerable<PurchaseOrderServiceModel> model)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Purchaseorders.AddRange(BindPurchaseOrders(model));
                    if (SaveDbChanges())
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }
        public bool PostOrUpdatePurchaseOrder(PurchaseOrderServiceModel model)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var record = _dbContext.Purchaseorders.SingleOrDefault(s => string.Equals(s.ProrNumber, model.OrderNumber, StringComparison.OrdinalIgnoreCase));
                    if (record == null)
                    {
                        _dbContext.Purchaseorders.AddRange(BindPurchaseOrders(new List<PurchaseOrderServiceModel> { model }));
                    }
                    else
                    {
                        record.ProrDate = model.OrderDate;
                        record.ProrStatus = model.OrderStatus;
                        record.ProrModifiedby = CurrentUserId;
                        record.ProrModifieddate = DateTime.UtcNow;

                        PostOrUpdatePurchaseOrderLines(model.Lines);
                    }
                    if (SaveDbChanges())
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }
        private void PostOrUpdatePurchaseOrderLines(IEnumerable<PurchaseOrderLinesServiceModel> model)
        {
            foreach (var line in model)
            {
                var record = _dbContext.Purchaseorderlines.SingleOrDefault(s => s.ProlNumber == line.LineNumber);
                if (record==null)
                {
                    _dbContext.Purchaseorderlines.AddRange(BindPurchaseOrderLines(record.ProlProrId, new List <PurchaseOrderLinesServiceModel> { line }));
                }
                else
                {
                    record.ProlStatus = line.LineStatus;
                    record.ProlDate = line.LineDate;
                    record.ProlModifiedby = CurrentUserId;
                    record.ProlModifieddate = DateTime.UtcNow;
                }
            }
        }
        private IEnumerable<Purchaseorders> BindPurchaseOrders(IEnumerable<PurchaseOrderServiceModel> model)
        {
            foreach (var order in model)
            {
                var record = new Purchaseorders
                {
                    ProrDate = order.OrderDate,
                    ProrNumber = order.OrderNumber,
                    ProrIsdeleted = false,
                    ProrStatus = order.OrderStatus
                };
                _dbContext.Purchaseorderlines.AddRange(BindPurchaseOrderLines(record.ProrId, order.Lines));
                yield return record;
            }
        }
        private IEnumerable<Purchaseorderlines> BindPurchaseOrderLines(long orderId, IEnumerable<PurchaseOrderLinesServiceModel> model)
        {
            foreach (var line in model)
            {
                yield return new Purchaseorderlines
                {
                    ProlProrId = orderId,
                    ProlDate = line.LineDate,
                    ProlIsdeleted = false,
                    ProlCreateddate = line.LineDate,
                    ProlItem = line.Item,
                    ProlItemdescription = line.ItemDescription,
                    ProlNumber = line.LineNumber,
                    ProlQuantity = line.Quantity
                };
            }
        }
    }
}
