using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // Exception az őse
    public class DeliveryException : Exception
    {
        // ctor
        // opcionális üzenet paraméterrel
        // üzenetet a beépített Exception ős tárol, így
        // ős osztály Exception ctorjának tovább adjuk a base()-el
        public DeliveryException(string message= "The parcel cannot be shipped from a parcel locker.") : base(message)
        {
        }
    }
}
