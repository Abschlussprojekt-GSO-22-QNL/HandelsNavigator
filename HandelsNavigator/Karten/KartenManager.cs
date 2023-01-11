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


        Vector2 groesse = new Vector2(2000,2000);

        List<KartenObjekt> kartenObjekte = new List<KartenObjekt>();




        public Vector2 Groesse
        {
            get { return groesse; }
        }

        public int PixelSchrittProGridsprungHorizontal
        {
            get { return (int)groesse.X / 100; }
        }
        public int PixelSchrittProGridsprungVertikal
        {
            get { return (int)groesse.Y / 100; }
        }


        public KartenManager(int AufloesungX, int AufloesungY)
        {

            groesse.X = AufloesungX;
            groesse.Y = AufloesungY;

        }



        public Bitmap KarteNeuDarstellen()
        {

            Bitmap karte = new Bitmap((int)groesse.X, (int)groesse.Y, PixelFormat.Format32bppPArgb);
            Graphics grafik = Graphics.FromImage(karte);


            Pen stift = new Pen(Color.FromKnownColor(KnownColor.Black), 2);
            Pen stiftDebug = new Pen(Color.FromKnownColor(KnownColor.Red), 2);
            Brush pinsel = new SolidBrush(Color.FromKnownColor(KnownColor.Black));

            grafik.FillRectangle(Brushes.White,0,0,karte.Width,karte.Height);

            foreach(KartenObjekt obj in kartenObjekte)
            {
                Pen stiftZuBenutzen = stift;

                if (obj.Label.Contains("Debug"))
                    stiftZuBenutzen = stiftDebug;

                grafik.DrawRectangle(stiftZuBenutzen, VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y, VonGridNachPixelKonvertieren(obj.Groesse).X, VonGridNachPixelKonvertieren(obj.Groesse).Y);
                grafik.DrawString(obj.Label, new Font(SystemFonts.DefaultFont, FontStyle.Bold), pinsel, new PointF(VonGridNachPixelKonvertieren(obj.Position).X + (VonGridNachPixelKonvertieren(obj.Groesse).X / 2), VonGridNachPixelKonvertieren(obj.Position).Y + (VonGridNachPixelKonvertieren(obj.Groesse).Y / 2)));
            }

            return karte;

        }

        public Vector2 VonGridNachPixelKonvertieren(Vector2 gridPunkt)
        {

            if(gridPunkt.X < 0.0f | gridPunkt.Y < 0.0f)
                gridPunkt = new Vector2(0.0f,0.0f);

            Vector2 angepeilterPixel = new Vector2(0, 0);

            
            int xAchse = (int)Math.Round((PixelSchrittProGridsprungHorizontal * (gridPunkt.X*100)),0);
            int yAchse = (int)Math.Round((PixelSchrittProGridsprungVertikal * (gridPunkt.Y * 100)),0);

            angepeilterPixel.X = xAchse;
            angepeilterPixel.Y = yAchse;

            return angepeilterPixel;
        }

        public void ObjektHinzufügen(KartenObjekt obj)
        {
            kartenObjekte.Add(obj);
        }

        public void DebugLinienHinzufuegen()
        {
            //Vertikal
            for(float i = 0.0f;i < 1.0f;i += 0.1f)
            {
                KartenObjekt objVert = new KartenObjekt(new Vector2(0.0f,i),new Vector2(1.0f, 0.001f), $"Debug {i}v");
                kartenObjekte.Add(objVert);
            }
            //Horizontal
            for (float i = 0.0f; i < 1.0f; i += 0.1f)
            {
                KartenObjekt objHori = new KartenObjekt(new Vector2(i, 0.0f), new Vector2(0.001f, 1.0f), $"Debug {i}h");
                kartenObjekte.Add(objHori);
            }
            
        }

        public void DebugKnotenpunkteHinzufuegen()
        {
            //Vertikal
            for (float i = 0.05f; i < 0.95f; i += 0.1f)
            {
                for (float z = 0.05f; z < 0.95f; z += 0.1f)
                {
                    KartenObjekt kontenpunkt = new KartenObjekt(new Vector2(i, z), new Vector2(0.01f, 0.01f), $"K({i},{z})");
                    kartenObjekte.Add(kontenpunkt);
                }
            }
            //Horizontal
            

        }




    }
}
