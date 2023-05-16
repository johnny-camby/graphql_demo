
using Data.Repository.Entities;
using Data.Repository.Interfaces;
using GraphQLResolvers.Mutations.Dtos;

namespace GraphQLResolvers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CustomerMutationResolver
    {
        [GraphQLName("createCustomer")]
        [GraphQLDescription("Create New Customer")]
        public async Task<CustomerEntity> CreateCustomerAsync(CreateCustomerRequest request,
            [Service]IXmlImporterRepository<CustomerEntity> customerRepository)
        {
            var customerEntity = new CustomerEntity
            {
                CustomerID = request.CustomerID,
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Fax = request.Fax,
                Phone = request.Phone,
                FullAddress = new FullAddressEntity
                {
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country,
                    PostalCode = request.PostalCode,
                    Region = request.Region
                }
            };
            return await customerRepository.AddAsync(customerEntity);
        }

        [GraphQLName("updateCustomer")]
        [GraphQLDescription("Update Customer")]
        public async Task UpdateCustomerAsync(UpdateCustomerRequest request,
            [Service]IXmlImporterRepository<CustomerEntity> customerRepository)
        {
            var customerEntity = await customerRepository.GetAsync(request.Id);

            if(customerEntity == null)
            {
                throw new GraphQLException(new Error("Customer not found", "CUSTOMER_NOT_FOUND"));
            }

            customerEntity = new CustomerEntity
            {
                Id = customerEntity.Id,
                CustomerID = request.CustomerID,
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Fax = request.Fax,
                Phone = request.Phone,
                FullAddressId = customerEntity.FullAddressId,
                FullAddress = new FullAddressEntity
                {
                    FullAddressId = customerEntity.FullAddressId,
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country,
                    PostalCode = request.PostalCode,
                    Region = request.Region
                }
            };
            await customerRepository.UpdateAsync(customerEntity);
        }

        [GraphQLName("deleteCustomer")]
        [GraphQLDescription("Delete Customer")]
        public async Task DeleteCustomer(DeleteCustomerRequest request,
            [Service]IXmlImporterRepository<CustomerEntity> customerRepository)
        {
            var customerToDelete = await customerRepository.GetAsync(request.Id);

            if (customerToDelete == null)
            {
                throw new GraphQLException(new Error("Customer not found", "CUSTOMER_NOT_FOUND"));
            }
            await customerRepository.DeleteAsync(customerToDelete);
        }
    }
}
