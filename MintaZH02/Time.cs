using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH02
{
    // 
    public class Time : IComparable
    {
        // adattagok, mezők
        int ora;
        int perc;
        int masodperc;

        // Propertyk
        public int Ora
        {
            get => ora;
            set
            {
                if (value < 0) throw new TimeException("Negative");
                if (value > 3) throw new TimeException("Bigger than 3");

                this.ora = value;
            }
        }
        public int Perc
        {
            get => perc;
            set
            {
                if (value < 0) throw new TimeException("Negative");
                if (value > 59) throw new TimeException("Bigger than 59");

                this.perc = value;
            }
        }
        public int Masodperc
        {
            get => masodperc;
            set
            {
                if (value < 0) throw new TimeException("Negative");
                if (value > 59) throw new TimeException("Bigger than 59");

                this.masodperc = value;
            }
        }

        // ctor -> setteren keresztül
        public Time(int ora, int perc, int masodperc)
        {
            Ora = ora;
            Perc = perc;
            Masodperc = masodperc;
        }

        // ctor 2 paraméteres -> ora = 0
        public Time(int perc, int masodperc) : this(0, perc, masodperc)
        {
        }

        public override string? ToString()
        {
            if (this.ora > 0)
                return $"{this.ora:00}:{this.perc:00}:{this.masodperc:00}";

            return $"{this.perc:00}:{this.masodperc:00}";
        }

        public static Time Parse(string input)
        {
            // feldarabolom input string
            string[] db = input.Split(":");

            // hibakezelés, ha nem 2 vagy 3 darabból áll
            if (db.Length < 2 || db.Length > 3)
                throw new TimeException("");

            // ha két darabos -> 2 paraméteres ctor
            if (db.Length == 2)
                return new Time(int.Parse(db[0]), int.Parse(db[1]));

            // 3 paraméteres ctor hívása
            return new Time(int.Parse(db[0]), int.Parse(db[1]), int.Parse(db[2]));
        }

        public override bool Equals(object? obj)
        {
            // hibakezelés
            if (obj is not Time) throw new ArgumentException();

            // obj átalakítás Time elemmé
            Time? other = obj as Time;

            // minden adattag megegyezik
            return this.ora == other?.ora && this.perc == other.perc
                && this.masodperc == other.masodperc;
        }

        public int CompareTo(object? obj)
        {
            // hibakezelés
            if (obj is not Time) throw new ArgumentException();

            // this mp érték számítása
            int thisMp = this.ora * 60 * 60 + this.perc * 60 + this.masodperc;

            Time? other = obj as Time;

            // other mp érték számítása
            int otherMp = other.ora * 3600 + other.perc * 60 + other.masodperc;

            // 
            return thisMp.CompareTo(otherMp);
        }
    }
}