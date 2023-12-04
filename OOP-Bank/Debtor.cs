namespace SussyBank
{
    public class Debtor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double BeginningBalance { get; set; }

        public double OutstandingBalance { get; set; }

        public double AdditionalPurchase { get; set; }

        public int BankId { get; set; }

        public double Interest { get; set; }

        public double BCF { get; set; }
    }
}
