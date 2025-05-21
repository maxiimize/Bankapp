using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.Viewmodels;

namespace Bankapp.Pages.Customers
{
    public class SearchModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public SearchModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public PagedResult<CustomerSearchViewModel> SearchResult { get; set; }

        public void OnGet()
        {
            SearchResult = _customerService.SearchCustomers(Name, City, PageNumber, 50);
        }
    }
}
