using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH01
{
    // futár osztályunk
    // public, mert tesztelni fogjuk
    public class Courier
    {
        // meg kell nézni, hogy az elkészített osztályokban mi a közös
        // van-e egy közös ősük, vagy van-e egy közös interface, amit megvalósítanak
        // FragileParcel : Parcel
        // NormalParcel : Parcel
        // Parcel : IDeliverable, IComperable
        // Envelope : IDeliverable

        // FragileParcel, NormalParcel őse a sima Parcel, ami megvalósítja a két interface-t
        // így ezek is megvalósítják, tehát:
        // FragileParcel : Parcel, IDeliverable, IComperable
        // NormalParcel : Parcel, IDeliverable, IComperable
        // Parcel : IDeliverable, IComperable
        // Envelope : IDeliverable
        // közös mindegyikben az IDeliverable -> ő lesz a tömb típusa

        // mezők
        // tömb a csomagoknak
        IDeliverable[] packages;

        // segédváltozó, packages indexelésére fogom használni
        // üres a packages, így az első elemet a 0. indexre kell tenni
        int idx = 0;

        // össztömeg, kezdetben 0
        // csomag felvételnél frissíteni kell !
        int totalWeight = 0;

        // Property  (feladat szerint)
        public int TotalWeight { get => totalWeight; private set => totalWeight = value; }

        // ctor
        // tömb elemszámát itt adjuk meg
        public Courier(int capacity)
        {
            // inicializáljuk a tömböt
            this.packages = new IDeliverable[capacity];
        }

        // Metódusok
        // csomag felvétele
        public void PickUpItem(IDeliverable item)
        {
            // ha már nem fér bele -> Exception
            if (this.idx == this.packages.Length)
                throw new DeliveryException("Out of capacity.");

            // minden más esetben belerakjuk a tömbbe
            // megnöveljük utána az idx segédváltozó értékét
            // hozzáadjuk az össz tömeghez az item tömegét
            this.packages[this.idx++] = item;
            this.totalWeight += item.Weight;

            /*
            // A fenti kód megoldható az eldöntés programozási tétel felhasználásával is
            // ekkor nem kell idx segéd változó
            // lásd: https://users.nik.uni-obuda.hu/sergyan/Programozas1Jegyzet.pdf - 25. oldal

            // tömbökkel dolgozunk, így figyelj az indexelésre és a hosszára! (-1)-ek!
            // első vizsgált index 0
            int i = 1 - 1;
            int N = this.packages.Length - 1;

            // a P itt most az, hogy a null ref a tömb[i] értéke
            while ((i <= N) && !(this.packages[i] == null)) i++;

            // találtunk-e P-nek megfelelő tömb elemet?
            bool van = i <= N;

            // nem találtunk -> mert tele van -> Exception
            if (!van) throw new DeliveryException("Out of capacity.");

            // találtunk -> i a null helye -> üres -> lehet tenni
            this.packages[i] = item;
            // össztömeghez item tömegének hozzáadása
            this.totalWeight += item.Weight;
            */
        }

        // Kiválogatás és sorrendezés
        // ? a null return miatt (nem kötelező)
        public IDeliverable[]? FragilesSorted()
        {
            // Vállaljuk be azt, hogy 2x járjuk be az összes elemet
            // egyszer az eredménytömb méretéért
            // egyszer pedig a kiválogatás miatt

            // eredmény tömb méretének meghatározása
            int count = 0;
            // foreach helyett, mehetett volna for-al is!
            foreach (IDeliverable item in this.packages)
            {
                // ha törékeny osztályba való az item
                if (item is FragileParcel) count++;
            }

            // ha nincs, akkor hát nincs :)
            if (count == 0) return null;

            // eredmény tömb -> ebbe gyűjtjük amit majd visszaadunk
            IDeliverable[] result = new IDeliverable[count];

            // melyik indexre kerülhet az új elem?
            int resultIdx = 0;

            // foreach helyett, mehetett volna for-al is!
            foreach (IDeliverable item in this.packages)
            {
                // ha törékeny osztályba való az item
                if (item is FragileParcel) result[resultIdx++] = item;
            }

            // beépített rendezést hívunk
            // itt használjuk fel a CompareTo-t
            Array.Sort(result);

            // visszaadjuk
            return result;
        }
    }
}
