using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    // abstract mert van legalább egy abstract metódusa
    // és mert a feladat azt mondta! :)
    // megvalósítja az Interface-t
    internal abstract class Flat : IRealEstate
    {
        // mezők, adattagok, belső változók
        // protected láthatóságok, hogy a leszármazottakban
        // (pl.: Lodgings) majd hozzáférhetőek legyenek
        protected double area;
        protected double roomsCount;
        protected int inhabitantsCount;
        protected double unitPrice;

        // Property
        public int InhabitantsCount
        {
            get => inhabitantsCount; 
            protected set => inhabitantsCount = value;
        }

        // ctor
        protected Flat(double area, double roomsCount, int inhabitantsCount, double unitPrice)
        {
            this.area = area;
            this.roomsCount = roomsCount;
            this.inhabitantsCount = inhabitantsCount;
            this.unitPrice = unitPrice;
        }

        // abstract metódus -> astract osztály
        // Flat leszármazottjában kell ilyen metódusnak lenni!
        public abstract bool MoveIn(int newInhabitants);

        // további Metódusok
        // lakás összértéke
        public int TotalValue()
        {
            // area, unitPrice double, de int-et kell visszaadni, mert
            // az Interface ezt írta elő
            // Math.Floor, Math.Ceiling -> megbeszéltük órán
            return (int)Math.Round(this.area * this.unitPrice);
        }

        // ToString() overrideolása
        public override string? ToString()
        {
            return $"A: {this.area}, " +
                $"RC: {this.roomsCount}, " +
                $"IC: {this.inhabitantsCount}, " +
                $"UP: {this.unitPrice}";
        }
    }
}
