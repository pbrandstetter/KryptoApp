using KrypoLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KryptoApp_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            User user = new User();
            user.Username = "hasa";
            user.Password = "cjkxa";
            RegisterAsync(user);
        }

        static HttpClient client = new HttpClient();

        public static async Task<HttpContent> RegisterAsync(User user)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Console.WriteLine("error");
            HttpResponseMessage resp = await client.PostAsync("localhost:5000/api/user/register/", httpContent);
            Console.WriteLine(resp);
            Console.WriteLine(resp.IsSuccessStatusCode);
            Console.WriteLine(resp.Content);
            Console.WriteLine(resp.StatusCode);
            return resp.Content;
        }
        public static async Task<Uri> LoginAsync(User user)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage resp = await client.PostAsync("api/user/login", httpContent);
            return resp.Headers.Location;
        }
        public static async Task<string> GetAllUserAsync()
        {

            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/user");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        /*    public static async void SendMassage(Message message, string sendUsername, string receiveUsername)
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(message,sendUsername,receiveUsername), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage resp = await client.PostAsync("api/user/login", httpContent);
                return resp.Headers.Location;
            }*/

    }
}
