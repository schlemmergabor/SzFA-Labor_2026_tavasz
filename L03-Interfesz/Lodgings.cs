using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    // öröklődik a Flat-ből, megvalósítja az IRent-et
    // mivel Flat megvalósította IRealEstate-et, így Ő is
    // először Ős osztály vessző, utána Iface-k
    internal class Lodgings : Flat, IRent
    {
        // mező - foglalt hónapok száma
        int bookedMonths;

        // ctor
        // : base(...) -> ős ctorjának meghívása
        // ős-be tettük a beköltözők számát, ami kezdetben 0
        // így ezzel a 0-val hívd meg az ős ctorját!
        public Lodgings(double area, double roomsCount, int unitPrice)
            : base(area, roomsCount, 0, unitPrice)
        {
            // ős ctor-ja után beállítjuk az osztály mezőit
            this.bookedMonths = 0;
        }

        // Proprety IRent-ből
        public bool IsBooked
        {
            get
            {
                // foglalt -> true - ha hónapokszáma > 0
                return this.bookedMonths > 0;
            }
        }

        // Metódus IRent-ből
        public bool Book(int months)
        {
            // ha nincs foglalva -> lehet foglalni
            if (!this.IsBooked)
            {
                this.bookedMonths = months;
                // sikerült lefoglalni
                return true;
            }

            // foglalva van -> nem sikerült foglalni
            return false;
        }

        // Metódus IRent-ből
        public int GetCost(int months)
        {
            // ha nincs benne még senki -> 0
            // ha nem lenne ez, akkor 0-val osztás veszélye lehet!
            if (this.inhabitantsCount == 0) return 0;

            // 0-val nem osztunk, fenti Early Exit lekezeli
            // double castolás kell -> mert az int/int = int
            // castolás nélkül tizedesjegy vesztés lenne, ezek után
            // (int) castolás kell -> mert a metódus visszatérési értéke ez
            return (int)Math.Round((double)this.TotalValue() / 240 / this.inhabitantsCount) * months;
        }

        // Flat absctract metódusa - override
        // a feltételeket lásd a feladatleírásában...
        public override bool MoveIn(int newInhabitants)
        {
            // Early exit megoldásssal

            // nincs még lefoglalva -> nem lehet költözni
            if (!this.IsBooked) return false;

            // össz új létszám -> eddigi bentlakók + új lakók !!!
            int total = this.inhabitantsCount + newInhabitants;

            // többen vannak, mint 8 egy szobában -> nem lehet költözni
            if (total > this.roomsCount * 8) return false;

            // nincs mindenkinek legalább 2 nm -> nem lehet költözni
            if (this.area < total * 2) return false;

            // lakószám növelés - az újakkal
            this.inhabitantsCount += newInhabitants;
            // sikerült a beköltözés
            return true;
        }

        // override ToString()
        public override string? ToString()
        {
            // ős ToString() meghívása + extra szöveges információ
            return base.ToString() + $" BM: {this.bookedMonths}";
        }

    }
}
