using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L01_OOP
{
    internal class Cage
    {
        // mezők
        // belső tömb, ebben tároljuk majd az állatokat
        // tömb definiálása, itt nincs meg még a tömb mérete
        Animal[] animals;

        // éppen aktuális állatok száma
        int numOfAnimals = 0;

        // feladatban korlátozás az állatok számára
        int maxNumOfAnimals = 10;

        // konstruktor, ctor-al készítettük
        public Cage(int size)
        {
            // nem engedjük maxAllat méreténél nagyobb ketrec-et
            // if (size > maxNumOfAnimals) size  = maxNumOfAnimals;

            // Ternary operátorral, Elvis operátor
            size = size > maxNumOfAnimals ? maxNumOfAnimals : size;

            // tömb létrehozása
            this.animals = new Animal[size];

            // másik megoldás, egy sorban
            // this.animals = new Animal[size > maxNumOfAnimals ? maxNumOfAnimals : size];
        }

        // Metódusok
        public bool Add(Animal animal)
        {
            // Early Exit programozási technika
            // -> Metódusból, Ciklusból, stb. a kód elején
            // kiugrunk a "nem kedvező" feltételek esetén
            // majd utána a "kedvező" esetnek megfelelően
            // folytatjuk a kódolást

            // kedvezőtlen eset -> tele van a ketrec
            // tehát ez lesz most az Early Exit
            // visszajelezzük, hogy nem volt sikeres a felvétel (false)
            if (this.animals.Length == this.numOfAnimals) return false;

            // ide csak akkor jut a kód, ha nem volt Early Exit
            // azaz van még hely az állatnak
            // új indexre elhelyezzük az állatot,
            // az index pont az állatok száma lesz
            this.animals[this.numOfAnimals] = animal;

            // megnöveljük az állatok számát
            this.numOfAnimals++;

            // jelezzük a felvétel sikerességét
            return true;
        }

        public void Delete(string name)
        {
            // Gyűjtsük ki azokat az állatokat, amik megmaradnak
            // majd az eredménnyel írjuk felül az eredetei tömböt

            // átmeneti ugyanakkor méretű legyen, mint a kezdeti tömb
            // mert lehet, hogy egyetlen állatot sem kell törölni
            Animal[] temp = new Animal[this.animals.Length];

            // új tömb indexelésére használom,
            // ennyi állat maradt meg a törlés után
            int index = 0;

            // végig járom az állatokat tartalmazó tömböt
            // de csak addig, amíg van benne állat
            for (int i = 0; i < this.numOfAnimals; i++)
            {
                // ha neve különböző -> megmarad az állat 
                if (this.animals[i].Name != name)
                    
                    // bele tesszük a temp tömbbe
                    // megnöveljük az index értékét
                    temp[index++] = this.animals[i];
            }

            // tömb és állatok számának frissítése
            this.animals = temp;
            this.numOfAnimals = index;
        }

        // egy másik "megoldásra" példa
        public void Delete_v2(string name)
        {
            // Tételezzük fel, hogy az állatok neve egyedi,
            // azaz nincs két egyforma nevű állatunk, valamint
            // hogy nem számít az állatok sorrendje.
            // Ekkor jó az alábbi ötlet, algoritmus.

            // Az állatokat tároló tömb úgy néz ki, hogy
            // a tömb elején (kisebb indexeken) vannak
            // az állatok, majd utána vannak az üres részek
            // amik a tömb esetében null referenciák

            // végig nézzük a tömböt, addig amíg van benne állat
            for (int i = 0; i < this.numOfAnimals; i++)
            {
                // most nincs Early Exit
                // ha a ciklus az allatok.Length-ig menne, akkor
                // lehetnének null ref. elemek, így akkor kellene
                // Early Exit és akkor ez a kód lenne:

                // A tömb bejárása során eljutottunk a null ref-ig
                // így eddig nem találtuk meg, utána se lehet
                // if (this.animals[i] == null) return false;


                if (this.animals[i].Name == name)
                {
                    // megvan az állat indexe -> i. -> őt kell kitörölni
                    // a tömb végéről az i.-be pakoljuk az állatot
                    this.animals[i] = this.animals[this.numOfAnimals - 1];

                    // a tömb végét töröljük -> null
                    this.animals[this.numOfAnimals - 1] = null;

                    this.numOfAnimals--;

                    // nincs több ugyanolyan nevű állat 
                    // a ciklusnak tovább futnia
                    break;
                }
            }

            // Ha tele van a tömb és nem találtuk meg, akkor fog
            // ide kerülni a program vezérlése -> nem kell csinálni semmit
        }

        // Extra Metódusok a feladatok megoldásához
        public int CountSpecificAnimalsInCage(Species sp)
        {
            // megszámlálás programozás tétel
            int count = 0;
            for (int i = 0; i < this.numOfAnimals; i++)
            {
                if (this.animals[i].Species == sp) count++;
            }
            return count;

            // Early Exit technikával a kód az alábbi lenne:
            // for (int i = 0; i < this.animals.Length; i++)
            // {
            //    // itt van az Early Exit
            //    if (this.animals[i] == null) return db;
            //    if (this.animals[i].Species == sp) db++;
            // }
            // return db;
        }

        public bool DoesCageContainSpeciesAndGender(Species sp, bool gen)
        {
            // eldöntés tétel -> lásd jegyzet
            // haszáld Early Exit-el (könnyebb

            for (int i = 0; i < this.numOfAnimals; i++)
            {
                // Early Exit
                // első egyezésnél return true;
                if (this.animals[i].Species == sp &&
                    this.animals[i].Gender == gen) return true;
            }
            // végig jártuk a tömb azon részét ahol vannak állatok
            // de nem találtunk egyezést -> nincs ilyen állat
            return false;
        }

        // Animal[]? azt jelenti, hogy Animal[] tömböt és
        // null ref. értéket is vissza adhat a metódus
        public Animal[]? GetAnimalsBySpecies(Species sp)
        {
            // ennyi db adott fajú állat van
            int count = this.CountSpecificAnimalsInCage(sp);

            // nincs ilyen állat -> null ref. vissza
            if (count == 0) return null;

            // átmeneti tömb, ebbe gyűjtjük majd ki az állatokat
            Animal[] temp = new Animal[count];

            // temp melyik indexébe kerül majd a megtalált állat
            int index = 0;

            for (int i = 0; i < this.numOfAnimals; i++)
            {
                if (this.animals[i].Species == sp)
                {
                    // temp jó indexébe & index léptetése
                    temp[index++] = this.animals[i];
                }
            }

            // visszaadjuk a temp tömböt
            return temp;
        }

        public double AvgWeightBySpecies(Species sp)
        {
            // !!! Figyelj a 0-val való osztásra !!!

            // adott fajú állatok dbszáma
            int count = this.CountSpecificAnimalsInCage(sp);

            // ha nincs állat -> 0 az átlagsúly
            if (count == 0) return 0.0;

            // kigyűjtjük egy tömbbe az állatokat
            Animal[]? temp = this.GetAnimalsBySpecies(sp);

            // összegváltozó
            int sum = 0;

            // kigyűjtött tömböt járjuk végig
            for (int i = 0; i < temp.Length; i++)
            {
                sum += temp[i].Weight;
            }

            // int-et osztok int-el ezért kell a double!
            return (double)sum / count;
        }

        public bool IsCouple()
        {
            // végig megyek a tömbön ameddig állatokat tartalmaz
            // elég 1-től menni, mert ha 1 állat van benne (0 az indexe),
            // akkor nem lehet "párja"
            for (int i = 1; i < this.numOfAnimals; i++)
            {
                // megnézem az előző állatokat, hogy
                // ellentétes nemű, de ugyanaz a faj

                // a mostani állatom amit nézek -> i. indexű
                // előzőeket a j indexel nézem
                // j 0-tól indul, i a külső ciklusban az állat
                for (int j = 0; j < i; j++)
                {
                    // ha a két i és j index állatai faja egyezik, de neme nem
                    // akkor megvan a páros és Early Exit-es true
                    if (this.animals[i].Species == this.animals[j].Species &&
                        this.animals[i].Gender != this.animals[j].Gender)
                        return true;
                }
            }
            // nem volt ilyen állat
            return false;
        }

        // az osztály ToString() metódusának felülírása
        // ez jelenik meg, ha Console.WriteLine-be teszed az objektumot
        // Object ősosztálytól öröklődik -> ezért az override
        public override string? ToString()
        {
            string temp = "Cage contents: \n";

            // végig járjuk az állatokat
            for (int i = 0; i < this.numOfAnimals; i++)
            {
                // hozzáfűzzük a temp végére az adatokat
                temp += $"{this.animals[i].ToString()}\n";
            }

            return temp;
        }

        // Ketrec előállítása szövegfájl alapján
        // static metódus, így nem kell a Cage-t példányosítani
        // ahhoz, hogy megtudjuk hívni
        // Cage? visszatérési érték azért, mert null-t is visszaadhat
        public static Cage? Load(string filename)
        {
            // ha nem létezik a fájl -> null
            if (!File.Exists(filename)) return null;

            // beolvassuk egy tömbbe a fájl tartalmát
            string[] lines = File.ReadAllLines(filename);

            // átmeneti ketrec -> sorszámnyi mérettel
            Cage temp = new Cage(lines.Length);

            // sorok feldolgozása
            for (int i = 0; i < lines.Length; i++)
            {
                // feldarabolás , mentén
                string[] piece = lines[i].Split(",");

                // male / female -> ből true/false
                // Ternary operatorral
                bool gen = piece[1] == "male" ? true : false;

                int kg = int.Parse(piece[2]);

                // Faj Enum-má Parse-olás
                Species sp = (Species)Enum.Parse(typeof(Species), piece[3]);

                Animal a = new Animal(piece[0], gen, kg, sp);

                temp.Add(a);

            }
            return temp;
        }

        // Ketrecek betöltése mappából
        public static Cage[] LoadFromFolder(string folder)
        {
            // lekérjük a mappában a fájlik listáját
            string[] files = Directory.GetFiles(@$"..\..\..\{folder}");

            // átmeneti tömb
            Cage[] temp = new Cage[files.Length];

            // végig megyünk a fájlokon
            for (int i = 0; i < files.Length; i++)
            {
                // egyesével betöltjük őket
                temp[i] = Load(files[i]);
            }
            return temp;
        }
    }
}
