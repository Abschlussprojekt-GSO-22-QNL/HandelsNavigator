using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HandelsNavigator.Suchleisten
{
    public class Suchleiste 
    {

        ListBox listbox = null;
        DataTable vorschlaege = null;
        string cn_string = "Server=joigoing.duckdns.org; Database=navigator; Uid=navigator; Pwd = Wilde3377!;";


        public Suchleiste(ListBox listbox)
        {
            this.listbox = listbox;
            
            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            csb.Server = "joingoin.duckdns.org";
            csb.Port = 3306;
            csb.UserID = "navigator";
            csb.Password = "Wilde3377!";
            csb.Database = "Navigator";

            cn_string = csb.ToString();
        }

        //private void Liste_Laden()
        //{
        //    cn_string = ""; // TODO: DATENBANK VERKNÜPFEN
        //    SqlConnection cn = new SqlConnection(cn_string);
        //    string sql_Text = "SELECT IdProdukt, Name, Preis, Details FROM dbo.[Table]";
        //    SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, cn);
        //    DataTable tblData = new DataTable(sql_Text);
        //    sql_adapt.Fill(tblData);

        //    //Anzeigen
        //    listbox.DisplayMember = "Name";
        //    listbox.ValueMember = "[Preis]";
        //    listbox.DataSource = tblData;

        //    cn.Close();

        //}
        public void ProduktInListe_Suchen(string text)
        {

            string produkt = "%" + text + "%";
            MySqlConnection cn = new MySqlConnection(cn_string);
            string sql_Text = "SELECT ID_Produkt, Name, Preis, ID_KartenObjekte FROM Produkt WHERE(Name like N'" + produkt + "')";

            MySqlDataAdapter sql_adapt = new MySqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            //lbxListe.DisplayMember = "Name";
            //lbxListe.ValueMember = "[Preis]";
            //lbxListe.DataSource = tblData;
            cn.Close();

            vorschlaege = tblData;

            ListBoxFuellen();

        }

        public void ListBoxFuellen()
        {
            listbox.DisplayMember = "Name";
            listbox.ValueMember = "[Preis]";
            listbox.DataSource = vorschlaege;

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
