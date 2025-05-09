using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Viewmodels
{
    public class CustomerSearchViewModel
    {
        public int CustomerId { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string? NationalId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int? AccountId { get; set; }

    }
}
