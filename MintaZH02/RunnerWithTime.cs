using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02
{
    public class RunnerWithTime : IComparable
    {
        // Auto-Property, private módosítható
        public string Nev { get; private set; }
        public Time Eredmeny { get; private set; }

        // statikus Parse...

        public static RunnerWithTime Parse(string input)
        {
            // input string feldarabolása
            string[] db = input.Split(',');

            // hibakezelés, ha nem két részből áll
            if (db.Length != 2)
                throw new ArgumentException("wrong format");

            // "üres" objektum létrehozása
            RunnerWithTime result = new RunnerWithTime();

            // Nev prop beállítása
            result.Nev = db[0];

            // Eredmeny prop beállítása
            result.Eredmeny = Time.Parse(db[1]);

            // értékekkel beállított objektum visszaadása
            return result;
        }

        public int CompareTo(object? obj)
        {
            // hibakezelés
            if (obj is not RunnerWithTime)
                throw new ArgumentException();

            // átalakítás
            RunnerWithTime? other = obj as RunnerWithTime;

            // ha az időeredményük azonos
            if (this.Eredmeny.Equals(other.Eredmeny))
                // név szerinti A-Z CompareTo
                return this.Nev.CompareTo(other.Nev);

            // különböző eredményeknél, az idő szerinti CompareTo
            return this.Eredmeny.CompareTo(other.Eredmeny);
        }

        public override string? ToString()
        {
            return $"{this.Nev} ({this.Eredmeny})";
        }

        public override bool Equals(object? obj)
        {
            // hibakezelés
            if (obj is not RunnerWithTime)
                throw new ArgumentException();

            // átalakítás
            RunnerWithTime? other = obj as RunnerWithTime;

            // nevük és eredményük is egyezik
            return this.Nev.Equals(other.Nev) &&
                this.Eredmeny.Equals(other.Eredmeny);
        }
    }
}