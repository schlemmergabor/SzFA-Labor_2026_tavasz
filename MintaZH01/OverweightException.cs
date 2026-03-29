using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // Exception -> hibajelzésre használt osztály
    // throw-olható catch-elhető -> így
    // őse a beépített Exception
    // public a tesztelés miatt
    public class OverweightException : Exception
    {
        // Exception speciális osztály
        // lehet belső változója, mezője, propja
        // ctor-ja is lehet
        // itt most nem kellett
    }
}
