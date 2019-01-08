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
    /// class to access PurchaseOrder services
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrdersService _purchaseOrdersService;
        private readonly CustomLogger<PurchaseOrdersController> _logger;
        /// <summary>
        /// constructor for GateEntries controller
        /// </summary>
        /// <param name="purchaseOrdersService"></param>
        /// <param name="logger"></param>
        public PurchaseOrdersController(
            IPurchaseOrdersService purchaseOrdersService,
            ILogger<PurchaseOrdersController> logger)
        {
            _logger = new CustomLogger<PurchaseOrdersController>(logger);
            _purchaseOrdersService = (PurchaseOrdersService)purchaseOrdersService;
        }
        /// <summary>
        /// Method to get all PurchaseOrders along with lines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPurchaseOrders")]
        public IActionResult GetAllPurchaseOrders()
        {
            try
            {
                return Ok(_purchaseOrdersService.GetAllPurchaseOrders());
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
    }
}