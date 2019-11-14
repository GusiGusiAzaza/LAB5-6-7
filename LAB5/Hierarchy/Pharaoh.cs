using System;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal sealed class Pharaoh : PriestNoblesOfficials
    {
        public int Territories;

        public Pharaoh(string name, string type) : base(name, type)
        {
            Age = Rand.Next(5, 100);
            HardcoreLvl = 1000;
            AuthorityLvl = 1000;
            Intelligence = 3000;
            WriteReadSkills = 100;
            Territories = 10000;
            Money = 100000;
            Rules = 150;
            Duties.Clear();
            Duties.Add("Making and implementing rules and regulations");
            Duties.Add("Rule the army");
            Duties.Add("Protect provinces");
            Duties.Add("Сollect taxes");
        }

        public override void Work()
        {
            Buf1 = Territories;
            Buf2 = Rules;
            var buf3 = Money;
            Territories += Rand.Next(1000);
            Rules += Rand.Next(1, 10);
            Money += Rand.Next(1000, 10000);
            if (Intelligence < MaxIntelligence)
                Intelligence += 250;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Ruling the whole Egypt (Territories + {Territories - Buf1}({Territories}), " +
                $"Rules + {Rules - Buf2}({Rules}), Money + {Money - buf3}({Money}))");
            Console.ResetColor();
        }
    }
}