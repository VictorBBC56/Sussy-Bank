namespace SussyBank
{
    public class SubBank_Manage : SubBankInterface
    {
        public List<SubBank> SubBanks { get; set; }

        public SubBank_Manage(int num = 1)
        {
            SubBanks = new List<SubBank>();

            GenerateSubBank(num);
            SaveSubBank();
        }
        public void GenerateSubBank(int num)
        {
            Random random = new Random();

            for (int i = 0; i < num; i++)
            {
                SubBanks.Add(new SubBank
                {
                    Id = i + 1,
                    District = $"District{i + 1}",
                    City = $"City{i + 1}",
                });
            }
        }

        public void SaveSubBank()
        {
            using (StreamWriter writer = new StreamWriter("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/SubBank.txt"))
            {
                foreach (SubBank SubBank in SubBanks)
                {
                    writer.WriteLine($"{SubBank.Id}," +
                    $"{SubBank.District}," +
                    $"{SubBank.City}");
                }
            }
            Console.WriteLine("save");
        }

    }
}
