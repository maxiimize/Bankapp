using Bankapp.Viewmodels;

namespace Bankapp.Interfaces
{
    public interface ICustomerService
    {
        CustomerOverviewVM GetCustomerOverview(int customerId);
        PagedResult<CustomerOverviewVM> SearchCustomers(string name, string city, int pageNumber, int pageSize);
    }
}
