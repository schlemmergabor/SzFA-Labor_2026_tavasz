using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    public class StackException : Exception
    {
        // mező
        // IngredientStack stack;
        IStack stack;

        // ctor
        // opcionális üzenet paraméterrel
        
        // eredeti sor ez, alatta a MockTest miatti kódsor van
        // public StackException(IngredientStack stack, string message="StackException Error!") : base(message)
        public StackException(IStack stack, string message = "StackException Error!") : base(message)
        {
            this.stack = stack;
        }
    }
}
