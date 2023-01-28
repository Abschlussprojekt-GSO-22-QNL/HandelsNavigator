using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HandelsNavigator.Suchleiste
{
    public class Suchleiste 
    {

        ListBox listbox = null;
        string cn_string = "";


        public Suchleiste(ListBox listbox)
        {
            this.listbox = listbox;           
        }

        private void Suchleiste_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dbProdukteDataSet.Table". Sie können sie bei Bedarf verschieben oder entfernen.
            //this.tableTableAdapter.Fill(this.dbProdukteDataSet.Table); //?????
            Liste_Laden();
        }

        private void Liste_Laden()
        {
            cn_string = ""; // TODO: DATENBANK VERKNÜPFEN
            SqlConnection cn = new SqlConnection(cn_string);
            string sql_Text = "SELECT IdProdukt, Name, Preis, Details FROM dbo.[Table]";
            SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            listbox.DisplayMember = "Name";
            listbox.ValueMember = "[Preis]";
            listbox.DataSource = tblData;

            cn.Close();

        }
        private DataTable ProduktInListe_Suchen(string text)
        {
            string produkt = "%" + text + "%";
            cn_string = ""; // TODO: DATENBANK VERKNÜPFEN
            SqlConnection cn = new SqlConnection(cn_string);
            string sql_Text = "SELECT IdProdukt, Name, Preis, Details FROM[Table] WHERE(Name like N'" + produkt + "')";

            SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            //lbxListe.DisplayMember = "Name";
            //lbxListe.ValueMember = "[Preis]";
            //lbxListe.DataSource = tblData;
            cn.Close();

            return tblData;

        }

        //private void tbxSuchleiste_TextChanged(object sender, EventArgs e)
        //{
        //    ProduktInListe_Suchen();
        //}

        //private void fillByToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.tableTableAdapter.FillBy(this.dbProdukteDataSet.Table);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
