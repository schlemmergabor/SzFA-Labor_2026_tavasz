using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    public class StackFullException : StackException
    {
        // mező
        // eltároljuk, hogy melyik élelmiszert nem tudtuk eltenni
        FoodIngredient fi;

        // ctor
        // opcionális üzenet paraméterrel

        // eredeti sor ez, alatta a MockTest miatti kódsor van
        // public StackFullException(IngredientStack stack, FoodIngredient fi, string message = "Stack is Full!") : base(stack, message)
        public StackFullException(IStack stack, FoodIngredient fi, string message = "Stack is Full!") : base(stack, message)
        {
            this.fi = fi;
        }
    }
}
