using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NB.MOBILEPO.BAL.Helpers;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Services;
using NB.MOBILEPO.WEBAPI.Extensions;

namespace NB.MOBILEPO.WEBAPI.Controllers
{
    /// <summary>
    /// class to access Dashboard services
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IDashboardService _dashboardService;
        private readonly CustomLogger<DashboardService> _logger;
        /// <summary>
        /// constructor for Dashboard controller
        /// </summary>
        /// <param name="dashboardService"></param>
        /// <param name="logger"></param>
        public DashboardController(IDashboardService dashboardService, ILogger<DashboardService> logger)
        {
            _logger = new CustomLogger<DashboardService>(logger);
            _dashboardService = (DashboardService)dashboardService;
        }
        /// <summary>
        /// Destructor for Dashboard controller
        /// </summary>
        ~DashboardController()
        {
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Method to get Dashboard data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDashboardData/{months:int?}")]
        public IActionResult GetDashboardData(int? months)
        {
            try
            {
                return Ok(_dashboardService.GetDashboardData(months));
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
    }
}