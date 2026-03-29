using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // interfész
    // előírásokat tartalmaz Property-re és Metódus-ra
    // mező nem lehet benne,
    // kitöltött/megvalósított metódus nem lehet benne
    // mindig nagy I betűvel kezdődik

    // public a láthatósága a tesztelések miatt
    public interface IDeliverable
    {
        // Property előírások
        // súly előírás, get, set
        int Weight { get; set; }

        // cím előírás, get, set
        string Address { get; set; }

        // Metódus előírás
        double CalculatePrice(bool fromLocker);
    }
}
