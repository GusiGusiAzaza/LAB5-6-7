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
        private readonly CollectionCreationInfo _time;
        private List<T> _people = new List<T>();

        private bool _isSorted;
        private int _position = -1;
        public string Name { get; set; }

        //// Singleton
        //private static Egypt<T> _instance;
        //private Egypt(string name, params T[] list)
        //{
        //    _time = new CollectionCreationInfo(DateTime.Now);
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
            _time = new CollectionCreationInfo(DateTime.Now);
            Name = name;
            foreach (var person in list)
                Add(person);
        }

        public Egypt(string name, string path)
        {
            _time = new CollectionCreationInfo(DateTime.Now);
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
            if (IsExist(person.Name))
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
            if (!IsExist(person.Name))
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

            Remove(person);
            Console.WriteLine($"\'{person.Name}\' was found dead");
            Console.ResetColor();
        }

        public void FindWithName(string name)
        {
            if (IsExist(name))
            {
                var person = _people.Find(n => n.Name == name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n------------------------------------\n");
                Console.WriteLine($"Egyptian \'{name}\':");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{person}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("------------------------------------\n");
                Console.ResetColor();
                
                return;
                //return _people.Find(n => n.Name == name);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"(FindWithName)Person \'{name}\' not found(collection '{Name}')\n");
            Console.ResetColor();
            //throw new PersonNotFoundException($"(FindWithName)Person \'{name}\' not found(collection '{Name}')");
        }

        public bool IsExist(string name)
        {
            //name = name.ToLower();
            return _people.Exists(n => n.Name == name);
        }

        public T FirstOrDefault(Predicate<T> pred)
        {
                return _people.Find(pred);
        }

        public List<T> FindAll(Predicate<T> pred)
        {
            return _people.FindAll(pred);
        }

        public void SortByAuthority()
        {
            _isSorted = true;
            _people.Sort((a, b) => b.AuthorityLvl.CompareTo(a.AuthorityLvl));
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n-------------------------COLLECTION INFORMATION--------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Owner: {_time.Owner}");
            Console.WriteLine($"Time of creation: {_time.Time}");
            Console.WriteLine($"Location: {Name}");
            Console.WriteLine($"Number of people: {_people.Count}");
            Console.WriteLine("List of people:\n\n");
            if (!_isSorted) SortByAuthority();
            foreach (var p in _people)
            {
                Console.WriteLine($"-{p.Type} \'{p.Name}\' (Age: {p.Age}, Money: {p.Money})");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.ResetColor();
        }
        
        public void DetailPrint()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "\n///////////////////////////////COLLECTION DETAIL INFORMATION///////////////////////////////");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Owner: {_time.Owner}");
            Console.WriteLine($"Time of creation: {_time.Time}");
            Console.WriteLine($"Location: {Name}");
            Console.WriteLine($"Number of people: {_people.Count}");
            Console.WriteLine($"List of people:\n");
            if (!_isSorted) SortByAuthority();
            foreach (var p in _people)
            {
                Console.WriteLine(p);
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "///////////////////////////////END OF COLLECTION DETAIL INFORMATION/////////////////////////\n");
            Console.ResetColor();
        }

        public void BriefInfoFile(string path)
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
            using var saveFileStream = File.Create(path);
            var serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, _people);
            saveFileStream.Close();
        }

        public void ReadFromFile(string path)
        {
            Simulation.CheckPathValidity(path);
            using var openFileStream = File.OpenRead(path);
            var deserializer = new BinaryFormatter();
            _people = (List<T>) deserializer.Deserialize(openFileStream);
            Length = _people.Count;
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
            Length = _people.Count;
        }

        public override string ToString()
        {
            var str = "\n-------------------------COLLECTION INFORMATION--------------------------\n";
            str += $"Owner: {_time.Owner}";
            str += $"\nTime of creation: {_time.Time}";
            str += $"\nLocation: {Name}";
            str += $"\nNumber of people: {_people.Count}";
            str += $"\nList of people:\n";
            if (!_isSorted) SortByAuthority();
            foreach (var p in _people)
            {
                str += $"\n-{p.Type} \'{p.Name}\' (Age: {p.Age}, Money: {p.Money})";
            }
            str += "\n------------------------------------------------------------------\n";
            
            return str;
        }

        public static Egypt<T> operator +(Egypt<T> coll1, Egypt<T> coll2)
        {
            var united = new Egypt<T>("Egypt 2.0", coll1._people.ToArray());
            foreach (T person in coll2)
            {
                if (!united.IsExist(person.Name))
                    united.Add(person);
            }

            return united;
        }

        public static Egypt<T> operator -(Egypt<T> coll1, Egypt<T> coll2)
        {
            var ununited = new Egypt<T>(coll1.Name, coll1._people.ToArray());
            foreach (T person in coll1)
            {
                if (coll2.IsExist(person.Name))
                    ununited.Remove(person);
            }
            
            return ununited;
        }

        private struct CollectionCreationInfo
        {
            public readonly DateTime Time;
            public readonly string Owner;

            internal CollectionCreationInfo(DateTime time)
            {
                Time = time;
                Owner = "Kirill Harevich";
            }
        }
    }
}