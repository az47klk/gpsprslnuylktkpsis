namespace Final_projesi
{
    partial class PaketDuzenle
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.paketNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paketIsmi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paketFiyati = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.aktifMi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yonlendirme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.disIptalBtn = new System.Windows.Forms.Button();
            this.disKaydetBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.paketIsmiTbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.icKaydetBtn = new System.Windows.Forms.Button();
            this.aktifMiCbox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.paketNoLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fiyatTbox = new System.Windows.Forms.TextBox();
            this.yonlendirmeCbox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gunSayisiTbox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.paketNo,
            this.paketIsmi,
            this.paketFiyati,
            this.gun,
            this.aktifMi,
            this.yonlendirme});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView1.Location = new System.Drawing.Point(13, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(463, 346);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // paketNo
            // 
            this.paketNo.Text = "No";
            // 
            // paketIsmi
            // 
            this.paketIsmi.Text = "Paket Ismı";
            this.paketIsmi.Width = 100;
            // 
            // paketFiyati
            // 
            this.paketFiyati.Text = "Paket Fiyatı";
            this.paketFiyati.Width = 80;
            // 
            // gun
            // 
            this.gun.Text = "Gün Sayısı";
            this.gun.Width = 80;
            // 
            // aktifMi
            // 
            this.aktifMi.Text = "Aktiflik";
            // 
            // yonlendirme
            // 
            this.yonlendirme.Text = "Yönlendir";
            this.yonlendirme.Width = 70;
            // 
            // disIptalBtn
            // 
            this.disIptalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.disIptalBtn.Location = new System.Drawing.Point(726, 335);
            this.disIptalBtn.Name = "disIptalBtn";
            this.disIptalBtn.Size = new System.Drawing.Size(75, 23);
            this.disIptalBtn.TabIndex = 1;
            this.disIptalBtn.Text = "İptal";
            this.disIptalBtn.UseVisualStyleBackColor = true;
            this.disIptalBtn.Click += new System.EventHandler(this.disIptalBtn_Click);
            // 
            // disKaydetBtn
            // 
            this.disKaydetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.disKaydetBtn.Location = new System.Drawing.Point(571, 335);
            this.disKaydetBtn.Name = "disKaydetBtn";
            this.disKaydetBtn.Size = new System.Drawing.Size(149, 23);
            this.disKaydetBtn.TabIndex = 2;
            this.disKaydetBtn.Text = "Tüm Değişiklikleri Kaydet";
            this.disKaydetBtn.UseVisualStyleBackColor = true;
            this.disKaydetBtn.Click += new System.EventHandler(this.disKaydetBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Paket İsmi:";
            // 
            // paketIsmiTbox
            // 
            this.paketIsmiTbox.Location = new System.Drawing.Point(88, 59);
            this.paketIsmiTbox.Name = "paketIsmiTbox";
            this.paketIsmiTbox.Size = new System.Drawing.Size(220, 20);
            this.paketIsmiTbox.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.gunSayisiTbox);
            this.groupBox1.Controls.Add(this.yonlendirmeCbox);
            this.groupBox1.Controls.Add(this.icKaydetBtn);
            this.groupBox1.Controls.Add(this.aktifMiCbox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.paketNoLbl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fiyatTbox);
            this.groupBox1.Controls.Add(this.paketIsmiTbox);
            this.groupBox1.Location = new System.Drawing.Point(483, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 281);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paketi Düzenle";
            // 
            // icKaydetBtn
            // 
            this.icKaydetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.icKaydetBtn.Location = new System.Drawing.Point(233, 252);
            this.icKaydetBtn.Name = "icKaydetBtn";
            this.icKaydetBtn.Size = new System.Drawing.Size(75, 23);
            this.icKaydetBtn.TabIndex = 2;
            this.icKaydetBtn.Text = "Kaydet";
            this.icKaydetBtn.UseVisualStyleBackColor = true;
            this.icKaydetBtn.Click += new System.EventHandler(this.icKaydetBtn_Click);
            // 
            // aktifMiCbox
            // 
            this.aktifMiCbox.AutoSize = true;
            this.aktifMiCbox.Location = new System.Drawing.Point(9, 171);
            this.aktifMiCbox.Name = "aktifMiCbox";
            this.aktifMiCbox.Size = new System.Drawing.Size(60, 17);
            this.aktifMiCbox.TabIndex = 5;
            this.aktifMiCbox.Text = "Aktif mi";
            this.aktifMiCbox.UseVisualStyleBackColor = true;
            this.aktifMiCbox.CheckedChanged += new System.EventHandler(this.aktifMiCbox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Yönlendirme:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(288, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "TL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Paket Fiyatı:";
            // 
            // paketNoLbl
            // 
            this.paketNoLbl.AutoSize = true;
            this.paketNoLbl.Location = new System.Drawing.Point(85, 31);
            this.paketNoLbl.Name = "paketNoLbl";
            this.paketNoLbl.Size = new System.Drawing.Size(10, 13);
            this.paketNoLbl.TabIndex = 3;
            this.paketNoLbl.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Paket No:";
            // 
            // fiyatTbox
            // 
            this.fiyatTbox.Location = new System.Drawing.Point(88, 98);
            this.fiyatTbox.Name = "fiyatTbox";
            this.fiyatTbox.Size = new System.Drawing.Size(194, 20);
            this.fiyatTbox.TabIndex = 4;
            this.fiyatTbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // yonlendirmeCbox
            // 
            this.yonlendirmeCbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yonlendirmeCbox.FormattingEnabled = true;
            this.yonlendirmeCbox.Location = new System.Drawing.Point(88, 203);
            this.yonlendirmeCbox.Name = "yonlendirmeCbox";
            this.yonlendirmeCbox.Size = new System.Drawing.Size(220, 21);
            this.yonlendirmeCbox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gün Sayısı: ";
            // 
            // gunSayisiTbox
            // 
            this.gunSayisiTbox.Location = new System.Drawing.Point(88, 134);
            this.gunSayisiTbox.Name = "gunSayisiTbox";
            this.gunSayisiTbox.Size = new System.Drawing.Size(220, 20);
            this.gunSayisiTbox.TabIndex = 8;
            // 
            // PaketDuzenle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 370);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.disKaydetBtn);
            this.Controls.Add(this.disIptalBtn);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(823, 333);
            this.Name = "PaketDuzenle";
            this.Text = "PaketDuzenle";
            this.Load += new System.EventHandler(this.PaketDuzenle_Load);
            this.Click += new System.EventHandler(this.PaketDuzenle_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button disIptalBtn;
        private System.Windows.Forms.Button disKaydetBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox paketIsmiTbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader paketNo;
        private System.Windows.Forms.ColumnHeader paketIsmi;
        private System.Windows.Forms.ColumnHeader paketFiyati;
        private System.Windows.Forms.ColumnHeader gun;
        private System.Windows.Forms.ColumnHeader aktifMi;
        private System.Windows.Forms.ColumnHeader yonlendirme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fiyatTbox;
        private System.Windows.Forms.CheckBox aktifMiCbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button icKaydetBtn;
        private System.Windows.Forms.Label paketNoLbl;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox yonlendirmeCbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gunSayisiTbox;
    }
}