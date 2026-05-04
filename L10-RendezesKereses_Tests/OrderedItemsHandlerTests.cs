using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using L10_RendezesKereses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace L10_RendezesKereses_Tests
{
    internal class OrderedItemsHandlerTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void IsOrderedTest(bool expected)
        {
            // növekvő kezdeti tömb
            PhoneBookItem[] tomb = new PhoneBookItem[] {
                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" },
                new PhoneBookItem() { Name = "Teta", Number = "harom" }
            };

            // tömb kezelés
            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            // true -> növekvő -> true
            Assert.That(oih.IsOrdered(expected), Is.EqualTo(expected));
        }

        [Test]
        public void IsNotOrderedTest()
        {
            // ABC-be nincs rendezve most a tömb
            PhoneBookItem[] tomb = new PhoneBookItem[] {
                new PhoneBookItem() { Name = "Teta", Number = "harom" },
                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" }
            };

            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            Assert.That(oih.IsOrdered(), Is.False);
        }

        [TestCase(SortingMethod.Selection)]
        [TestCase(SortingMethod.Insertion)]
        [TestCase(SortingMethod.Bubble)]
        public void SortTest(SortingMethod sm)
        {
            PhoneBookItem[] tomb = new PhoneBookItem[] {
                new PhoneBookItem() { Name = "Teta", Number = "harom" },
                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" }
            };

            // ide teszem, mert majd rendezve ilyen sorrendben várom
            PhoneBookItem[] orderedArray = new PhoneBookItem[]
            {
                tomb[1], tomb[2],tomb[0]
            };

            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            // kezdetben nincs rendezve
            Assert.That(oih.IsOrdered(), Is.False);

            // rendezés
            oih.Sort(sm);
            ;
            // most már rendezve van
            Assert.That(oih.IsOrdered(), Is.True);

            // lehetne még egy olyan, hogy az egyes indexeket is ellenőrizzük
            // azért nem itt van az orderedArray, mert ott is lefut a Sort
            Assert.That(oih.X, Is.EqualTo(orderedArray));
        }
        [Test]
        public void BinarySearchException()
        {
            PhoneBookItem[] tomb = new PhoneBookItem[] {
                new PhoneBookItem() {Name = "Xavier", Number="negy"},
                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" },
                new PhoneBookItem() { Name = "Teta", Number = "harom" }
            };
            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            Assert.Throws<NotOrderedItemsException>(() => oih.BinarySearch(tomb[1]));
        }
        [Test]
        public void BinarySearchOKTest()

        {
            PhoneBookItem[] tomb = new PhoneBookItem[] {

                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" },
                new PhoneBookItem() { Name = "Teta", Number = "harom" }
            };

            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            //módosítottam az algoritmuson, hogy string-re is megtalálja
            IComparable ? pbi = oih.BinarySearch("Beta");
            //szintén módosítva, hogy string-et találjon meg
            IComparable? pbi2 = oih.BinarySearchRecursive("Beta");
            
            Assert.That(pbi, Is.EqualTo(tomb[1]));
            Assert.That(pbi2, Is.EqualTo(tomb[1]));

        }

        [Test]
        public void BinarySearchNotOKTest()

        {
            PhoneBookItem[] tomb = new PhoneBookItem[] {

                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" },
                new PhoneBookItem() { Name = "Teta", Number = "harom" }
            };

            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            IComparable? pbi = oih.BinarySearch(new PhoneBookItem() { Name = "Xavier" });
            IComparable? pbi2 = oih.BinarySearchRecursive(new PhoneBookItem() { Name = "Xavier" });

            Assert.That(pbi, Is.EqualTo(null));
        }
        [Test]
        public void FindFirstNotLessTest()
        {
            PhoneBookItem[] tomb = new PhoneBookItem[] {

                new PhoneBookItem() { Name = "Alfa", Number = "egy" },
                new PhoneBookItem() { Name = "Beta", Number = "ketto" },
                new PhoneBookItem() { Name = "Teta", Number = "harom" }
            };

            OrderedItemsHandler oih = new OrderedItemsHandler(tomb);

            Assert.That(oih.FindFirstNotLess(new PhoneBookItem() { Name = "Alfa" }), Is.EqualTo(1));

            Assert.That(oih.FindFirstNotLess(new PhoneBookItem() { Name = "Beta" }), Is.EqualTo(2));

            Assert.That(oih.FindFirstNotLess(new PhoneBookItem() { Name = "Teta", Number = "ketto" }), Is.EqualTo(-1));
        }
    }
}
