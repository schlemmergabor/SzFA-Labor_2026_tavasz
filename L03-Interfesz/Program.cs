namespace L03_Interfesz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. feladat
            // Tömb létrehozása néhány mém macskával tesztelés céljából

            MemeCat[] cats = new MemeCat[]
            {
                // 
                new MemeCat(8, "Nyan Cat"),
                new MemeCat(10, "Grumpy Cat"),
                new MemeCat(2, "Smudge Cat")
            };

            // Beépített rendezés az életkor alapján
            Array.Sort(cats);

            // Breakpoint ellenőrzéshez egy üres utasítás
            ;


            // Fájl beolvasása
            // figyelj rá, hogy static miatt itt az osztály neve van!
            ApartmentHouse OE = ApartmentHouse.LoadFromFile(@"..\..\..\files\datas.txt");

            // tesztelés
            // Lodgings osztály -> OE[0] -> Lodgings
            Lodgings f = (Lodgings)OE.Container[0];
            Console.WriteLine(f);

            // Metódusok a Flat osztályból
            // lakás értéke - 18 304 000 - OK
            Console.WriteLine(f.TotalValue());

            // lakók száma - 0 - OK
            Console.WriteLine(f.InhabitantsCount);

            // Metódusok a Lodgings osztályból
            // le van-e foglalva? - false - mert nem lakják - OK
            Console.WriteLine(f.IsBooked);

            // be lehet-e költözni? - false - nem - OK
            Console.WriteLine(f.MoveIn(2));

            // foglaljuk le 12 hónapra - sikeres - true - OK
            Console.WriteLine(f.Book(12));

            // le van-e most foglalva - true - OK
            Console.WriteLine(f.IsBooked);

            // próbáljuk újra lefoglalni - false - OK
            Console.WriteLine(f.Book(1));

            // beköltözés - true - OK
            Console.WriteLine(f.MoveIn(2));

            // újabb beköltözés - szobák száma/ létszám miatt - false - OK
            Console.WriteLine(f.MoveIn(15));

            // Metódusok a FamilyApartment osztályból
            // FamilyApartment osztály -> OE[0] -> Lodgings
            FamilyApartment fa = (FamilyApartment)OE.Container[1];

            // lefoglalni nem tudjuk, mert nem bérelhető
            // költözni tudunk - true - OK
            Console.WriteLine(fa.MoveIn(2));

            // baba születik - true - OK
            Console.WriteLine(fa.ChildIsBorn());

            // létszám ellenőrzése - 3 - OK
            Console.WriteLine(fa.InhabitantsCount);

            // egyik nagyszülő költözik - 4 true - OK
            Console.WriteLine(fa.MoveIn(1));

            // másik már nem fér el - false - OK
            Console.WriteLine(fa.MoveIn(1));

            // Metódusok a Garage osztályból
            Garage g = (Garage)OE.Container[2];

            // ToString() tesztelése
            Console.WriteLine(g);

            // foglaljuk a garázst - true - OK
            Console.WriteLine(g.Book(12));

            // újra foglalnánk - false - OK
            Console.WriteLine(g.Book(12));

            // beáll a kocsi
            g.UpdateOccupied();
            Console.WriteLine(g);
        }
    }
}
