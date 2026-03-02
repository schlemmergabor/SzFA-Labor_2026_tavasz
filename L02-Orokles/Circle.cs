using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L02_Oroklodes
{
    internal class Circle : Shape
    {
        // adattagok, mezők
        int radius;

        // ctor ami 2 és 3 paraméteres is
        // opcionális paraméterrel (isHoley)
        // ha nem kap értéket, akkor a = utáni értéket veszi fel
        public Circle(int radius, string color, bool isHoley = false) : base(isHoley, color)
        {
            this.radius = radius;
        }

        // Property
        public int Radius { get => radius; set => radius = value; }

        // Metódusok
        // Ős abstractjai
        public override double Area()
        {
            return this.radius * this.radius * Math.PI;
        }

        public override double Perimeter()
        {
            return 2 * this.radius * Math.PI;
        }

        // Örökölt override-ok
        public override string? ToString()
        {
            return "Circle - " + base.ToString();
        }
        public override bool Equals(object? obj)
        {
            // részletesen lásd Rectangle.Equals() metódusát

            if (obj is not Circle) return false;

            Circle? temp = obj as Circle;

            return this.radius == temp?.radius && this.Color == temp?.Color && this.isHoley == temp?.isHoley && base.Equals(obj);
        }

    }
}