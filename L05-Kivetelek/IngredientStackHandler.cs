using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    public class IngredientStackHandler
    {
        // mező

        // eredeti sor ez, alatta a MockTest miatti kódsor van
        // IngredientStack stack;
        IStack stack;

        // eredeti sor ez, alatta a MockTest miatti kódsor van
        // public IngredientStackHandler(IngredientStack stack)
        public IngredientStackHandler(IStack stack)
        {
            this.stack = stack;
        }

        // Metódusok
        // Van jobb, szebb megoldás is erre! :)
        public FoodIngredient[] AddItems(FoodIngredient[] foodIngredients)
        {
            // visszatérési tömb -> ebbe gyűjtjük majd azokat amelyek nem fértek bele
            // kezdetben 0 a mérete
            FoodIngredient[] result = new FoodIngredient[0];

            // foreach-el végig megyünk a tömbön
            foreach (FoodIngredient item in foodIngredients)
            {
                // megpróbáljuk beletenni a stackbe
                try
                {
                    this.stack.Push(item);
                }
                // nem fért bele
                catch (StackFullException ex)
                {
                    // cél result megnövelése és végére az item

                    // result-nál 1-el nagyobb tömb
                    FoodIngredient[] temp = new FoodIngredient[result.Length + 1];

                    // elemek átmásolása
                    for (int i = 0; i < result.Length; i++)
                    {
                        temp[i] = result[i];
                    }

                    // utolsó indexre az item
                    // temp[temp.Length - 1] = item;
                    temp[^1] = item;
                    
                    // a result legyen az eddigi temp
                    result = temp;
                }
            }
            // kigyűjtött eredmény vissza
            return result;
        }
    }
}
