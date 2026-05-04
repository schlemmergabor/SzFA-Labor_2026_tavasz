using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L10_RendezesKereses
{
    // public a tesztelés miatt
    // IComparable, mert ilyet tárol a Handler
    public class PhoneBookItem : IComparable
    {
        // autoProp
        public string Name { get; set; }
        public string Number { get; set; }

        // IComparable által előírt metódus
        public int CompareTo(object? obj)
        {
            // hibakezelés1
            if (obj == null) throw new ArgumentNullException();

            // hibakezelés2
            if (!(obj is PhoneBookItem) && !(obj is string))
                throw new ArgumentException();

            // ha az obj PBI, akkor átalakítjuk
            if (obj is PhoneBookItem pbi)
                return this.Name.CompareTo(pbi.Name);
            
            // itt az obj már csak string lehet
            return this.Name.CompareTo(obj as string);
        }

        // egyezőség vizsgálat (nem a == !!!)
        public override bool Equals(object? obj)
        {
            // hibakezelés1
            if (obj == null) throw new ArgumentNullException();
            
            // hibakezelés2
            if (obj is not PhoneBookItem)
                throw new ArgumentException();

            // neve és száma is egyezzen meg
            // Name, Number string típus, a == operátor 
            // értéket fog náluk vizsgálni és jó lesz!
            return (obj is PhoneBookItem pbi) &&
                this.Name == pbi.Name &&
                this.Number == pbi.Number;
        }
    }
}