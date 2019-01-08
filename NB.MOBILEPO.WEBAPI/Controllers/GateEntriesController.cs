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
    /// class to access GateEntries services
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GateEntriesController : ControllerBase
    {
        private readonly IGateEntriesService _gateEntriesService;
        private readonly CustomLogger<GateEntriesController> _logger;
        /// <summary>
        /// constructor for GateEntries controller
        /// </summary>
        /// <param name="gateEntriesService"></param>
        /// <param name="logger"></param>
        public GateEntriesController(
            IGateEntriesService gateEntriesService, 
            ILogger<GateEntriesController> logger)
        {
            _logger = new CustomLogger<GateEntriesController>(logger);
            _gateEntriesService = (GateEntriesService)gateEntriesService;
        }
        /// <summary>
        /// Method to post new gateEntry record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostGateEntry")]
        public IActionResult PostGateEntry([FromBody]GateEntryModel model)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _gateEntriesService.PostGateEntry(model, out string NewGateEntryNumber);
                return Ok(new {
                    GateEntryNumber = NewGateEntryNumber
                });
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
        /// <summary>
        /// Method to get gateEntries by gateEntry number
        /// </summary>
        /// <param name="gateEntryNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByGateEntryNumber/{gateEntryNumber}")]
        public IActionResult GetGateEntry(string gateEntryNumber)
        {
            try
            {
                return Ok(_gateEntriesService.GetGateEntry(gateEntryNumber));
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }
    }
}