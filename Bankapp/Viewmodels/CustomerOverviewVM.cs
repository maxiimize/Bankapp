namespace Bankapp.Viewmodels
{
    public class CustomerOverviewVM
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public decimal TotalBalance { get; set; }
        public List<AccountSummaryVM> Accounts { get; set; } = new();
    }
}
