using L08_TrainingCosts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace L08_TrainingCosts_Tests
{
    [TestFixture]
    internal class MonthlyCostsTests
    {
        [Test]
        public void LoadFromNonExisting()
        {
            Assert.Throws<FileNotFoundException>(() => MonthlyCosts.LoadFrom("non_existing.csv"));
        }

        [Test]
        public void LoadFromEmpty()
        {
            MonthlyCosts februaryCosts = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_02.csv");
            Assert.That(februaryCosts.TrainingCosts.Length, Is.EqualTo(0));
        }

        [Test]
        public void LoadFromSuccessful()
        {
            MonthlyCosts januaryCosts = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_01.csv");
            Assert.That(januaryCosts.TrainingCosts.Length, Is.EqualTo(6));
        }

        //////////////////////////////////////////
        //                                      //
        // Innen kezdődik a feladatok megoldása //
        //                                      //
        //////////////////////////////////////////

        [TestCase("01", 65900)]
        [TestCase("02", 0)]
        public void TotalCostTest(string month, int value)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_{month}.csv");
            Assert.That(mc.TotalCost(), Is.EqualTo(value));
        }

        [TestCase(TrainingType.Swimming, 31500)]
        [TestCase(TrainingType.Cycling, 34400)]
        [TestCase(TrainingType.Hiking, 0)]
        public void TotalCostTestWithPredicate(TrainingType ttype, int value)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");
            // itt lambda kifejezést használtuk -> névtelen metódus
            // lambda => bal oldalán bemenet, jobb oldalán kimenet
            Assert.That(mc.TotalCost(x => x.Type == ttype), Is.EqualTo(value));
        }

        // 1.2. feladathoz teszt, csak az úszásra és kerékpározásra fordított költségek
        [Test]
        public void TotalCostSwimmingCyclingTest()
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");

            Assert.That(mc.TotalCost(a => a.Type == TrainingType.Cycling || a.Type == TrainingType.Swimming), Is.EqualTo(65900));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void HasMatchingCostTest(bool value)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");

            if (value)
            {
                // 7300-as kerékpáros költség -> van
                Assert.That(mc.HasMatchingCost(y => y.Type == TrainingType.Cycling && y.Cost == 7300), Is.EqualTo(value));
            }
            else
            {
                // olyan költség aminek a leírása xyz -> nincs
                Assert.That(mc.HasMatchingCost(x => x.Description == "xyz"), Is.EqualTo(value));
            }
        }

        [TestCase(1000, true)]
        [TestCase(100000, false)]
        public void AllMatchingCostTest(int value, bool expected)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_01.csv");
            // 1000-nél mindegyik nagyobb,
            // 100000-nél nem mindegyik nagyobb
            Assert.That(mc.AllMatchingCost(x => x.Cost > value), Is.EqualTo(expected));
        }

        [TestCase(1000, 2, true)]
        [TestCase(10000, 3, true)]
        [TestCase(10000, 4, false)]
        public void HasMinKCostTest(int value, int count, bool expected)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_01.csv");

            Assert.That(mc.HasMinKCost(x => x.Cost > value, count), Is.EqualTo(expected));
        }


        [TestCase(1000, 2, 1)]
        [TestCase(10000, 3, 5)]
        [TestCase(10000, 4, -1)]
        public void GetKthCostTest(int value, int k, int index)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_01.csv");

            // ha egy olyat tesztet várunk, ami nincs benne -> null ref
            if (index == -1)
            {
                Assert.That(mc.GetKthCost(x => x.Cost > value, k), Is.Null);
            }
            // ha benne van, akkor a tömböt Prop-on keresztül ellenőrizzük
            else
            {
                Assert.That(mc.GetKthCost(x => x.Cost > value, k), Is.EqualTo(mc.TrainingCosts[index]));
            }
        }

        [TestCase(1000, 6)]
        [TestCase(10000, 3)]
        [TestCase(15000, 0)]
        public void CountCostTest(int value, int count)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@"..\..\..\csv_files\2024_01.csv");

            Assert.That(mc.CountCost(x => x.Cost > value), Is.EqualTo(count));
        }

        [TestCase("01", 5)]
        [TestCase("02", -1)]
        public void BiggestCostTest(string month, int index)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_{month}.csv");

            // ha indexet várunk
            if (index > -1)
            {
                Assert.That(mc.BiggestCost(), Is.EqualTo(mc.TrainingCosts[index]));
            }
            // ha Exception dobást várunk
            else
            {
                Assert.Throws<ZeroLengthArrayException>(() => mc.BiggestCost());
            }
        }
        [TestCase("01", 5)]
        [TestCase("02", -1)]
        public void BiggestCostsIndexesTest(string month, int index)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_{month}.csv");

            // ha indexet várunk
            if (index > -1)
            {
                Assert.That(mc.BiggestCostsIndexes(), Is.EqualTo(new int[] { 5 }));
            }
            // ha Exception dobást várunk
            else
            {
                Assert.Throws<ZeroLengthArrayException>(() => mc.BiggestCostsIndexes());
            }
        }

        [TestCase("01", TrainingType.Swimming, 5)]
        [TestCase("01", TrainingType.Running, -1)]
        [TestCase("01", TrainingType.Cycling, 0)]
        public void BiggestCostWithPredicateTest(string month, TrainingType tt, int index)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_{month}.csv");

            Assert.That(mc.BiggestCost(x => x.Type == tt), Is.EqualTo(index == -1 ? null : mc.TrainingCosts[index]));

        }

        [TestCase("01")]
        public void BiggestCostAlternativeTest(string month)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_{month}.csv");

            Assert.That(mc.BiggestCostAlternative(), Is.EqualTo(mc.TrainingCosts[5]));
        }

        [TestCase(TrainingType.Cycling, new int[] { 0, 1, 2 })]
        [TestCase(TrainingType.Swimming, new int[] { 3, 4, 5 })]
        public void CostsIndexesTest(TrainingType tp, int[] exp)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");

            Assert.That(mc.CostsIndexes(x => x.Type == tp), Is.EqualTo(exp));
        }

        [TestCase(TrainingType.Cycling, new int[] { 0, 1, 2 })]
        [TestCase(TrainingType.Swimming, new int[] { 3, 4, 5 })]
        public void CostsArray(TrainingType tc, int[] exp)
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");

            // várt tömb hossza
            TrainingCost[] expected = new TrainingCost[exp.Length];
            // feltöltjük az indexelt elemekkel
            for (int i = 0; i < exp.Length; i++)
            {
                expected[i] = mc.TrainingCosts[exp[i]];
            }
            // megegyezik-e
            Assert.That(mc.CostsArray(x => x.Type == tc), Is.EqualTo(expected));
        }

        [Test]
        public void PartitionSortTest()
        {
            MonthlyCosts mc = MonthlyCosts.LoadFrom(@$"..\..\..\csv_files\2024_01.csv");

            // a feltétel, ami szerint rendezünk
            Predicate<TrainingCost> pre = x => x.Cost > 10000;
            
            // rendezés
            mc.PartitionSort(pre);

            // elemek sorrendje, a fenti szerint az első 3 helyre
            // olyan fog kerülni ami teljesíti a feltételt
            Assert.That(pre(mc.TrainingCosts[0]), Is.True);
            Assert.That(pre(mc.TrainingCosts[1]), Is.True);
            Assert.That(pre(mc.TrainingCosts[2]), Is.True);

            Assert.That(pre(mc.TrainingCosts[3]), Is.False);
            Assert.That(pre(mc.TrainingCosts[4]), Is.False);
            Assert.That(pre(mc.TrainingCosts[5]), Is.False);
        }
    }
}
