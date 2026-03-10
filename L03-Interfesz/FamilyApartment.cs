using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    internal class FamilyApartment : Flat
    {
        // mező, gyerekek száma
        int childrenCount;

        // ctor
        // base (ős) 0 a lakók száma
        public FamilyApartment(double area, double roomsCount, int unitPrice)
            : base(area, roomsCount, 0, unitPrice)
        {
            // gyermekek száma kezdetben 0
            this.childrenCount = 0;
        }

        // Metódusok
        // gyermek születik
        public bool ChildIsBorn()
        {
            // felnöttek száma = össz - gyerekekszáma
            int adults = this.inhabitantsCount - this.childrenCount;

            // ha legalább 2-n vannak -> GO :)
            if (adults >= 2)
            {
                // lakók száma megnő
                this.inhabitantsCount++;
                // gyerekek száma is megnő
                this.childrenCount++;
                // elkészült a baba :)
                return true;
            }
            return false;
        }

        // Flat-ből override, beköltözés
        // feltételeket nézd meg a feladatleírásban
        public override bool MoveIn(int newInhabitants)
        {
            // összLakókSzáma -> felnőttek + újak (felnőttek) + (gyerekek * 0.5)
            int adults = this.inhabitantsCount - this.childrenCount;

            double total = adults + newInhabitants + this.childrenCount * 0.5;

            // többen vannak, mint 2 egy szobában -> nem lehet költözni
            if (total > this.roomsCount * 2) return false;

            // összNm = (felnőtt + újak) * 10 + gyerek * 5
            int totalnm = (adults + newInhabitants) * 10 + this.childrenCount * 5;

            // nincs elég nm mindenkinek -> nem lehet költözni
            if (this.area < totalnm) return false;

            // lakószám növelés - az újakkal
            // csak felnőttek költöznek
            this.inhabitantsCount += newInhabitants;
            return true;
        }

        // szokásos override
        public override string? ToString()
        {
            return base.ToString() + $" CC: {this.childrenCount}";
        }
    }
}
