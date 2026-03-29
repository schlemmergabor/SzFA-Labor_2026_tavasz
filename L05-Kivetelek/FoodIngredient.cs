using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    // public, mert kell a teszteléshez, példányosításhoz
    // enum, mert felsorolás
    public enum Egyseg
    {
        // enum értékei
        Liter, Kilogramm, Darab, Csomag
    }

    public class FoodIngredient
    {
        // mezők
        string nev;
        double mennyiseg;
        Egyseg egyseg;

        // ctor
        public FoodIngredient(string nev, double mennyiseg, Egyseg egyseg)
        {
            this.nev = nev;
            this.mennyiseg = mennyiseg;
            this.egyseg = egyseg;
        }

        // override
        public override string? ToString()
        {
            return $"{this.nev} - {this.mennyiseg} - {this.egyseg}";
        }
    }
}
