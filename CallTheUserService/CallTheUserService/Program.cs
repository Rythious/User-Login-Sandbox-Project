using System.Text;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace CallTheUserService
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServiceTest();
        }
    }

    public class ServiceTest
    {
        public ServiceTest()
        {
            var httpClient = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:52833/Login")
            {
                Content = new StringContent("{ \"userName\": \"MyPasswordIsPassword\", \"password\": \"Password\"}", Encoding.UTF8, "application/json")
            };

            var result = httpClient.SendAsync(httpRequestMessage);

            result.Wait();

            var stream = result.Result.Content.ReadAsStreamAsync().Result;
            var readStream = new StreamReader(stream, Encoding.UTF8);
            var text = readStream.ReadToEnd();

            Console.WriteLine(text);
            Console.ReadLine();
        }
    }
}
