using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L11_Halmazok
{
    public class Participant : IParticipant
    {
        // Property
        public string Name { get; private set; }

        // ctor
        public Participant(string name)
        {
            Name = name;
        }

        // Megyeznek-e
        public override bool Equals(object? obj)
        {
            // hibakezelés
            if (obj == null)
                throw new ArgumentNullException();
            if (obj is not Participant)
                throw new ArgumentException();

            Participant? other = obj as Participant;
            
            // nevük megegyezik-e
            return this.Name.Equals(other?.Name);
        }

        public int CompareTo(object? obj)
        {
            // hibakezelés
            if (obj == null)
                throw new ArgumentNullException();
            if (obj is not Participant)
                throw new ArgumentException();

            // Participant? other = obj as Participant;

            if (this.Equals(obj)) return 0;

            Participant? other = obj as Participant;
            return this.Name.CompareTo(other?.Name); // A-Z
        }

        public override string? ToString()
        {
            return $"{this.Name}";
        }
        public static Participant Parse(string s)
        {
            // hibakezelés, split, ...
            // most az egsszerűség kedvéért csak szövegből adja vissza

            return new Participant(s);
        }
    }

}
