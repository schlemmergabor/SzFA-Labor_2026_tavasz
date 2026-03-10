using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    internal class MemeCat : IComparable
    {
        // privát adattag, mező, belső változó
        double funScore;
        string name;

        // Konstruktor, amely inicializálja a viccességet és a nevet
        public MemeCat(int funScore, string name)
        {
            this.funScore = funScore;
            this.name = name;
        }

        // Az Array.Sort() metódus ezt használja az objektumok összehasonlítására
        // A visszatérési érték:
        // -1: ha az aktuális objektum (this) kisebb, mint az összehasonlított objektum
        //  0: ha egyenlők
        // +1: ha az aktuális objektum nagyobb, mint az összehasonlított objektum
        public int CompareTo(object? obj)
        {
            // Az obj objektumot MemeCat típussá alakítjuk
            MemeCat temp = obj as MemeCat;

            // Ha nem sikerül a MemeCat-é alakítás, akkor ...
            // hibát jelzünk egy Exception (kivétel) dobásával
            // Később a félév során erről részletesebben lesz szó
            if (temp == null) throw new ArgumentException("Object is not a MemeCat!");

            // viccesség (funScore) mezők szerinti összehasonlítás

            // ha a this után jön a temp (obj)
            if (this.funScore < temp.funScore) return -1;

            // ha a temp (obj) után jön a this
            if (this.funScore > temp.funScore) return 1;

            // ha megegyezik a sorrendjük
            return 0;

        }
    }
}
