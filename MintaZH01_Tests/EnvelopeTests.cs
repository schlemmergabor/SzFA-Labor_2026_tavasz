using MintaZH01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01_Tests
{
    // Envelope osztály tesztelési metódusai
    internal class EnvelopeTests
    {
        // TestCase -> paraméteres tesztek
        // zárójelben a konkrét értékeket látod
        [TestCase(100, true, 400)]
        [TestCase(100, false, 400)]
        [TestCase(49, true, 200)]
        [TestCase(501, true, 1000)]

        // paraméteres metódus -> egyezzen a TestCase-ben lévő típusokkal!
        public void CalculatePriceTest(int weight, bool fromlocker, int expected)
        {
            // Envelope példányt csinálok a paraméterekkel
            Envelope envelope1 = new Envelope(weight, "Sample address", "Sample Desc");

            // egyezőséget vizsgálok
            // tesztelendő metódus, várt érték
            Assert.That(envelope1.CalculatePrice(fromlocker), Is.EqualTo(expected));
        }

        [TestCase(2001, true)]
        [TestCase(2002, false)]
        [TestCase(5000, true)]
        [TestCase(2020, false)]
        public void ThrowException(int weight, bool fromlocker)
        {
            // Envelope példányt csinálok a paraméterekkel
            Envelope envelope1 = new Envelope(weight, "Sample address", "Sample Desc");

            // dob-e kivételt a meghívott metódus
            // Throws-nál a < > közé írod, hogy milyen Exception-t vársz
            // () belsejében névtelen metódus -> lesz róla szó
            // => után kell a metódus hívás, ami dob kivételt
            Assert.Throws<OverweightException>( ()=> envelope1.CalculatePrice(fromlocker) );
        }
    }
}

