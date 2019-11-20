using System;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class Merchant : Craftsmen
    {
        public int StuffToSell;

        public Merchant(string name, string type) : base(name, type)
        {
            HardcoreLvl = Rand.Next(75, 150);
            AuthorityLvl = Rand.Next(40, 100);
            Intelligence = Rand.Next(50, 86);
            Money = Rand.Next(250, 750);
            Duties.Clear();
            Duties.Add("Exist");
            Duties.Add("Sell stuff");
            Duties.Add("Buy stuff");
            Duties.Add("Making money");
        }

        public sealed override void SellStuff()
        {
            if (StuffToSell == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\'{Name}\': Nothing to Sell");
                Console.ResetColor();
            }
            else
            {
                Buf1 = StuffToSell;
                Buf2 = Money;
                Money += StuffToSell * Rand.Next(20, 150);
                StuffToSell = 0;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                    $"\n--{Type} \'{Name}\': SellStuff (Money + {Money - Buf2}({Money}), StuffToSell - {Buf1}({StuffToSell}))");
                Console.ResetColor();
            }
        }

        public sealed override void BuyResources(int amount)
        {
            base.BuyResources(amount);
        }

        public override void Work()
        {
            StuffToSell += 5;
            Money -= 100;
            if (Intelligence < MaxIntelligence)
            {
                Intelligence += 15;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Trading all day long (StuffToSell + 5({StuffToSell}), Money - 100({Money}))");
            Console.ResetColor();
        }
    }
}