using Microsoft.AspNetCore.Mvc;

namespace WhatsappNet.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        [HttpGet]
        public IActionResult Sample()
        {
            return Ok("Ok sample");
        }
    }
}
