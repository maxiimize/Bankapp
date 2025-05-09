using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Bankapp.Pages.Accounts
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;

        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public string TransactionType { get; set; }

        [BindProperty]
        public int FromAccountId { get; set; }

        [BindProperty]
        public int ToAccountId { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        public string Message { get; set; }

        public void OnGet(int? fromAccountId = null)
        {
            if (fromAccountId.HasValue)
                FromAccountId = fromAccountId.Value;
        }

        public IActionResult OnPost()
        {
            try
            {
                switch (TransactionType)
                {
                    case "Deposit":
                        _accountService.Deposit(ToAccountId, Amount);
                        Message = $"Insättning av {Amount:C} till konto {ToAccountId} genomförd.";
                        break;
                    case "Withdraw":
                        _accountService.Withdraw(FromAccountId, Amount);
                        Message = $"Uttag av {Amount:C} från konto {FromAccountId} genomförd.";
                        break;
                    case "Transfer":
                        _accountService.Transfer(FromAccountId, ToAccountId, Amount);
                        Message = $"Överföring av {Amount:C} från konto {FromAccountId} till konto {ToAccountId} genomförd.";
                        break;
                    default:
                        Message = "Ogiltig transaktionstyp.";
                        break;
                }
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                Message = "Fel: " + inner;
            }

            return Page();
        }
    }
}
