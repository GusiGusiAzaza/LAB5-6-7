using System;
using LAB5.Base;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class Scribe : Egyptian
    {
        public int WriteReadSkills;

        public Scribe(string name, string type) : base(name, type)
        {
            Age = Rand.Next(10, 50);
            HardcoreLvl = 40;
            AuthorityLvl = 200;
            Intelligence = 500;
            Money = Rand.Next(500, 1000);
            WriteReadSkills = Rand.Next(45, 80);
            Duties.Add("Write on papyrus");
            Duties.Add("Learn to read and write");
            Duties.Add("Update databases(food, soldiers, gifts to the Gods, etc.)");
        }

        public override void Work()
        {
            Buf1 = Money;
            Buf2 = WriteReadSkills;
            WriteReadSkills += Rand.Next(1, 3);
            Money += Rand.Next(50, 150);
            if (Intelligence < MaxIntelligence)
                Intelligence += 40;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Writing big papyrus (Money + {Money - Buf1}({Money}), Rules + {WriteReadSkills - Buf2}({WriteReadSkills}))");
            Console.ResetColor();
        }
    }
}