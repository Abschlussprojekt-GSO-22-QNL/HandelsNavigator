using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandelsNavigator.Karten
{
    internal class Sprite
    {

        private Bitmap grafik = new Bitmap(10,10);

        public Bitmap Grafik
        {
            get { return grafik; }
        }



        public Sprite(Bitmap grafik)
        {
            this.grafik = grafik;
        }


        public Bitmap GrafikAngepasst(int groesseX, int groesseY,bool linie)
        {
            try
            {

                if(linie)
                {
                    if(groesseX > groesseY)
                    {
                        groesseY = 20;
                    }
                    else
                    {
                        groesseX = 20;
                    }
                }



                Bitmap b = new Bitmap(groesseX, groesseY);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(grafik, 0, 0, groesseX, groesseY);

                }
                return b;

            }
            catch(Exception ex)
            {

                Debug.WriteLine($"Konnte Grafik nicht anpassen. | {ex.ToString()}");
                return new Bitmap(10,10);

            }
        }








    }
}
