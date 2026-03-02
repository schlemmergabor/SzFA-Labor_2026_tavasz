using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L02_Oroklodes
{
    // abstract -> legalább 1 abstract metódusa vagy Propertyje van
    internal abstract class Shape
    {
        // adattagok, mezők, belső változók
        // lyukas-e, default értéke false
        protected bool isHoley;
        // szín
        string color;

        // Proprety, Tulajdonság -> VS-ből generáltuk
        public string Color
        {
            get => color;
            set => color = value;
        }

        // ctor 2 paraméteres -> VS-ből generáltuk
        protected Shape(bool isHoley, string color)
        {
            this.isHoley = isHoley;
            this.color = color;
        }

        // ctor 1 paraméteres 
        // meghívja a saját ctor-ját
        // this -> saját magunk
        protected Shape(string color) : this(false, color)
        {
        }

        // Metódus
        public void MakeHoley()
        {
            // kilyukaszt
            this.isHoley = true;
        }

        // abstract metódusok jelentése: metódus definiálva
        // megadod a metódus szignatúráját (visszatérési érték, neve, paraméterei)
        // de itt nem töltöd ki a metódus törzsét, nincs megadva, hogy mit csinál
        //
        // hol fogjuk megadni, hogy mit csinál a metódus???
        // ---> a leszármazott osztályban!!!
        // 
        // ezen abstract metódus miatt abstract az osztály
        // mivel az osztály abstract nem lehet példányosítani, mivel
        // nem tudnánk, hogy ez a metódus mit csinál...
        public abstract double Perimeter();
        public abstract double Area();

        // Object osztályból származó ToString() felülírása
        // VS-ből generáljuk
        // Figyelj arra, ha metódust kell hívni ott van a ()
        public override string? ToString()
        {
            return $"Color: {this.color} - " +
                $"{(this.isHoley ? "Holey" : "not Holey")} - " +
                $"P: {this.Perimeter()} - " +
                $"A: {this.Area()}";

        }

        // Kilyukaszt, ha nagyobb a területe
        public void MakeHoleyIfAreaBigger()
        {
            if (this.Area() > this.Perimeter()) this.MakeHoley();
        }

        // Equals generáltatva röviden
        public override bool Equals(object? obj)
        {
            return obj is Shape shape &&
                   isHoley == shape.isHoley &&
                   color == shape.color;
        }
    }
}