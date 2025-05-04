using Services.Viewmodels;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        CustomerOverviewVM GetCustomerOverview(int customerId);

        PagedResult<CustomerSearchViewModel> SearchCustomers(string name, string city, int pageNumber, int pageSize);
    }
}
