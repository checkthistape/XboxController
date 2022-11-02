using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XBoxAsMouse
{
    internal class RestConnector
    {
        public async Task SendMsgAsync(int x1, int y1, int a1, int b1)
        {
            
            var client = new HttpClient();
            object content = new
            {
                x = x1,
                y = y1,
                a = a1,
                b = b1
            };
            var json = JsonConvert.SerializeObject(content);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://192.168.1.122:5000/robo", data);
            Console.WriteLine(result.StatusCode);

        }
    }

}
