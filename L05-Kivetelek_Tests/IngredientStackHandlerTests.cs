using L05_Kivetelek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek_Tests
{
    public class IngredientStackHandlerTests
    {
        [Test]
        public void HandlerTest()
        {
            // stack 2 elemű
            // tömböt megpróbálok beletenni
            // visszavárom az elemeket, amiket indexekkel adtam meg
            IngredientStack stack = new IngredientStack(2);

            FoodIngredient[] food = new FoodIngredient[] {
                new FoodIngredient("zsemle", 1, Egyseg.Darab),
                new FoodIngredient("tej", 1, Egyseg.Darab),
                new FoodIngredient("vaj", 1, Egyseg.Darab),
                new FoodIngredient("paprika", 1, Egyseg.Darab),
                new FoodIngredient("kolbász", 1, Egyseg.Darab)
            };

            IngredientStackHandler handler = new IngredientStackHandler(stack);

            FoodIngredient[] nemFertBele = handler.AddItems(food);

            // ezt várom vissza
            FoodIngredient[] vart =
            {
                food[2], food[3], food[4]
            };

            Assert.That(nemFertBele, Is.EqualTo(vart));
        }
    }
}
