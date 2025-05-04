using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.Viewmodels;

namespace Bankapp.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DetailsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        public AccountDetailsVM Account { get; set; }

        public IActionResult OnGet()
        {
            Account = _accountService.GetAccountDetails(AccountId);

            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
