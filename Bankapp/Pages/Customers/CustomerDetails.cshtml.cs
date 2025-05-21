using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.Viewmodels;

namespace Bankapp.Pages.Customers
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerDetailsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        public CustomerOverviewVM Customer { get; set; }

        public IActionResult OnGet()
        {
            Customer = _customerService.GetCustomerOverview(CustomerId);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
