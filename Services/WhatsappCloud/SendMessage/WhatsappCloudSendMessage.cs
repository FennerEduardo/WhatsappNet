using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace WhatsappNet.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage: IWhatsappCloudSendMessage
    {
        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string endpoint = "https://graph.facebook.com";
                string version = "v17.0";
                string phoneNumberId = "161341700402393";
                string accessToken = "EAAFTDveXqEkBO1wLmiAJsHRZCrZBtC4DBhAuLJJaqGUvyu90hmMpLJvbUPkKaq7JFMEFV3kZALXBjXqPTCzgTenNBxb5OtTmNch7KaU1xericELXzjPBgZAYDSSX5wO4AtE3EzldEZCWzSqhQO9Ue7Y144r5nVxCiipHZBi2DE8CiFDlVOwLgWwpZBZCq0cugliK0CsaX55Q5A80DZBWZBJEgZCl6OT8l2PdHe5HhzB2pvVvbjZBhehaZAw9P";
                string uri = $"{endpoint}/{version}/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
