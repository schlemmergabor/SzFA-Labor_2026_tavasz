using System.Security.Cryptography;

namespace MintaZH02
{
    internal class Program
    {
        static string[] ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            int count = int.Parse(lines[0]);

            string[] result = new string[count];
            for (int i = 1; i < lines.Length; i++)
            {
                result[i-1] = lines[i];
            }
            return result;
        }

        static Races ReadFolder(string path)
        {
            // könyvtárból beolvas txt és Races

            // kellenek a txt fájlok
            string[] files = Directory.GetFiles(path, "*.txt");

            RaceResults[] rr = new RaceResults[files.Length];

            // egyes RR-ek feltöltése
            for (int i = 0; i < files.Length; i++)
            {
                rr[i] = new RaceResults(ReadFile(files[i]).Length, ReadFile(files[i]));
            }

            return new Races(rr);
        }
        static void Main(string[] args)
        {
            // adatok beolvasása
            Races races = Program.ReadFolder(@"..\..\..\datas");

            // Jani legjobb eredménye
            Console.WriteLine($"Jani legjobb eredménye: {races.BestPerformance("Jani")}");

            // kik milyen idővel 01:45:00 és 02:00:00 között
            RunnerWithTime[] runners = races.AllBetween(Time.Parse("01:45:00"), Time.Parse("02:00:00"));

            Console.WriteLine("\n\nA következő futóknak van 01:45:00 és 02:00:00 közötti eredménye:\n");
            ;
            foreach (RunnerWithTime item in runners)
            {
                Console.WriteLine($"{item.Nev}\t\t{item.Eredmeny}");
            }
        }
    }
}
