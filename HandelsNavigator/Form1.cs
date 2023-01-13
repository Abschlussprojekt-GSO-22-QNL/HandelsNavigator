using HandelsNavigator.Karten;
using System.Diagnostics;
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

            PCBTest.SizeMode = PictureBoxSizeMode.Zoom;

            manager = new KartenManager(1920, 1080);
            //manager.DebugKnotenpunkteZeigen = true;

            //manager.DebugLinienHinzufuegen();

            KartenObjekt testObjekt = new KartenObjekt(new Vector2(0.5f,0.2f),new Vector2(0.2f,0.1f),"Test-Objekt 1");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.1f), "Test-Objekt 2");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.2f, 0.0f), new Vector2(0.1f, 0.4f), "Test-Objekt 3");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.6f, 0.3f), new Vector2(0.1f, 0.6f), "Test-Objekt 4");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.7f, 0.3f), new Vector2(0.1f, 0.1f), "Test-Objekt 5");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.8f, 0.5f), new Vector2(0.1f, 0.1f), "Test-Objekt 6");
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.7f, 0.7f), new Vector2(0.1f, 0.1f), "Test-Objekt 7");
            manager.ObjektHinzufügen(testObjekt);

            manager.PfadDarstellen(new Vector2(0f,0f), new Vector2(0f, 0f));
            


            PCBTest.Image =  manager.KarteNeuDarstellen();

            Debug.WriteLine("Joa");

        }
    }
}