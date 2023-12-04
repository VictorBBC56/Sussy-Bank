namespace SussyBank
{
    public class Bank : SubBank
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type{ get; set; }

        public string Country { get; set; }

        private double AccumulatedBalance { get; set; }

        internal double Profit { get; set; }

        public double GetAccumulate()
        {
            return AccumulatedBalance;
        }

        public void SetAccumulate(double accumulate)
        {
            AccumulatedBalance = accumulate;
        }
    }
}
