using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Bankapp.Pages.Accounts
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;

        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty, Required(ErrorMessage = "Transaktionstyp krävs.")]
        public string TransactionType { get; set; }

        [BindProperty]
        [Range(1, int.MaxValue, ErrorMessage = "Ogiltigt Från-konto.")]
        public int FromAccountId { get; set; }

        [BindProperty]
        [Range(1, int.MaxValue, ErrorMessage = "Ogiltigt Till-konto.")]
        public int ToAccountId { get; set; }

        [BindProperty]
        [Range(0.01, double.MaxValue, ErrorMessage = "Belopp måste vara större än 0.")]
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
                if (Amount <= 0)
                {
                    Message = "Beloppet måste vara större än 0.";
                    return Page();
                }

                switch (TransactionType)
                {
                    case "Deposit":
                        if (ToAccountId <= 0)
                            throw new Exception("Vänligen ange ett giltigt mål-konto för insättning.");
                        _accountService.Deposit(ToAccountId, Amount);
                        Message = $"Insättning av {Amount:C} till konto {ToAccountId} genomförd.";
                        break;

                    case "Withdraw":
                        if (FromAccountId <= 0)
                            throw new Exception("Vänligen ange ett giltigt från-konto för uttag.");
                        _accountService.Withdraw(FromAccountId, Amount);
                        Message = $"Uttag av {Amount:C} från konto {FromAccountId} genomförd.";
                        break;

                    case "Transfer":
                        if (FromAccountId <= 0 || ToAccountId <= 0)
                            throw new Exception("Vänligen ange både från- och till-konto för överföring.");
                        if (FromAccountId == ToAccountId)
                            throw new Exception("Från- och till-konto får inte vara samma.");
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
                Message = "Fel: " + ex.Message;
            }

            return Page();
        }
    }
}
