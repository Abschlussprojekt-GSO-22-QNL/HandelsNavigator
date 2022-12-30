using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace HandelsNavigator.Karten
{
    internal class KartenManager
    {

        PictureBox renderZiel = null;


        Vector2 groesse = new Vector2(1920,1080);

        List<KartenObjekt> kartenObjekte = new List<KartenObjekt>();




        public Vector2 Groesse
        {
            get { return groesse; }
        }


        public KartenManager(int AufloesungX, int AufloesungY, ref PictureBox RenderZiel)
        {

            groesse.X = AufloesungX;
            groesse.Y = AufloesungY;
            renderZiel = RenderZiel;

        }



        public void KarteNeuDarstellen()
        {

            Bitmap karte = new Bitmap((int)groesse.X, (int)groesse.Y, PixelFormat.Format32bppPArgb);
            Graphics grafik = Graphics.FromImage(karte);


            Pen stift = new Pen(Color.FromKnownColor(KnownColor.Black), 2);
            Brush pinsel = new SolidBrush(Color.FromKnownColor(KnownColor.Black));


            foreach(KartenObjekt obj in kartenObjekte)
            {
                grafik.DrawRectangle(stift, obj.Position.X,obj.Position.Y,obj.Groesse.X,obj.Groesse.Y);
                grafik.DrawString(obj.Label,new Font(SystemFonts.DefaultFont,FontStyle.Bold),pinsel,new PointF(obj.Position.X+obj.Groesse.X/2,obj.Position.Y+obj.Groesse.Y/2));
            }

            renderZiel.Image = karte;

        }




    }
}
