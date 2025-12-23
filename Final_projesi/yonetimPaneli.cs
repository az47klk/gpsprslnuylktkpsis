using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics.Eventing.Reader;


namespace Final_projesi{
    public partial class yonetimPaneli : Form{

        public yonetimPaneli(){
            InitializeComponent();
            Program.odemeKontrolu();
        }

        private void BosluklariKaldir(ToolStripItem item){ // bu fonksion menustrip elemanlarinin 
            if (item is ToolStripMenuItem menuItem){       // daha hos gorunmesi icin yazildi.
                if (menuItem.DropDown is ToolStripDropDownMenu dropDownMenu){
                    dropDownMenu.ShowImageMargin = false;
                    dropDownMenu.ShowCheckMargin = false;
                }
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    BosluklariKaldir(subItem);
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e) { 
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                BosluklariKaldir(item);
            }
            refreshListView();
            memberInfo.Enabled = false; 
            editMember.Enabled = false;
            tarihCheckBox.Enabled = false;
            deleteMember.Enabled = false;
            mevcutButton.Enabled = false;

        }

        public List<Uye> UyeAra(string arananMetin)
        {
            string aranan = arananMetin.ToLower();

            List<Uye> bulunanlar = Program.uyeler.Where(u =>
                u.uyeNumarasi.ToString().Contains(aranan) ||
                (u.isim != null && u.isim.ToLower().Contains(aranan)) ||
                (u.adres != null && u.adres.ToLower().Contains(aranan)) ||
                (u.telefonNumarasi != null && u.telefonNumarasi.Contains(aranan))
            ).ToList();

            return bulunanlar;
        }

        public void refreshListView(List<Uye> gosterilecekListe = null)
        {
            List<Uye> kaynakListe = (gosterilecekListe == null) ? Program.uyeler : gosterilecekListe;

            listView1.Items.Clear();
            foreach (Uye u in kaynakListe)
            {
                if (mevcutCheckBox.Checked && u.uyeIcerideMi == false) {
                    continue;
                }

                TimeSpan fark = DateTime.Now - u.uyelikTarihi;
                string farkStr = Convert.ToInt32(fark.TotalDays).ToString() + " gün";

                double borc = 0;
                bool sonOdemeBool = false;
                DateTime tmpDate = new DateTime();
                string sonOdemeTemp = "";
                foreach (odeme o in u.odemeGecmisi)
                {
                    if (o.odendiMi == false)
                    {
                        borc += o.odenecekUcret;
                        if (sonOdemeBool == false)
                        {
                            sonOdemeBool = true;
                            tmpDate = o.sonOdemeTarihi;
                            sonOdemeTemp = tmpDate.ToString(UyeConfigurations.dateFormat);
                        }
                    }
                }

                if (borcluCheckBox.Checked && borc <= 0)
                {
                    continue;
                }

                if (tarihCheckBox.Checked && tmpDate > DateTime.Now) {
                    continue;
                }
                ListViewItem s = new ListViewItem(u.uyeNumarasi.ToString());
                s.SubItems.Add(u.uyeIcerideMi ? "Evet" : "Hayir");
                s.SubItems.Add(u.cinsiyet == UyeConfigurations.CINSIYET_ERKEK ? "Erkek" : "Kadın");
                s.SubItems.Add(u.isim);

                int bulunanPaketIndex = Program.paketler.FindIndex(x => x.paketNo == u.paketNo);

                if (bulunanPaketIndex != -1)
                {
                    var aktifPaket = Program.paketler[bulunanPaketIndex];

                    if (aktifPaket.aktifMi)
                    {
                        s.SubItems.Add(aktifPaket.paketIsmi);
                    }
                    else
                    {
                        var hedefPaket = Program.paketler.FirstOrDefault(x => x.paketNo == aktifPaket.yonlendirme);

                        if (hedefPaket != null)
                        {
                            s.SubItems.Add(hedefPaket.paketIsmi);
                        }
                        else
                        {
                            s.SubItems.Add("Yönlendirilen Paket Yok");
                        }
                    }
                }
                else
                {
                    s.SubItems.Add("Paket Bulunamadı!");
                }
               
                s.SubItems.Add(borc.ToString() + " TL");
                s.SubItems.Add(sonOdemeTemp);

               
                s.SubItems.Add(farkStr);
                s.SubItems.Add(u.uyelikTarihi.ToString(UyeConfigurations.dateFormat));

                listView1.Items.Add(s);
            }

            statusBarUyeSayisi.Text = $"Üye Sayısı: {Program.uyeler.Count}";
            int iceridekiKisiSayisi = Program.uyeler.Count(x => x.uyeIcerideMi);
            statusBarMevcutUyeSayisi.Text = "Salonda Mevcut Üye Sayısı: " + iceridekiKisiSayisi.ToString();
        }

