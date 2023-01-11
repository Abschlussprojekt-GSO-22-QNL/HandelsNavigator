using HandelsNavigator.Karten;
using System.Numerics;

namespace HandelsNavigator
{
    public partial class Form1 : Form
    {
        private KartenManager manager;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //TESTING PLS DONT DELETE

            PCBTest.SizeMode = PictureBoxSizeMode.StretchImage;

            manager = new KartenManager(1920, 1080);

            manager.DebugLinienHinzufuegen();
            manager.DebugKnotenpunkteHinzufuegen();

            KartenObjekt testObjekt = new KartenObjekt(new Vector2(0.5f,0.2f),new Vector2(0.2f,0.1f),"Test-Objekt 1");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.1f), "Test-Objekt 2");
            manager.ObjektHinzufügen(testObjekt);
            


            PCBTest.Image =  manager.KarteNeuDarstellen();


        }
    }
}