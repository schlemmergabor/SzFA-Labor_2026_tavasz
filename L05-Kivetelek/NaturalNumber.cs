using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    // public, mert tesztelni fogjuk
    public class PositiveNumber
    {
        public static double Parse(string s)
        {
            // InvarianCulture -> tizedespont és tizedesvessző is jó 
            double value = double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);

            // ha negatív a szám -> hiba -> Exceptiont dobunk
            if (value < 0)
                throw new WrongNumberException(value);

            return value;
        }
    }
}
