using System.Security.AccessControl;

namespace L02_Oroklodes
{
    internal class Program
    {
        // oldalhossz alapján vagy Rect-et vagy Sq-t ad vissza
        // közös ősük Shape
        static Shape MakeShape(int width, int height, string color)
        {
            // oldal paraméterek megegyeznek -> Square
            if (width == height) return new Square(width, color);

            // minden más esetben Rectangle-t adunk vissza
            return new Rectangle(width, height, color);
        }

        // Visszaadja a legnagyobb alakzatot
        static Shape GetBiggest(Shape[] tömb)
        {
            // max tétel lásd jegyzet, így kérjük vizsgán, ZH-n!
            int maxIndex = 0;

            // figyelj rá, hogy 1-től indul
            for (int i = 1; i < tömb.Length; i++)
            {
                // indexeken keresztül nézed meg 
                // nem tárolsz számított értéket
                if (tömb[i].Area() > tömb[maxIndex].Area())
                    maxIndex = i;
            }

            // figyelj, hogy mit returnolsz vissza
            return tömb[maxIndex];
        }

        static void Main(string[] args)
        {
            // Készítettünk egy tömböt, amibe tudunk téglalapot, négyzetet pakolni
            // közös Ősük a Rectangle, így a tömb referenciája ez lesz
            Rectangle[] rects = new Rectangle[] {
                new Rectangle(11, 22, "red"),
                new Square(13, "yellow")
            };

            // Korai kötés -> Fordítási időben dönti el, melyik metódus fusson le
            // Nincs az ős osztályban virtual
            // Nincs a leszármazott osztályban override

            // beállítjuk a Square szélességét
            rects[1].Width = 100;
            ; // breakpoint-al ellenőriztük

            // Azt akarjuk, hogy úgy működjön, ahogy "várnánk"
            // Ez lesz a Késői Kötés
            // Késői kötés -> Futtatási időben dönti el, melyik metódus fusson le
            // KELL:
            // Ős osztályba -> virtual
            // Leszámazott osztályba -> override

            // Ebben a változatban már benne van -> Késői kötés van

            // Miért kell az Equals? -> Ezt magyaráztam itt el

            // Két objektum, ami "látszólag" ugyanaz
            Rectangle r1 = new Rectangle(11, 22, "green");
            Rectangle r2 = new Rectangle(11, 22, "green");

            // r1 == r2 -> referenciákat hasonlít össze

            // eldönteni, r1 ugyanaz-e, mint az r2
            // Nekünk kell megírni az osztály Equals-ját!
            bool seged = r1.Equals(r2);

            // További feladatmegoldás

            // 1. öt síkidom egy tömbben

            // Shape a referencia, mert kört és téglalapot is teszek bele
            Shape[] shapes = new Shape[]
            {
                new Square(10, "yello"),
                new Rectangle(11, 12, "red"),
                new Rectangle(12, 13, "yello"),
                new Circle(2, "blue", true),
                new Rectangle(20, 11, true, "purple")
            };

            // 2. metódus ami lyukaszt, ha nagyobb a területe
            // -> Shape-be került, mert minden Shape-re kell ezt tudni

            // 3. metódus ami Rect v Square-t hoz létre (lásd feljebb)

            // letesztelem

            Shape stg = MakeShape(10, 10, "yello");

            Shape stg2 = MakeShape(12, 13, "brown");

            // 4. metódus a legnagyobb területű elemre (lásd feljebb)

            Shape biggest = GetBiggest(shapes);

            ;
        }

    }
}