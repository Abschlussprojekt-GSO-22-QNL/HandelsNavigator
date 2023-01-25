using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace HandelsNavigator.Karten
{
    internal class KartenManager
    {

        public bool DebugKnotenpunkteZeigen = false;
        float navigationsrasterGröße = 0.1f;

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


        public KartenManager(int AufloesungX, int AufloesungY, float navigationsrasterGröße)
        {

            groesse.X = AufloesungX;
            groesse.Y = AufloesungY;
            if (navigationsrasterGröße > 0.5f)
            {
                navigationsrasterGröße = 0.5f;
            }
            else
            {
                this.navigationsrasterGröße = navigationsrasterGröße;
            }



            this.knotenpunkte = KnotenpunkteGenerieren();

            astar = new Astar(knotenpunkte);

            
        }



        public Bitmap KarteNeuDarstellen()
        {

            Bitmap karte = new Bitmap((int)groesse.X, (int)groesse.Y, PixelFormat.Format32bppPArgb);
            Graphics grafik = Graphics.FromImage(karte);


            Pen stift = new Pen(Color.FromKnownColor(KnownColor.Black), 5);
            Pen stiftDebug = new Pen(Color.FromKnownColor(KnownColor.Red), 5);
            Pen stiftBesetzt = new Pen(Color.FromKnownColor(KnownColor.Pink), 5);
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

            grafik.FillRectangle(Brushes.White,0,0,karte.Width,karte.Height); // - TODO, durch Hintergrund ersetzen

            foreach(KartenObjekt obj in kartenObjekte)
            {

                bool linie = false;
                if (obj.Typ == "NaviLinie")
                    linie = true;


                if(obj.Sprite != null)
                {
                    grafik.DrawImage(obj.Sprite.GrafikAngepasst((int)VonGridNachPixelKonvertieren(obj.Groesse).X, (int)VonGridNachPixelKonvertieren(obj.Groesse).Y,linie), VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y);
                    grafik.DrawString(obj.Label, new Font(SystemFonts.DefaultFont, FontStyle.Bold), pinsel, new PointF(VonGridNachPixelKonvertieren(obj.Position).X + (VonGridNachPixelKonvertieren(obj.Groesse).X / 2), VonGridNachPixelKonvertieren(obj.Position).Y + (VonGridNachPixelKonvertieren(obj.Groesse).Y / 2)));
                }
                else
                {
                    Pen stiftZuBenutzen = stift;
                    pinsel = new SolidBrush(Color.FromKnownColor(KnownColor.Black));

                    if (obj.Label.Contains("Debug"))
                        stiftZuBenutzen = stiftDebug;
                    if (obj.Label.Contains("KNOTENPUNKT BESETZT"))
                    {
                        stiftZuBenutzen = stiftBesetzt;
                        pinsel = new SolidBrush(Color.FromKnownColor(KnownColor.Pink));
                    }


                    if (obj.Typ == "NaviLinie")
                    {
                        grafik.DrawLine(stiftZuBenutzen, VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y, VonGridNachPixelKonvertieren(obj.Groesse).X, VonGridNachPixelKonvertieren(obj.Groesse).Y);
                    }
                    else
                    {
                        grafik.DrawRectangle(stiftZuBenutzen, VonGridNachPixelKonvertieren(obj.Position).X, VonGridNachPixelKonvertieren(obj.Position).Y, VonGridNachPixelKonvertieren(obj.Groesse).X, VonGridNachPixelKonvertieren(obj.Groesse).Y);
                        grafik.DrawString(obj.Label, new Font(SystemFonts.DefaultFont, FontStyle.Bold), pinsel, new PointF(VonGridNachPixelKonvertieren(obj.Position).X + (VonGridNachPixelKonvertieren(obj.Groesse).X / 2), VonGridNachPixelKonvertieren(obj.Position).Y + (VonGridNachPixelKonvertieren(obj.Groesse).Y / 2)));
                    }

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
            for(float i = 0.0f;i < 1.1f;i += 0.1f)
            {
                KartenObjekt objVert = new KartenObjekt(new Vector2(0.0f,i),new Vector2(1.0f, 0.001f), $"Debug {i}v");
                kartenObjekte.Add(objVert);
            }
            //Horizontal
            for (float i = 0.0f; i < 1.1f; i += 0.1f)
            {
                KartenObjekt objHori = new KartenObjekt(new Vector2(i, 0.0f), new Vector2(0.001f, 1.0f), $"Debug {i}h");
                kartenObjekte.Add(objHori);
            }
            
        }

        private List<NavigationsKnotenpunkt> KnotenpunkteGenerieren()
        {

            List<NavigationsKnotenpunkt> knotenpunkte = new List<NavigationsKnotenpunkt>();

            for (float x = 0.05f; x < 0.96f; x += navigationsrasterGröße)
            {
                for (float y = 0.05f; y < 0.96f; y += navigationsrasterGröße)
                {
                    NavigationsKnotenpunkt kontenpunkt = new NavigationsKnotenpunkt(x,y);
                    knotenpunkte.Add(kontenpunkt);
                }
            }


            return knotenpunkte;
        }


        public void PfadDarstellen(Vector2 start, Vector2 ziel)
        {

            List<Vector2> pfad = new List<Vector2>();
            var startKnotenpunkt = NahstenKnotenpunktFinden(start);
            var zielKnotenpunkt = NahstenKnotenpunktFinden(ziel);

            KartenObjekt objDebug = new KartenObjekt(new Vector2(startKnotenpunkt.X,startKnotenpunkt.Y), new Vector2(0.01f, 0.01f), "DEBUG START");
            kartenObjekte.Add(objDebug);
            objDebug = new KartenObjekt(new Vector2(zielKnotenpunkt.X, zielKnotenpunkt.Y), new Vector2(0.01f, 0.01f), "DEBUG ENDE");
            kartenObjekte.Add(objDebug);

            pfad = astar.PfadBerechnen(startKnotenpunkt,zielKnotenpunkt);

            if (pfad != null)
            {
                for (int i = 0; i < pfad.Count - 1; i++)
                {

                    KartenObjekt objLinie = new KartenObjekt(new Vector2(0f, 0f), new Vector2(0f, 0f), "");

                    if (i == pfad.Count - 1)
                    {
                        objLinie = new KartenObjekt(pfad[i], new Vector2(0.05f, 0.05f), $"Debug {i}L", "NaviLinie",new Sprite(HandelsNavigator.Properties.Resources.NavigationsEnde));
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
                //throw new Exception("Route nicht kalkulierbar!");
                MessageBox.Show("Nicht möglich.");
            }

            if(!DebugKnotenpunkteZeigen)
                if (astar.NichtGefundeneNachbarn.Count > 0)
                {
                    for (int i = 0; i < astar.NichtGefundeneNachbarn.Count - 1; i++)
                    {

                        KartenObjekt objFehlerKnoten = new KartenObjekt(new Vector2(0f, 0f), new Vector2(0f, 0f), "");


                       objFehlerKnoten = new KartenObjekt(new Vector2(astar.NichtGefundeneNachbarn[i].X, astar.NichtGefundeneNachbarn[i].Y), new Vector2(0.01f, 0.01f), $"Nicht gefunden {i}FN", "NICHT GEFUNDENER KNOTENPUNKT");


                        kartenObjekte.Add(objFehlerKnoten);
                    }
                }
                else
                {
                    Debug.WriteLine("Alle Nachbarn gefunden :)");
                }
            if (astar.NichtBetretbareNachbarn.Count > 0)
            {
                for (int i = 0; i < astar.NichtBetretbareNachbarn.Count - 1; i++)
                {

                    KartenObjekt objFehlerKnoten = new KartenObjekt(new Vector2(0f, 0f), new Vector2(0f, 0f), "");


                    objFehlerKnoten = new KartenObjekt(new Vector2(astar.NichtBetretbareNachbarn[i].X, astar.NichtBetretbareNachbarn[i].Y), new Vector2(0.01f, 0.01f), $"Nicht gefunden {i}FN", "NICHT BETRETBARER KNOTENPUNKT");


                    kartenObjekte.Add(objFehlerKnoten);
                }
            }
            else
            {
                Debug.WriteLine("Alle Nachbarn gefunden :)");
            }



        }

        private NavigationsKnotenpunkt NahstenKnotenpunktFinden(Vector2 vec)
        {

            var letztBesteEntfernung = float.MaxValue;
            var letztBesterKnotenpunkt = new NavigationsKnotenpunkt();


            foreach(NavigationsKnotenpunkt knoten in knotenpunkte)
            {
                var entf = Vector2.Distance(vec, new Vector2(knoten.X, knoten.Y));

                if (entf < 0)
                    entf = entf * (-1);

                if(entf < letztBesteEntfernung) 
                {
                    letztBesteEntfernung = entf;
                    letztBesterKnotenpunkt = knoten;
                }

            }



            return letztBesterKnotenpunkt;
        }




    }
}
