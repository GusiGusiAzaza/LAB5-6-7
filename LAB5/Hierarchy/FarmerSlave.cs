using System;
using LAB5.Base;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class FarmerSlave : Egyptian
    {
        public int Grain;
        public int Livestock;

        public FarmerSlave(string name, string type) : base(name, type)
        {
            HardcoreLvl = Rand.Next(200, 300);
            AuthorityLvl = Rand.Next(1, 3);
            Intelligence = 1;
            Money = 0;
            Duties.Add("Dig dirt");
            Duties.Add("Graze animals");
        }

        public override void Work()
        {
            Buf1 = Grain;
            Buf2 = Livestock;
            Grain += Rand.Next(200);
            Livestock += Rand.Next(5);
            if (Intelligence < MaxIntelligence)
                Intelligence += 1;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Doing drudgery (Grain + {Grain - Buf1}({Grain}), Livestock + {Livestock - Buf2}({Livestock}))");
            Console.ResetColor();
        }

        public override void SellStuff()
        {
            if (Grain == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\'{Name}\': Nothing to Sell");
                Console.ResetColor();
            }
            else
            {
                Buf1 = Money;
                Buf2 = Grain;
                Money += Grain * 1;
                Grain = 0;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(
                    $"\n--{Type} \'{Name}\': SellStuff (Money + {Money - Buf1}({Money}), Grain = 0)");
                Console.ResetColor();
            }
        }

        public void SellLiveStock()
        {
            if (Livestock == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\'{Name}\': Nothing to Sell");
                Console.ResetColor();
            }
            else
            {
                Buf1 = Money;
                Buf2 = Livestock;
                Money += Livestock * 200;
                Livestock = 0;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(
                    $"\n--{Type} \'{Name}\': SellLiveStock (Money + {Money - Buf1}({Money}), Livestock - {Buf2}({Livestock}))");
                Console.ResetColor();
            }
        }
    }
}