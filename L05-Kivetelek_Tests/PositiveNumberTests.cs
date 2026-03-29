using L05_Kivetelek;

namespace L05_Kivetelek_Tests
{
    public class PositiveNumberTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseTest()
        {
            Assert.Throws<WrongNumberException>( ()=> PositiveNumber.Parse("-1"));
        }
    }

    
}