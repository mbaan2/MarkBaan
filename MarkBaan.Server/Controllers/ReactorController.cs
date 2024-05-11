using MarkBaan.Server.Model;
using MarkBaan.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarkBaan.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactorController : ControllerBase
    {
        private readonly IReactor _reactor;
        private readonly IValveControl _valveControl;
        private readonly IPressureSensor _pressureSensor;

        public ReactorController(IReactor reactor, IValveControl valveControl, IPressureSensor pressureSensor)
        {
            _reactor = reactor;
            _valveControl = valveControl;
            _pressureSensor = pressureSensor;
        }

        [HttpGet("GetPressure")]
        public JsonResult GetPressure()
        {
            return new JsonResult(_pressureSensor.Pressure);
        }

        [HttpGet("GetValveStatus")]
        public JsonResult GetValveStatus()
        {
            return new JsonResult(_valveControl.ValveStatus);
        }

        [HttpGet("GetPressureValues")]
        public ActionResult GetPressureValues()
        {
            return Ok(new { 
                ReleasePressure  = _reactor.ReleasePressure,
                IncreasePressure = _reactor.IncreasePressure 
            });
        }

        [HttpPut("AdjustPressureValues")]
        public ActionResult SetPressure([FromBody] PressureModel model)
        {
            bool approvedRelease = _reactor.CheckValueNewReleasePressure(model.ReleasePressure);
            bool approvedIncrease = _reactor.CheckValueNewIncreasePressure(model.IncreasePressure);
            if(approvedRelease && approvedIncrease)
            {
                try
                {
                    _reactor.ReleasePressure = Convert.ToDecimal(model.ReleasePressure);
                    _reactor.IncreasePressure = Convert.ToDecimal(model.IncreasePressure);
                } catch
                {
                    return BadRequest("Unable to convert values, please try again");
                }

                return Ok("Increase en release pressure updated.");
            }

            if (!approvedRelease && approvedIncrease) return BadRequest("Release pressure set too high, this is not allowed.");
            if (approvedRelease && !approvedIncrease) return BadRequest("Increase pressure set too low, this is not allowed.");
            return BadRequest("Release pressure set too high, this is not allowed. Increase pressure set too low, this is not allowed.");
        }
    }
}
