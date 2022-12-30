using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Karten
{
    internal class KartenObjekt
    {

        Vector2 groesse = new Vector2(1920, 1080);
        Vector2 position = new Vector2(0, 0);

        string label = "UNBENNANT";

        public bool LabelZeigen = true;


        public Vector2 Groesse
        {
            get { return groesse; }
        }
        public Vector2 Position
        {
            get { return position; }
        }

        public string Label
        {
            get { return label; }
        }



        public KartenObjekt(Vector2 Position, Vector2 Groesse, string Label)
        {

            groesse = Groesse;
            position = Position;
            label = Label;


        }



    }
}
