using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // public a tesztelés miatt
    // : Parcel -> ebből származik
    public class FragileParcel : Parcel
    {
        // ctor, meghívja az őse ctor-ját
        public FragileParcel(int weight, string address, PlacementMode mode)
            : base(weight, address, mode)
        {
            // ha az elhelyezési mód tetszőleges, akkor exception-t dobunk
            // amiben bele tesszük a csomagra (this)-re a ref-et.
            if (this.Mode == PlacementMode.Arbitrary)
                throw new IncorrectOrientationException(this);
        }

        // új árat kalkulálunk
        public override double CalculatePrice(bool fromLocker)
        {
            // Early Exit technikával,
            // csomagautomatás feladat esetén -> Exception
            if (fromLocker)
                throw new DeliveryException("A fragile parcel cannot be shipped from a locker.");

            // minden más esetben díjszámítás és return
            return 1000 + this.Weight * 2;
        }
    }
}
