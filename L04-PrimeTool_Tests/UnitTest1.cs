using L04_PrimeTool;
using Moq;

namespace L04_PrimeTool_Tests
{
    public class Tests
    {
        // SetUp címkével megjelölt metódusok minden teszt elõtt lefutnak
        [SetUp]
        public void Setup()
        {
            // Console.WriteLine("Tesztelés folyamatban...");
        }

        // Test címkével megjelölt metódus-ok lesznek a Teszt-ek
        // VS-ben Test menüpont -> Run All Tests
        [Test]
        public void PrimeToolTest1()
        {

            PrimeTool pt1 = new PrimeTool(2);
            if (pt1.IsPrime() == true)
            {
                Assert.Pass();
            }
            Assert.Fail();

        }

        [Test]
        public void PrimeToolTest2()
        {

            PrimeTool pt1 = new PrimeTool(10);

            Assert.That(pt1.IsPrime(), Is.EqualTo(false));
        }

        // TestCase -> paraméterezheted a tesztjeidet
        // itt konkrét értékeket írsz
        [TestCase(true, 5)]
        [TestCase(false, 20)]
        [TestCase(true, 17)]
        [TestCase(false, 15)]

        // metódusnál egyeztetni kell a paraméterek típusát!
        public void PrimeToolTestWithTestCases(bool exp, int num)
        {

            PrimeTool pt1 = new PrimeTool(num);

            Assert.That(pt1.IsPrime(), Is.EqualTo(exp));
        }


        [TestCase(true)]
        [TestCase(false)]
        public void MockTest(bool ret)
        {
            // mock példány készítése
            Mock<IPrimeTool> mock = new Mock<IPrimeTool>();

            // metódus beállítása -> melyikre -> milyen választ adjon
            // IsPrime() hívásakor true értéket adjon vissza
            mock.Setup(x => x.IsPrime()).Returns(ret);

            // PTM készítése -> mock Object-et kell átadni
            PrimeToolManager ptm = new PrimeToolManager(mock.Object);

            // várt szöveg elõállítása
            string exp = "It's " + (ret ? "" : "not ") + "a Prime.";

            // teszteled, mintha nem mock lenne...
            Assert.That(ptm.IsPrime2Text(), Is.EqualTo(exp));
        }

        [TestCase(35, new int[] { 4, 3, 2, 1, 10, 15 })]
        [TestCase(31, new int[] { 4, 3, -2, 1, 10, 15 })]
        [TestCase(1, new int[] { -4, -3, 2, 1, -10, 15 })]
        public void TotalTest(int vartErtek, int[] tömb)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);

            Assert.That(A.Total(), Is.EqualTo(vartErtek));
        }

        [TestCase(true, new int[] { 1, 2, 3 }, 3)]
        [TestCase(false, new int[] { 7, 8, 3 }, 9)]
        [TestCase(true, new int[] { 1, 2, 3 }, 2)]
        public void ContainsTest(bool vartErtek, int[] tömb, int szam)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            Assert.That(A.Contains(szam), Is.EqualTo(vartErtek));
        }

        [TestCase(true, new int[] { 1, 2, 3 })]
        [TestCase(true, new int[] { 1, 1, 3 })]
        [TestCase(false, new int[] { 1, 2, -3 })]
        public void SortedTest(bool vartErtek, int[] tömb)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            Assert.That(A.Sorted(), Is.EqualTo(vartErtek));
        }

        [TestCase(8, new int[] { 1, 10, 3 }, 1)]
        [TestCase(8, new int[] { 1, 7, 3 }, -1)]
        [TestCase(-11, new int[] { 3, -10, -3 }, 0)]
        public void FirstGreaterTest(int szam, int[] tömb, int vartErtek)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            Assert.That(A.FirstGreater(szam), Is.EqualTo(vartErtek));
        }

        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(0, new int[] { 1, 1, 3 })]
        [TestCase(3, new int[] { 10, 2, -30 })]
        public void CountEvensTest(int vartErtek, int[] tömb)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            Assert.That(A.CountEvens(), Is.EqualTo(vartErtek));
        }
        [TestCase(2, new int[] { 1, 2, 3 })]
        [TestCase(0, new int[] { 10, 1, 3 })]
        [TestCase(0, new int[] { 10, 2, -30 })]
        public void MaxIndexTest(int vartErtek, int[] tömb)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            Assert.That(A.MaxIndex(), Is.EqualTo(vartErtek));
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, -3 })]
        [TestCase(new int[] { -1, -10, 0 })]
        public void SortTest(int[] tömb)
        {
            ArrayStatistics A = new ArrayStatistics(tömb);
            // Rendezzük
            A.Sort();
            // Megnézzük, hogy rendezett-e
            Assert.That(A.Sorted(), Is.EqualTo(true));

        }
    }
}