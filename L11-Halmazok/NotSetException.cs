using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L11_Halmazok
{
    public class NotSetException : Exception
    {
        // mező
        IParticipant[] items;

        // ctor
        public NotSetException(IParticipant[] items)
        {
            this.items = items;
        }
    }

}
