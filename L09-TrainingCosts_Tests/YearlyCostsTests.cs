using L08_TrainingCosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L08_TrainingCosts_Tests
{
    [TestFixture]
    internal class YearlyCostsTests
    {
        [Test]
        public void LoadFromNonExistingDirectory()
        {
            Assert.Throws<DirectoryNotFoundException>(() => YearlyCosts.LoadFrom("non_existing_directory"));
        }

        [Test]
        public void LoadFromSuccessful()
        {
            Assert.DoesNotThrow(() => YearlyCosts.LoadFrom(@"..\..\..\csv_files"));

            YearlyCosts yearlyCosts = YearlyCosts.LoadFrom(@"..\..\..\csv_files");
            Assert.That(yearlyCosts.Costs.Length, Is.EqualTo(12));
            for (int i = 0; i < 2; ++i)
            {
                Assert.That(yearlyCosts.Costs, Is.Not.Null);
            }
        }
        [Test]
        public void MonthlyMaxCostTest()
        {
            YearlyCosts yc = YearlyCosts.LoadFrom(@"..\..\..\csv_files");

            Assert.That(yc.MonthlyMaxCost(), Is.EqualTo(0));
        }
        [TestCase(TrainingType.Swimming, 0)]
        [TestCase(TrainingType.Cycling, 0)]
        public void MonthlyMaxCostWithPredicateTest(TrainingType tp, int expected)
        {
            YearlyCosts yc = YearlyCosts.LoadFrom(@"..\..\..\csv_files");

            Assert.That(yc.MonthlyMaxCost(tp), Is.EqualTo(expected));
        }

        [Test]
        public void SameCostsTest()
        {
            YearlyCosts yc = YearlyCosts.LoadFrom(@"..\..\..\csv_files");
            // 01, 03 hónapban vannak ugyanolyan költések

            // 0 -> 01. hónap
            // 2 -> 03. hónap
            TrainingCost[] expected = new TrainingCost[]
            {
                yc.Costs[2].TrainingCosts[0],
                yc.Costs[0].TrainingCosts[2],
                yc.Costs[2].TrainingCosts[1],
                yc.Costs[0].TrainingCosts[3]
            };

            Assert.That(yc.SameCosts(0, 2), Is.EqualTo(expected));
        }

        [TestCase(TrainingType.Swimming,4)]
        [TestCase(TrainingType.Running,0)]
        [TestCase(TrainingType.Cycling,4)]
        [TestCase(TrainingType.Hiking,0)]
        public void CostsBySportsTest(TrainingType tp, int expected)
        {
            YearlyCosts yc = YearlyCosts.LoadFrom(@"..\..\..\csv_files");

            int[] costCounts = yc.CostsBySports();

            int index = (int)tp;

            Assert.That(costCounts[index], Is.EqualTo(expected));
        }
    }
}
