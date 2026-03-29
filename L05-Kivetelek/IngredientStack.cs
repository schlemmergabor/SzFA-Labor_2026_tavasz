using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace L05_Kivetelek
{
    // A Stack-ünk úgy fog működni, hogy van egy tömb és egy itemCount segédváltozó.
    // itemCount-ben az tároljuk, hogy melyik indexen van a legutoljára eltárolt elem
    // illetve, hogy hány elemünk van a tömbben
    // itemCount kezdben 0
    // Push(...) esetében eltesszük az indexre és itemCount++
    // Pop() esetében kivesszük és --itemCount
    // Pop-nál nem állítjuk null ref-re (megspóroljuk)
    // ugyanis egy újabb Push(...)-al felül fogjuk majd azt írni.

    // eredeti sor ez, alatta a MockTest miatti kódsor van
    // public class IngredientStack
    public class IngredientStack : IStack
    {
        // mező
        // tömb, itemCount
        FoodIngredient[] foods;
        int itemCount = 0;

        // ctor
        public IngredientStack(int number)
        {
            // tömb létrehoása megfelelő mérettel
            // ekkor még null minden eleme
            this.foods = new FoodIngredient[number];
        }

        // Metódusok
        // lambda-val megadva
        // => return
        // mivel bool a metódus, ezért egy bool kifejezést írsz utána
        public bool Empty() => this.itemCount == 0;

        // stack üres helyére helyez
        public void Push(FoodIngredient newItem)
        {
            // ha tele van -> hiba -> Exception
            if (this.itemCount == this.foods.Length)
                // dobunk új saját kivételt
                // paramétere this, el nem helyezett elem
                throw new StackFullException(this, newItem);

            // el tudjuk helyezni a tömbben
            // a változó után van a ++, azaz
            // először felhasználja az értékét az indexelésre, majd
            // utána növeli meg a változó értékét
            this.foods[this.itemCount++] = newItem;
        }

        // kivesz egy elemet a hátsó indexről
        public FoodIngredient Pop()
        {
            // ha üres -> új saját kivétel dobása
            if (this.Empty())
                throw new StackEmptyException(this);

            // --itemcount
            // először csökkenti az értékét, majd felhasználja indexeléshez
            // majd utána a return
            return this.foods[--itemCount];
        }

        // csak "megmutatja" mi a felső elem
        // a metódus visszaétérési értékénél azért van ?
        // mert akkor engedi, hogy legyen null ref. is
        // így a VS nem húzza zölden alá
        public FoodIngredient? Top()
        {
            // null ref. ha üres
            if (this.Empty()) return null;

            return this.foods[itemCount - 1];
        }
    }
}
