namespace Bankapp.Viewmodels
{
    public class StatisticsVM
    {
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public Dictionary<string, decimal> TotalBalancePerCountry { get; set; } = new();
    }
}
