using Microsoft.AspNetCore.Components.Routing;
using System.Reflection.PortableExecutable;

namespace WhatsappNet.Util
{
    public class Util : IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "text",
                text = new
                {
                    body = message
                }
            };
        } 
        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "image",
                image = new
                {
                    link = url
                }
            };
        } 
        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }
        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }
        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }
        public object LocationMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "location",
                location = new
                {
                    latitude = "4.5955650592839",
                    longitude = "-74.15872313067904",
                    name = "Bonabú",
                    address = "57v Sur-9, Cra. 67, Bogotá"
                }
            };
        }
        public object ButtonsMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    header = new  {
                        type = "text",
                        text = "Hola, ¿cómo podemos ayudarte hoy?"
                    },
                    body = new 
                    {
                        text = "Elige una opción"
                    },
                    action = new
                    {
                        buttons = new List<object> {
                            new
                            {
                                type = "reply",
                                reply = new {
                                    id = "opcion_1",
                                    title = "Opción 1"
                                }
                            },

                            new {
                                type = "reply",
                                reply = new {
                                    id = "opcion_2",
                                    title = "Opción 2"
                                }
                            }
                    },
                    footer = new
                        {
                            text = "Estas son las opciones disponibles"
                        }
                    }
                }
            };
        }
    }
}
