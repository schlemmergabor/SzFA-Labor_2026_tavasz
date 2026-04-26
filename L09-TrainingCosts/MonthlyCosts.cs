using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace L08_TrainingCosts
{
    public class MonthlyCosts
    {
        public TrainingCost[] TrainingCosts { get; set; }

        public static MonthlyCosts LoadFrom(string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException();

            MonthlyCosts result = new MonthlyCosts();
            result.TrainingCosts = new TrainingCost[FileLength(filename)];

            using (StreamReader sr = new StreamReader(filename))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    result.TrainingCosts[i] = TrainingCost.Parse(line);
                    ++i;
                }
            }

            return result;
        }

        private static int FileLength(string filename)
        {
            return File.ReadAllLines(filename).Length;
        }

        //////////////////////////////////////////
        //                                      //
        // Innen kezdődik a feladatok megoldása //
        //                                      //
        //////////////////////////////////////////


        // 1.1. Teljes költés a hónapban
        public int TotalCost()
        {
            // kezdeti össz költség nulla
            int sum = 0;

            // végig megyünk a tömböm
            foreach (TrainingCost item in this.TrainingCosts)
            {
                // ha az item nem null (ezt jelenti a ?.)
                // akkor a Cost-ot adjuk hozzá
                // ha a null lenne, akkor a 0-t (ez a ??)
                sum += item?.Cost ?? 0;
            }
            return sum;
        }

        // 1.2. Teljes költés feltételel
        // predikátumfüggvénnyel

        // Predicate<> beépített delegált (metódus ref.)
        // bemenete <TrainingCost>, kimenete bool
        public int TotalCost(Predicate<TrainingCost> pre)
        {
            int sum = 0;

            foreach (TrainingCost item in this.TrainingCosts)
            {
                // ha a pre delegált (metódus) igaz,
                // csak akkor számoljuk az összeghez
                if (pre(item))
                    sum += item?.Cost ?? 0;
            }
            return sum;
        }

        // 1.3. Volt-e adott feltételnek megfelelő költés
        // Predicate (lásd feljebb)
        public bool HasMatchingCost(Predicate<TrainingCost> pre)
        {
            foreach (TrainingCost item in TrainingCosts)
            {
                // Early Exit
                // amint teláltunk egy ilyen elemet, akkor volt
                // tehát mehet a return
                if (pre(item)) return true;
            }
            // ide akkor kerül a vezérlés, ha nem volt ilyen elem
            return false;
        }

        // 1.4. Minden költség megfelel a feltételnek
        // FunFact: 3 felkiáltójelben tér el, mint az előző
        public bool AllMatchingCost(Predicate<TrainingCost> pre)
        {
            foreach (TrainingCost item in TrainingCosts)
            {
                // ha az aktuális elem nem tejesíti a metódust
                // akkor nem minden elem teljesíti ->  a false
                if (!pre(item)) return !true;
            }
            // ha ide jut, akkor mindent teljesít -> true
            return !false;
        }

        // 1.5. Volt-e legalább k db feltételes költés
        public bool HasMinKCost(Predicate<TrainingCost> pre, int k)
        {
            // kezdetben a db nulla
            int count = 0;
            // végig járjuk az összes költést tartalmazó tömböt
            foreach (TrainingCost item in this.TrainingCosts)
            {
                // ha az item teljesíti a feltételt, akkor db++
                if (pre(item)) count++;

                // ha már van k db-nyi, akkor return
                if (count == k) return true;
            }
            // ha ide jutunk, akkor nem volt k db-nyi
            // feltételt teljesítő elemünk
            return false;
        }
        // 1.6. Melyik volt a k adik költés (ha volt)
        // TrainingCost? a visszatérés, ? miatt lehet return null
        // átírjuk az 1.5-ös feladat megoldását
        public TrainingCost? GetKthCost(Predicate<TrainingCost> pre, int k)
        {
            int count = 0;

            foreach (TrainingCost item in this.TrainingCosts)
            {
                if (pre(item)) count++;

                // ha már van k db-nyi, akkor return item-et
                if (count == k) return item;
            }

            return null;
        }

        // 1.7. Hány darab feltételnek megfelelt költés volt
        // jegyzet - Megszámlálás programozási tétel
        public int CountCost(Predicate<TrainingCost> pre)
        {
            int db = 0;
            int n = this.TrainingCosts.Length - 1; // -1 az indexelés miatt

            // -1 az indexelés miatt
            for (int i = 1 - 1; i <= n; i++)
            {
                if (pre(this.TrainingCosts[i])) db++;
            }
            return db;
        }

        // 1.8. Legnagyobb kiadású költés
        // jegyzet - Maximumkiválasztás programozási tétel
        public TrainingCost BiggestCost()
        {
            // ha nem volt a hónapban költés -> Exception
            if (this.TrainingCosts.Length == 0) throw new ZeroLengthArrayException();

            int max = 1 - 1; // -1 az indexelés miatt
            int n = this.TrainingCosts.Length - 1; // -1 az indexelés miatt 
            for (int i = 2 - 1; i <= n; i++)
            {
                if (this.TrainingCosts[i].Cost > this.TrainingCosts[max].Cost)
                    max = i;
            }
            return this.TrainingCosts[max];
        }

        // 1.9. Legnagyobb költéssel járók indexe
        public int[] BiggestCostsIndexes()
        {
            // ha nem volt a hónapban költés -> Exception
            if (this.TrainingCosts.Length == 0) throw new ZeroLengthArrayException();

            // visszatérési tömb
            // mérete akkora, mint az eredeti tömb, mert lehet, hogy mind max lesz
            // kezdőértékei 0,0,0,0
            int[] result = new int[this.TrainingCosts.Length];

            // a legnagyobb érték az első indexű lesz
            // a resultban 0-tól indexelve tesszük majd a
            // max-ok indexét
            result[0] = 0;

            // hány db max értékünk van?
            int db = 1; // 0. indexű a max érték

            // 1. indextől fogjuk nézni a többi elemet
            for (int i = 1; i < this.TrainingCosts.Length; i++)
            {
                // ha az i. elem nagyobb, mint az eddigi max
                // akkor találtunk egy új, nagyobb max-ot, így
                // result[0] eredménye lesz -> ezt adjuk át a másik indexének
                if (this.TrainingCosts[i].Cost > this.TrainingCosts[result[0]].Cost)
                {
                    // ő lesz az új max, tehát megy a result elejére
                    result[0] = i;
                    // maxok db számát frissítjük 1-re
                    db = 1;
                }

                // ha nem nagyobb, viszont megegyezik az eddigi max-al
                // akkor egy újabb, az eddigiekkel megegyező max-ot találunk az indexen
                // result[0]-n van a max elem
                else if (this.TrainingCosts[i].Cost == this.TrainingCosts[result[0]].Cost)
                {
                    // elmentjük a result megfelelő indexére az i-t
                    result[db] = i;
                    // növeljük a max-ok db számát
                    db++;
                }

                // ha kisebbet találtunk mint az eddigi max nem csinálunk semmit
                // léphet a következő elemre a ciklusunk
            }

            // átméretezzük a result tömböt -> levágjuk a felesleges hátsó indexeket
            Array.Resize(ref result, db);

            // és így már egy olyan tömbbel térünk vissza amiben csak annyi elem van
            // ahány legnagyobb értékünk érték van
            return result;
        }

        // 1.10. Legnagyobb feltételes kiadás
        public TrainingCost? BiggestCost(Predicate<TrainingCost> pre)
        {
            // ha nem volt a hónapban költés -> Exception
            if (this.TrainingCosts.Length == 0) throw new ZeroLengthArrayException();

            // ha nem volt legalább 1 db ilyen költés -> akkor nem lehet legnagyobb
            // térjünk vissza egy null ref.-el
            if (!this.HasMatchingCost(pre)) return null;

            // ide akkor kerül a vezérlés ha van legalább 1
            // feltételes költség

            // kezdeti legnagyobb költség - végtelen legyen

            int maxValue = int.MinValue;
            int maxIndex = -1; // kezdeti legnagyobb indexe

            // egész tömböt végig kell nézni
            for (int i = 0; i < this.TrainingCosts.Length; i++)
            {
                // ha megfelel a feltételnek
                // és az értéke nagyobb, mint az eddigi legnagyobb
                if (pre(this.TrainingCosts[i]) && this.TrainingCosts[i].Cost > maxValue)
                {
                    // legnagyobb érték és index frissítése
                    maxValue = this.TrainingCosts[i].Cost;
                    maxIndex = i;
                }
            }

            return this.TrainingCosts[maxIndex];
        }

        // 1.11. Költségek újraszámolás, kerékpár, futás / 2 ->
        // így melyik volt a legnagyobb költés
        public TrainingCost BiggestCostAlternative()
        {
            // ha nem volt a hónapban költés -> Exception
            if (this.TrainingCosts.Length == 0) throw new ZeroLengthArrayException();

            int maxIndex = 1 - 1; // -1 az indexelés miatt
            int n = this.TrainingCosts.Length - 1; // -1 az indexelés miatt 

            // maxindex elem költség számítása
            // ha kerékpár vagy futás, akkor fele, amúgy az eredeti érték
            // Ternary (Elvis) operatorral
            int maxCost = this.TrainingCosts[0].Type == TrainingType.Cycling ||
                this.TrainingCosts[0].Type == TrainingType.Running
                ? this.TrainingCosts[0].Cost / 2
                : this.TrainingCosts[0].Cost;

            for (int i = 2 - 1; i <= n; i++)
            {
                // i. elem költsége
                int itemCost = this.TrainingCosts[i].Cost;

                // ha futás vagy kerékpározás, akkor osztunk 2-vel
                if (this.TrainingCosts[i].Type == TrainingType.Cycling ||
                    this.TrainingCosts[i].Type == TrainingType.Running)
                    itemCost /= 2;

                // új maxértéket találtnk
                if (itemCost > maxCost)
                {
                    // maxindex, maxCost frissítése
                    maxIndex = i;
                    maxCost = itemCost;
                }
            }
            return this.TrainingCosts[maxIndex];
        }

        // 1.12. Adott feltételnek megfelelő költések indexe
        public int[] CostsIndexes(Predicate<TrainingCost> pre)
        {
            // megszámoljuk hány ilyen feltételnek eleget tevő elem van
            // ehhez felhasználjuk a már megírt metódusunkat
            int count = this.CountCost(pre);

            if (count == 0) throw new ZeroLengthArrayException();

            // indexeket tároló visszatérési tömb
            int[] result = new int[count];
            // melyik index-re fogjuk tenni a következő talált elemet?
            int db = 0;

            // végig megyünk i-vel indexelve a tömbön
            for (int i = 0; i < this.TrainingCosts.Length; i++)
            {
                // ha adott tulajdonságú az i. elem
                if (pre(this.TrainingCosts[i]))
                    // elmentjük és db++
                    result[db++] = i;
            }

            return result;
        }

        // 1.13. Minden adat ami egy feltételnek megfelel -> objektumtömb
        public TrainingCost[] CostsArray(Predicate<TrainingCost> pre)
        {
            // használjuk fel az 1.12-t
            int[] indexes = this.CostsIndexes(pre);

            // ha exception van, az a felsőbb szintű hívóra megy tovább

            // eredménytömb
            TrainingCost[] result = new TrainingCost[indexes.Length];

            // melyik indexet tárolom?
            int db = 0;

            // végig megyek az indexeken
            for (int i = 0; i < indexes.Length; i++)
            {
                // elmentem az eredménybe
                // az i. által az indexes tömbben szereplő
                // indexet adó TC-t. :)
                result[db++] = this.TrainingCosts[indexes[i]];
            }
            return result;
        }

        // 1.14. Tömb átalakítása -> feltételek előre
        // jegyzet - Szétválogatás programozás tétel az eredeti tömbben 
        public void PartitionSort(Predicate<TrainingCost> pre)
        {
            int bal = 1 - 1; // -1 indexelés miatt
            int jobb = this.TrainingCosts.Length - 1; // -1 indexelés miatt
            TrainingCost seged = this.TrainingCosts[0];
            while (bal < jobb)
            {
                while ((bal < jobb) &&
                    (!(pre(this.TrainingCosts[jobb]))))
                    jobb--;

                if (bal<jobb)
                {
                    this.TrainingCosts[bal++] =this.TrainingCosts[jobb];

                    while ((bal < jobb) && (pre(this.TrainingCosts[bal])))
                        bal++;

                    if (bal < jobb)
                        this.TrainingCosts[jobb--] = this.TrainingCosts[bal];
                }
            }
            this.TrainingCosts[bal] = seged;
        }
    }
}