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
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.Services;
using NB.MOBILEPO.WEBAPI.Extensions;

namespace NB.MOBILEPO.WEBAPI.Controllers
{
    /// <summary>
    /// class to access Shipments services
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private IShipmentsService _shipmentsService;
        private readonly CustomLogger<ShipmentsService> _logger;
        /// <summary>
        /// constructor for Shipments controller
        /// </summary>
        /// <param name="shipmentsService"></param>
        /// <param name="logger"></param>
        public ShipmentsController(IShipmentsService shipmentsService, ILogger<ShipmentsService> logger)
        {
            _logger = new CustomLogger<ShipmentsService>(logger);
            _shipmentsService = (ShipmentsService)shipmentsService;
        }
        /// <summary>
        /// Destructor for Shipments controller
        /// </summary>
        ~ShipmentsController()
        {
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Method to get shipments by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByInvoiceNumber/{invoiceNumber}")]
        public IActionResult GetByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                return Ok(_shipmentsService.GetShipmentByInvoiceNumber(invoiceNumber));
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
        /// <summary>
        /// Method to get all Shipments along with lines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllShipments")]
        public IActionResult GetAllShipments()
        {
            try
            {
                return Ok(_shipmentsService.GetAllShipments());
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
        /// <summary>
        /// Method to post new shipment record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostShipment")]
        public IActionResult PostShipment([FromBody]ShipmentModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else if (_shipmentsService.IsInvoiceExisted(model.InvoiceNumber))
                {
                    return BadRequest("Duplicate Invoice");
                }
                //else if(!_shipmentsService.IsShipmentAllowed(model.Lines, out string message))
                //{
                //    return BadRequest(message);
                //}
                else
                {
                    bool res = _shipmentsService.PostShipment(model);
                    if (res)
                    {
                        return Ok();
                    }
                    return BadRequest("Shipment Creation failed.");
                }
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
    }
}