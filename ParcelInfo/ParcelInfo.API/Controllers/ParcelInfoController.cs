using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParcelInfoService.Domain;
using ParcelInfoService.Managers;
using ParcelInfoService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailApplication.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParcelInfoController : ControllerBase
    {
        private readonly ILogger<ParcelInfoController> _logger;
        private readonly IParcelManager _parcelManager;

        public ParcelInfoController(ILogger<ParcelInfoController> logger,
            IParcelManager parcelManager)
        {
            _logger = logger;
            _parcelManager = parcelManager;
        }
        [HttpPost("parcels/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateParcel([FromBody]Parcel parcel)
        {
            if (await _parcelManager.CreateParcel(parcel))
                return Ok("Success parcel create");
            else
                return BadRequest("Parcel not created");
        }
        [HttpDelete("parcels/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteParcel([FromRoute] string id)
        {

            if (await _parcelManager.DeleteParcel(id))
                return Ok("Success delete parcel");
            else
                return BadRequest("Error delete parcel");
           
        }
        [HttpGet("parcels/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ParcelModel>> GetParcelById([FromRoute] string id)
        {
            
            var result=await _parcelManager.GetParcelById(id);
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }
        [HttpGet("parcels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ParcelModel>>> GetParcels()
        {
            var result=await _parcelManager.GetParcels();
            if(result==null)
                return NotFound();
            return Ok(result);
        }
    }
}
