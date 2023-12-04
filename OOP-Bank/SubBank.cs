namespace SussyBank
{
    public class SubBank : Debtor
    {
        public int Id { get; set; }

        public string District { get; set; }    
        
        public string City { get; set; }

        protected double Rate = 0.2;

    }
}
