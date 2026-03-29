using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // DeliveryException az őse
    public class IncorrectOrientationException : DeliveryException
    {
        // mezőben eltároljuk a csomagra a referenciát
        FragileParcel fp;

        // base(...) -el meghívjuk az ősének a ctorját, amiben
        // belekódoljuk a hibaüzenetet
        // utána ebben a példányban eltároljuk a referenciát
        public IncorrectOrientationException(FragileParcel fp) : base("The parcel placement mode is not correct.")
        {
            this.fp = fp;
        }
    }
}
