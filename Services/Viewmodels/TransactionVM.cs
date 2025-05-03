namespace Services.Viewmodels
{
    public class TransactionVM
    {
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public string? Bank { get; set; }
    }
}
