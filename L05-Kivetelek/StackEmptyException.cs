using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    public class StackEmptyException : StackException
    {
        // ctor
        // Opcionális üzenet paraméterrel

        // eredeti sor ez, alatta a MockTest miatti kódsor van
        // public StackEmptyException(IngredientStack stack, string message = "Stack is Empty!") : base(stack, message)
        public StackEmptyException(IStack stack, string message = "Stack is Empty!") : base(stack, message)
        {
        }
    }
}
