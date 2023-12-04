namespace SussyBank
{
    internal class Bank_Manage : BankInterface
    {
            public List<Bank> Banks { get; set; }

            public Bank_Manage(int num = 1)
            {
                Banks = new List<Bank>();

                GenerateBank(num);
                SaveBank();
            }

            public void GenerateBank(int num)
            {
                Random random = new Random();

                for (int i = 0; i < num; i++)
                {
                Banks.Add(new Bank
                {
                    Id = i + 1,
                    Name = "Bank",
                    Type = "Type",
                    Country = "Country",
                    Profit = 0,
                });
                }
            }

            public void SaveBank()
            {
            using (StreamWriter writer = new StreamWriter("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/Bank.txt"))
            {
                foreach (Bank Bank in Banks)
                {
                    writer.WriteLine($"{Bank.Id}," +
                    $"{Bank.Name}," +
                    $"{Bank.Type}," +
                    $"{Bank.Country}," +
                    $"{Bank.GetAccumulate()}," +
                    $"{Bank.Profit}");
                }
            }
                Console.WriteLine("Save");
            }

            
    }
}
