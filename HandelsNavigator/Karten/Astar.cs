using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Karten
{
    internal class Astar
    {

        private List<NavigationsKnotenpunkt> navigationsKnotenpunkte;
        static private List<NavigationsKnotenpunkt> nichtGefundeneNachbarn = new List<NavigationsKnotenpunkt>();
        static private List<NavigationsKnotenpunkt> nichtBetretbar = new List<NavigationsKnotenpunkt>();

        public List<NavigationsKnotenpunkt> NichtGefundeneNachbarn
        {
            get { return nichtGefundeneNachbarn; }
        }

        public List<NavigationsKnotenpunkt> NichtBetretbareNachbarn
        {
            get { return nichtBetretbar; }
        }


        public Astar(List<NavigationsKnotenpunkt> navigationsKnotenpunke) 
        { 
            this.navigationsKnotenpunkte = navigationsKnotenpunke;
        }

        public List<Vector2> PfadBerechnen(NavigationsKnotenpunkt start, NavigationsKnotenpunkt ziel)
        {

            var pfad = new List<Vector2>();

            start.DistanzSetzen(ziel.X,ziel.Y);


            var aktiveKnotenpunkte = new List<NavigationsKnotenpunkt>();
            aktiveKnotenpunkte.Add(start);
            var besuchteKnotenpunkte = new List<NavigationsKnotenpunkt>();

            while(aktiveKnotenpunkte.Any())
            {

                var ueberpruefungsKnotenpunkt = aktiveKnotenpunkte.OrderByDescending(x => x.KostenDistanz).Last();

                //if(ueberpruefungsKnotenpunkt.X == ziel.X && ueberpruefungsKnotenpunkt.Y == ziel.Y)
                if(ueberpruefungsKnotenpunkt.X >= ziel.X - 0.02f && ueberpruefungsKnotenpunkt.X <= ziel.X + 0.02f && ueberpruefungsKnotenpunkt.Y >= ziel.Y - 0.02f && ueberpruefungsKnotenpunkt.Y <= ziel.Y + 0.02f)
                {

                    var knotenpunkt = ueberpruefungsKnotenpunkt;

                    while(true)
                    {
                        pfad.Add(new Vector2(knotenpunkt.X,knotenpunkt.Y));


                        knotenpunkt = knotenpunkt.UrsprungsKnotenpunkt;

                        if(knotenpunkt == null)
                        {
                            Debug.WriteLine("Pfad vervollständigt!");
                            return pfad;
                        }
                    }

                }

                besuchteKnotenpunkte.Add(ueberpruefungsKnotenpunkt);
                aktiveKnotenpunkte.Remove(ueberpruefungsKnotenpunkt);

                var betretbareKnotenpunkte = BetretbareKnotenpunkte(navigationsKnotenpunkte,ueberpruefungsKnotenpunkt,ziel);

                foreach(var betretbarerKnotenpunkt in betretbareKnotenpunkte)
                {
                    if (besuchteKnotenpunkte.Any(x => x.X == betretbarerKnotenpunkt.X && x.Y == betretbarerKnotenpunkt.Y))
                    {
                        continue;
                    }

                    if (aktiveKnotenpunkte.Any(x => x.X == betretbarerKnotenpunkt.X && x.Y == betretbarerKnotenpunkt.Y))
                    {
                        var bestehenderKnotenpunkt = aktiveKnotenpunkte.First(x => x.X == betretbarerKnotenpunkt.X && x.Y == betretbarerKnotenpunkt.Y);
                        if (bestehenderKnotenpunkt.KostenDistanz > betretbarerKnotenpunkt.KostenDistanz)
                        {
                            aktiveKnotenpunkte.Remove(bestehenderKnotenpunkt);
                            aktiveKnotenpunkte.Add(betretbarerKnotenpunkt);
                        }
                    }
                    else
                    {
                        aktiveKnotenpunkte.Add(betretbarerKnotenpunkt);
                    }
                }

            }
            
            Console.WriteLine("No Path Found!");
            return null;

        }

    

        private static List<NavigationsKnotenpunkt> BetretbareKnotenpunkte(List<NavigationsKnotenpunkt> knotenpunkte, NavigationsKnotenpunkt aktuellerKnotenpunkt, NavigationsKnotenpunkt zielKnotenpunkt)
        {

            var moeglicheKnotenpunkte = new List<NavigationsKnotenpunkt>
            {
                new NavigationsKnotenpunkt {X = aktuellerKnotenpunkt.X, Y = aktuellerKnotenpunkt.Y - 0.10f, UrsprungsKnotenpunkt = aktuellerKnotenpunkt, Kosten = aktuellerKnotenpunkt.Kosten + 0.10f },
                new NavigationsKnotenpunkt {X = aktuellerKnotenpunkt.X, Y = aktuellerKnotenpunkt.Y + 0.10f, UrsprungsKnotenpunkt = aktuellerKnotenpunkt, Kosten = aktuellerKnotenpunkt.Kosten + 0.10f },
                new NavigationsKnotenpunkt {X = aktuellerKnotenpunkt.X - 0.10f, Y = aktuellerKnotenpunkt.Y, UrsprungsKnotenpunkt = aktuellerKnotenpunkt, Kosten = aktuellerKnotenpunkt.Kosten + 0.10f },
                new NavigationsKnotenpunkt {X = aktuellerKnotenpunkt.X + 0.10f, Y = aktuellerKnotenpunkt.Y, UrsprungsKnotenpunkt = aktuellerKnotenpunkt, Kosten = aktuellerKnotenpunkt.Kosten + 0.10f }
            };


            moeglicheKnotenpunkte.ForEach(knotenpunkt => knotenpunkt.DistanzSetzen(zielKnotenpunkt.X, zielKnotenpunkt.Y));


            //foreach(NavigationsKnotenpunkt knotenpunkt in moeglicheKnotenpunkte)
            for(int i = moeglicheKnotenpunkte.Count -1; i > -1; i--)
            {

                if (moeglicheKnotenpunkte[i].X < 0f || moeglicheKnotenpunkte[i].Y < 0f)
                {
                    moeglicheKnotenpunkte.Remove(moeglicheKnotenpunkte[i]);
                }
                if(knotenpunkte.Count > 0)
                    if (
                        knotenpunkte
                            .Where(knoten => knoten.X >= (moeglicheKnotenpunkte[i].X - 0.002f) && knoten.X <= (moeglicheKnotenpunkte[i].X + 0.002f))
                            .Where(knoten => knoten.Y >= (moeglicheKnotenpunkte[i].Y - 0.002f) && knoten.Y <= (moeglicheKnotenpunkte[i].Y + 0.002f)).ToList().Count() > 0
                            )
                    {
                        moeglicheKnotenpunkte[i].Besetzt = knotenpunkte
                        .Where(knoten => knoten.X >= (moeglicheKnotenpunkte[i].X - 0.001f) && knoten.X <= (moeglicheKnotenpunkte[i].X + 0.001f))
                        .Where(knoten => knoten.Y >= (moeglicheKnotenpunkte[i].Y - 0.001f) && knoten.Y <= (moeglicheKnotenpunkte[i].Y + 0.001f))
                        .First().Besetzt;
                        Debug.WriteLine("Kontenpunktstatus übertragen");                    
                    }
                    else
                    {
                        Debug.WriteLine("Nachbar nicht gefunden");
                        nichtGefundeneNachbarn.Add(moeglicheKnotenpunkte[i]);
                        moeglicheKnotenpunkte.Remove(moeglicheKnotenpunkte[i]);
                    }

            }



                


            var maxX = 0.96f;
            var maxY = 0.96f;

            return moeglicheKnotenpunkte
                .Where(knotenpunkt => knotenpunkt.X >= 0 && knotenpunkt.X <= maxX)
                .Where(knotenpunkt => knotenpunkt.Y >= 0 && knotenpunkt.Y <= maxY)
                .Where(knotenpunkt => knotenpunkt.Besetzt == false || (knotenpunkt.X == zielKnotenpunkt.X && knotenpunkt.Y == zielKnotenpunkt.Y))
                .ToList();
        }


    }
}
