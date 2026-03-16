using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L04_PrimeTool
{
    // 1.b órai feladat
    // cél, hogy bemutassuk a Mock tecnikát
    public class PrimeToolManager
    {
        // PrimeTool pt
        // eltároltuk a PrimeTool egy példányát
        // függőség lesz, addig nem tudjuk a PTM-et tesztelni
        // amíg nincs készen a PrimeTool

        // utána cseréltük le az IPrimeTool ifacera
        IPrimeTool pt;

        // ctor
        public PrimeToolManager(IPrimeTool pt)
        {
            this.pt = pt;
        }

        // Metódus
        public string IsPrime2Text()
        {
            // Itt használjuk a PrimeTool IsPrime metódusát
            // ami még nincs készen
            // de tesztelni akarjuk, hogy a PTM jól működik-e
            // erre jó a Mock
            if (pt.IsPrime()) return "It's a Prime.";

            return "It's not a Prime.";
        }
    }
}
