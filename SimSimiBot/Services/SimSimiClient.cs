using Newtonsoft.Json;
using SimSimiBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimSimiBot.Services
{
    public class SimSimiClient
    {
        /// <summary>
        /// Use your API KEY from the SimSimi Portal
        /// </summary>
        private const string SimSimiKey = "YOUR_API_KEY";


        public async Task<string> SimSimiRequest(string message)
        {
            var request = $"http://sandbox.api.simsimi.com/request.p?key={SimSimiKey}&lc=es&ft=1.0&text={message}";
            var httpResponse = await new HttpClient().GetAsync(request);

            if (!httpResponse.IsSuccessStatusCode)
                return String.Empty;

            var response = await httpResponse.Content.ReadAsStringAsync();

            var simSimi = JsonConvert.DeserializeObject<SimSimiResponse>(response);
            if (simSimi.Result == 100)
                return simSimi.Response;

            return String.Empty;
        }
    }
}
