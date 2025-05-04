using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Viewmodels;
using DataAccessLayer.Models;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _context;

        public AccountService(BankAppDataContext context)
        {
            _context = context;
        }

        public AccountDetailsVM GetAccountDetails(int accountId, int skip, int take)
        {
            var account = _context.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountDetailsVM
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Balance = a.Balance,
                    Created = a.Created,
                    CustomerId = a.Dispositions.FirstOrDefault().CustomerId,
                    TotalTransactionCount = a.Transactions.Count,
                    Transactions = a.Transactions
                        .OrderByDescending(t => t.Date)
                        .Skip(skip)
                        .Take(take)
                        .Select(t => new TransactionVM
                        {
                            Date = t.Date,
                            Amount = t.Amount,
                            Type = t.Type,
                            Operation = t.Operation,
                            Bank = t.Bank,
                            Balance = t.Balance
                        }).ToList()
                }).FirstOrDefault();

            return account;
        }

        public List<TransactionVM> GetTransactions(int accountId, int skip, int take)
        {
            return _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .Take(take)
                .Select(t => new TransactionVM
                {
                    Date = t.Date,
                    Amount = t.Amount,
                    Type = t.Type,
                    Operation = t.Operation,
                    Bank = t.Bank,
                    Balance = t.Balance
                }).ToList();
        }





        public void Deposit(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Beloppet måste vara större än 0.");

            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) throw new Exception("Konto hittades inte.");

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                Balance = account.Balance,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Deposit",
                Operation = "Cash deposit"
            });

            _context.SaveChanges();
        }

        public void Withdraw(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Beloppet måste vara större än 0.");

            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) throw new Exception("Konto hittades inte.");

            if (account.Balance < amount)
                throw new Exception("Otillräckligt saldo.");

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Amount = -amount,
                Balance = account.Balance,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Withdrawal",
                Operation = "Cash withdrawal"
            });

            _context.SaveChanges();
        }

        public void Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Beloppet måste vara större än 0.");

            var fromAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == fromAccountId);
            var toAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == toAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("Ett eller båda konton hittades inte.");

            if (fromAccount.Balance < amount)
                throw new Exception("Otillräckligt saldo på frånkontot.");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = fromAccountId,
                Amount = -amount,
                Balance = fromAccount.Balance,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Transfer Out",
                Operation = "Transfer to account " + toAccountId
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = toAccountId,
                Amount = amount,
                Balance = toAccount.Balance,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Transfer In",
                Operation = "Transfer from account " + fromAccountId
            });

            _context.SaveChanges();
        }
    }
}
