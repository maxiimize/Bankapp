namespace Services.Viewmodels
{
    public class AccountDetailsVM
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
        public int CustomerId { get; set; }
        public int TotalTransactionCount { get; set; }
        public List<TransactionVM> Transactions { get; set; } = new();

    }
}
