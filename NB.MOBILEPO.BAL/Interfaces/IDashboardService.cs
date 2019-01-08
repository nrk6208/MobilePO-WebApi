using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IDashboardService
    {
        //IEnumerable<DashboardDataRestModel> GetDashboardData();
        IEnumerable<DashboardDataMonthlyRestModel> GetDashboardData(int? months);
    }
}
