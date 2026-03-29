using MintaZH01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01_Tests
{
    // Courier osztályt tesztelő metódusok
    internal class CourierTests
    {
        [Test]
        public void TotalWeightTest()
        {
            // teszt tömb, amibe beleteszek csomagokat
            IDeliverable[] parcels = new IDeliverable[]
            {
                new NormalParcel(101, "Sample address"),
                new FragileParcel(30, "Sample address", PlacementMode.Horizontal),
                new Envelope(200, "Sample address", "Sample desc")
            };

            // futár aki majd felveszi a csomagokat
            Courier courier = new Courier(10);

            // kiszámítom az össz tömeget
            int totalWeight = 0;

            // végig járom a teszt tömbömet
            foreach (IDeliverable item in parcels)
            {
                // hozzáadom a súlyhoz
                totalWeight += item.Weight;

                // felveszi a futár a csomagot
                courier.PickUpItem(item);
            }

            // megyegyik-e a futár össztömeg és a számolt össztömeg
            Assert.That(courier.TotalWeight, Is.EqualTo(totalWeight));
        }
        [Test]
        public void FragileSortedTest()
        {
            // futár, aki majd felveszi a csomagokat
            Courier courier = new Courier(10);

            // legyen benne törékeny különböző súly-al
            // ráadásul ne növekvő sorrendben legyenek!
            IDeliverable[] parcels = new IDeliverable[]
            {
                new FragileParcel(100, "Sample address", PlacementMode.Horizontal),
                new Envelope(100, "Sample address", "Sample desc."),
                new NormalParcel(201, "c"),
                new FragileParcel(99, "Sample address", PlacementMode.Vertical)
            };

            // felveszi a csomagokat a futár
            for (int i = 0; i < parcels.Length; i++)
            {
                courier.PickUpItem(parcels[i]);
            }

            // ezeket a csomagokat várjuk a metódustól, ilyen sorrendben
            // figyelj rá, hogy nem használsz itt new-t!
            IDeliverable[] expected = new IDeliverable[]
            {
                parcels[3], parcels[0]
            };

            // teszt ellenőrzés
            Assert.That(courier.FragilesSorted(), Is.EqualTo(expected));
        }
    }
}
