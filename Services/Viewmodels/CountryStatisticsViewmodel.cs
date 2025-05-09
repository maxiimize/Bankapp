using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Viewmodels
{
    public class CountryStatisticsViewModel
    {
        public string Country { get; set; } = string.Empty;
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }
    }

}
