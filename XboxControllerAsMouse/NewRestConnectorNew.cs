using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XBoxAsMouse
{
    internal class RestConnector
    {

        public async Task SendMsgAsync(string msg)
        {
            using (var client = new HttpClient());
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", "alice@example.com"),
            new KeyValuePair<string, string>("password", "password1")
            });

            var result = await client.PostAsync("http://192.168.1.122:5000/robo", content).ConfigureAwait(false);
            string resultContent = await result.Content.ReadAsStringAsync();
        }
    }
}
