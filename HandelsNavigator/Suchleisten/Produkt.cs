using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Suchleisten
{
    public class Produkt
    {
        private string name;
        private string typ;
        private double preis;

        private Vector2 position = new Vector2(0f, 0f);

        public string Name { get { return name; } }
        public string Typ { get { return typ; } }
        public double Preis { get { return preis; } }

        public Vector2 Position { get { return position; } }


        public Produkt(string name,string typ,double preis,Vector2 position)
        {
            this.name = name;
            this.typ = typ;
            this.preis = preis;
            this.position = position;
        }

    }
}
