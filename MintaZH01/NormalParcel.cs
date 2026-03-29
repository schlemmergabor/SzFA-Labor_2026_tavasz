using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // public a tesztelések miatt, 
    // : Parcel, mert az őse a Parcel osztály
    public class NormalParcel : Parcel
    {
        // ctor
        // meghívjuk az őse base () ctorját a paraméterekkel
        // majd véletlenszerűen beállítunk egy elhelyezési mód-ot
        public NormalParcel(int weight, string address) : base(weight, address)
        {
            // véletlenszám generáláshoz object
            Random rnd = new Random();
            // véletlenszám, az enum elemeinek hosszával
            // 0, 1, 2 közül fog választani
            int vSz = rnd.Next(Enum.GetValues<PlacementMode>().Length);

            // int-ből Enum-á castoljuk
            this.Mode = (PlacementMode)vSz;
        }

        // Metódus
        // Árszámítás másképp
        public override double CalculatePrice(bool fromLocker)
        {
            int dij = 500 + this.Weight * 1;
            if (fromLocker) dij -= 250;

            return dij;
            // itt megpróbálom ugyanezt egy sorban visszadni :) 
            // return 500 + this.Weight + (fromLocker ? -250 : 0);
        }
    }
}
