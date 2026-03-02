using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L02_Oroklodes
{
    internal class Square : Rectangle
    {
        // adattag, mező, belső változó
        // nincs, mert a width = height lesz
        // amit már a Rect-ben eltároltunk

        // ctor - 2 paraméteres
        // meghívódik az ős ctorja a base-el
        public Square(int height, string color)
            : base(height, height, color)
        {
        }

        // ctor - 3 paraméteres
        // meghívódik az ős ctorja a base-el
        public Square(int height, bool isHoley, string color)
            : base(height, height, isHoley, color)
        {
        }

        // Property-k, Tulajdonság-ok
        // Késői kötés miatt override van
        // Ős-ben kellett a virtual is hozzá!
        public override int Width
        {
            get { return this.width; }
            set { this.width = this.height = value; }
        }
        public override int Height
        {
            get { return this.height; }
            set { this.width = this.height = value; }
        }

        // ToString override -> Square is odaírjuk elé
        public override string? ToString()
        {
            return "Square - " + base.ToString();
        }

        // Equals() override nincs, így az Ősé fog hívódni
        // mivel mind a két oldal egyenlő, így amikor a Rect-ben
        // ellenőrizzük ott is ugyanúgy fog működni és oké lesz!
    }
}