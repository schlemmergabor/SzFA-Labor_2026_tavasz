using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L02_Oroklodes
{
    internal class Rectangle : Shape
    {
        // adattagok, mezők
        // protected jelentése, hogy az osztály és
        // a leszármazottak is hozzáférhetnek
        protected int width, height;


        // ctor -> VS-ből generáltuk
        // majd "kézzel" hozzányúltunk
        // 4 paraméteres ctor -> meghívja az ős (base) ctorját
        public Rectangle(int width, int height, bool isHoley, string color)
            : base(isHoley, color)
        {
            this.width = width;
            this.height = height;
        }

        // 3 paraméteres esetében meghívja a saját (this) ctorját
        // a megfelelő paraméterek átadásával
        public Rectangle(int width, int height, string color)
            : this(width, height, false, color)
        {
        }

        // Property, Tulajdonságok
        // virtual, mert késői kötést akarunk és a leszármazottban majd overrideolunk
        public virtual int Width
        {
            get => width;
            set => width = value;
        }
        public virtual int Height
        {
            get => height;
            set => height = value;
        }

        // Metódusok

        // Ősből (Shape) származó abstract metódusok megvalósítása
        // overrideolni kell, VS-ből generáltuk le
        // (nem kell emlékezni a szignatúrára)
        // terület számítás T=a*b
        public override double Area()
        {
            return this.width * this.height;
        }

        public override double Perimeter()
        {
            return 2 * (this.width + this.height);
        }

        // ToString kiegészítése a Rect szóval
        public override string? ToString()
        {
            return "Rect. - " + base.ToString();
        }


        // Object-ből származó Equals metódus
        // összetudjuk hasonlítani két objektumot, hogy
        // ugyanaz-e a tartalmuk (ugyanazok-e)
        // mivel az objektum == objektum a két referenciát
        // hasonlítja össze, ezért ezt kell használni/megírni
        public override bool Equals(object? obj)
        {
            // Early Exit
            // ha a metódus paraméterül átadott obj nem Rectangle típus
            // akkor nem lehetnek egyezőek -> false
            if (obj is not Rectangle) return false;

            // Az obj-t átalakítjuk Rect típussá
            // ? megengedni, hogy null referencia legyen (pl. hiba esetén)
            Rectangle? temp = obj as Rectangle;

            // Akkor egyezik meg, ha minden értéke
            // (magasságok, szélességk, szín, lyukasságok) megegyezik -> true
            return this.height == temp?.height &&
                base.Equals(obj) &&
                this.width == temp?.width &&
                this.Color == temp?.Color &&
                this.isHoley == temp?.isHoley;

        }
    }
}