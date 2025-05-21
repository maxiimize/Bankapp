using Services.Viewmodels;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        AccountDetailsVM GetAccountDetails(int accountId, int skip, int take);
        List<TransactionVM> GetTransactions(int accountId, int skip, int take);
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
        void Transfer(int fromAccountId, int toAccountId, decimal amount);
    }
}
