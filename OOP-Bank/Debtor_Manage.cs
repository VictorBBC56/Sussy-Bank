namespace SussyBank
{
    public class Debtor_Manage : DebtorInterface
    {
        public List<Debtor> Debtors { get; set; }

        public Debtor_Manage(int num = 1)
        {
            Debtors = new List<Debtor>();

            GenerateDebtor(num);
            SaveDebtor();
        }
        public void GenerateDebtor(int num)
        {
            Random random = new Random();

            

            for (int i = 0; i < num; i++)
            {
                var BeginBalance = random.Next(10000, 1000000);
                Debtors.Add(new Debtor
                {
                    Id = i + 1,
                    Name = $"Debtor{i + 10}",
                    BeginningBalance = BeginBalance,
                    OutstandingBalance = random.Next(BeginBalance),
                    AdditionalPurchase = random.Next(10000, 1000000),
                    BankId = random.Next(1,6)

                });
            }
        }

        public void SaveDebtor()
        {
            using (StreamWriter writer = new StreamWriter("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/Debtor.txt"))
            {
                foreach (Debtor Debtor in Debtors)
                {
                    writer.WriteLine($"{Debtor.Id}," +
                    $"{Debtor.Name}," +
                    $"{Debtor.BeginningBalance}," +
                    $"{Debtor.OutstandingBalance}," +
                    $"{Debtor.AdditionalPurchase}," +
                    $"{Debtor.BankId}");
                }
            }
            Console.WriteLine("Save");
        }

    }
}
