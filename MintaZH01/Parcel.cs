using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // Parcel elhelyezési módjánhoz készített felsorolás típus
    // Enum, ahogy a feladat kérte
    // ide tedd a namespace alá, hogy mindenhonnan elérhető legyen !
    public enum PlacementMode
    {
        Arbitrary, Horizontal, Vertical
    }

    // public a tesztelés miatt
    // abstract mert a feladat azt kérte
    // plusz mert van legalább 1 abstract metódusa -> előírás a leszármazott osztályokra
    // két interface-t valósít meg
    // ezeket írod a : után vesszővel elválasztva
    // A nyelv egyik beépített interface-e az IComparable
    // IComparable -> beépített rendezéshez (Array.Sort()) kell
    public abstract class Parcel : IDeliverable, IComparable
    {
        // mező
        // elhelyezési mód tárolása
        PlacementMode mode;
       
        // Property-k (interface-ek által előírt)
        public int Weight { get; set; }
        public string Address { get; set; }

        // Property (elhelyezési módhoz)
        // feladat szerint publikusan lekérdezhető kell
        // de mivel, majd hozzá kell nyúlni a leszármazott osztályból,
        // így a set protected lesz!
        public PlacementMode Mode { get => mode; protected set => mode = value; }

        // ctor
        // opcionális mode paraméterrel -> így két ctor lesz!
        public Parcel(int weight, string address, PlacementMode mode = PlacementMode.Arbitrary)
        {
            this.mode = mode;
            Weight = weight;
            Address = address;
        }

        // Metódusok
        // abstract (mert a feladat azt kérte)
        // nincs metódus törzs, nincs kitöltve
        // leszármazott osztályban kell lennie a megvalósításnak!
        public abstract double CalculatePrice(bool fromLocker);

        // IComparable által előírt metódus
        // Array.Sort(...) ezt fogja használni
        public int CompareTo(object? obj)
        {
            // ha az obj paraméter olyan osztálybeli, mint
            // amiben most vagyunk (Parcel),
            // akkor készítünk belőle egy Parcel példányt
            // és elnevezem antoherParcel-nek
            if (obj is Parcel anotherParcel)

                // this és azz anotherParcel is ugyanaz az osztály
                // összetudjuk hasonlítani a mezőjüket
                // minden beépített típusnak (int, double)
                // van saját összehasonlítója, ami növekvőbe rendez
                return this.Weight.CompareTo(anotherParcel.Weight);

            // ha nem sikerült átalakítani Parcel-é
            // mert, pl az obj az KisKutya osztályból való -> hiba
            throw new ArgumentException("Obj is not a Parcel!");
        }

        // Override
        public override string? ToString()
        {
            return $"Címzett: {this.Address} " +
                $"/ Elhelyzési Mód: {this.Mode}  " +
                $"/ Tömeg:{this.Weight} g";
        }
    }
}
