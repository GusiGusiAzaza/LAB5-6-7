using System;
using System.Collections.Generic;
using LAB5.Exception_Classes;

namespace LAB5.Base
{
    [Serializable]
    internal class Egyptian : IHierarchy
    {
        private const int StringPropertyMinLength = 2;
        private const int StringPropertyMaxLength = 30;
        private const int MinAge = 0;
        private const int MaxAge = 101;
        private const int MaxHardcoreLvl = 10000;
        public const int MaxIntelligence = 5000;
        
        protected static readonly Random Rand = new Random((int) DateTime.Now.Ticks);
        private int _age;
        private int _hardLvl;
        private string _name;
        private string _type;
        private protected int Buf1;
        private protected int Buf2;
        public List<string> Duties = new List<string>();
        public int Intelligence = 0;

        public Egyptian(string name, string type)
        {
            Age = Rand.Next(10, 35);
            Type = type;
            Name = name;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < StringPropertyMinLength || value.Length > StringPropertyMaxLength)
                {
                    throw new PersonArgumentException("Unacceptable Name value for", value);
                }
                _name = value;
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                if (value.Length < StringPropertyMinLength || value.Length > StringPropertyMaxLength)
                {
                    throw new PersonArgumentException($"Unacceptable Type value for {Name}", value);
                }
                _type = value;
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < MinAge || value > MaxAge)
                {
                    throw new PersonArgumentException($"Unacceptable Age value for {Name}", value);
                }
                _age = value;
            }
        }

        public int HardcoreLvl
        {
            get => _hardLvl;
            set
            {
                if (value > MaxHardcoreLvl)
                {
                    Console.WriteLine("You are hardcore");
                    
                    return;
                }

                if (value < 0)
                {
                    throw new PersonArgumentException($"Unacceptable Hardcore lvl value for {Name}", value);
                    //Console.WriteLine("Hardcore with minus? Nonono");
                    //return;
                }

                _hardLvl = value;
            }
        }

        public int AuthorityLvl { get; set; }
        public int Money { get; set; }

        public virtual void Work()
        {
            Console.WriteLine($"\'{Name}\': Doing nothing");
        }

        public virtual void SellStuff()
        {
            Money++;
        }

        public virtual string GetDuties()
        {
            var str = "";
            foreach (var d in Duties)
            {
                str += $"{d}, ";
            }

            return str.Remove(str.Length - 2);
        }

        public override string ToString()
        {
            return "------------------------------------\n" +
                   $"{Name} is {Age} y/o. " +
                $"Properties:\nType: {Type}\n" +
                $"Duties: {GetDuties()}\n" +
                $"HardcoreLVL: {HardcoreLvl}\n" +
                $"AuthorityLVL: {AuthorityLvl}\n" +
                $"Intelligence: {Intelligence}\n" +
                $"Money: {Money}\n------------------------------------";
        }

        public override int GetHashCode()
        {
            return Money * 237 + Intelligence * 57 + AuthorityLvl * 77 + 10000000;
        }
    }
}