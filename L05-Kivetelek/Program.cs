namespace L05_Kivetelek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // NaturalNumber és try catch használata
           
            // try -> ide kerülnek a "kritikus" kódok,
            // amik kivételt, hibát dobhatnak
            try
            {
                double number = PositiveNumber.Parse(Console.ReadLine());
            }
            // saját kivétel elkapása és példányosítása (ex)
            catch (WrongNumberException ex)
            {
                // ex példányon keresztül eléred a propertyket
                Console.WriteLine("Error. "+ ex.Number + " is not a Positive Number! "+ ex.Message);
            }
            // beépített formatException (pl.: nem szám)
            catch (FormatException ex)
            {
                Console.WriteLine("Error. It isn't a number! "+ ex.Message);
            }
            // Minden más, általános Exception elkapása
            catch (Exception ex)
            {
                Console.WriteLine("Error. "+ ex.Message);
            }
        }
    }
}
