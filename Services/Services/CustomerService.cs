using System;
using System.Collections.Generic;
using System.Linq;
using Services.Interfaces;
using Services.Viewmodels;
using DataAccessLayer.Models;

namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _context;

        public CustomerService(BankAppDataContext context)
        {
            _context = context;
        }

        public CustomerOverviewVM GetCustomerOverview(int customerId)
        {
            var customer = _context.Customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerOverviewVM
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    TotalBalance = c.Dispositions
                        .Where(d => d.Account != null)
                        .Select(d => d.Account.Balance)
                        .Sum(),
                    Accounts = c.Dispositions
                        .Where(d => d.Account != null)
                        .Select(d => new AccountSummaryVM
                        {
                            AccountId = d.Account.AccountId,
                            Frequency = d.Account.Frequency,
                            Balance = d.Account.Balance
                        }).ToList()
                }).FirstOrDefault();

            return customer;
        }

        public PagedResult<CustomerSearchViewModel> SearchCustomers(string name, string city, int pageNumber, int pageSize)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Givenname.Contains(name) || c.Surname.Contains(name));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(c => c.City.Contains(city));

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var customers = query
                .OrderBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerSearchViewModel
                {
                    CustomerId = c.CustomerId,
                    GivenName = c.Givenname,
                    Surname = c.Surname,
                    NationalId = c.NationalId ?? "",
                    StreetAddress = c.Streetaddress,
                    City = c.City
                }).ToList();

            return new PagedResult<CustomerSearchViewModel>
            {
                Items = customers,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
