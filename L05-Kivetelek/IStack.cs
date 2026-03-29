using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    // MockTest miatt létrehozott interface
    public interface IStack
    {
        // ezt a metódus mock-oljuk ki
        void Push(FoodIngredient newItem);
    }
}
