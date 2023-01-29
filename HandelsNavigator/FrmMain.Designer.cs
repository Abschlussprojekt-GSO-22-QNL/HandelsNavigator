namespace HandelsNavigator
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlSuchleiste = new System.Windows.Forms.Panel();
            this.LsbVorschlaege = new System.Windows.Forms.ListBox();
            this.TbxSuchleiste = new System.Windows.Forms.TextBox();
            this.PnlKarte = new System.Windows.Forms.Panel();
            this.PcbKarte = new System.Windows.Forms.PictureBox();
            this.PnlInfos = new System.Windows.Forms.Panel();
            this.RTbxProduktdetails = new System.Windows.Forms.RichTextBox();
            this.PnlSuchleiste.SuspendLayout();
            this.PnlKarte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PcbKarte)).BeginInit();
            this.PnlInfos.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlSuchleiste
            // 
            this.PnlSuchleiste.BackColor = System.Drawing.SystemColors.Control;
            this.PnlSuchleiste.Controls.Add(this.LsbVorschlaege);
            this.PnlSuchleiste.Controls.Add(this.TbxSuchleiste);
            this.PnlSuchleiste.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlSuchleiste.Location = new System.Drawing.Point(0, 0);
            this.PnlSuchleiste.Name = "PnlSuchleiste";
            this.PnlSuchleiste.Size = new System.Drawing.Size(800, 87);
            this.PnlSuchleiste.TabIndex = 0;
            // 
            // LsbVorschlaege
            // 
            this.LsbVorschlaege.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsbVorschlaege.FormattingEnabled = true;
            this.LsbVorschlaege.ItemHeight = 15;
            this.LsbVorschlaege.Location = new System.Drawing.Point(0, 16);
            this.LsbVorschlaege.Name = "LsbVorschlaege";
            this.LsbVorschlaege.Size = new System.Drawing.Size(800, 71);
            this.LsbVorschlaege.TabIndex = 1;
            this.LsbVorschlaege.SelectedIndexChanged += new System.EventHandler(this.LsbVorschlaege_SelectedIndexChanged);
            // 
            // TbxSuchleiste
            // 
            this.TbxSuchleiste.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TbxSuchleiste.Dock = System.Windows.Forms.DockStyle.Top;
            this.TbxSuchleiste.Location = new System.Drawing.Point(0, 0);
            this.TbxSuchleiste.Name = "TbxSuchleiste";
            this.TbxSuchleiste.Size = new System.Drawing.Size(800, 16);
            this.TbxSuchleiste.TabIndex = 0;
            this.TbxSuchleiste.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TbxSuchleiste.TextChanged += new System.EventHandler(this.TbxSuchleiste_TextChanged);
            // 
            // PnlKarte
            // 
            this.PnlKarte.BackColor = System.Drawing.SystemColors.Control;
            this.PnlKarte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlKarte.Controls.Add(this.PcbKarte);
            this.PnlKarte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlKarte.Location = new System.Drawing.Point(0, 87);
            this.PnlKarte.Name = "PnlKarte";
            this.PnlKarte.Size = new System.Drawing.Size(500, 363);
            this.PnlKarte.TabIndex = 2;
            // 
            // PcbKarte
            // 
            this.PcbKarte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PcbKarte.Location = new System.Drawing.Point(0, 0);
            this.PcbKarte.Name = "PcbKarte";
            this.PcbKarte.Size = new System.Drawing.Size(498, 361);
            this.PcbKarte.TabIndex = 0;
            this.PcbKarte.TabStop = false;
            // 
            // PnlInfos
            // 
            this.PnlInfos.BackColor = System.Drawing.SystemColors.Control;
            this.PnlInfos.Controls.Add(this.RTbxProduktdetails);
            this.PnlInfos.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlInfos.Location = new System.Drawing.Point(500, 87);
            this.PnlInfos.MinimumSize = new System.Drawing.Size(300, 0);
            this.PnlInfos.Name = "PnlInfos";
            this.PnlInfos.Size = new System.Drawing.Size(300, 363);
            this.PnlInfos.TabIndex = 1;
            // 
            // RTbxProduktdetails
            // 
            this.RTbxProduktdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTbxProduktdetails.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RTbxProduktdetails.Location = new System.Drawing.Point(0, 0);
            this.RTbxProduktdetails.Name = "RTbxProduktdetails";
            this.RTbxProduktdetails.ReadOnly = true;
            this.RTbxProduktdetails.Size = new System.Drawing.Size(300, 363);
            this.RTbxProduktdetails.TabIndex = 0;
            this.RTbxProduktdetails.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PnlKarte);
            this.Controls.Add(this.PnlInfos);
            this.Controls.Add(this.PnlSuchleiste);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.PnlSuchleiste.ResumeLayout(false);
            this.PnlSuchleiste.PerformLayout();
            this.PnlKarte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PcbKarte)).EndInit();
            this.PnlInfos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel PnlSuchleiste;
        private Panel PnlKarte;
        private PictureBox PcbKarte;
        private Panel PnlInfos;
        private TextBox TbxSuchleiste;
        private ListBox LsbVorschlaege;
        private RichTextBox RTbxProduktdetails;
    }
}