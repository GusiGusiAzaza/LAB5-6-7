using System;
using LAB5.Base;

namespace LAB5.Hierarchy
{
    [Serializable]
    internal class Soldier : Egyptian
    {
        public int SoldierSkills;

        public Soldier(string name, string type) : base(name, type)
        {
            HardcoreLvl = 500;
            AuthorityLvl = 250;
            Intelligence = 100;
            SoldierSkills = Rand.Next(50, 100);
            Money = Rand.Next(500, 1000);
            Duties.Add("Protect territories");
            Duties.Add("Kill enemies");
            Duties.Add("Conquering territories");
            Duties.Add("Supervise the farmers and slaves");
        }

        public override void Work()
        {
            if (Intelligence < MaxIntelligence)
                Intelligence += 25;
            Buf1 = Money;
            Buf2 = SoldierSkills;
            var buf3 = AuthorityLvl;
            Money += Rand.Next(322);
            SoldierSkills += 5;
            AuthorityLvl += 5;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n---{Type} \'{Name}\': Fighting for the Egypt (Money + {Money - Buf1}({Money}), " +
                              $"SoldierSkills + {SoldierSkills - Buf2}({SoldierSkills}), AuthorityLvl + {AuthorityLvl - buf3}({AuthorityLvl}))");
            Console.ResetColor();
        }
    }
}