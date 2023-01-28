using HandelsNavigator.Karten;
using HandelsNavigator.Suchleisten;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace HandelsNavigator
{
    public partial class FrmMain : Form
    {

        private KartenManager? manager;
        private Suchleiste? suchleiste;

        string cn_string = "Server=joigoing.duckdns.org; Database=navigator; Uid=navigator; Pwd = Wilde3377!;";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            // SUCHLEISTE ZENTRIEREN

            TbxSuchleiste.Multiline = true;

            TbxSuchleiste.Width = PnlSuchleiste.Width;
            TbxSuchleiste.Height = PnlSuchleiste.Height / 3;

            TbxSuchleiste.Left = (PnlSuchleiste.Width - TbxSuchleiste.Width)/2;

            // Suchleiste initialisieren

            suchleiste = new Suchleiste(LsbVorschlaege);

            //TextChanged verknüpfen













            //TESTING PLS DONT DELETE

            PcbKarte.SizeMode = PictureBoxSizeMode.Zoom;

            manager = new KartenManager(1920, 1080, 0.1f);
            //manager.DebugKnotenpunkteZeigen = true;

            //manager.DebugLinienHinzufuegen();

            Bitmap Regal = HandelsNavigator.Properties.Resources.Regal;

            KartenObjekt testObjekt = new KartenObjekt(new Vector2(0.5f, 0.2f), new Vector2(0.2f, 0.1f), "Test-Objekt 1", "Regal", new Sprite(Regal));
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.1f, 0.5f), new Vector2(0.4f, 0.1f), "Test-Objekt 2", "Regal", new Sprite(Regal));
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.2f, 0.0f), new Vector2(0.1f, 0.3f), "Test-Objekt 3", "Regal", new Sprite(Regal));
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.6f, 0.3f), new Vector2(0.1f, 0.7f), "Test-Objekt 4", "Regal", new Sprite(Regal));
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.7f, 0.3f), new Vector2(0.1f, 0.1f), "Test-Objekt 5", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.8f, 0.5f), new Vector2(0.1f, 0.1f), "Test-Objekt 6", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.7f, 0.7f), new Vector2(0.1f, 0.1f), "Test-Objekt 7", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.0f, 0.6f), new Vector2(0.1f, 0.1f), "Test-Objekt 8", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.4f, 0.6f), new Vector2(0.1f, 0.1f), "Test-Objekt 9", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);
            testObjekt = new KartenObjekt(new Vector2(0.95f, 0.0f), new Vector2(0.1f, 1f), "Test-Objekt 10", "Regal", new Sprite(Regal)); ;
            manager.ObjektHinzufügen(testObjekt);


            //KartenObjekt testObjekt = new KartenObjekt(new Vector2(0.0f, 0.0f), new Vector2(0.2f, 0.1f), "Test-Objekt 1");
            //manager.ObjektHinzufügen(testObjekt);



            manager.PfadDarstellen(new Vector2(0.8f, 0.95f), new Vector2(0.35f, 0.7f));



            PcbKarte.Image = manager.KarteNeuDarstellen();

            Debug.WriteLine("Joa");
        }


        public float[] PositionLaden(int index)
        {
            MySqlConnection cn = new MySqlConnection(cn_string);
            string sql_Text = "SELECT OrtX, OrtY FROM KartenObjekte WHERE(ID_KartenObjekte =" + index + ")";

            MySqlDataAdapter sql_adapt = new MySqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            //lbxListe.DisplayMember = "Name";
            //lbxListe.ValueMember = "[Preis]";
            //lbxListe.DataSource = tblData;
            cn.Close();


            return new float[] { (float)tblData.Rows[0]["OrtX"], (float)tblData.Rows[0]["OrtY"] };
        }


        private void TbxSuchleiste_TextChanged(object sender, EventArgs e)
        {
            suchleiste.ProduktInListe_Suchen(TbxSuchleiste.Text);
        }

        private void LsbVorschlaege_SelectedIndexChanged(object sender, EventArgs e)
        {
            PositionLaden(LsbVorschlaege.SelectedIndex); //TODO INDEX AUS PRODUKTEN AUSLESEN UND FK IN KartenObjekten Suchen , Positionen abrufen
        }
    }
}
