using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L08_TrainingCosts
{
    public class YearlyCosts
    {
        public MonthlyCosts[] Costs { get; set; } = new MonthlyCosts[12];

        public static YearlyCosts LoadFrom(string folderName)
        {
            if (!Directory.Exists(folderName)) throw new DirectoryNotFoundException();

            YearlyCosts result = new YearlyCosts();
            foreach (string filename in Directory.GetFiles(folderName))
            {
                // itt kell egy -1, mert anélkül 1-es indexre teszi a költést.
                int index = int.Parse(filename.Substring(filename.Length - 6, 2)) - 1;
                result.Costs[index] = MonthlyCosts.LoadFrom(filename);
            }
            return result;
        }

        //////////////////////////////////////////
        //                                      //
        // Innen kezdődik a feladatok megoldása //
        //                                      //
        //////////////////////////////////////////
        
        // 2.1. Melyik hónapban volt a legtöbb költés?
        public int MonthlyMaxCost()
        {
            int maxIndex = 1 - 1; // -1 az indexlés miatt
            for (int i = 1; i <= this.Costs.Length - 1; i++)
            {
                if (this.Costs[i] is not null)
                    if (this.Costs[i].TotalCost() > this.Costs[maxIndex].TotalCost() )
                        maxIndex = i;
            }
            return maxIndex; // 0. lesz a január
        }

        // 2.2. Melyik hónapban volt a legtöbb pénz egy sportágra
        public int MonthlyMaxCost(TrainingType tp)
        {
            // régebbi feladatokban predicate volt
            // ezt fogjuk majd felhasználni
            Predicate<TrainingCost> pre = x => x.Type == tp;

            int maxIndex = 1 - 1;
            for (int i = 1; i <= this.Costs.Length - 1; i++)
            {
                if (this.Costs[i] is not null)
                    if (this.Costs[i].TotalCost(pre) > this.Costs[maxIndex].TotalCost(pre)) maxIndex = i;
            }
            return maxIndex;
        }

        // 2.3. Kiadások, amelyek sportága, leírása azonos két hónapban
        // mi1 -> monthIndex 1
        // mi2 -> monthIndex 2
        public TrainingCost[] SameCosts(int mi1, int mi2)
        {
            // eredménytömb, kezdetben 0
            TrainingCost[] result = new TrainingCost[0];

            // első hónap költésein végig megyek
            for (int i = 0; i < this.Costs[mi1].TrainingCosts.Length; i++)
            {
                // típusa és leírása megegyezik
                Predicate<TrainingCost> pre =
                    x => x.Type == this.Costs[mi1].TrainingCosts[i].Type &&
                    x.Description == this.Costs[mi1].TrainingCosts[i].Description;

                try
                {
                    // második hónapból lekérem az ilyen
                    // predicate-nek megfelelő költéseket
                    TrainingCost[] result1 = this.Costs[mi2].CostsArray(pre);

                    // ha nem lett exception, akkor van ilyen költés
                    // adjuk hozzá a tömbhöz
                    
                    // új méretű tömb
                    // +1, mert a feltételt is hozzáteszem
                    TrainingCost[] newResult = new TrainingCost[result1.Length + result.Length+1];
                    // eddigiek átmásolása
                    for (int j = 0; j < result.Length; j++)
                    {
                        newResult[j] = result[j];
                    }

                    // result1 hozzámásolása
                    for (int j = result.Length; j < result.Length+result1.Length; j++)
                    {
                        newResult[j] = result1[j- result.Length];
                    }
                    // feltétel hozzátevése
                    newResult[newResult.Length - 1] = this.Costs[mi1].TrainingCosts[i];
                    // csere
                    result = newResult;

                }
                catch (Exception)
                {
                    continue;
                    //throw; // ugyanazt a kivételt dobja tovább
                }
            }
            return result;
        }
        // 2.4. Melyik sportágra hányszor fordított pénzt egy évben
        public int[] CostsBySports()
        {
            // Sportágak enum-ből csinálok tömböt
            Array enumValues = Enum.GetValues(typeof(TrainingType));

            // enum mögött int van, így
            int[] enumInts = new int[enumValues.Length];

            // kezdetben minden sportköltésből 0 van
            int[] costsCount = new int[enumValues.Length];

            // kigyűjtöm az enum-nak megfelelő int-eket
            for (int i = 0; i < enumValues.Length; i++)
            {
                enumInts[i] = (int)enumValues?.GetValue(i);

                // éves költség ehhez az enumhoz
                for (int j = 0; j < this.Costs.Length; j++)
                {
                    if (this.Costs[j] is not null)
                        costsCount[i] += 
                            this.Costs[j].CountCost(x => x.Type == (TrainingType)enumValues?.GetValue(i));
                }
            }

            return costsCount;
        }
    }
}
