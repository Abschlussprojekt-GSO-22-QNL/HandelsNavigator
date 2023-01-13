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

                var ueberpruefungsKnotenpunkt = aktiveKnotenpunkte.OrderBy(x => x.KostenDistanz).First();

                if(ueberpruefungsKnotenpunkt.X == ziel.X && ueberpruefungsKnotenpunkt.Y == ziel.Y)
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
                        continue;

                    if (aktiveKnotenpunkte.Any(x => x.X == betretbarerKnotenpunkt.X && x.Y == betretbarerKnotenpunkt.Y))
                    {
                        var bestehenderKnotenpunkt = aktiveKnotenpunkte.First(x => x.X == betretbarerKnotenpunkt.X && x.Y == betretbarerKnotenpunkt.Y);
                        if (bestehenderKnotenpunkt.KostenDistanz > bestehenderKnotenpunkt.KostenDistanz)
                        {
                            aktiveKnotenpunkte.Remove(bestehenderKnotenpunkt);
                            aktiveKnotenpunkte.Add(bestehenderKnotenpunkt);
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


            foreach(NavigationsKnotenpunkt knotenpunkt in moeglicheKnotenpunkte)
            {

                if(
                    knotenpunkte
                        .Where(knoten => knoten.X >= (knotenpunkt.X - 0.001f) && knoten.X <= (knotenpunkt.X + 0.001f))
                        .Where(knoten => knoten.Y >= (knotenpunkt.Y - 0.001f) && knoten.Y <= (knotenpunkt.Y + 0.001f)).ToList().Count > 0
                        )
                            knotenpunkt.Besetzt = knotenpunkte
                            .Where(knoten => knoten.X >= (knotenpunkt.X - 0.001f) && knoten.X <= (knotenpunkt.X + 0.001f))
                            .Where(knoten => knoten.Y >= (knotenpunkt.Y - 0.001f) && knoten.Y <= (knotenpunkt.Y + 0.001f))
                            .First().Besetzt;
            }



                Debug.WriteLine("Kontenpunktstatus übertragen");


            var maxX = 0.95f;
            var maxY = 0.95f;

            return moeglicheKnotenpunkte
                .Where(knotenpunkt => knotenpunkt.X >= 0 && knotenpunkt.X <= maxX)
                .Where(knotenpunkt => knotenpunkt.Y >= 0 && knotenpunkt.Y <= maxY)
                .Where(knotenpunkt => knotenpunkt.Besetzt == false || (knotenpunkt.X == zielKnotenpunkt.X && knotenpunkt.Y == zielKnotenpunkt.Y))
                .ToList();


            return new List<NavigationsKnotenpunkt>();
        }


    }
}
