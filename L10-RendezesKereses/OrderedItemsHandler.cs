using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L10_RendezesKereses
{
    // Rendezési algoritmusokhoz enum
    public enum SortingMethod
    {
        Selection, Bubble, Insertion
    }

    // Rendezett elem kezelésére osztály
    public class OrderedItemsHandler
    {
        // mezők, adattagok
        // azért lett x, mert a jegyzetben is x a tömb neve!
        IComparable[] x;

        // Metódus referencia, delegált
        // ezt fogjuk használni a növekvő, csökkenő rendezettséghez
        Func<IComparable, IComparable, bool> Method;

        // ctor
        public OrderedItemsHandler(IComparable[] x)
        {
            this.x = x;
        }

        // Property -> teszteléshez készítettem
        // public get (teszthez), private set
        public IComparable[] X { get => x; private set => x = value; }

        // Metódusok, feladatok megoldása

        // private setMethod -> beállítjuk a met. ref. (delegált)at.
        // ha true, akkor növekvő, ha false, akkor csökkenő rendezettség lesz
        // minden további metódus meghívása előtt ezt fogjuk beállítani
        private void SetMethod(bool isAscending = true)
        {
            if (isAscending)
                Method = (a, b) => a.CompareTo(b) < 0; // növekvő
            else
                Method = (a, b) => a.CompareTo(b) > 0; // csökkenő
        }

        // 1. feladat - rendezett-E a tömb?
        // jegyzet - növekvő rendezettség vizsgálat 28.oldal
        // Early Exit-el egyszerűbb lenne? :)
        public bool IsOrdered(bool isAscending = true)
        {
            // beállítjuk a delegáltat
            this.SetMethod(isAscending);

            int n = x.Length - 1; // indexelés miatt -1

            int i = 1 - 1; // indexelés miatt -1

            while ((i <= n - 1) && (Method(x[i], x[i + 1])))
                i++;

            bool rendezett = i > n - 1;
            return rendezett;
        }

        // 2. feladat -> Rendezés
        public void Sort(SortingMethod sortingMethod,
            bool isAscending = true)
        {
            // beállítjuk a delegáltat
            this.SetMethod(isAscending);

            // eldöntjük, hogy melyik algoritmussal
            switch (sortingMethod)
            {
                case SortingMethod.Selection:
                    SelectionSort();
                    break;
                case SortingMethod.Bubble:
                    BubbleSort();
                    break;
                case SortingMethod.Insertion:
                    InsertionSort();
                    break;
                default:
                    break;
            }
            // ha nem volt SetMethod hívás, és fordított sorrend kellene
            // akkor itt lenne egy Reverse() hívás még
        }

        // tömb megfordítása
        public void Reverse()
        {
            // helyben cseréljük, segédtömb nélkül
            // for (int i = 0; i < x.Length / 2; i++)
            // {
            //    (x[i], x[x.Length - 1 - i]) = (x[x.Length - 1 - i], x[i]);
            // }

            // segédtömb segítségével cseréljük
            IComparable[] result = new IComparable[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[x.Length - 1 - i];
            }
            x = result;
        }
        private void SelectionSort()
        {
            int n = this.x.Length - 1; // indexelés miatt -1

            for (int i = 1 - 1; i <= n - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j <= n; j++)
                {
                    if (this.Method(x[j], this.x[min]))
                        min = j;
                }
                (this.x[i], this.x[min]) = (this.x[min], this.x[i]); // csere
            }
        }

        // javított beillesztéses rendezés
        // lásd jegyzet 106. oldal
        private void BubbleSort()
        {
            int i = this.x.Length - 1;
            while (i >= 2)
            {
                int idx = 0;
                for (int j = 1 - 1; j <= i - 1; j++)
                {
                    if (this.Method(this.x[j + 1], this.x[j]))
                    {
                        (this.x[j], this.x[j + 1]) = (this.x[j + 1], this.x[j]);
                        idx = j;
                    }
                }
                i = idx;
            }
        }
        private void InsertionSort()
        {
            for (int i = 1; i < x.Length; i++)
            {
                int j = i - 1;
                IComparable temp = x[i];

                while ((j >= 0) && this.Method(temp, x[j]))
                {
                    x[j + 1] = x[j];
                    j--;
                }
                x[j + 1] = temp;
            }
        }

        // Bináris keresés - iteratív (ciklussal) módon
        // ha benne van adjuk vissza az elemet
        public IComparable? BinarySearch(IComparable value, bool isAscending = true)
        {
            // ha nem rendezett
            if (!this.IsOrdered(isAscending))
                throw new NotOrderedItemsException(this.x);

            // beállítjuk a delegáltat, hogy mind növekvő
            // mind csökkenő módban működjön
            this.SetMethod(isAscending);

            int bal = 1 - 1; // -1 index miatt
            int jobb = this.x.Length - 1; // -1 index miatt

            int center = (bal + jobb) / 2;

            // Az algoritmus az alábbi kód lenne:
            // while ((bal <= jobb) && (!this.x[center].Equals(value)))

            // de nem ezt fogom használni. Mert így a Name és a Numbernek is
            // meg kell egyezni, azaz amit keresek annak is tudom a számát...
            // inkább vizsgáljuk csak a Név egyezőséget
            // Equalsban Name és Number -nek is egyeznie kellett...
            while ((bal <= jobb) && (!((this.x[center] as PhoneBookItem)?.Name == (value as string))))
            {
                if (this.Method(value, this.x[center]))
                    jobb = center - 1;
                else
                    bal = center + 1;

                center = (bal + jobb) / 2;
            }

            bool van = bal <= jobb;

            // ha benne van
            if (van) return this.x[center];

            // nincs benne
            return null;
        }

        // Bináris Keresés - Rekurzív (önnmaga hívásával) módon
        public IComparable? BinarySearchRecursive(IComparable value, bool isAscending = true)
        {
            // ha nem rendezett
            if (!IsOrdered(isAscending)) throw new NotOrderedItemsException(this.x);

            // beállítjuk a delegáltat, hogy mind növekvő
            // mind csökkenő módban működjön
            this.SetMethod(isAscending);

            // itt indítjuk a keresést
            // private láthatóságú "belső" metódust hívok meg
            return BinarySearchRecursive(0, x.Length - 1, value);
        }

        // Bináris Keresés - privát metódus
        // itt adom a meg a két indexet is!
        private IComparable? BinarySearchRecursive(int bal, int jobb, IComparable value)
        {
            // Nincs benne az elem
            if (bal > jobb) return null;

            int center = (bal + jobb) / 2;

            // megtaláltuk -> őt kerestük
            // if (this.x[center].Equals(value)) return center;
            // de
            // inkább ezt a kódot használom, mert így csak a Name-et kell keresni
            // fenti esetében pedig a Name, Number páros-ra keresnék 
            // aminek nincs sok értelme...
            if ((this.x[center] as PhoneBookItem)?.Name == (value as string)) return this.x[center];

            // kisebb felében van -> 
            if (this.Method(this.x[center], value))
                return BinarySearchRecursive(bal, center - 1, value);

            // nagyobb felében van -> ...
            return BinarySearchRecursive(center + 1, jobb, value);
        }

        // növekvően rendezettnél nem kisebb index
        // első olyat ami a value-nál nagyobb -> index
        public int FindFirstNotLess(IComparable value, bool isAscending = true)
        {
            this.SetMethod(isAscending);
            // ha nem rendezett
            if (!IsOrdered(isAscending)) throw new NotOrderedItemsException(this.x);

            int bal = 0;
            int jobb = this.X.Length - 1;
            int result = -1; // Ha nincs ilyen elem

            while (bal <= jobb)
            {
                int center = bal + (jobb - bal) / 2;
                
                if (this.Method(value, this.x[center]))
                {
                    result = center;
                    jobb = center - 1; 
                }
                else
                {
                    bal = center + 1;
                }
            }

            return result;
        }
    }
}