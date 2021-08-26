using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParcelStatusService;
using ParcelStatusService.Helper;
using ParcelStatusService.Manager;
using System;
using System.Threading.Tasks;

namespace MailApplication.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParcelStatusController : ControllerBase
    {
        private readonly ILogger<ParcelStatusController> _logger;
        private readonly IParcelStatusManager _parcelStatusManager;
        private readonly IStatusHelper _statusHelper;
        public ParcelStatusController(ILogger<ParcelStatusController> logger,
            IParcelStatusManager parcelManager,
            IStatusHelper statusHelper)
        {
            _logger = logger;
            _parcelStatusManager = parcelManager;
            _statusHelper = statusHelper;
        }
        [HttpPost("{id}/status/waitingsender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusWaitingSender([FromRoute] int id)
        {

            if (await _parcelStatusManager.GetStatus(id.ToString()) == null)
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.WaitingSender, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpPost("{id}/status/insenderoffice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusInSenderOffice([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.InSenderOffice))
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.InSenderOffice, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpPost("{id}/status/readytoship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusReadyToShip([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.ReadyToShip))
            {
                await _parcelStatusManager.StartSending(id);
                await _parcelStatusManager.UpdateStatus(StatusENUM.ReadyToShip, id);

                return Ok("Status change and sending message to queue Success");
            }
            else
            {
                return BadRequest("Status change error");
            }

        }
        [HttpPost("{id}/status/ontheway")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusOnTheWay([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.OnTheWay))
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.OnTheWay, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpPost("{id}/status/inrecipientoffice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusInRecipientOffice([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.InRecipientOffice))
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.InRecipientOffice, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpPost("{id}/status/received")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusReceived([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.Received))
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.Received, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpPost("{id}/status/receivedbysender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetStatusReceivedBySender([FromRoute] int id)
        {

            if (await _statusHelper.CheckStatus(id, StatusENUM.ReceivedBySender))
            {
                await _parcelStatusManager.UpdateStatus(StatusENUM.ReceivedBySender, id);
                return Ok("Success status change");
            }
            else
            {
                return BadRequest("Status change error");
            }
        }
        [HttpGet("status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetStatus([FromRoute] int id)
        {
            
            var result = await _parcelStatusManager.GetStatus(id.ToString());
            if (result != null)
                return Ok(result);
            else
                return NotFound("Status not found");
        }
        [HttpDelete("status/remove/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveStatus([FromRoute] int id)
        {
            
            var currentStatus = await _parcelStatusManager.GetStatus(id.ToString());
            if (currentStatus.Equals(StatusENUM.WaitingSender.ToString()))
            {
                await _parcelStatusManager.RemoveStatus(id.ToString());
                return Ok("Success remove");
            }
            else
            {
                return BadRequest("Remove error");
            }
        }
    }
}
