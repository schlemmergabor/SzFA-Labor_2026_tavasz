using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L10_RendezesKereses
{
    public class NotOrderedItemsException : Exception
    {
        IComparable[] array;

        public NotOrderedItemsException(IComparable[] array)
        {
            this.array = array;
        }
    }
}
