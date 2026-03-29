using L05_Kivetelek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek_Tests
{
    public class IngredientStackTests
    {
        
        [Test]
        public void EmptyTest1()
        {
            // példányosítás
            // nincs Push, így
            // Empty() -> true
            IngredientStack s = new IngredientStack(2);

            Assert.That(s.Empty(), Is.EqualTo(true));
        }


        [Test]
        public void EmptyTest2()
        {
            // példányosítás
            // van push, így
            // Empty() -> false
            IngredientStack s = new IngredientStack(2);
            FoodIngredient f = new FoodIngredient("cukor", 0.5, Egyseg.Kilogramm);

            s.Push(f);

            Assert.That(s.Empty(), Is.EqualTo(false));
        }

        [Test]
        // ugyanaz, mint EmptyTest2
        public void PushTest1()
        {
            // példányosítás
            // van push, így
            // Empty() -> false
            IngredientStack s = new IngredientStack(2);
            FoodIngredient f = new FoodIngredient("cukor", 0.5, Egyseg.Kilogramm);

            s.Push(f);

            Assert.That(s.Empty(), Is.EqualTo(false));
        }

        [Test]
        // dob-e kivételt?
        public void PushTest2()
        {
            // példányosítás - 1 mérettel
            // van push, így
            // második push kivételt kell dobjon
            IngredientStack s = new IngredientStack(1);
            FoodIngredient f = new FoodIngredient("cukor", 0.5, Egyseg.Kilogramm);

            s.Push(f);

            Assert.Throws<StackFullException>( () => s.Push(f) );
        }

        [Test]
        public void PopTest1()
        {
            // dob-e Exception-t ha üres
            IngredientStack s = new IngredientStack(0);
            Assert.Throws<StackEmptyException>(() => s.Pop());
        }
        [Test]
        public void PopTest2()
        {
            // Push után ugyanazt Pop olja-e vissza
            IngredientStack s = new IngredientStack(2);
            FoodIngredient f = new FoodIngredient("cukor", 0.5, Egyseg.Kilogramm);

            s.Push(f);

            Assert.That(s.Pop(), Is.EqualTo(f));
        }

        [Test]
        public void TopTest()
        {
            IngredientStack s = new IngredientStack(1);
            FoodIngredient food =
                new FoodIngredient("zsemle", 2, Egyseg.Darab);

            s.Push(food);

            Assert.That(s.Top(), Is.EqualTo(food));
        }

    }
}
