using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02
{
    public class Races
    {
        // Auto-Property
        RaceResults[] tomb;

        // ctor
        public Races(RaceResults[] raceResults)
        {
            // belső tömb beállítása
            this.tomb = raceResults;
        }

        // Metódusok

        // adott nevü futó legjobb eredménye
        public Time? BestPerformance(string name)
        {
            // Eredmény objektum kezdetben null
            // feltételezzük, hogy nem futott
            Time? result = null;

            // min. prog. tétel

            // végig járjuk a tárolt tömböt
            for (int i = 0; i < this.tomb.Length; i++)
            {
                // Mivel idő szerint rendezettek a futók adatai
                // kelett Contains-t készíteni ami az Early Exit miatt
                // az elsőt (legkisebb elemet adja vissza)
                // így ezt használom fel

                // kimeneti paraméter
                RunnerWithTime rwt;

                // ha az i. tömbben van-e Név-hez eredmény?
                if (this.tomb[i].Contains(x => x.Nev == name, out rwt))
                {
                    // ez az első ilyen "találat"?
                    if (result == null)
                    {
                        // elmentjük eddigi legjobb időeredményként
                        result = rwt.Eredmeny;
                    }
                    // ha volt már eredmény
                    else
                    {
                        // meg kell vizsgázni, hogy a most talált, az jobb-e mint az eddigi

                        // ha jobb -> elmentjük
                        if (rwt.Eredmeny.CompareTo(result) < 0)
                            result = rwt.Eredmeny;
                    }
                }

            }

            // eredmény return
            return result;
        }

        // Halmazok Uniója metódus
        // lásd a jegyzetben az algoritmust
        private RunnerWithTime[] Union(RunnerWithTime[] first, RunnerWithTime[] second)
        {
            // a1 = first, n1 = a1.Length-1, a2 = second, n2 = a2.Length-1

            RunnerWithTime[] b = new RunnerWithTime[first.Length
                + second.Length];

            int i = 1 - 1;
            int j = 1 - 1;
            int db = 0;

            // amíg az indexekkel nem mentünk végig mind a két halmazon
            while ((i < first.Length ) && (j < second.Length ))
            {
                // ha az i. (first aktuális indexe) a kisebb
                if (first[i].CompareTo(second[j]) < 0)
                    // hozzátesszük és léptetjük az i-t
                    b[db++] = first[i++];


                else
                {
                    // ha a j. (second aktuális indexe) a kisebb
                    if (first[i].CompareTo(second[j]) > 0)
                        // hozzátesszük és lépetjük a j-t
                        b[db++] = second[j++];
                    else
                    // i. és j. indexű elemek egyenlőek
                    // mindkettőt bele kell tenni
                    {
                        b[db++] = first[i++];
                        b[db++] = second[j++];
                    }
                }
            }
            // még maradt az első halmazból -> hozzáadjuk
            while (i < first.Length)
            {
                b[db++] = first[i++];
            }

            // még maradt a második halmazból -> kell ez is
            while (j < second.Length)
            {
                b[db++] = second[j++];
            }

            // eredménytömböt méretre kell vágni :-)
            Array.Resize(ref b, db);

            return b;
        }

        // idő szerint visszaadja, bármely versenyről
        public RunnerWithTime[] AllBetween(Time lower, Time upper)
        {
            // létrehozom az eredmény tömböt, ami kezdetben 0 méretű
            RunnerWithTime[] result = new RunnerWithTime[0];


            RunnerWithTime[] r1;
            // végig megyek a tárolt tömbön
            for (int i = 0; i < this.tomb.Length; i++)
            {
                // "lekérdezem" az i. indexen
                r1 = this.tomb[i].Between(lower, upper);

                // ha nincs ilyen időpont, jöhet a következő
                if (r1 == null) // r1.Length <1
                    continue;

                // ha volt időpont, akkor hozzákell "fűzni" az eddigiekhez

                // új eredménytömb
                RunnerWithTime[] newResult = new RunnerWithTime[result.Length + r1.Length];

                // elemek átmásolása
                for (int j = 0; j < result.Length; j++)
                {
                    newResult[j] = result[j];
                }

                // újak a végére fűzés
                int k = result.Length;
                for (int j = 0; j < r1.Length; j++)
                {
                    newResult[k++] = r1[j];
                }

                // frissítés
                result = newResult;
            }

            // result -> ból kellene rendezett RWT

            // készítsünk belőle RaceResult-ot
            // kell az input string
            string[] inputs = new string[result.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                // inputstringek -> mint a teszteknél
                inputs[i] = $"{result[i].Nev},{result[i].Eredmeny}";
            }

            // ctor meghívódik -> Sortolás megtörténik
            RaceResults temp = new RaceResults(inputs.Length, inputs);

            // használjuk ki, hogy 4 órás futi nem lehet
            return temp.Between(Time.Parse("00:00:00"), Time.Parse("03:59:59"));
        }

        // Extra metódus
        public RunnerWithTime[] UnionForTest(RunnerWithTime[] first, RunnerWithTime[] second)
        {
            return this.Union(first, second);
        }
    }
}
