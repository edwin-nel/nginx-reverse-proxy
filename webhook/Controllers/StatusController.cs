using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wouter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {

        static OnomondoSms lastReceived;

        [HttpGet]
        public IActionResult Status()
        {
            return Ok("service online");
        }

        [HttpPost("onomondo-sms")]
        public ActionResult ReceiveOnomondoSmsAsync([FromBody] OnomondoSms model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            lastReceived = model;
            return Ok();
        }

        [HttpGet("LastReceived")]
        public IActionResult LastReceived()
        {
            return Ok(lastReceived);
        }

        [HttpGet("LastReceivedAndReset")]
        public IActionResult LastReceivedAndReset()
        {
            var result = lastReceived;
            lastReceived = null;
            return Ok(result);
        }
    }

    public class OnomondoSms
    {
        public string type { get; set; }
        public string time { get; set; }
        public string sim_id { get; set; }
        public string iccid { get; set; }
        public string sim_label { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }
}
