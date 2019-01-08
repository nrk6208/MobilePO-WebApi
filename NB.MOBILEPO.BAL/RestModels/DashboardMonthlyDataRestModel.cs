using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class DashboardDataRestModel
    {
        public int Year { get; set; }
        public IEnumerable<DashboardDataMonthlyRestModel> Records { get; set; }
    }
    public class DashboardDataMonthlyRestModel
    {
        public string Month { get; set; }
        public IEnumerable<DashboardDataMonthlyRecordsRestModel> Records { get; set; }
    }
    public class DashboardDataMonthlyRecordsRestModel
    {
        public int OrderedQuantity { get; set; }
        public int ShippedQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int DefectedQuantity { get; set; }
    }
}
