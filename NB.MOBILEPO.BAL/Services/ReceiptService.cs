using Microsoft.AspNetCore.Http;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NB.MOBILEPO.BAL.Services
{
    public class ReceiptService : CommonService, IReceiptService
    {
        public ReceiptService(
            MobilePoDbContext dbContext,
            IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~ReceiptService()
        {
            GC.SuppressFinalize(this);
        }

        public void PostReceipt(ReceiptModel model)
        {
            model.ReceiptNumber = "GRN00001";   // should be taken from LN
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var receipt = _dbContext.Receipts.Add(new Receipts
                    {
                        RcptCreatedby = CurrentUserId,
                        RcptCreateddate = DateTime.UtcNow,
                        RcptGetrId = model.GateEntryId,
                        RcptProrId = model.OrderId,
                        RcptReceiptno = model.ReceiptNumber
                    });
                    if (SaveDbChanges())
                    {
                        _dbContext.Receiptlines.AddRange(bindReceiptLines(model.ReceiptLines, receipt.Entity.RcptId));
                        if(SaveDbChanges())
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        private IEnumerable<Receiptlines> bindReceiptLines(IEnumerable<ReceiptLinesModel> receiptLines, long receiptId)
        {
            foreach (var line in receiptLines)
            {
                yield return new Receiptlines
                {
                    RctlCreatedby = CurrentUserId,
                    RctlCreateddate = DateTime.UtcNow,
                    RctlQuantity = line.Quantity,
                    RctlProlId = line.OrderLineId,
                    RctlRcptId = receiptId
                };
            }
        }
    }
}
