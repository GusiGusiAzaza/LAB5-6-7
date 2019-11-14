using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LAB5.Exception_Classes;
using LAB5.Hierarchy;
using Newtonsoft.Json;

namespace LAB5.Base
{
    internal sealed class Egypt<T> : IEnumerator, IEnumerable where T : IHierarchy
    {
        private readonly CollInfo _time;
        private List<T> _people = new List<T>();

        private int _position = -1;
        public string Name;

        //// Singleton
        //private static Egypt<T> _instance;
        //private Egypt(string name, params T[] list)
        //{
        //    _time = new CollInfo(DateTime.Now);
        //    Name = name;
        //    foreach (T person in list)
        //        Add(person);
        //}
        //public static Egypt<T> GetInstance(string name, params T[] list)
        //{
        //    if (_instance == null)
        //        _instance = new Egypt<T>(name, list);
        //    return _instance;
        //}
        //// Singleton end

        public Egypt(string name, params T[] list)
        {
            _time = new CollInfo(DateTime.Now);
            Name = name;
            foreach (var person in list)
                Add(person);
        }
        public Egypt(string name, string path)
        {
            _time = new CollInfo(DateTime.Now);
            Name = name;
            Simulation.CheckPathValidity(path);
            JsonReadFromFile(path);
        }
        public T this[int index]
        {
            get
            {
                if (index <= Length && index >= 0)
                    return _people[index];
                throw new EgyptIndexOutOfRangeException($"Incorrect index({index})(collection: '{Name}')");
                //Console.WriteLine($"Incorrect index({index})(collection: '{Name}')");
                //return default;
            }
            set => _people[index] = value;
        }
        public int Length { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        bool IEnumerator.MoveNext()
        {
            if (_position >= _people.Count - 1)
            {
                ((IEnumerator) this).Reset();
                return false;
            }

            _position++;
            return true;
        }
        void IEnumerator.Reset()
        {
            _position = -1;
        }
        object IEnumerator.Current => _people[_position];

        public void Add(T person)
        {
            if (IsThere(person.Name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nPerson \"{person.Name}\" is already in(collection: '{Name}')");
                Console.ResetColor();
                return;
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(person);
            if (!Validator.TryValidateObject(person, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error.ErrorMessage);
                    Console.ResetColor();
                }

                return;
            }

            _people.Add(person);
            Length++;
        }
        public void Remove(T person)
        {
            if (!_people.Contains(person))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n(Removing)Person '{person}' not found in collection {Name}");
                Console.ResetColor();
                //throw new PersonNotFoundException($"(Removing)Person '{person}' not found in collection {Name}");
            }

            _people.Remove(person);
            Length--;
        }
        public void Kill(T person)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (person is Pharaoh)
            {
                Console.WriteLine("You cant just kill pharaoh :)");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\'{person.Name}\' was found dead");
            Console.ResetColor();
            Remove(person);
        }
        public void FindWithName(string name)
        {
            foreach (var person in _people)
                if (person.Name == name)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n------------------------------------\n");
                    Console.WriteLine($"Egyptian \'{name}\':");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{person}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("------------------------------------\n");
                    Console.ResetColor();
                    return;
                }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"(FindWithName)Person \'{name}\' not found(collection '{Name}')\n");
            Console.ResetColor();
            //throw new PersonNotFoundException($"(FindWithName)Person \'{name}\' not found(collection '{Name}')");
        }
        public bool IsThere(string name)
        {
            //name = name.ToLower();
            foreach (var p in _people)
                if (p.Name == name)
                    return true;

            return false;
        }

        public T FirstOrDefault(Predicate<T> pred)
        {
            if (_people.Find(pred) != null)
                return _people.Find(pred);
            return default;
        }
        public List<T> FindAll(Predicate<T> pred)
        {
            return _people.FindAll(pred);
        }

        public string SortByAuthority()
        {
            var s = "";
            _people.Sort((a, b) => b.AuthorityLvl.CompareTo(a.AuthorityLvl));
            foreach (var p in _people) s += $"\n-{p.Type} \'{p.Name}\' (Age: {p.Age}, Money: {p.Money})";

            return s;
        }
        public string GetPpl()
        {
            var s = "";
            foreach (var p in _people) s += p.ToString();

            return s;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n-------------------------COLLECTION INFO--------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Owner: {_time.Owner}");
            Console.WriteLine($"Time of creation: {_time.Time}");
            Console.WriteLine($"Location: {Name}");
            Console.WriteLine($"Number of people: {_people.Count}");
            Console.WriteLine($"List of people:\n{SortByAuthority()}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.ResetColor();
        }
        public void DetailPrint()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "\n///////////////////////////////COLLECTION DETAIL INFO///////////////////////////////");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Owner: {_time.Owner}");
            Console.WriteLine($"Time of creation: {_time.Time}");
            Console.WriteLine($"\nLocation: {Name}\n");
            Console.WriteLine($"Number of people: {_people.Count}");
            Console.WriteLine($"\nList of people:\n{GetPpl()}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "///////////////////////////////END OF COLLECTION DETAIL INFO/////////////////////////\n");
            Console.ResetColor();
        }
        public void InfoFile(string path)
        {
            Simulation.CheckPathValidity(path);
            using var sw = File.CreateText(path);
            sw.WriteLine("///////////////////////////////////////////////////////\n" +
                         $"//////////// Created: {File.GetLastWriteTime(path)} /////////////\n" +
                         "///////////////////////////////////////////////////////\n\n");
            sw.WriteLine("Index\tObject Type\n");
            for (var i = 0; i < Length; i++) sw.WriteLine($"{i}\t{this[i].GetType()}");

            sw.WriteLine($"{this}");
        }

        public void SaveToFile(string path)
        {
            Simulation.CheckPathValidity(path);
            Stream saveFileStream = File.Create(path);
            var serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, _people);
            saveFileStream.Close();
        }
        public void ReadFromFile(string path)
        {
            Simulation.CheckPathValidity(path);
            Console.WriteLine("Reading saved file");
            Stream openFileStream = File.OpenRead(path);
            var deserializer = new BinaryFormatter();
            _people = (List<T>) deserializer.Deserialize(openFileStream);
            openFileStream.Close();
        }

        public void JsonSaveToFile(string path)
        {
            Simulation.CheckPathValidity(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(_people, Formatting.Indented));
        }
        public void JsonReadFromFile(string path)
        {
            Simulation.CheckPathValidity(path);
            _people = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
        }

        public override string ToString()
        {
            var str = "\n-------------------------COLLECTION INFO--------------------------\n";
            str += $"Owner: {_time.Owner}";
            str += $"\nTime of creation: {_time.Time}";
            str += $"\nLocation: {Name}";
            str += $"\nNumber of people: {_people.Count}";
            str += $"\nList of people:\n{SortByAuthority()}";
            str += "\n------------------------------------------------------------------\n";
            return str;
        }

        public static Egypt<T> operator +(Egypt<T> coll1, Egypt<T> coll2)
        {
            var united = new Egypt<T>("Egypt 2.0", coll1._people.ToArray());
            foreach (T person in coll2)
                if (!united.IsThere(person.Name))
                    united.Add(person);
            return united;
        }
        public static explicit operator T(Egypt<T> coll)
        {
            return coll.FirstOrDefault(n => n.AuthorityLvl > 800);
        }

        private struct CollInfo
        {
            public readonly DateTime Time;
            public readonly string Owner;

            internal CollInfo(DateTime time)
            {
                Time = time;
                Owner = "Kirill Harevich";
            }
        }
    }
}