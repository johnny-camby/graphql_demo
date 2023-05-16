using Data.Repository.Entities;
using Data.Repository.Interfaces;
using GraphQLResolvers.Queries.Dto;

namespace GraphQLResolvers.Queries
{
    [ExtendObjectType("Query")]
    public class CustomerQueryResolver
    {
        [GraphQLName("customers")]
        [GraphQLDescription("Customers API")]
        public async Task<IEnumerable<CustomerEntity>> GetAllCustomerAsync([Service]IXmlImporterRepository<CustomerEntity> customerRepository)
        {
            return await customerRepository.GetAsync();
        }

        [GraphQLName("customer")]
        [GraphQLDescription("Get Customer API")]
        public async Task<CustomerDetailVm> GetCustomerAsync(Guid id, [Service]IXmlImporterRepository<CustomerEntity> customerRepository)
        {
            var customer =  await customerRepository.GetAsync(id);

            var data = new CustomerDetailVm 
            {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                ContactName  = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Fax =  GetFax(customer.Fax),
                Phone = customer.Phone,
                CustomerID = customer.CustomerID,
                FullAddressId = customer.FullAddressId,
                FullAddress = new FullAddress
                {
                    FullAddressId = customer.FullAddressId,
                    Address = customer.FullAddress.Address,
                    City = customer.FullAddress.City,
                    Country = customer.FullAddress.Country,
                    PostalCode = customer.FullAddress.PostalCode,
                    Region = customer.FullAddress.Region
                }                           
            };

            return data;
        }

        private string GetFax(string? fax)
        {
            if(fax == null)
            {
                return string.Empty;
            }
            else
            {
                return fax;
            }
        }
    }
}
