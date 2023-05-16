﻿
namespace GraphQLResolvers.Queries.Dto
{
    public class FullAddress
    {
        public Guid FullAddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
