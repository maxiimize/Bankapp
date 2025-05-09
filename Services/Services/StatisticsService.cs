using System;
using System.Collections.Generic;
using System.Linq;
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


            var customerAccounts = _context.Customers
                .Select(c => new
                {
                    c.Country,
                    Accounts = c.Dispositions.Select(d => d.Account).Where(a => a != null).Select(a => new { a.AccountId, a.Balance })
                })
                .ToList();

            var balancePerCountry = customerAccounts
                .GroupBy(c => c.Country)
                .ToDictionary(
                    g => g.Key,
                    g => g.SelectMany(c => c.Accounts)
                          .GroupBy(a => a.AccountId)
                          .Select(a => a.First().Balance)
                          .Sum()
                );

            return new StatisticsVM
            {
                TotalCustomers = totalCustomers,
                TotalAccounts = totalAccounts,
                TotalBalancePerCountry = balancePerCountry
            };
        }

            public List<CountryStatisticsViewModel> GetCountryStatistics()
        {
            var data = _context.Customers
                .Select(c => new
                {
                    c.Country,
                    Accounts = c.Dispositions
                                .Where(d => d.Account != null)
                                .Select(d => d.Account)
                })
                .ToList();

            var grouped = data
                .GroupBy(c => c.Country)
                .Select(g => new CountryStatisticsViewModel
                {
                    Country = g.Key,
                    TotalCustomers = g.Count(),
                    TotalAccounts = g.SelectMany(x => x.Accounts).Select(a => a.AccountId).Distinct().Count(),
                    TotalBalance = g.SelectMany(x => x.Accounts).Select(a => a.Balance).Sum()
                })
                .OrderBy(x => x.Country)
                .ToList();

            return grouped;
        }

    }
}

