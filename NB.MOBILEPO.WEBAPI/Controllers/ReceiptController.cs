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
    /// class to access Receipt services
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _service;
        private readonly CustomLogger<ReceiptController> _logger;
        /// <summary>
        /// constructor for Receipt controller
        /// </summary>
        /// <param name="receiptService"></param>
        /// <param name="logger"></param>
        public ReceiptController(
            IReceiptService receiptService,
            ILogger<ReceiptController> logger)
        {
            _logger = new CustomLogger<ReceiptController>(logger);
            _service = (ReceiptService)receiptService;
        }
        /// <summary>
        /// Method to create Receipt
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostReceipt")]
        public IActionResult PostReceipt([FromBody] ReceiptModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _service.PostReceipt(model);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
    }
}