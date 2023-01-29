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

        public Vector2 StationsStandort = new Vector2(0.1f, 0.1f);

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

            //KartenManager vorbereiten

            PcbKarte.SizeMode = PictureBoxSizeMode.Zoom;

            manager = new KartenManager(1920, 1080, 0.1f);
            //manager.DebugKnotenpunkteZeigen = true;
            //manager.DebugLinienHinzufuegen();

            //Datenbank Connection vorbereiten

            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            csb.Server = "joingoin.duckdns.org";
            csb.Port = 3306;
            csb.UserID = "navigator";
            csb.Password = "Wilde3377!";
            csb.Database = "Navigator";

            cn_string = csb.ToString();

            RegaleHinzufuegen();
            manager.KarteNeuDarstellen();

        }


        public Produkt ProduktLaden(string produkt)
        {
            MySqlConnection cn = new MySqlConnection(cn_string);
            string sql_Text = "SELECT name,Kartenname,preis, OrtX, OrtY FROM ProduktOrten WHERE(Name like N'" + produkt + "')";

            MySqlDataAdapter sql_adapt = new MySqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            //lbxListe.DisplayMember = "Name";
            //lbxListe.ValueMember = "[Preis]";
            //lbxListe.DataSource = tblData;
            cn.Close();

            if (tblData.Rows.Count > 0)
            {
                return new Produkt((string)tblData.Rows[0]["name"], (string)tblData.Rows[0]["Kartenname"], Convert.ToDouble(tblData.Rows[0]["preis"]), new Vector2(Convert.ToSingle(tblData.Rows[0]["OrtX"]), Convert.ToSingle(tblData.Rows[0]["OrtY"])));
            }
            else
            {
                return new Produkt("NICHT GEFUNDEN", "Fehler",0.0, new Vector2(0f, 0f));
            }
        }

        public void RegaleHinzufuegen()
        {
            Bitmap Regal = HandelsNavigator.Properties.Resources.Regal;

            MySqlConnection cn = new MySqlConnection(cn_string);
            string sql_Text = "SELECT * FROM KartenObjekte";

            MySqlDataAdapter sql_adapt = new MySqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            //lbxListe.DisplayMember = "Name";
            //lbxListe.ValueMember = "[Preis]";
            //lbxListe.DataSource = tblData;
            cn.Close();

            if (tblData.Rows.Count > 0)
            {
                foreach(DataRow r in tblData.Rows)
                {
                    KartenObjekt ObjektZumHinzufuegen = new KartenObjekt(new Vector2(Convert.ToSingle(r["OrtX"]), Convert.ToSingle(r["OrtY"])), new Vector2(Convert.ToSingle(r["GroeßeX"]), Convert.ToSingle(r["GroeßeY"])), r["KartenName"].ToString(), "Regal", new Sprite(Regal));
                    manager.ObjektHinzufügen(ObjektZumHinzufuegen);
                }
            }
        }


        private void TbxSuchleiste_TextChanged(object sender, EventArgs e)
        {
            suchleiste.ProduktInListe_Suchen(TbxSuchleiste.Text);
        }

        private void LsbVorschlaege_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produkt produkt;

            if (LsbVorschlaege.SelectedValue != null)
            {
                produkt = ProduktLaden(((DataRowView)LsbVorschlaege.SelectedItem)[1].ToString()); //TODO INDEX AUS PRODUKTEN AUSLESEN UND FK IN KartenObjekten Suchen , Positionen abrufen
                manager.ObjekteAlleEntfernen();
                RegaleHinzufuegen();
                manager.PfadDarstellen(StationsStandort, new Vector2(produkt.Position.X, produkt.Position.Y));
                PcbKarte.Image = manager.KarteNeuDarstellen();

                RTbxProduktdetails.Rtf = $"{{\\rtf1\\pc \\qc \\b {produkt.Name} \\b0 \\par {produkt.Typ} \\par \\i Preis: \\i0 {produkt.Preis}€ \\par \\par \\par Bitte achten Sie links auf die Karten-Darstellung um Ihr gewünschtes Produkt zu finden.}}";

            }


        }
    }
}
