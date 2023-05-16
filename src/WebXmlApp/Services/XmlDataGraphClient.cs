using Data.Repository.Entities;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System.Text.Json;
using WebXmlApp.Models;
using static HotChocolate.ErrorCodes;
using static WebXmlApp.Models.CustomerAndFullAddressResponse;

namespace WebXmlApp.Services
{
    public class XmlDataGraphClient
    {
        private readonly GraphQLHttpClient _client;
        public XmlDataGraphClient(GraphQLHttpClient client)
        {
            _client = client;
        }

        public async Task<CustomerContent> GetCustomer(Guid id)
        {
            var queryRequest = new GraphQLRequest
            {
                Query = @"
                query CustomerQuery($custId: UUID!)
                { customer (id: $custId) 
                    { 
                      customerID companyName contactName contactTitle phone fax fullAddressId id 
                      fullAddress { 
                          fullAddressId address city region postalCode country 
                        } 
                    } 
                }",
                OperationName = "CustomerQuery",
                Variables = new { custId = id }
            };

            GraphQLResponse<CustomerAndFullAddressResponse>? graphQLResponse = null;

            try
            {
                graphQLResponse = await _client.SendQueryAsync<CustomerAndFullAddressResponse>(queryRequest);
            }
            catch (Exception ex)
            {
                // log It
            }

            return graphQLResponse.Data.Customer;
        }
    }
}
