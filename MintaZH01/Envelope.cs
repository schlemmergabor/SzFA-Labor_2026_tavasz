using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // public a tesztelés miatt

    // Az osztály neve utána van a : 
    // Ez után megvalósított interfész(ek) neve
    // azzal kezd, hogy megvalósítod az interfészt,
    // azaz amit előír (most 2 Prop és 1 Metódus)
    // készítsd el -> piros aláhúzásra jobb gomb
    // Quick actions -> Implement Interface
    public class Envelope : IDeliverable
    {
        // mező, adattag, belső változó
        string description;

        // Property-k, amiket most előírt az interface
        public int Weight { get; set; }

        public string Address { get; set; }

        // ctor a feladat szerint
        public Envelope(int weight, string address, string description)
        {   
            this.Weight = weight;
            this.Address = address;
            this.description = description;
        }

        // Metódusok
        // ez most az interface által előírt metódus
        // a feladat szerint megoldva
        public double CalculatePrice(bool fromLocker)
        {
            if (this.Weight <= 50) return 200;
            if (this.Weight <= 500) return 400;
            if (this.Weight <= 2000) return 1000;
            
            // 2000 felett hiba van -> Exception-t dob
            // Exception külön osztályban találod
            throw new OverweightException();
        }

        // ToString() felülírása
        public override string? ToString()
        {
            return $"Címzett: {this.Address} / " +
                $"Leírás: {this.description} / " +
                $"Tömeg:{this.Weight} g";
        }
    }
}
