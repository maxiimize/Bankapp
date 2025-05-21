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

        public AccountDetailsVM Account { get; set; }

        public IActionResult OnGet(int accountId)
        {
            Account = _accountService.GetAccountDetails(accountId, 0, 20); // Visa de första 20
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }


        public IActionResult OnGetLoadMoreTransactions(int accountId, int skip)
        {
            var transactions = _accountService.GetTransactions(accountId, skip, 20);

            if (!transactions.Any())
                return Content(""); // Returnerar tomt om inga fler transaktioner

            var rowsHtml = string.Join("", transactions.Select(tx => $@"
        <tr>
            <td>{tx.Date:yyyy-MM-dd}</td>
            <td>{tx.Type}</td>
            <td>{tx.Operation}</td>
            <td>{tx.Amount.ToString("C")}</td>
            <td>{(tx.Balance == 0 ? "<span class='text-muted'>0</span>" : tx.Balance.ToString("C"))}</td>
        </tr>
    "));

            return Content(rowsHtml, "text/html");
        }
    }
}
