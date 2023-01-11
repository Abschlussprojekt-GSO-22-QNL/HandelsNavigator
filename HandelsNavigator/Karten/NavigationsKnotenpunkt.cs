using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Karten
{
    internal class NavigationsKnotenpunkt
    {

        Vector2 position = new Vector2(0, 0);

        public int Cost { get; set; }
        public float Distance { get; set; }
        public float CostDistance => Cost + Distance;
        public NavigationsKnotenpunkt Parent { get; set; }

        public Vector2 Position
        {
            get { return position; }
        }


        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - position.X) + Math.Abs(targetY - position.Y);
        }


    }
}
