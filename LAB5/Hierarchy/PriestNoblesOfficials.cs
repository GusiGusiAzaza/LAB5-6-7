using System;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class PriestNoblesOfficials : Scribe
    {
        public int Rules;

        public PriestNoblesOfficials(string name, string type) : base(name, type)
        {
            HardcoreLvl = 150;
            AuthorityLvl = 400;
            Intelligence = 400;
            Money = Rand.Next(1000, 2500);
            WriteReadSkills = 100;
            Rules = Rand.Next(15, 50);
            Duties.Clear();
            Duties.Add("Making laws");
            Duties.Add("Keep peace in the society");
            Duties.Add("Making rituals and ceremonies");
            Duties.Add("Keep the Gods happy");
        }

        public override void Work()
        {
            Buf1 = Rules;
            Buf2 = Money;
            Rules += Rand.Next(1, 5);
            Money += Rand.Next(100, 250);
            if (Intelligence < MaxIntelligence)
                Intelligence += 125;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"\n---{Type} \'{Name}\': Managing territories (Money + {Money - Buf2}({Money}), Rules + {Rules - Buf1}({Rules}))");
            Console.ResetColor();
        }
    }
}