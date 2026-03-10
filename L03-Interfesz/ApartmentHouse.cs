using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace L03_Interfesz
{
    internal class ApartmentHouse
    {
        // mezők
        int countFlat;
        int countGarage;

        int maxFlat;
        int maxGarage;

        // Auto-Property -> prop tab tab
        // set csak private
        // itt csak definiálom a Property-t
        // tömb miatt majd inicializálni (létrehozni) is kell ctorban!!!
        // Flat, Garage közös őse nincs, de mindkettő IRealEstate-t megvalósító
        public IRealEstate[] Container { get; private set; }

        // ctor
        public ApartmentHouse(int maxFlat, int maxGarage)
        {
            this.maxFlat = maxFlat;
            this.maxGarage = maxGarage;

            // kezdetben 0 a lakások és garázsok száma
            this.countFlat = 0;
            this.countGarage = 0;

            // tömb inicializálása
            // itt létrehozzuk a tömböt, mert itt tudjuk a méretét!
            this.Container = new IRealEstate[maxFlat + maxGarage];
        }

        // IRealEstate a paraméter, mert mind Garage, mind Flat mehet bele
        public bool Add(IRealEstate obj)
        {
            // ha a paraméter Flat
            if (obj is Flat)
            {
                // még lehet lakás
                if (this.countFlat < this.maxFlat)
                {
                    // a tömb szabad indexére felvesszük
                    Container[this.countFlat + this.countGarage] = obj;
                    this.countFlat++;
                    return true;
                }
            }

            // ha a paraméter Garage
            if (obj is Garage)
            {
                // még lehet garázs
                if (this.countGarage < this.maxGarage)
                {
                    // szabad indexére felvesszük
                    Container[this.countFlat + this.countGarage] = obj;
                    this.countGarage++;
                    return true;
                }
            }

            // ha tele van, vagy, ha más nem lehet, vagy más obj-t adtunk át
            return false;
        }

        // Property-k
        // visszaadja házban lakók számát
        public int InhabitantsCount
        {
            get
            {
                int persons = 0;
                // ciklus a nem a tömb.Length-ig, hanem a benne lévő elemekig
                int maxIndex = this.countFlat + this.countGarage;

                for (int i = 0; i < maxIndex; i++)
                {
                    // ha lakás, akkor lakhatnak benne
                    if (this.Container[i] is Flat)
                    {
                        // this.Container[i]-t vesszük Flat-ként
                        // mit jelent a ? és a ??
                        // ha nem null lesz -> sikerül az átalakítás akkor
                        //     InhabitantsCount-ot adjuk hozzá
                        // ha null lesz -> nem sikerült az átalakítás akkor
                        //     0-t adunk hozzá
                        persons += (this.Container[i] as Flat)?.InhabitantsCount ?? 0;
                    }
                }
                return persons;
            }
        }

        // használatban lévők össz értéke
        public int TotalValue()
        {
            int sum = 0;

            // végigjárom a tömböt, most másképp, mint az InhabitantsCount Propertynél
            foreach (IRealEstate item in Container)
            {
                // az item az Lakás
                // ha igen, akkor f példányt csinálunk belőle
                // ha lakók száma > 0
                if (item is Flat f && f.InhabitantsCount > 0)
                    sum += f.TotalValue();

                // az item az Garázs és le van foglalva
                if (item is Garage g && g.IsBooked)
                    sum += g.TotalValue();
            }
            return sum;
        }

        // static metódus
        // osztályhoz van rendelve, nem az objektumhoz
        public static ApartmentHouse LoadFromFile(string fileName)
        {
            // beolvasom a file minden sorát
            string[] lines = File.ReadAllLines(fileName);

            // kiszedem a file-ból a garázsok számát
            int countOfGarage = 0;

            foreach (string s in lines)
            {
                if (s.Split(" ")[0] == "Garazs") countOfGarage++;
            }

            // lakások száma sorok száma - garázsok
            int countOfFlat = lines.Length - countOfGarage;

            // új ah létrehozása
            ApartmentHouse ah = new ApartmentHouse(countOfFlat, countOfGarage);

            // mégegyszer végig járjuk a beolvasott sorokat
            for (int i = 0; i < lines.Length; i++)
            {
                // fel daraboljuk a sorokat
                string[] pc = lines[i].Split(" ");

                // nm ér double
                // System.Global... InvariantCulture -> lekezeli a . és a , -t is
                double nm = double.Parse(pc[1], System.Globalization.CultureInfo.InvariantCulture);

                // szobaszám -> példában int...
                double rc = double.Parse(pc[2]);


                switch (pc[0])
                {
                    case "Alberlet":
                        int up = int.Parse(pc[3]);
                        ah.Add(new Lodgings(nm, rc, up));
                        break;

                    case "CsaladiApartman":
                        int up2 = int.Parse(pc[3]);
                        ah.Add(new FamilyApartment(nm, rc, up2));
                        break;

                    case "Garazs":
                        // fütött-e a garázs?
                        bool heated = pc[3] == "futott";
                        int up3 = int.Parse(pc[2]);
                        ah.Add(new Garage(nm, up3, heated));
                        break;

                }
            }
            // visszaadjuk a teljes ApartMentHouse-t
            return ah;
        }
    }
}
