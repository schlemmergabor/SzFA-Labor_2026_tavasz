using L08_TrainingCosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L08_TrainingCosts_Tests
{
    [TestFixture]
    internal class TrainingCostTests
    {
        [Test]
        public void ParseNull()
        {
            Assert.Throws<ArgumentNullException>(() => TrainingCost.Parse(null));
        }

        [Test]
        public void ParseEmpty()
        {
            Assert.Throws<ArgumentException>(() => TrainingCost.Parse(""));
        }

        [Test]
        public void ParseInvalid()
        {
            Assert.Throws<ArgumentException>(() => TrainingCost.Parse(",,"));
        }

        [Test]
        public void ParseInvalidDate()
        {
            Assert.Throws<FormatException>(() => TrainingCost.Parse("Running,Cap,2024:04:01,4900"));
        }

        [Test]
        public void ParseInvalidCost()
        {
            Assert.Throws<FormatException>(() => TrainingCost.Parse("Hiking,Hiking stick,2024.04.01,450.9"));
        }

        [Test]
        public void ParseValid()
        {
            Assert.DoesNotThrow(() => TrainingCost.Parse("Swimming,Swimming googles,2024.04.01,9800"));
        }
    }

}
