namespace Final_projesi
{
    partial class yonetimPaneli
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
            System.Windows.Forms.ColumnHeader isim;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.yönetimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paketEkleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.girisEkraniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardımToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uye_iceride_mi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cinsiyet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Paket = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.borc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.son_odeme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uyelik_suresi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uyelik_tarihi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addMember = new System.Windows.Forms.Button();
            this.editMember = new System.Windows.Forms.Button();
            this.deleteMember = new System.Windows.Forms.Button();
            this.memberInfo = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarUyeSayisi = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarMevcutUyeSayisi = new System.Windows.Forms.ToolStripStatusLabel();
            this.mevcutCheckBox = new System.Windows.Forms.CheckBox();
            this.borcluCheckBox = new System.Windows.Forms.CheckBox();
            this.tarihCheckBox = new System.Windows.Forms.CheckBox();
            this.mevcutButton = new System.Windows.Forms.Button();
            this.Yenile = new System.Windows.Forms.Button();
            isim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // isim
            // 
            isim.Text = "İsim Soyisim";
            isim.Width = 100;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yönetimToolStripMenuItem,
            this.yardımToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // yönetimToolStripMenuItem
            // 
            this.yönetimToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.yönetimToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.yönetimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paketEkleToolStripMenuItem,
            this.girisEkraniToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.yönetimToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.yönetimToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.yönetimToolStripMenuItem.Name = "yönetimToolStripMenuItem";
            this.yönetimToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.yönetimToolStripMenuItem.Text = "Yönetim";
            // 
            // paketEkleToolStripMenuItem
            // 
            this.paketEkleToolStripMenuItem.Name = "paketEkleToolStripMenuItem";
            this.paketEkleToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.paketEkleToolStripMenuItem.Text = "Paketleri Düzenle";
            this.paketEkleToolStripMenuItem.Click += new System.EventHandler(this.paketEkleToolStripMenuItem_Click);
            // 
            // girisEkraniToolStripMenuItem
            // 
            this.girisEkraniToolStripMenuItem.Name = "girisEkraniToolStripMenuItem";
            this.girisEkraniToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.girisEkraniToolStripMenuItem.Text = "Giriş Ekranını Göster";
            this.girisEkraniToolStripMenuItem.Click += new System.EventHandler(this.girisEkraniToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Çıkış";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // yardımToolStripMenuItem
            // 
            this.yardımToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hakkındaToolStripMenuItem});
            this.yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
            this.yardımToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.yardımToolStripMenuItem.Text = "Hakkında";
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hakkındaToolStripMenuItem.Text = "Demo Veriyi Yükle";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.uye_iceride_mi,
            this.cinsiyet,
            isim,
            this.Paket,
            this.borc,
            this.son_odeme,
            this.uyelik_suresi,
            this.uyelik_tarihi});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 56);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(738, 359);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // id
            // 
            this.id.Text = "No";
            this.id.Width = 40;
            // 
            // uye_iceride_mi
            // 
            this.uye_iceride_mi.Text = "Giris";
            this.uye_iceride_mi.Width = 59;
            // 
            // cinsiyet
            // 
            this.cinsiyet.Text = "Cinsiyet";
            this.cinsiyet.Width = 55;
            // 
            // Paket
            // 
            this.Paket.Text = "Paket";
            this.Paket.Width = 82;
            // 
            // borc
            // 
            this.borc.Text = "Borç";
            this.borc.Width = 97;
            // 
            // son_odeme
            // 
            this.son_odeme.Text = "Son Ödeme Tarihi";
            this.son_odeme.Width = 120;
            // 
            // uyelik_suresi
            // 
            this.uyelik_suresi.Text = "Üyelik Süresi";
            this.uyelik_suresi.Width = 80;
            // 
            // uyelik_tarihi
            // 
            this.uyelik_tarihi.Text = "Üyelik Tarihi";
            this.uyelik_tarihi.Width = 100;
            // 
            // addMember
            // 
            this.addMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addMember.Location = new System.Drawing.Point(595, 421);
            this.addMember.Name = "addMember";
            this.addMember.Size = new System.Drawing.Size(155, 40);
            this.addMember.TabIndex = 3;
            this.addMember.Text = "Üye Ekle";
            this.addMember.UseVisualStyleBackColor = true;
            this.addMember.Click += new System.EventHandler(this.addMember_Click);
            // 
            // editMember
            // 
            this.editMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editMember.Location = new System.Drawing.Point(434, 421);
            this.editMember.Name = "editMember";
            this.editMember.Size = new System.Drawing.Size(155, 40);
            this.editMember.TabIndex = 4;
            this.editMember.Text = "Üyeyi Düzenle";
            this.editMember.UseVisualStyleBackColor = true;
            this.editMember.Click += new System.EventHandler(this.editMember_Click);
            // 
            // deleteMember
            // 
            this.deleteMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteMember.Location = new System.Drawing.Point(595, 467);
            this.deleteMember.Name = "deleteMember";
            this.deleteMember.Size = new System.Drawing.Size(155, 40);
            this.deleteMember.TabIndex = 4;
            this.deleteMember.Text = "Üyeyi Sil";
            this.deleteMember.UseVisualStyleBackColor = true;
            this.deleteMember.Click += new System.EventHandler(this.deleteMember_Click);
            // 
            // memberInfo
            // 
            this.memberInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.memberInfo.Location = new System.Drawing.Point(434, 467);
            this.memberInfo.Name = "memberInfo";
            this.memberInfo.Size = new System.Drawing.Size(155, 40);
            this.memberInfo.TabIndex = 4;
            this.memberInfo.Text = "Üye Bilgileri";
            this.memberInfo.UseVisualStyleBackColor = true;
            this.memberInfo.Click += new System.EventHandler(this.memberInfo_Click_1);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(61, 30);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(689, 20);
            this.searchTextBox.TabIndex = 5;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Arama: ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarUyeSayisi,
            this.statusBarMevcutUyeSayisi});
            this.statusStrip1.Location = new System.Drawing.Point(0, 510);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(762, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarUyeSayisi
            // 
            this.statusBarUyeSayisi.Name = "statusBarUyeSayisi";
            this.statusBarUyeSayisi.Size = new System.Drawing.Size(71, 17);
            this.statusBarUyeSayisi.Text = "Üye Sayısı: 0";
            // 
            // statusBarMevcutUyeSayisi
            // 
            this.statusBarMevcutUyeSayisi.Name = "statusBarMevcutUyeSayisi";
            this.statusBarMevcutUyeSayisi.Size = new System.Drawing.Size(114, 17);
            this.statusBarMevcutUyeSayisi.Text = "Mevcut Üye Sayısı: 0";
            // 
            // mevcutCheckBox
            // 
            this.mevcutCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mevcutCheckBox.AutoSize = true;
            this.mevcutCheckBox.Location = new System.Drawing.Point(12, 429);
            this.mevcutCheckBox.Name = "mevcutCheckBox";
            this.mevcutCheckBox.Size = new System.Drawing.Size(168, 17);
            this.mevcutCheckBox.TabIndex = 9;
            this.mevcutCheckBox.Text = "Salonda mevcut üyeleri göster";
            this.mevcutCheckBox.UseVisualStyleBackColor = true;
            this.mevcutCheckBox.CheckedChanged += new System.EventHandler(this.mevcutCheckBox_CheckedChanged);
            // 
            // borcluCheckBox
            // 
            this.borcluCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.borcluCheckBox.AutoSize = true;
            this.borcluCheckBox.Location = new System.Drawing.Point(12, 452);
            this.borcluCheckBox.Name = "borcluCheckBox";
            this.borcluCheckBox.Size = new System.Drawing.Size(122, 17);
            this.borcluCheckBox.TabIndex = 9;
            this.borcluCheckBox.Text = "Borcu olanları göster";
            this.borcluCheckBox.UseVisualStyleBackColor = true;
            this.borcluCheckBox.CheckedChanged += new System.EventHandler(this.borcluCheckBox_CheckedChanged);
            // 
            // tarihCheckBox
            // 
            this.tarihCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tarihCheckBox.AutoSize = true;
            this.tarihCheckBox.Location = new System.Drawing.Point(12, 475);
            this.tarihCheckBox.Name = "tarihCheckBox";
            this.tarihCheckBox.Size = new System.Drawing.Size(203, 17);
            this.tarihCheckBox.TabIndex = 9;
            this.tarihCheckBox.Text = "Son ödeme tarihi geçen üyeleri göster";
            this.tarihCheckBox.UseVisualStyleBackColor = true;
            this.tarihCheckBox.CheckedChanged += new System.EventHandler(this.tarihCheckBox_CheckedChanged);
            // 
            // mevcutButton
            // 
            this.mevcutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mevcutButton.Location = new System.Drawing.Point(273, 467);
            this.mevcutButton.Name = "mevcutButton";
            this.mevcutButton.Size = new System.Drawing.Size(155, 40);
            this.mevcutButton.TabIndex = 4;
            this.mevcutButton.Text = "Mevcut Olarak İşaretle";
            this.mevcutButton.UseVisualStyleBackColor = true;
            this.mevcutButton.Click += new System.EventHandler(this.mevcutButton_Click);
            // 
            // Yenile
            // 
            this.Yenile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Yenile.Location = new System.Drawing.Point(273, 421);
            this.Yenile.Name = "Yenile";
            this.Yenile.Size = new System.Drawing.Size(155, 40);
            this.Yenile.TabIndex = 4;
            this.Yenile.Text = "Yenile";
            this.Yenile.UseVisualStyleBackColor = true;
            this.Yenile.Click += new System.EventHandler(this.yenileButton_click);
            // 
            // yonetimPaneli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 532);
            this.Controls.Add(this.tarihCheckBox);
            this.Controls.Add(this.borcluCheckBox);
            this.Controls.Add(this.mevcutCheckBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.memberInfo);
            this.Controls.Add(this.deleteMember);
            this.Controls.Add(this.Yenile);
            this.Controls.Add(this.mevcutButton);
            this.Controls.Add(this.editMember);
            this.Controls.Add(this.addMember);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(755, 300);
            this.Name = "yonetimPaneli";
            this.Text = "Yonetim Paneli";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.yonetimPaneli_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.yonetimPaneli_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem yönetimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader son_odeme;
        private System.Windows.Forms.ColumnHeader uyelik_tarihi;
        private System.Windows.Forms.ColumnHeader Paket;
        private System.Windows.Forms.Button addMember;
        private System.Windows.Forms.Button editMember;
        private System.Windows.Forms.Button deleteMember;
        private System.Windows.Forms.Button memberInfo;
        private System.Windows.Forms.ColumnHeader cinsiyet;
        private System.Windows.Forms.ColumnHeader uyelik_suresi;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem yardımToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hakkındaToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader uye_iceride_mi;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarUyeSayisi;
        private System.Windows.Forms.CheckBox mevcutCheckBox;
        private System.Windows.Forms.CheckBox borcluCheckBox;
        private System.Windows.Forms.CheckBox tarihCheckBox;
        private System.Windows.Forms.ToolStripStatusLabel statusBarMevcutUyeSayisi;
        private System.Windows.Forms.ToolStripMenuItem paketEkleToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader borc;
        private System.Windows.Forms.Button mevcutButton;
        private System.Windows.Forms.ToolStripMenuItem girisEkraniToolStripMenuItem;
        private System.Windows.Forms.Button Yenile;
    }
}

