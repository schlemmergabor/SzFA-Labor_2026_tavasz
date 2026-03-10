using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L03_Interfesz
{
    // Interface neve
    // interface kulcsszóra figyelj
    // "előrírásokat" tartalmaz
    // metódus, proprety lehet benne,
    // mező, kódrészlet nincs !!!
    internal interface IRealEstate
    {
        // egyetlen metódus, int visszatéréssel
        // paraméter nélkül
        // alapból public abstract
        int TotalValue();
    }
}
