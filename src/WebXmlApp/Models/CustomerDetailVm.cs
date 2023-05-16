using Data.Repository.Entities;

namespace WebXmlApp.Models
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
        public FullAddressVm FullAddress { get; set; }      
    }
}
