using System;
using System.Collections.Generic;
using System.Text;

namespace HandelsNavigator.Suchleiste
{
    public class Suchleiste 
    {
        public Suchleiste()
        {
           
        }

        private void Suchleiste_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dbProdukteDataSet.Table". Sie können sie bei Bedarf verschieben oder entfernen.
            this.tableTableAdapter.Fill(this.dbProdukteDataSet.Table);
            Liste_Laden();
        }

        private void Liste_Laden()
        {
            string cn_string = Properties.Settings.Default.AppConnectionString;
            SqlConnection cn = new SqlConnection(cn_string);
            string sql_Text = "SELECT IdProdukt, Name, Preis, Details FROM dbo.[Table]";
            SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            lbxListe.DisplayMember = "Name";
            lbxListe.ValueMember = "[Preis]";
            lbxListe.DataSource = tblData;

        }
        private void ProduktInListe_Suchen()
        {
            string produkt = "%" + tbxSuchleiste.Text + "%";
            string cn_string = Properties.Settings.Default.AppConnectionString;
            SqlConnection cn = new SqlConnection(cn_string);
            string sql_Text = "SELECT IdProdukt, Name, Preis, Details FROM[Table] WHERE(Name like N'" + produkt + "')";

            SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, cn);
            DataTable tblData = new DataTable(sql_Text);
            sql_adapt.Fill(tblData);

            //Anzeigen
            lbxListe.DisplayMember = "Name";
            lbxListe.ValueMember = "[Preis]";
            lbxListe.DataSource = tblData;

        }

        private void tbxSuchleiste_TextChanged(object sender, EventArgs e)
        {
            ProduktInListe_Suchen();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tableTableAdapter.FillBy(this.dbProdukteDataSet.Table);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
