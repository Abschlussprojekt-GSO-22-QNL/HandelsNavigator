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

        public bool DebugKnotenpunkteZeigen = false;

        Vector2 groesse = new Vector2(2000,2000);

        List<KartenObjekt> kartenObjekte = new List<KartenObjekt>();
        List<NavigationsKnotenpunkt> knotenpunkte = new List<NavigationsKnotenpunkt>();

        Astar astar = new Astar(new List<NavigationsKnotenpunkt>());




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
            this.knotenpunkte = KnotenpunkteGenerieren();

            astar = new Astar(knotenpunkte);

        }



        public Bitmap KarteNeuDarstellen()
        {

            Bitmap karte = new Bitmap((int)groesse.X, (int)groesse.Y, PixelFormat.Format32bppPArgb);
            Graphics grafik = Graphics.FromImage(karte);


            Pen stift = new Pen(Color.FromKnownColor(KnownColor.Black), 2);
            Pen stiftDebug = new Pen(Color.FromKnownColor(KnownColor.Red), 2);
            Brush pinsel = new SolidBrush(Color.FromKnownColor(KnownColor.Black));

            if (DebugKnotenpunkteZeigen)
            {
                foreach (NavigationsKnotenpunkt knoten in knotenpunkte)
                {
                    KartenObjekt obj = new KartenObjekt(new Vector2(knoten.X, knoten.Y), new Vector2(0.01f, 0.01f), "KNOTENPUNKT");
                    if(knoten.Besetzt)
                        obj = new KartenObjekt(new Vector2(knoten.X, knoten.Y), new Vector2(0.01f, 0.01f), "KNOTENPUNKT BESETZT");
                    kartenObjekte.Add(obj);
                }
            }

            grafik.FillRectangle(Brushes.White,0,0,karte.Width,karte.Height);

            foreach(KartenObjekt obj in kartenObjekte)
            {
                Pen stiftZuBenutzen = stift;

                if (obj.Label.Contains("Debug"))
                    stiftZuBenutzen = stiftDebug;

                if(obj.Typ == "NaviLinie")
                {
                    grafik.DrawLine(stiftZuBenutzen, VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y, VonGridNachPixelKonvertieren(obj.Groesse).X, VonGridNachPixelKonvertieren(obj.Groesse).Y);
                }
                else 
                {
                    grafik.DrawRectangle(stiftZuBenutzen, VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y, VonGridNachPixelKonvertieren(obj.Groesse).X, VonGridNachPixelKonvertieren(obj.Groesse).Y);
                    grafik.DrawString(obj.Label, new Font(SystemFonts.DefaultFont, FontStyle.Bold), pinsel, new PointF(VonGridNachPixelKonvertieren(obj.Position).X + (VonGridNachPixelKonvertieren(obj.Groesse).X / 2), VonGridNachPixelKonvertieren(obj.Position).Y + (VonGridNachPixelKonvertieren(obj.Groesse).Y / 2)));
                }                
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

            var zuSperrendeKnotenpunkte = knotenpunkte.Where(x => x.X >= obj.Position.X && x.X <= obj.Position.X + obj.Groesse.X && x.Y >= obj.Position.Y && x.Y <= obj.Position.Y + obj.Groesse.Y).ToList();

            foreach(NavigationsKnotenpunkt knoten in zuSperrendeKnotenpunkte)
            {
                knoten.Besetzt = true;
            }
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

        private List<NavigationsKnotenpunkt> KnotenpunkteGenerieren()
        {

            List<NavigationsKnotenpunkt> knotenpunkte = new List<NavigationsKnotenpunkt>();

            for (float x = 0.05f; x < 0.95f; x += 0.1f)
            {
                for (float y = 0.05f; y < 0.95f; y += 0.1f)
                {
                    NavigationsKnotenpunkt kontenpunkt = new NavigationsKnotenpunkt(x,y);
                    knotenpunkte.Add(kontenpunkt);
                }
            }


            return knotenpunkte;
        }


        public void PfadDarstellen(Vector2 ziel, Vector2 start)
        {

            List<Vector2> pfad = new List<Vector2>();

            var startKnotenpunkt = knotenpunkte.First();
            var zielKnotenpunkt = knotenpunkte.Last();


             pfad = astar.PfadBerechnen(startKnotenpunkt,zielKnotenpunkt);

            if (pfad != null)
            {
                for (int i = 0; i < pfad.Count - 1; i++)
                {

                    KartenObjekt objLinie = new KartenObjekt(new Vector2(0f, 0f), new Vector2(0f, 0f), "");

                    if (i == pfad.Count - 1)
                    {
                        objLinie = new KartenObjekt(pfad[i], new Vector2(0.05f, 0.05f), $"Debug {i}L", "NaviLinie");
                    }
                    else
                    {
                        objLinie = new KartenObjekt(pfad[i], pfad[i + 1], $"Debug {i}L", "NaviLinie");
                    }
                    kartenObjekte.Add(objLinie);
                }
            }
            else
            {
                MessageBox.Show("Es konnte keine mögliche Route gefunden werden!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }




    }
}
