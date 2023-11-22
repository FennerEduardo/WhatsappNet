using Microsoft.AspNetCore.Mvc;
using WhatsappNet.Models.WhatsappCloud;
using WhatsappNet.Services.WhatsappCloud.SendMessage;
using WhatsappNet.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WhatsappNet.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        private readonly IUtil _util;
        public WhatsappController(IWhatsappCloudSendMessage whatsappCloudSendMessage, IUtil util)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
            _util = util;
        }

        [HttpGet("sample")]
        public async Task<IActionResult> Sample()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "573125202013",   
                recipient_type = "individual",
                type = "text",
                text = new {                
                    body =  "*hello_world* - _hello world_ - ~hello world~ - ```hello world ```"
                }
            };
            var result = await _whatsappCloudSendMessage.Execute(data);
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

                    object objectMessage;

                    switch (userText.ToUpper())
                    {
                        case "TEXT":
                           objectMessage =  _util.TextMessage("Test message send", userNumber);
                            break;

                        case "IMAGE":
                           objectMessage =  _util.ImageMessage("https://fennereduardo.com/img/logotexto.png", userNumber);
                            break;

                        case "AUDIO":
                           objectMessage =  _util.AudioMessage("https://sistema.litigius.com.co/storage/post/1647969717reglamento-interno-del-trabajo.mp3", userNumber);
                            break;
                            
                        case "VIDEO":
                           objectMessage =  _util.VideoMessage("https://samplelib.com/lib/preview/mp4/sample-5s.mp4", userNumber);
                            break;

                        case "DOCUMENT":
                            objectMessage = _util.DocumentMessage("https://fennereduardo.com/img/logotexto.png", userNumber);
                            break;
                            
                        case "LOCATION":
                            objectMessage = _util.LocationMessage(userNumber);
                            break;

                        case "BUTTON":
                            objectMessage = _util.ButtonsMessage(userNumber);
                            break;

                        default:
                            objectMessage = _util.TextMessage("I can't get it!", userNumber);
                            break;

                    }

                   await _whatsappCloudSendMessage.Execute(objectMessage);
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
