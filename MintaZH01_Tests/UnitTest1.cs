namespace MintaZH01_Tests
{
    // Új projekt, Template-nél -> nUnit -> C#-ot válaszd ki
    // Testfixture nélkül is megy, de vizsgán mondd el! :)

    // Add -> Project Reference -> L06-MintaZH1-re pipa !!!

    // Tesztelésre külön-külön tesztosztályokat csinálok

    // Az osztály tesztelési célokat szolgál
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}