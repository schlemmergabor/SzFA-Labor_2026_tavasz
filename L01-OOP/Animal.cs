using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L01_OOP
{
    internal class Animal
    {
        // Adattagok, belső változók, mezők
        string name; // alapértéke null
        bool gender; // alapértéke false
        int weight; // alapértéke 0
        Species species; // alapértéke a 0. Faj Enum értéke (most Dog)

        // Property-k
        // kézzel írtra példa
        public string Name
        {
            // belső mező lekérdezése
            get { return this.name; }
            // beállítani nem kell, így ez most kitörlöd, vagy komment
            // vagy private set-re teszed
            // set { this.name = value; }
        }

        // További Property-k, ezeket VS-el generáltattuk
        // set-et private láthatóságra tettük
        public bool Gender { get => gender; private set => gender = value; }
        public int Weight { get => weight; private set => weight = value; }
        internal Species Species { get => species; private set => species = value; }

        // konstruktor VS-ből generáltuk
        // ctor
        public Animal(string name, bool gender, int weight, Species species)
        {
            this.name = name;
            this.gender = gender;
            this.weight = weight;
            this.species = species;
        }

        // Metódusok

        // az osztály ToString() metódusának felülírása
        // ez jelenik meg, ha Console.WriteLine-be teszed az objektumot
        // Object ősosztálytól öröklődik -> ezért az override
        public override string? ToString()
        {
            return $"{this.Name} - {this.Species} - {this.Weight} - {(this.Gender ? "male" : "female")}";
        }


        // Ennek az osztálynak nincs több metódusa

    }
}
