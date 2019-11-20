namespace LAB5.Base
{
    internal interface IHierarchy
    {
        int HardcoreLvl { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        int AuthorityLvl { get; set; }
        int Age { get; set; }
        int Money { get; set; }
        void Work();
        void SellStuff();
    }
}