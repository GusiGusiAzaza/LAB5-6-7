using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB5.Base;
using LAB5.Hierarchy;

namespace LAB5
{
    internal class Simulation
    {
        private static void Main(string[] args)
        {
            Village nile = new Village("Nile River");
            Egyptian f1 = new FarmerSlave("Pashedu", "Farmer");
            Egyptian f2 = new FarmerSlave("Nykara", "Farmer");
            Egyptian c1 = new Craftsmen("Nikaure", "Craftsmen");
            Egyptian c2 = new Craftsmen("Djedi", "Craftsmen");
            Egyptian m1 = new Merchant("Paser", "Merchant");
            Egyptian scr1 = new Scribe("Pipi", "Scribe");
            Egyptian sld1 = new Soldier("Qen", "Soldier");
            Egyptian sld2 = new Soldier("Shoshenq", "Soldier");
            Egyptian pr1 = new PriestNoblesOfficials("Wennefer", "Priest");
            Egyptian pr2 = new PriestNoblesOfficials("Siese", "Treasurer");
            Egyptian pr3 = new PriestNoblesOfficials("Djedptahiufankh", "Prophet");
            Egyptian pharaoh = new Pharaoh("Tutankhamun", "Pharaoh");

                c1.Work();
                Console.WriteLine(pharaoh.ToString());
        }
    }
}
