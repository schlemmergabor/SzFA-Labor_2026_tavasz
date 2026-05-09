using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02
{
    public class RaceResults
    {
        // adattag, futók eredményeivel
        // tömb, ami kezdetben null (méretét se tudjuk még!)
        RunnerWithTime[] adatok;

        // ctor
        // runnersCount lehet, hogy emberek száma?
        public RaceResults(int runnersCount, string[] inputs)
        {
            // tömb létrehozás (méretbeállítás)
            this.adatok = new RunnerWithTime[runnersCount];

            // egyesével feltöltjük az elemeit
            for (int i = 0; i < runnersCount; i++)
            {
                this.adatok[i] = RunnerWithTime.Parse(inputs[i]);
            }

            // ha nem rendezett -> rendezést csinálunk
            // feladat kérte, hogy legyen két private metódus erre!
            if (!this.IsSorted()) this.Sort();
        }

        // növekvő rendezettség vizsgálat
        private bool IsSorted()
        {
            // Jegyzetben van erre algoritmus, de itt most
            // EarlyExit-et használunk

            // páronként összehasonlítjuk a tömb elemeit
            // indexelésre figyelj !!!
            for (int i = 0; i < this.adatok.Length - 1; i++)
            {
                // rossz (nem növekvő) sorrendben vannak
                // azaz az i. nagyobb, mint az i+1.
                if (this.adatok[i].CompareTo(this.adatok[i + 1]) > 0)
                    return false;
            }

            // minden jó (növekvő) sorrendben volt
            return true;
        }

        // rendezés a javított beillesztéses rendezés segítségével
        private void Sort()
        {
            // tömb hossza
            // -1 az indexelés miatt
            int n = this.adatok.Length - 1;

            for (int i = 2 - 1; i <= n; i++)
            {
                int j = i - 1;
                RunnerWithTime seged = this.adatok[i];

                // j=0 kell, hogy az első indexet is kezelje!
                while ((j >= 1 - 1) &&
                    (this.adatok[j].CompareTo(seged) > 0))
                {
                    this.adatok[j + 1] = this.adatok[j];
                    j--;
                }
                this.adatok[j + 1] = seged;
            }
        }

        // További Metódusok

        // nem kisebb => tehát nagyobb, egyenlő a time-nál
        // Bináris Keresés-re visszavezethető feladat
        private int LowerBound(Time time)
        {
            // két index
            int bal = 0;
            int jobb = this.adatok.Length - 1;

            // eredmény kezdetben -1 (nincs)
            int eredmeny = -1;

            // center index
            int center = (bal + jobb) / 2;

            while (bal <= jobb)
            {
                // ha a center elem nagyobb, vagy egyenlő -> tőle balra van az elem
                // jobb indexet "igazítjuk"
                if (this.adatok[center].Eredmeny.CompareTo(time) >= 0)
                {
                    jobb = center - 1;
                    // megjegyezzük ezt egy jó eredménynek
                    eredmeny = center;
                }
                else
                {
                    // center elemnél kisebb -> tőle jobbra van
                    // bal indexet "húzzuk"
                    bal = center + 1;
                }
                // centerindex újraszámítása
                center = (bal + jobb) / 2;
            }
            // index return
            return eredmeny < 0 ? 0 : eredmeny;
        }

        // az első time-nál nagyobb elem indexe
        // ugyanaz, mint a LowerBound, csak >= helyett > van !
        // figyelj arra, ha minden tömb elem kisebb, akkor túl kell indexelni a tömbön
        private int UpperBound(Time time)
        {
            int bal = 0;
            int jobb = this.adatok.Length - 1;

            // ha nincs nagyobb, akkor a tömb vége utáni index legyen -> túlindexelés
            int eredmeny = this.adatok.Length;

            int center = (bal + jobb) / 2;
            ;
            while (bal <= jobb)
            {
                // itt van a >= helyett 
                if (this.adatok[center].Eredmeny.CompareTo(time) > 0)
                {
                    eredmeny = center;
                    jobb = center - 1;
                }
                else
                {
                    bal = center + 1;
                }
                center = (bal + jobb) / 2;
            }
            return eredmeny;
        }

        // Két időpont közötti eredmények visszaadása
        public RunnerWithTime[] Between(Time lower, Time upper)
        {
            // hibakezelés
            // ha a lower nagyobb, mint az upper paraméter
            if (lower.CompareTo(upper) > 0)
                throw new ArgumentException("Lower is not lower than upper!");

            // a két időpont közötti kezdőIndex
            int index1 = this.LowerBound(lower);

            // a két időpont közötti végsőIndex
            // ami már nagyobb az upper időpontnál
            int index2 = this.UpperBound(upper);

            // eredmény tömb méretének kiszámítása
            int size = index2 - index1;

            // ha nincs eredmény -> return 0 elemű tömb
            if (size < 1) return new RunnerWithTime[0];

            // eredménytömb létrehozása
            RunnerWithTime[] result = new RunnerWithTime[size];

            // eredménytömb feltöltéséhez használt segédváltozó
            int j = 0;

            // végig megyünk az adatokon a kezdőIndex-től a végsőIndex-ig
            for (int i = index1; i < index2; i++)
            {
                // feltöltjük az eredmény megfelelő indexét, majd j++
                result[j++] = this.adatok[i];
            }

            // eredmény return
            return result;
        }

        // Van-e ilyen futó
        // out -> kimeneti paraméter, aminek ha értéket adsz, azt
        // metóduson kívülről is el fogod érni
        public bool Contains(Predicate<RunnerWithTime> predicate, out RunnerWithTime runnerPerformance)
        {
            // Early Exit-el

            // végigmegyünk a tömb elemein
            for (int i = 0; i < this.adatok.Length; i++)
            {
                // ha a predikátum függvény igazat ad
                if (predicate(this.adatok[i]))
                {
                    // kimeneti paraméter beállítása
                    runnerPerformance = this.adatok[i];

                    // metódus visszatér
                    return true;
                }
            }
            // nem volt, akkor a kimeneti paraméter null
            runnerPerformance = null;

            // nem találtunk ilyen elemet
            return false;
        }
    }
}