using MintaZH02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02_Tests
{
    internal class RacesTests
    {
        [Test]
        public void BestPerformanceTest()
        {
            // két input string
            string[] inputs1 = new string[]
            {
                "Jani,1:12:34",
                "Pisti,3:34:56",
                "Bazsi,3:12:34",
                "Peti,2:01:45",
                "Zsolti,2:02:33"
            };
            string[] inputs2 = new string[]
            {
                "Jani,1:10:34",
                "Bazsi,2:12:34",
                "Peti,1:01:45",
                "Peti,1:01:01"
            };

            // RaceResults tömbök
            RaceResults[] rrs = new RaceResults[]
            {
                new RaceResults(inputs1.Length, inputs1),
                new RaceResults(inputs2.Length, inputs2)
            };

            // Races objektum
            Races r = new Races(rrs);

            // Jani legjobb eredménye - 1:10:34 -e?
            Assert.That(r.BestPerformance("Jani"), Is.EqualTo(Time.Parse("1:10:34")));

            // Pisti eredménye jó-e?
            Assert.That(r.BestPerformance("Pisti"), Is.EqualTo(Time.Parse("3:34:56")));

            // Peti eredménye jó-e?
            Assert.That(r.BestPerformance("Peti"), Is.EqualTo(Time.Parse("01:01:01")));
        }
        [Test]
        public void AllBetweenTest()
        {
            // két input string
            string[] inputs1 = new string[]
            {
                "Jani,1:12:34",
                "Pisti,3:34:56",
                "Bazsi,3:12:34",
                "Peti,2:01:45",
                "Zsolti,2:02:33"
            };
            string[] inputs2 = new string[]
            {
                "Jani,1:10:34",
                "Bazsi,2:12:34",
                "Peti,1:01:45",
                "Peti,1:01:01"
            };

            // RaceResults tömbök
            RaceResults[] rrs = new RaceResults[]
            {
                new RaceResults(inputs1.Length, inputs1),
                new RaceResults(inputs2.Length, inputs2)
            };

            // Races objektum
            Races r = new Races(rrs);

            // elvárt eredménytömb
            RunnerWithTime[] expected = new RunnerWithTime[]
            {
                RunnerWithTime.Parse("Peti,2:01:45"),
                RunnerWithTime.Parse("Zsolti,2:02:33"),
                RunnerWithTime.Parse("Bazsi,2:12:34")
            };

            // Metódus által adott eredmény
            RunnerWithTime[] result = r.AllBetween(Time.Parse("01:30:00"),Time.Parse("02:30:00"));

            // az eredmény az elvárt-e
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void UnionTest()
        {
            
            string[] inputs1 = new string[]
            {
                "Jani,1:12:34",
                "Bazsi,3:12:34"
            };
            string[] inputs2 = new string[]
            {
                "Jani,1:12:34",
                "Peti,2:01:45",
                "Pisti,3:34:56"
            };
            RaceResults[] rr = new RaceResults[]
            {
                new RaceResults(2, inputs1),
                new RaceResults(3, inputs2)
            };

            Races r = new Races(rr);

            // halmazok
            RunnerWithTime[] a = new RunnerWithTime[]
            {
                RunnerWithTime.Parse(inputs1[0]),
                RunnerWithTime.Parse(inputs1[1])
            };
            RunnerWithTime[] b = new RunnerWithTime[]
            {
                RunnerWithTime.Parse(inputs2[0]),
                RunnerWithTime.Parse(inputs2[1]),
                RunnerWithTime.Parse(inputs2[2])
            };

            RunnerWithTime[] result = r.UnionForTest(a,b);

            // az eredmény mérete jó
            Assert.That(result.Length, Is.EqualTo(a.Length+b.Length));

            // jók az indexen lévő eredmények is
            Assert.That(result[0], Is.EqualTo(a[0]));
            Assert.That(result[1], Is.EqualTo(b[0]));
            Assert.That(result[2], Is.EqualTo(b[1]));
            Assert.That(result[3], Is.EqualTo(a[1]));
            Assert.That(result[4], Is.EqualTo(b[2]));


        }
    }
}
