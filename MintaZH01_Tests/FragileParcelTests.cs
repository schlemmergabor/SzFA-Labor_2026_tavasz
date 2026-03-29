using MintaZH01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01_Tests
{
    // FragileParcel-t tesztelő metódusok
    internal class FragileParcelTests
    {
        // törékenyt nem adhatunk fel automatából -> exception
        [Test]
        public void FromLockerTest1()
        {
            FragileParcel fp = new FragileParcel(100, "Sample address", PlacementMode.Horizontal);

            Assert.Throws<DeliveryException>(() => fp.CalculatePrice(true));
        }

        // nem dob kivételt, ha nem automatából adjuk fel
        [Test]
        public void FromLockerTest2()
        {
            FragileParcel fp = new FragileParcel(100, "Sample address", PlacementMode.Horizontal);

            Assert.DoesNotThrow(() => fp.CalculatePrice(false));
        }

        // hibás elhelyezési mód -> kivételt dob
        [Test]
        public void IncorrectPlacementMode()
        {
            Assert.Throws<IncorrectOrientationException>(()=> new FragileParcel(100, "Sample address", PlacementMode.Arbitrary));
        }
    }
}
