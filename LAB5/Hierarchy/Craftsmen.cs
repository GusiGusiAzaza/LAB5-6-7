using System;
using LAB5.Base;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class Craftsmen : Egyptian
    {
        public int Resources;
        public int Things;

        public Craftsmen(string name, string type) : base(name, type)
        {
            HardcoreLvl = Rand.Next(100, 150);
            AuthorityLvl = Rand.Next(15, 35);
            Intelligence = Rand.Next(20, 50);
            Money = Rand.Next(300);
            Resources = 1000;
            Duties.Add("Craft weapons");
            Duties.Add("Craft tools");
            Duties.Add("Sell crafted stuff");
            Duties.Add("Making money");
        }

        public override void SellStuff()
        {
            if (Things == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\'{Name}\': Nothing to Sell");
                Console.ResetColor();
            }
            else
            {
                Buf1 = Money;
                Money += Things * Rand.Next(50, 100);
                Things = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n--{Type} \'{Name}\' SellStuff (Money + {Money - Buf1}({Money}), Things = 0)");
                Console.ResetColor();
            }
        }

        public virtual void BuyResources(int amount)
        {
            if (Money < amount * 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{Name} has not enough money to buy resources");
                Console.ResetColor();
            }
            else
            {
                Money -= amount * 2;
                Resources += amount;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"\n--{Type} \'{Name}\': Buying resources (Money - {amount * 2}({Money}), Resources + {amount}({Resources}))");
                Console.ResetColor();
            }
        }

        public override void Work()
        {
            Buf1 = Resources;
            Buf2 = Things;
            Resources -= Rand.Next(75, 125);
            Things += Rand.Next(5, 15);
            if (Intelligence < MaxIntelligence)
            {
                Intelligence += 10;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Crafting useful stuff (Resources - {Buf1 - Resources}({Resources}), " +
                $"Things + {Things - Buf2}({Things}))");
            Console.ResetColor();
        }
    }
}