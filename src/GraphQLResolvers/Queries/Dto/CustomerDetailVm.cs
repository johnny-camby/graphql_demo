
namespace GraphQLResolvers.Queries.Dto
{
    public class CustomerDetailVm
    {
        public Guid Id { get; set; }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; } = string.Empty;
        public Guid FullAddressId { get; set; }
        public FullAddress FullAddress { get; set; }
    }
}
