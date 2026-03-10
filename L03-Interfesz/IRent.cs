using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    internal interface IRent
    {
        // Property-k, amik csak get-elhetőek
        bool IsBooked { get; }

        // Metódusok
        int GetCost(int months);
        bool Book(int months);
    }
}
