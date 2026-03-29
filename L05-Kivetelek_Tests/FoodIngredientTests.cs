using L05_Kivetelek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek_Tests
{
    public class FoodIngredientTest
    {
        [TestCase("zsemle", 1.0, Egyseg.Darab)]
        [TestCase("tej", 0.5, Egyseg.Liter)]
        public void ToStringTest(string nev, double db, Egyseg e)
        {
            FoodIngredient f = new FoodIngredient(nev, db, e);

            string vart = $"{nev} - {db} - {e}";

            Assert.That(f.ToString(), Is.EqualTo(vart));
        }
    }
}
