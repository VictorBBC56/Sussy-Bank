namespace SussyBank
{
    internal class Display : Bank
    {
        public List<Bank> Banks { get; set; }

        public List<SubBank> SubBanks { get; set; }

        public List<Debtor> Debtors { get; set; }

        public Display()
        {
            Banks = new List<Bank>();
            SubBanks = new List<SubBank>();
            Debtors = new List<Debtor>();

            LoadBank();
            LoadSubBank();
            LoadDebtor();

        }

        public void LoadBank()
        {
           string[] lines = File.ReadAllLines("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/Bank.txt");
           Random random = new Random();
           foreach (string line in lines)
           {
               string[] parts = line.Split(',');

               Banks.Add(new Bank
               {
                   Id = Convert.ToInt32(parts[0]),
                   Name = parts[1],
                   Type = parts[2],
                   Country = parts[3],
                   Profit = Convert.ToDouble(parts[5]),

               });
           }

        }

        public void LoadSubBank()
        {
            string[] lines = File.ReadAllLines("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/SubBank.txt");
            Random random = new Random();
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                SubBanks.Add(new SubBank
                {
                    Id = Convert.ToInt32(parts[0]),
                    District = parts[1],
                    City = parts[2],

                });
            }

        }

        public void LoadDebtor()
        {
            string[] lines = File.ReadAllLines("C:/Users/sunth/OneDrive/Desktop/Homework/BankData/Debtor.txt");
            Random random = new Random();
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                Debtors.Add(new Debtor
                {
                    Id = Convert.ToInt32(parts[0]),
                    Name = parts[1],
                    BeginningBalance = Convert.ToDouble(parts[2]),
                    OutstandingBalance = Convert.ToDouble(parts[3]),
                    AdditionalPurchase = Convert.ToDouble(parts[4]),
                    BankId = Convert.ToInt32(parts[5]),
                    Interest = CalculateInterest(Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])),
                    BCF = BalanceCarriedForward(Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]), Interest, Convert.ToDouble(parts[4]))
                });
            }

        }

        public double BankProfit()
        {
            double Profit = Debtors.Sum(p => p.OutstandingBalance) - (Debtors.Sum(p => p.BeginningBalance) + Debtors.Sum(p => p.AdditionalPurchase));
            return Profit;
        }

        public double CalculateInterest(double BeginBalance, double OutstandBalance)
        {
            double Interest = (BeginBalance - OutstandBalance) * Rate;
            return Interest;
        }

        public double BalanceCarriedForward(double BeginBalance, double OutstandBalance, double Interest, double AddPurchase)
        {
            double BCF = BeginBalance - OutstandBalance + Interest + AddPurchase;
            return BCF;
            
        }

        //public double CalculateVariance(int id, string type)
        //{
        //    List<double> data = new List<double>();
        //    foreach (var debtor in Debtors)
        //    {
        //        if (debtor.BankId == id)

        //        {
        //            switch (type)
        //            {
        //                case "1": data.Add(debtor.Interest); break;
        //                case "2": data.Add(debtor.AdditionalPurchase); break;
        //                case "3": data.Add(debtor.BCF); break;
        //            }
        //        }
        //    }
        //    double mean = data.Average();
        //    double sumSquaredDifferences = data.Sum(x => Math.Pow(x - mean, 2));
        //    double populationVariance = sumSquaredDifferences / data.Count;
        //    data.Clear();
        //    return populationVariance = Math.Sqrt(populationVariance);
        //}

        public List<Debtor> GroupDebtor()
        {
            var GroupDebtor = Debtors.GroupBy(p => p.BankId).ToList();
            List<Debtor> groupList = new List<Debtor>();
            foreach (var group in GroupDebtor)
            {
                foreach (var debtor in group)
                {
                    groupList.Add(debtor);
                }
            }
            return groupList;
        }


        public void DisplayBank()
        {
            SetAccumulate(Debtors.Sum(p => p.OutstandingBalance));
            foreach (var item in Banks)
            {
                Console.WriteLine($"Id:{item.Id} Name:{item.Name} Type:{item.Type} Country:{item.Country}  Accumulate:{GetAccumulate():n2} Profit:{BankProfit():n2}");
                Console.WriteLine();
            }

            foreach (var subBank in SubBanks)
            {
                var SelectKey = GroupDebtor().Where(p => p.BankId == subBank.Id);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Bank Id:{subBank.Id} / Name:BBC{subBank.Id} / Accumulatedbalance:{SelectKey.Sum(p => p.OutstandingBalance):n0} / Profit:{SelectKey.Sum(p => p.OutstandingBalance) - (SelectKey.Sum(p => p.BeginningBalance) + SelectKey.Sum(p => p.AdditionalPurchase)):n0}/ Country : Thailand / District:{subBank.District} / City:{subBank.City} ");
                Console.WriteLine();
                Console.WriteLine($"Id\tName\t\tBeginningBalance\tOutstandingBalance\tInterest\tAdditionalPurchase\tBalanceCarriedForward");
                foreach (var debtors in Debtors)
                {
                    if (debtors.BankId == subBank.Id)
                    {
                        Console.WriteLine($"{debtors.Id}\t{debtors.Name}\t{debtors.BeginningBalance,12:n2}\t{debtors.OutstandingBalance,22:n2}\t{debtors.Interest,16:n2}\t{debtors.AdditionalPurchase,14:n2}\t{debtors.BCF,24:n2}");
                    }
                }

                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Sum:{SelectKey.Sum(p => p.Interest),76:f2}\t{SelectKey.Sum(p => p.AdditionalPurchase),14:n2}\t{SelectKey.Sum(p => p.BCF),24:n2}");
                Console.WriteLine($"Average:{SelectKey.Average(p => p.Interest),72:n2}\t{SelectKey.Average(p => p.AdditionalPurchase),14:f2}\t{SelectKey.Average(p => p.BCF),24:n2}");
                //Console.WriteLine($"Variance:{CalculateVariance(subBank.Id, "1"),71:n2}\t{CalculateVariance(subBank.Id, "2"),14:n2}\t{CalculateVariance(subBank.Id, "3"),24:n2}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
