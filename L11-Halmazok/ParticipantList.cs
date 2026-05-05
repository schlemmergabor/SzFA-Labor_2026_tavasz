using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L11_Halmazok
{
    // EZ lesz a HALMAZ !!!
    // Ne zavarjon meg a List elnevezés, tömböt kell használni
    public class ParticipantList
    {
        // Property, ebben lesznek a résztvevők
        public IParticipant[] Items { get; private set; }

        // ctor
        public ParticipantList(IParticipant[] items)
        {
            // halmaz-e ellenrőzések
            if (!IsOrdered(items) || !IsUnique(items))
                throw new NotSetException(items);

            // teljesíti a halmaz tulajdonságok -> beállítás
            this.Items = items;
        }
        // halmaz ellenőrzés - Rendezett-E
        private bool IsOrdered(IParticipant[] array)
        {
            // Early Exit-el
            // jegyzetben van egy bonyolultabb algoritmus
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i + 1].CompareTo(array[i]) < 0)
                    return false;
            }
            return true;
        }

        // halmaz ellenőrzés - EgyediE (nincsenek ismétlődő elemek)
        // itt kihasználjuk, hogy a fentit (rendezettE) már
        private bool IsUnique(IParticipant[] array)
        {
            // Ismét Early Exit-el (nem dupla for loopal!)
            // feltételezzük, hogy rendezett
            // így már csak páronként elég megnézni, hogy egyeznek-e?
            for (int i = 0; i < array.Length - 1; i++)
            {
                // találtunk egy egyező párt -> nem egyedi
                if (array[i + 1].Equals(array[i])) return false;
            }
            return true;
        }

        // Metódusok
        // Benne van-e a tömbben? Bináris Kereséssel
        public bool Contains(object participant)
        {
            int bal = 1 - 1;
            int jobb = this.Items.Length - 1;

            int center = (bal + jobb) / 2;

            while ((bal <= jobb) &&
                (!this.Items[center].Equals(participant))
                )
            {
                if (this.Items[center].CompareTo(participant) > 0)
                    jobb = center - 1;

                else bal = center + 1;

                center = (bal + jobb) / 2;
            }
            return bal <= jobb;
        }

        // Részhalmaza-e
        // részhalmaz vizsgálat algoritmussal -> jegyzet
        public bool IsSubset(ParticipantList other)
        {
            int i = 1 - 1, j = 1 - 1;

            int m = this.Items.Length - 1;
            int n = other.Items.Length - 1;

            while ((i <= m) && (j <= n) &&
                (this.Items[i].CompareTo(other.Items[j]) >= 0)
                )
            {
                if (this.Items[i].Equals(other.Items[j]))
                    i++;

                j++; // if-en kívül van !!!
            }
            return i > m;
        }

        // felhasználjuk a megírt metódust
        public static bool IsSubset(ParticipantList list1, ParticipantList list2)
        {
            return list1.IsSubset(list2);
        }

        // Metszet, algoritmus a jegyzetben
        // két halmaz metszetét adja vissza
        public ParticipantList Intersection(ParticipantList other)
        {
            int n1 = this.Items.Length - 1;
            int n2 = other.Items.Length - 1;

            // kisebb halmaz méretűre beállítjuk az eredmény halmazt
            IParticipant[] b = new IParticipant[(n1 < n2 ? n1 : n2) + 1];

            int i = 0, j = 0, db = 0;
            while ((i <= n1) && (j <= n2))
            {
                if (this.Items[i].CompareTo(other.Items[j]) < 0) i++;
                else
                    if (this.Items[i].CompareTo(other.Items[j]) > 0) j++;
                else
                {
                    b[db++] = this.Items[i];
                    i++; j++;
                }
            }

            // ez azért kell, hogy ne null elemeket adjunk vissza
            // a lista végén
            Array.Resize(ref b, db);
            return new ParticipantList(b);
        }

        public static ParticipantList Intersection(ParticipantList list1, ParticipantList list2)
        {
            return list1.Intersection(list2);
        }

    }

}
