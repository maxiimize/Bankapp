using Bankapp.Viewmodels;

namespace Bankapp.Interfaces
{
    public interface IAccountService
    {
        AccountDetailsVM GetAccountDetails(int accountId);
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
        void Transfer(int fromAccountId, int toAccountId, decimal amount);
    }

}
