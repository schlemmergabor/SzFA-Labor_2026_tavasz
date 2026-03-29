using L05_Kivetelek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace L05_Kivetelek_Tests
{
    // Test osztály az IngredientStackHandler Mock-al való teszteléséhez
    public class ISHMockTests
    {
        // Átmásoltam a IngredientStackHandlerTests.HandlerTest metódusát
        [Test]
        public void MockTest()
        {
            // ezt kommentbe teszem, mert nem szabad használni !
            // IngredientStack stack = new IngredientStack(2);

            // Mock Objektum létrehozása
            Mock<IStack> mock = new Mock<IStack>();

            // Beállítjuk, hogy a Push metódust bármilyen FoodIngredient-tel meghívva
            // ha több, mint kétszer hívtuk dobja az Exception-t
            mock.Setup(x => x.Push( It.IsAny<FoodIngredient>() ) ).Callback(
                () =>
                {
                    // ha több, mint kétszer hívtuk meg
                    if (mock.Invocations.Count > 2)
                        throw new StackFullException(mock.Object, It.IsAny<FoodIngredient>() );
                }
                );
            
            FoodIngredient[] food = new FoodIngredient[] {
                new FoodIngredient("zsemle", 1, Egyseg.Darab),
                new FoodIngredient("tej", 1, Egyseg.Darab),
                new FoodIngredient("vaj", 1, Egyseg.Darab),
                new FoodIngredient("paprika", 1, Egyseg.Darab),
                new FoodIngredient("kolbász", 1, Egyseg.Darab)
            };

            // Mock.Object-et adok át a Stack helyett
            IngredientStackHandler handler = new IngredientStackHandler(mock.Object);

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
