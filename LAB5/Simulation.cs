using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LAB5.Base;
using LAB5.Exception_Classes;
using LAB5.Hierarchy;
using Newtonsoft.Json;

namespace LAB5
{
    public enum Day : byte
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    internal class Simulation
    {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                writer?.Close();
            }
        }

        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                reader?.Close();
            }
        }

        public static bool CheckPathValidity(params string[] arr)
        {
            var invalidpaths = new List<string>();
            foreach (var path in arr)
                if (!File.Exists(path))
                    invalidpaths.Add(path);

            if (invalidpaths.Count > 0)
                throw new EgyptDirectoryNotFoundException("File directory not found", invalidpaths);
            return true;
        }

        private static void Main()
        {
            try
            {
                var workingDirectory = Environment.CurrentDirectory;
                var path = Directory.GetParent(workingDirectory).Parent?.FullName + @"\\Egypt.bin";
                var jsonPath = Directory.GetParent(workingDirectory).Parent?.FullName + @"\\Egypt.json";
                var infoPath = Directory.GetParent(workingDirectory).Parent?.FullName + @"\\Info.txt";
                //CheckPathValidity(path, jsonPath, infoPath);
                var currentDay = Day.Monday;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Starting simulation...\n");
                //var nile = new Village("Nile River");
                var f1 = new FarmerSlave("Pashedu", "Farmer");
                var f2 = new FarmerSlave("Nykara", "Farmer");
                var c1 = new Craftsmen("Nikaure", "Craftsmen");
                var c2 = new Craftsmen("Djedi", "Craftsmen");
                var m1 = new Merchant("Paser", "Merchant");
                var scr1 = new Scribe("Pipi", "Scribe");
                var sld1 = new Soldier("Qen", "Soldier");
                var sld2 = new Soldier("Shoshenq", "Soldier");
                var pr1 = new PriestNoblesOfficials("Wennefer", "Priest");
                var pr2 = new PriestNoblesOfficials("Siese", "Treasurer");
                var pr3 = new PriestNoblesOfficials("Djedptahiufankh", "Prophet");
                var pharaoh = new Pharaoh("Tutankhamun", "Pharaoh");
                var pharaoh2 = new Pharaoh("Tutankhamun", "Pharaoh");

                //var coll = Egypt<Egyptian>.GetInstance("Egypt", f1, f2, c1, c2, m1, scr1, sld1, sld2, pr1, pr2, pr3, pharaoh);
                //var coll2 = Egypt<Egyptian>.GetInstance("2Egypt2", pharaoh);
                var coll1 = new Egypt<Egyptian>("Egypt", f1, f2, c1, c2, m1, scr1, sld1, sld2, pr1, pr2, pr3);
                var coll2 = new Egypt<Egyptian>("2Egypt2", pharaoh, pharaoh2);
                var coll = coll1 + coll2;

                foreach (Egyptian p in coll)
                {
                    p.Work();
                    currentDay++;
                    p.Work();
                }

                f1.SellStuff();
                c1.SellStuff();
                m1.SellStuff();
                f2.SellLiveStock();
                f2.SellLiveStock();
                c2.BuyResources(3);
                c2.BuyResources(300);
                m1.BuyResources(1);
                Console.WriteLine(currentDay);

                coll.DetailPrint();
                coll.Print();
                coll.FindWithName("Siese");

                Console.WriteLine("\nReturned with predicate");
                Console.WriteLine(coll.FirstOrDefault(n => n.Money > 50000) + "\n");
                var rich = new Egypt<Egyptian>("RIchness", coll.FindAll(n => n.Money < 1000).ToArray());
                //rich.DetailPrint();

                Console.WriteLine(pharaoh is IHierarchy);
                Console.WriteLine(pharaoh is Egyptian);
                Console.WriteLine(pharaoh is PriestNoblesOfficials);
                Console.WriteLine(pharaoh is Merchant);
                Console.WriteLine(pharaoh is FarmerSlave);
                var merchant = new Merchant("Tom", "Farmer");
                //var craftsman = merchant as Craftsmen;
                //if (craftsman == null) 
                //    Console.WriteLine("Cast error");

                if (merchant is Craftsmen craftsman) //check
                    Console.WriteLine("Cast error");

                coll.Kill(scr1);
                coll.FindWithName("Pipi");
                coll.Kill(pharaoh);
                coll.FindWithName("Tutankhamun");
                //coll.DetailPrint();
                Console.WriteLine(c2.GetHashCode());
                Console.WriteLine(pharaoh.GetHashCode());
                Console.WriteLine(pr2.GetHashCode());

                coll.BriefInfoFile(infoPath);
                coll.SaveToFile(path);
                coll2.ReadFromFile(path);

                coll.JsonSaveToFile(jsonPath);
                var coll3 = new Egypt<Egyptian>("JSON Egypt", jsonPath);
                coll3.JsonReadFromFile(jsonPath);
                coll3.Print();
                coll.FindWithName("GL HF");
                Debug.Assert(pharaoh is Scribe);
                //coll.Kill(coll[5]);
                //coll2 -= coll;
                //coll2.Print();
                var coll6 = new Egypt<Egyptian>("Egypt 3.0", jsonPath);
                coll6.Print();


                ////!Exception generator!////

                //coll3[111].Work();
                //pharaoh.Age = 110;
                //pharaoh.Name = "a";
                //pharaoh.HardcoreLvl = -2;
                //const string errpath1 = @"H:\PROA\LB5,6,7\LB5\Egypt.bin";
                //const string errpath2 = @"H:\PRGA\LB5,6,7\LB5\Egypt.bin";
                //const string errpath3 = @"H:\3SEM\PROGA\\LB5\Egpt.bin";
                //CheckPathValidity(errpath1, errpath2, errpath3);

                ////!Exception generator!////
            }
            catch (EgyptDirectoryNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine("Unfound paths: ");
                foreach (var pathh in e.Invalidpaths) Console.Write($"-->{pathh}\n");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (PersonArgumentException e) when (e.IntValue != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Invalid int value: \'{e.IntValue}\'");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (PersonArgumentException e) when (e.StringValue != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Invalid string value: \'{e.StringValue}\'");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (EgyptIndexOutOfRangeException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (PersonNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (EgyptException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {e.Message}");
                Console.WriteLine($"Method: {e.TargetSite}");
                Console.WriteLine($"Stack: {e.StackTrace}");
            }

            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Program finally finished");
            }
        }
    }
}