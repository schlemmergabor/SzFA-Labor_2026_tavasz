using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MintaZH02;

namespace MintaZH02_Tests
{
    public class RaceResultsTests
    {
        [Test]
        public void BetweenTest()
        {
            // bemenet
            // vigyázz, mert a tömb nem rendezett idő szerint
            // ezt majd a ctor megcsinálja!
            string[] inputs = new string[]
            {
                "Jani,1:12:34",
                "Pisti,3:34:56",
                "Bazsi,3:12:34",
                "Peti,2:01:45",
                "Zsolti,2:02:33"
            };

            // RR létrehozása
            RaceResults rr = new RaceResults(inputs.Length, inputs);

            // kimenetként várt tömb
            // itt már idő szerint rendezetten kell !
            RunnerWithTime[] expected = new RunnerWithTime[]
            {
                RunnerWithTime.Parse("Peti,2:01:45"),
                RunnerWithTime.Parse("Zsolti,2:02:33"),
                RunnerWithTime.Parse("Bazsi,3:12:34"),
            };

            // eredmény amit a tesztelt metódus ad
            RunnerWithTime[] result = rr.Between(Time.Parse("2:01:45"),Time.Parse("03:30:00"));

            // eredménytömb megegyezik-e az elvárt tömbbel?
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void ContainsTest()
        {
            // bemenet
            string[] inputs = new string[]
            {
                "Jani,1:12:34",
                "Pisti,3:34:56",
                "Bazsi,3:12:34",
                "Peti,2:01:45",
                "Zsolti,2:02:33"
            };

            // RR létrehozása
            RaceResults rr = new RaceResults(inputs.Length, inputs);
            
            // Contains kimenetét ebbe fogjuk eltárolni
            RunnerWithTime result;

            // van-e Jani nevű
            Assert.That(rr.Contains(x => x.Nev == "Jani", out result), Is.True);
            
            // az eredmény nem null
            Assert.That(result, !Is.Null);
            
            // az eredmény az a RWT amit várunk
            Assert.That(result, Is.EqualTo(RunnerWithTime.Parse("Jani,1:12:34")));

            // Nézzünk egy olyat tesztet amikor nincs benne olyan elem

            // nincs 1:00:00 alatti idő
            Assert.That(rr.Contains(x => x.Eredmeny.CompareTo(Time.Parse("01:00:00")) < 0, out result), Is.False);

            Assert.That(result, Is.Null);
        }
        [Test]
        public void BetweenTest2()
        {
            string[] inputs2 = new string[]
            {
                "Jani,1:10:34",
                "Bazsi,2:12:34",
                "Peti,1:01:45",
                "Peti,1:01:01"
            };

            RaceResults rr = new RaceResults(inputs2.Length, inputs2);

            RunnerWithTime[] result = rr.Between(Time.Parse("01:30:00"),Time.Parse("02:30:00"));
            ;
        }
    }
}
