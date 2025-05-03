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
    public class StatisticsService : IStatisticsService
    {
        private readonly BankAppDataContext _context;

        public StatisticsService(BankAppDataContext context)
        {
            _context = context;
        }

        public StatisticsVM GetStatistics()
        {
            var totalCustomers = _context.Customers.Count();
            var totalAccounts = _context.Accounts.Count();

            var balancePerCountry = _context.Customers
                .GroupBy(c => c.Country)
                .Select(g => new
                {
                    Country = g.Key,
                    TotalBalance = g.SelectMany(c => c.Dispositions)
                                    .Where(d => d.Account != null)
                                    .Select(d => d.Account.Balance)
                                    .Sum()
                })
                .ToDictionary(x => x.Country, x => x.TotalBalance);

            return new StatisticsVM
            {
                TotalCustomers = totalCustomers,
                TotalAccounts = totalAccounts,
                TotalBalancePerCountry = balancePerCountry
            };
        }
    }

}
