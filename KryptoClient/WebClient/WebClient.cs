using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KryptoApp.WebClient
{
    public class WebClient
    {
        static HttpClient client = new HttpClient();

        public static async Task<Uri> RegisterAsync(Model.User user)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage resp = await client.PostAsync("api/user/register", httpContent);
            return resp.Headers.Location;
        }
    }
}
