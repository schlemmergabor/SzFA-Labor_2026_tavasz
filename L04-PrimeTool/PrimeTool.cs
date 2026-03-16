using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L04_PrimeTool
{
    // a tesztelés miatt az internal láthatóságot public-ra tesszük

    // mock tesztelés (1.b feladat) miatt megvalósítja az IPrimeTool-t
    public class PrimeTool : IPrimeTool
    {
        // mező -> ebben tároljuk a számot amit tesztelünk majd
        int number;

        // ctor
        public PrimeTool(int number)
        {
            this.number = number;
        }

        // Metódus
        // eldönti, hogy prím-e a szám
        public bool IsPrime()
        {
            // Early Exit-ek nélkül is jó az algoritmus
            //  if (this.number < 2) return false;
            // if (this.number == 2) return true;
            // if (this.number % 2 == 0) return false;

            // Prím teszt algoritmus - jegyzet - 28. oldal
            // https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf

            int i = 2;
            while ((i <= Math.Sqrt(this.number)) && !(this.number % i == 0))
            {
                i = i + 1;
            }

            bool prim = i > Math.Sqrt(this.number);
            return prim;

        }
    }
}
