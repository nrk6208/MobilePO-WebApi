using Microsoft.AspNetCore.Http;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace NB.MOBILEPO.BAL.Services
{
    public class DashboardService : CommonService, IDashboardService
    {
        public DashboardService(
               MobilePoDbContext dbContext,
               IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~DashboardService()
        {
            GC.SuppressFinalize(this);
        }
        //public IEnumerable<DashboardDataRestModel> GetDashboardData()
        //public IEnumerable<DashboardDataMonthlyRestModel> GetDashboardData()
        //{
        //    //DateTime consideredDate = DateTime.UtcNow.AddMonths(-5); //last 6 months
        //    DateTime consideredDate = DateTime.UtcNow.AddMonths(-11); //last 1 year

        //    return _dbContext.Purchaseorders
        //        .Include(i => i.Purchaseorderlines)
        //        .Include(i => i.Shipments)
        //        .ThenInclude(it => it.Shipmentlines)
        //        .Where(e => !e.ProrIsdeleted.Value && e.ProrDate > new DateTime(consideredDate.Year, consideredDate.Month, 1))
        //        //.GroupBy(g => g.ProrDate.Year)
        //        //.Select(s => new DashboardDataRestModel
        //        //{
        //        //    Year = s.Key,
        //        //    Records = s
        //            .GroupBy(gs => gs.ProrDate.ToString("MMM"))
        //                            .Select(gss => new DashboardDataMonthlyRestModel
        //                            {
        //                                Month = gss.Key,
        //                                Records = gss.Select(gsss => new DashboardDataMonthlyRecordsRestModel
        //                                {
        //                                    OrderedQuantity = gsss.Purchaseorderlines
        //                                                        .Where(plw => !plw.ProlIsdeleted.Value)
        //                                                        .Sum(pls => pls.ProlQuantity),
        //                                    ShippedQuantity = gsss.Shipments
        //                                                        .Sum(sps => sps.Shipmentlines.Sum(spls => spls.SpmlQuantity)),
        //                                    DeliveredQuantity = gsss.Receipts
        //                                                        .Sum(rcs => rcs.Receiptlines.Sum(rcls => rcls.RctlQuantity))
        //                                })
        //                            //})
        //        });

        //}
        public IEnumerable<DashboardDataMonthlyRestModel> GetDashboardData(int? months)
        {
            months = months ?? 12;
            DateTime consideredDate = DateTime.UtcNow.AddMonths(months.Value * -1);
            IEnumerable<string> consideredMonths = GetConsideredMonths(months.Value);
            List<DashboardDataMonthlyRestModel> result = new List<DashboardDataMonthlyRestModel>();
            var records = _dbContext.Purchaseorders
                .Include(i => i.Purchaseorderlines)
                .Include(i => i.Shipments)
                .ThenInclude(it => it.Shipmentlines)
                .Where(e => !e.ProrIsdeleted.Value && e.ProrDate > new DateTime(consideredDate.Year, consideredDate.Month, 1))
                .GroupBy(gs => gs.ProrDate.ToString("MMM"))
                .Select(gss => new DashboardDataMonthlyRestModel
                {
                    Month = gss.Key,
                    Records = gss.Select(gsss => new DashboardDataMonthlyRecordsRestModel
                    {
                        OrderedQuantity = gsss.Purchaseorderlines
                                            .Where(plw => !plw.ProlIsdeleted.Value)
                                            .Sum(pls => pls.ProlQuantity),
                        ShippedQuantity = gsss.Shipments
                                            .Sum(sps => sps.Shipmentlines.Sum(spls => spls.SpmlQuantity)),
                        DeliveredQuantity = gsss.Receipts
                                            .Sum(rcs => rcs.Receiptlines.Sum(rcls => rcls.RctlQuantity))
                    })
                });
            if (records.Count() < consideredMonths.Count())
            {
                foreach (var month in consideredMonths)
                {
                    var _res = records.SingleOrDefault(s => s.Month == month);
                    if (_res == null)
                        result.Add(new DashboardDataMonthlyRestModel
                        {
                            Month = month,
                            Records = Enumerable.Empty<DashboardDataMonthlyRecordsRestModel>()
                        });
                    else
                        result.Add(_res);
                }
            }

            return result;
        }
        private IEnumerable<string> GetConsideredMonths(int months)
        {
            for (int i = months - 1; i >= 0 ; i--)
            {
                yield return DateTime.UtcNow.AddMonths(i*-1).ToString("MMM");
            }
        }
    }
}
