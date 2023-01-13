using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Karten
{
    public class NavigationsKnotenpunkt
    {

        public float Kosten { get; set; }

        public bool Besetzt { get; set; }
        public float Distanz { get; set; }
        public float KostenDistanz => Kosten + Distanz;
        public NavigationsKnotenpunkt UrsprungsKnotenpunkt { get; set; }

        public float X
        {
            get;set;
        }

        public float Y
        {
            get;set;
        }


        public void DistanzSetzen(float zielX, float zielY)
        {
            this.Distanz = Math.Abs(zielX - X) + Math.Abs(zielY - Y);
        }

        public NavigationsKnotenpunkt(float x, float y)
        { 
            this.X = x;
            this.Y = y;
        }
        public NavigationsKnotenpunkt()
        {
            this.X = 0;
            this.Y = 0;
        }

    }
}
