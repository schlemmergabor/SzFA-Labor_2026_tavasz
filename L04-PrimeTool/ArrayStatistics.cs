using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L04_PrimeTool
{
    // 2. feladat
    public class ArrayStatistics
    {
        // mező
        int[] ints;

        // ctor
        public ArrayStatistics(int[] ints)
        {
            this.ints = ints;
        }

        // Metódusok

        // összeg számolása
        public int Total()
        {
            // Sorozatszámítás programozási tétel - jegyzet - 23. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf
            // érték0 -> feladattól függő kezdőérték (összegnél 0, szorzatnál 1)
            // (+) -> feladattól függő művelet -> most összeadás

            // tömbbel dolgozunk, figyelj, rá, hogy a jegyzetben i=1-től megy
            // nálunk 0-tól van a tömb indexelése így minden ami tömb balra shiftelődik
            // mérete - 1, indexe -1, stb.

            int n = this.ints.Length - 1; // -1, mert tömbbel dolgozunk

            int value = 0;

            for (int i = 1 - 1; i <= n; i++) // -1, mert tömbbel dolgozunk
            {
                value += this.ints[i];
            }

            return value;
        }

        // tartalmazza-e?
        public bool Contains(int number)
        {
            // Eldöntés programozási tétel - jegyzet - 25. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf
            // fontos megjegyzést lásd a Total() metódusnál!

            int n = this.ints.Length - 1; // -1, mert tömb

            int i = 1 - 1; // -1, mert tömb

            // figyelsz az indexre, figyelsz az n -re!!!
            // P tulajdonság most az, hogy megegyezik a number-el!
            while ((i <= n) && !(this.ints[i] == number))
            {
                i = i + 1;
            }
            bool van = i <= n;
            return van;
        }

        // növekvően rendezett-e?
        public bool Sorted()
        {
            // Növekvő rendezettség vizsgálata - jegyzet - 28. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf

            int n = this.ints.Length - 1; // -1, mert tömb

            int i = 1 - 1; // -1, mert tömb
            while ((i <= n - 1) && (this.ints[i] <= this.ints[i + 1]))
            {
                i = i + 1;
            }
            bool rendezett = i > n - 1;
            return rendezett;
        }

        // első nagyobb indexe
        public int FirstGreater(int value)
        {
            // Lineáris keresés programozási tétel - jegyzet - 30. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf

            int n = this.ints.Length - 1; // -1, mert tömb
            int i = 1 - 1; // -1, mert tömb

            // P tulajdonság: value-nál nagyobb

            while ((i <= n) && !(this.ints[i] > value))
            {
                i = i + 1;
            }
            bool van = i <= n;

            if (van) return i;
            // false -> nincs ilyen elem
            return -1;
        }

        // párosak száma
        public int CountEvens()
        {
            // Megszámlálás programozási tétel - jegyzet - 33. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf

            int n = this.ints.Length - 1; // -1, mert tömb
            int count = 0;

            // P tulajdonság -> páros
            for (int i = 1 - 1; i <= n; i++) // -1, mert tömb
            {
                if (this.ints[i] % 2 == 0)
                {
                    count +=1;
                }
            }
            return count;
        }

        // legnagyobb elem indexe
        public int MaxIndex()
        {
            // Maximumkiválasztás programozási tétel - jegyzet - 35. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf

            // Figyelj rá, hogy nem max érték, hanem a max indexe
            // Úgy vesszük, hogy kezdetben a 0. index a legnagyobb

            int n = this.ints.Length - 1; // -1, mert tömb
            int max = 1 - 1; // -1, mert tömb

            // 1-es indextől nézzük
            for (int i = (2 - 1); i <= n; i++)
            {
                // ha nagyobb, frissítjük az indexet !
                if (this.ints[i] > this.ints[max])
                {
                    max = i;
                }
            }

            return max;
        }

        // rendezés
        public void Sort()
        {
            // Buborékrendezés (bubble sort) - jegyzet - 102. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf
            // Példák: https://www.youtube.com/results?search_query=bubble+sort+visualization

            int n = this.ints.Length - 1; // -1, mert tömb

            for (int i = n; i >= (2 - 1); i--) // -1, mert tömb
            {
                for (int j = (1 - 1); j <= (i - 1); j++) // -1, mert tömb
                {
                    if (this.ints[j] > this.ints[j + 1])
                    {
                        // elemek cserélése segédváltozóval
                        //int temp = this.ints[j];
                        //this.ints[j] = this.ints[j + 1];
                        //this.ints[j + 1] = temp;

                        // elemek cserélése segédváltozó nélkül
                        (this.ints[j], this.ints[j + 1]) = (this.ints[j + 1], this.ints[j]);
                    }
                }
            }

        }
    }
}
