using Microsoft.AspNetCore.Mvc;
using WhatsappNet.Models.WhatsappCloud;

namespace WhatsappNet.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        [HttpGet("sample")]
        public IActionResult Sample()
        {
            return Ok("Ok sample");
        }

        [HttpGet]
        public IActionResult VerifyToken()
        {
            string AccessToken = "HOLAMUNDO1212122";

            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if(challenge != null && token != null && token == AccessToken) 
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveMessage([FromBody] WhatsAppCloudModel body)
        {
            try 
            {
                var Message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                if(Message != null)
                {
                    var userNumber = Message.From;
                    var userText = GetUserText(Message);
                }
                return Ok("EVENT_RECEIVED");
            }
            catch(Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserText(Message message)
        {
            string TypeMessage = message.Type;

            if(TypeMessage.ToUpper() == "TEXT")
            {
                return message.Text.Body;
            }
            else if (TypeMessage.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive.Type;

                if(interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply.Title;
                }
                else if (interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply.Title;
                }
                else
                {
                    return string.Empty;
                }
            } else
            {
                return string.Empty;
            }
        }
    }
}
