namespace L01_OOP
{ 
    // Az Animal osztály saját felsorolás típusa -> faj
    internal enum Species
    {
        Dog, Panda, Rabbit
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Teszteléshez
            // egy állat
            Animal a1 = new Animal("Kutyuska", true, 8, Species.Dog);

            // egy ketrec
            Cage c1 = new Cage(10);
            c1.Add(a1);
            Animal a2 = new Animal("Bogáncs", false, 6, Species.Dog);
            c1.Add(a2);
            c1.Delete("Kutyuska");
            ;


            // Négy Cage objektum tömbje és minta adatokkal feltöltése
            // tesztelés, ketrecek készítése
            Cage[] cages = new Cage[4];

            // ekkor még a cages[0], cages[1], stb. null értékűek, mert
            // nincs az indexeken konkrét Cage példány

            cages[0] = new Cage(5); // na most a [0]. indexen már ott a ketrec
            // többi indexen még null van, így meg kell azokra is csinálni

            cages[1] = new Cage(4);
            cages[2] = new Cage(3);

            // Ezzel tesztelem, hogy tényleg csak 10 állat lesz-e benne
            cages[3] = new Cage(30);

            // Ketrecek feltöltése állatokkal
            cages[0].Add(new Animal("Kutyi", true, 10, Species.Dog));
            cages[0].Add(new Animal("Molli", false, 140, Species.Panda));
            cages[0].Add(new Animal("Tappancs", true, 11, Species.Rabbit));
            cages[0].Add(new Animal("Morzsi", false, 7, Species.Dog));

            cages[1].Add(new Animal("Pandi", false, 189, Species.Panda));
            cages[1].Add(new Animal("Fehér", true, 7, Species.Rabbit));
            cages[1].Add(new Animal("Folti", true, 9, Species.Dog));

            cages[2].Add(new Animal("Ugri", true, 10, Species.Rabbit));
            cages[2].Add(new Animal("Füles", false, 10, Species.Rabbit));

            cages[3].Add(new Animal("Dalmi", false, 7, Species.Dog));
            cages[3].Add(new Animal("Bogáncs", true, 6, Species.Dog));
            cages[3].Add(new Animal("Öreg", false, 6, Species.Rabbit));
            cages[3].Add(new Animal("Pötyi", true, 77, Species.Panda));
            cages[3].Add(new Animal("Kutyi", false, 4, Species.Dog));

            // Extra metódusok tesztelése
            int numOfDogsInCage0 = cages[0].CountSpecificAnimalsInCage(Species.Dog);

            bool maleDogInCage0 = cages[0].DoesCageContainSpeciesAndGender(Species.Dog, true);

            Animal[] dogsInCage0 = cages[0].GetAnimalsBySpecies(Species.Dog);

            double avgDogsWeightInCage0 = cages[0].AvgWeightBySpecies(Species.Dog);

            bool coupleDogsInCage0 = cages[0].IsCouple();

            Cage maxDogsCage = Max(cages, Species.Dog);

            // ketrecben lévő állatok kiírása a konzolra
            Console.WriteLine(cages[0]);

            // ketre szöveges fájlból előállítása
            Cage c = Cage.Load(@"..\..\..\files\cage1.txt");

            // alkalmazás paraméterül adott mappából betöltés
            // paramétert úgy állítod be, hogy
            // Solution Explorer jobb gomb a projekt nevén -> Properties
            // Debug panel -> Debug Launch Profiles
            // Command Line Arguments-be kell beírni
            Cage[] cagesFromFolder = Cage.LoadFromFolder(args[0]);


            // Max példák
            // itt a cage1.txt betöltött elemeit kell kapnod,
            // mert abban van a legtöbb kutya
            Cage dogs = Max(cagesFromFolder, Species.Dog);

            Cage[] extraCages = new Cage[2];
            extraCages[0] = new Cage(10);
            extraCages[1] = new Cage(10);
            Animal rabbit = new Animal("TapsiHapsi", true, 3, Species.Rabbit);
            extraCages[0].Add(rabbit);
            extraCages[1].Add(rabbit);

            // itt null kell, hogy legyen, mert nincs kutya a ketrecekben
            Cage dogs2 = Max(extraCages, Species.Dog);
            ;
        }

        // Extra Metódusok
        // Melyik ketrecben található a legtöbb megadott fajú állat?
        static Cage? Max(Cage[] c, Species s)
        {
            // maximum tétel -> lásd jegyzet !!!
            // feltételezzük, hogy nincs ilyen ketrec
            int maxIndex = -1;

            // végig nézzük a ketreceket
            for (int i = 0; i < c.Length; i++)
            {
                // ha van ilyen fajú állat a ketrecben
                if (c[i].CountSpecificAnimalsInCage(s) != 0)
                {
                    // ha nem volt eddig olyan ketrec amiben volt ilyen állat
                    if (maxIndex == -1)
                    {
                        maxIndex = i;
                        // lépünk a következő i-re
                        continue;
                    }

                    // ha nagyobb, mint az eddigi index-en található érték
                    if (c[i].CountSpecificAnimalsInCage(s) > c[maxIndex].CountSpecificAnimalsInCage(s))

                        // akkor maxIndex elmentése
                        maxIndex = i;
                }

                
            }

            // volt ilyen ketrecet tartalmazó állat
            if (maxIndex == -1) return null;

            // visszaadom a ketrecet
            // mehetne a ketrec sorszáma, indexe ugyanúgy jó lenne
            return c[maxIndex];
        }
    }
}

