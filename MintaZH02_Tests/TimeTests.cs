using MintaZH02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02_Tests
{
    internal class TimeTests
    {
        [TestCase("01:01:01")]
        [TestCase("01:01")]
        // sikeres parszolás
        public void ParseTest(string input)
        {
            // ne dobjon Exception-t
            Assert.DoesNotThrow(() => Time.Parse(input));
        }

        [TestCase("01:01:01")]
        // sikeres parszolás -> ez szerintem jobb tesztelés
        // mert ellenőrzöm a Propertyk beállítását is!
        public void ParseTest2(string input)
        {
            Time t = Time.Parse(input);

            int ora = int.Parse(input.Split(":")[0]);
            int perc = int.Parse(input.Split(":")[1]);
            int mperc = int.Parse(input.Split(":")[2]);

            Assert.That(t.Ora, Is.EqualTo(ora));
            Assert.That(t.Perc, Is.EqualTo(perc));
            Assert.That(t.Masodperc, Is.EqualTo(mperc));
        }

        [TestCase("04:00:00")]
        [TestCase("01:60:00")]
        [TestCase("01:00:-1")]
        // Exception keletkezik
        public void ParseTestException(string input)
        {
            Assert.Throws<TimeException>( () => Time.Parse(input) );
        }

        [TestCase("10:01", "10:30", -1)]
        [TestCase("10:01", "10:01", 0)]
        [TestCase("10:31", "10:30", 1)]
        // CompareTo mindhárom értékének ellenőrzése
        public void CompareToTest(string input1, string input2, int exp)
        {

            Time t1 = Time.Parse(input1);
            Time t2 = Time.Parse(input2);

            Assert.That(t1.CompareTo(t2), Is.EqualTo(exp));
        }
    }
}
