using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    // kivételek őse a beépített Exception osztály
    public class WrongNumberException : Exception
    {
        // lehet mezője
        double number;

        // lehet Propertyje
        public double Number { get => number; private set => number = value; }

        // lehet ctorja
        // base az ős meghívása -> itt tudsz átadni üzenetet
        public WrongNumberException(double number) : base ("It's a negative number!")
        {
            this.number = number;
        }
    }
}
