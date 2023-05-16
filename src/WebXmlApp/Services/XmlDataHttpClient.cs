using Azure;
using Newtonsoft.Json;

namespace WebXmlApp.Services
{
    public class XmlDataHttpClient
    {
        private readonly HttpClient _httpClient;

        public XmlDataHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<CustomerContainer>> GetCustomers()
        {
            var response = await _httpClient.GetAsync(@"?query=
            { customers
              { id customerID companyName contactName contactTitle phone fax }
            }");
            var result = await response.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<Response<CustomerContainer>>(result);
            return x;
        }
    }
}