        private void memberInfo_Click(object sender, EventArgs e)
        {
            uyeBilgileri uyeBilgiEkrani = new uyeBilgileri();
            uyeBilgiEkrani.Activate();
            uyeBilgiEkrani.Show();
        }

        public void rst()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                seciliUyeyiBul().kopyala(Program.yonetimPaneliSelectedUye);
                memberInfo.Enabled = true;
                editMember.Enabled = true;
                deleteMember.Enabled = true;
                mevcutButton.Enabled = true;
                if (Program.yonetimPaneliSelectedUye.uyeIcerideMi) mevcutButton.Text = "Mevcut Değil Olarak İşaretle";
                else mevcutButton.Text = "Mevcut Olarak İşaretle";
            }
            else
            {
                memberInfo.Enabled = false;
                editMember.Enabled = false;
                deleteMember.Enabled = false;
                mevcutButton.Enabled = false;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                seciliUyeyiBul().kopyala(Program.yonetimPaneliSelectedUye);
                memberInfo.Enabled = true;
                editMember.Enabled = true;
                deleteMember.Enabled = true;
                mevcutButton.Enabled = true;
                if (Program.yonetimPaneliSelectedUye.uyeIcerideMi) mevcutButton.Text = "Mevcut Değil Olarak İşaretle";
                else mevcutButton.Text = "Mevcut Olarak İşaretle";
            }
            else
            {
                memberInfo.Enabled = false;
                editMember.Enabled = false;
                deleteMember.Enabled = false;
                mevcutButton.Enabled = false;
            }
        }

        private Uye seciliUyeyiBul()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                int secilenID = Convert.ToInt32(listView1.SelectedItems[0].Text);
                return Program.uyeler.FirstOrDefault(x => x.uyeNumarasi == secilenID);
            }
            return null;
        }

        private void borcluCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (borcluCheckBox.Checked) tarihCheckBox.Enabled = true;
            else
            {
                tarihCheckBox.Checked =false;
                tarihCheckBox.Enabled = false;
            }
            refreshListView();
        }

        private void mevcutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshListView();
        }

        private void tarihCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshListView();
        }

        private void yonetimPaneli_Click(object sender, EventArgs e)
        {
            listView1.SelectedItems.Clear();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            refreshListView(UyeAra(searchTextBox.Text));
        }

        private void addMember_Click(object sender, EventArgs e)
        {
            uyeEklemeForm uyeEklemeForm1 = new uyeEklemeForm();
            uyeEklemeForm1.FormClosed += (_sender, args) =>
            {
                this.refreshListView();
                this.rst();
            };

            uyeEklemeForm1.Show();
        }

        private void editMember_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                uyeEklemeForm uEkle = new uyeEklemeForm();
                uEkle.duzenlenecekUyeKopyala(seciliUyeyiBul());
                uEkle.uyeEklemeButtonu.Text = "Üyeyi Düzenle";
                uEkle.FormClosed += (_sender, args) =>
                {
                    this.refreshListView();
                    this.rst();
                };
                uEkle.ShowDialog();
            }
        }

        private void deleteMember_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                "Seçilen üyeyi silmek istediğinize emin misiniz?",
                "Dikkat",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2 
            );
            if (cevap == DialogResult.Yes) {
                Program.deleteMember(seciliUyeyiBul());
                refreshListView();
            }
            this.rst();
        }

        private void mevcutButton_Click(object sender, EventArgs e)
        {
            Uye u = seciliUyeyiBul();
            if (u.uyeIcerideMi)
            {
                u.uyeIcerideMi = false;
                SQLIslemleri.updateMember(u);
                refreshListView();
                mevcutButton.Enabled = false;
            }
            else
            {
                u.uyeIcerideMi = true;
                SQLIslemleri.updateMember(u);
                refreshListView();
                mevcutButton.Enabled = false;
            }
            this.rst();
        }

        private void paketEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaketDuzenle pk = new PaketDuzenle();
            pk.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.DemoVerileriYukle();
            refreshListView();
        }

        private void yonetimPaneli_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (listView1.SelectedItems.Count > 0)
                    {
                        DialogResult cevap = MessageBox.Show(
                            "Seçilen üyeyi silmek istediğinize emin misiniz?",
                            "Dikkat",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2
                        );
                        if (cevap == DialogResult.Yes)
                        {
                            Program.deleteMember(seciliUyeyiBul());
                            refreshListView();
                        }
                    }
                    break;
            }
        }

        private void yenileButton_click(object sender, EventArgs e)
        {
            Program.odemeKontrolu();
            refreshListView();
        }

        private void memberInfo_Click_1(object sender, EventArgs e)
        {
            uyeBilgileri u = new uyeBilgileri();
            u.FormClosed += (_sender, args) =>
            {
                this.refreshListView();
                this.rst();
            };
            u.ShowDialog();

        }

        private void girisEkraniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            girisEkrani g = new girisEkrani();
            g.Show();
        }
    }
}
