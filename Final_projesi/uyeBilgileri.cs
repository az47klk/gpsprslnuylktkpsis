using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_projesi
{
    public partial class uyeBilgileri : Form
    {
        public uyeBilgileri()
        {
            InitializeComponent();
            refreshInfo();
        }

        public void refreshInfo()
        {
            mevcutLabel.Text = Program.yonetimPaneliSelectedUye.uyeIcerideMi ? "Mevcut" : "Mevcut Değil";
            uyeNoLabel.Text = Program.yonetimPaneliSelectedUye.uyeNumarasi.ToString();
            isimLabel.Text = Program.yonetimPaneliSelectedUye.isim;
            telefonLabel.Text = Program.yonetimPaneliSelectedUye.telefonNumarasi;
            dogumTLabel.Text = Program.yonetimPaneliSelectedUye.dogumTarihi.ToString(UyeConfigurations.dateFormat) + $" ({yasHesaplama(Program.yonetimPaneliSelectedUye.dogumTarihi)})";
            cinsiyetLabel.Text = Program.yonetimPaneliSelectedUye.cinsiyet == UyeConfigurations.CINSIYET_ERKEK ? "Erkek" : "Kadın";
            adresLabel.Text = Program.yonetimPaneliSelectedUye.adres;
            aciklamaTbox.Text = Program.yonetimPaneliSelectedUye.aciklama;

            acilAdres.Text = Program.yonetimPaneliSelectedUye.acilKisiAdres;
            acilIsim.Text = Program.yonetimPaneliSelectedUye.acilKisiIsim;
            acilTelefon.Text = Program.yonetimPaneliSelectedUye.acilKisiNumara;

            hedeflerCbox.Checked = Program.yonetimPaneliSelectedUye.hedefler;
            kiloHedefCbox.Checked = Program.yonetimPaneliSelectedUye.kiloHedef;
            if (kiloHedefCbox.Checked) kiloHedefTbox.Enabled = true;
            else kiloHedefTbox.Enabled = false;
            kiloHedefTbox.Text = Program.yonetimPaneliSelectedUye.kiloHedefSayi.ToString();
            vucutCbox.Checked = Program.yonetimPaneliSelectedUye.vucutGelistirme;
            kolCbox.Checked = Program.yonetimPaneliSelectedUye.kolGelistirme;
            gogusCbox.Checked = Program.yonetimPaneliSelectedUye.gogusGelistirme;
            karinCbox.Checked = Program.yonetimPaneliSelectedUye.karinGelistirme;
            bacakCbox.Checked = Program.yonetimPaneliSelectedUye.bacakGelistirme;
            kondisyonCbox.Checked = Program.yonetimPaneliSelectedUye.kondisyonArttirma;
            // uyelik dondurma
            if (Program.yonetimPaneliSelectedUye.uyelikDondurma)
            {
                dondurmaLabel.Text = "Evet";

                dondurmaBaslangicLabel.Visible = true;
                dondurmaBitisLabel.Visible = true;
                dondurmaBaslangicLabel.Text = Program.yonetimPaneliSelectedUye.uyelikDondurmaBaslangic.ToString(UyeConfigurations.dateFormat);
                dondurmaBitisLabel.Text = Program.yonetimPaneliSelectedUye.uyelikDondurmaBitis.ToString(UyeConfigurations.dateFormat);
                dondurmaBaslangicDTPicker.Visible = false;
                dondurmaBitisDTime.Visible = false;

                uyeligiDondurButton.Text = "Üyeliği Tekrar Aktif Et";
            }
            else
            {
                dondurmaLabel.Text = "Hayır";

                dondurmaBaslangicLabel.Visible = false;
                dondurmaBitisLabel.Visible = false;
                dondurmaBaslangicDTPicker.Visible = true;
                dondurmaBitisDTime.Visible = true;

                uyeligiDondurButton.Text = "Üyeliği Dondur";
            }
            // yasaklama
            if (Program.yonetimPaneliSelectedUye.yasakliMi)
            {
                yasaklamaLabel.Text = "Yasaklı";
                yasaklamaLabel.ForeColor = System.Drawing.Color.Red;
                yasaklamaTarihiLabel.Text = Program.yonetimPaneliSelectedUye.yasaklamaTarihi.ToString(UyeConfigurations.dateFormat);
            }
            else
            {
                yasaklamaLabel.Text = "Yasaklı Değil";
                yasaklamaLabel.ForeColor = System.Drawing.Color.Green;
                yasaklamaTarihiLabel.Text = "-";
            }
            
            // odeme gecmisini getir
            listView1.Items.Clear();
            foreach (odeme o in Program.yonetimPaneliSelectedUye.odemeGecmisi)
            {
                ListViewItem s = new ListViewItem();
                s.Text = o.odemeNo.ToString();
                s.SubItems.Add(Program.paketler[Program.paketler.FindIndex(x=>x.paketNo == o.paket)].paketIsmi);
                s.SubItems.Add(o.DonemBaslangici.ToString(UyeConfigurations.dateFormat));
                s.SubItems.Add(o.DonemBitisi.ToString(UyeConfigurations.dateFormat));
                s.SubItems.Add(Program.paketler[Program.paketler.FindIndex(x => x.paketNo == o.paket)].gun.ToString());
                s.SubItems.Add(o.odenecekUcret.ToString() + " TL");
                s.SubItems.Add(o.odendiMi ? "Evet" : "Hayır");
                if (o.odendiMi)
                {
                    s.BackColor = Color.LightGreen;
                }
                else
                {
                    s.BackColor = Color.LightSalmon;
                }

                listView1.Items.Add(s);
            }
        }

        public void uyeligiDondur(object sender, EventArgs e)
        {
            if (!Program.yonetimPaneliSelectedUye.uyelikDondurma)
            {
                if (dondurmaBaslangicDTPicker.Value < dondurmaBitisDTime.Value)
                {
                    Program.yonetimPaneliSelectedUye.uyelikDondurma = true;
                    Program.yonetimPaneliSelectedUye.uyelikDondurmaBaslangic = dondurmaBaslangicDTPicker.Value;
                    Program.yonetimPaneliSelectedUye.uyelikDondurmaBitis = dondurmaBitisDTime.Value;

                    Program.updateMember(Program.yonetimPaneliSelectedUye);
                    
                    dondurmaLabel.Text = "Evet";

                    dondurmaBaslangicLabel.Visible = true;
                    dondurmaBitisLabel.Visible = true;
                    dondurmaBaslangicLabel.Text = Program.yonetimPaneliSelectedUye.uyelikDondurmaBaslangic.ToString(UyeConfigurations.dateFormat);
                    dondurmaBitisLabel.Text = Program.yonetimPaneliSelectedUye.uyelikDondurmaBitis.ToString(UyeConfigurations.dateFormat);
                    dondurmaBaslangicDTPicker.Visible = false;
                    dondurmaBitisDTime.Visible = false;

                    uyeligiDondurButton.Text = "Üyeliği Tekrar Aktif Et";
                }
                else
                {
                    MessageBox.Show("Lütfen uygun başlangıç ve bitiş tarihi giriniz.");
                }
            }
            else
            {
                Program.yonetimPaneliSelectedUye.uyelikDondurma = false;
                Program.yonetimPaneliSelectedUye.uyelikDondurmaBaslangic = dondurmaBaslangicDTPicker.Value;
                Program.yonetimPaneliSelectedUye.uyelikDondurmaBitis = dondurmaBitisDTime.Value;
                Program.updateMember(Program.yonetimPaneliSelectedUye);
                dondurmaLabel.Text = "Hayır";

                dondurmaBaslangicLabel.Visible = false;
                dondurmaBitisLabel.Visible = false;

                dondurmaBaslangicDTPicker.Visible = true;
                dondurmaBitisDTime.Visible = true;

                uyeligiDondurButton.Text = "Üyeliği Dondur";
            }
        }

        public void yasakla(object sender, EventArgs e)
        {

            if (Program.yonetimPaneliSelectedUye.yasakliMi)
            {
                yasaklamaLabel.Text = "Yasaklı Değil";
                yasaklamaLabel.ForeColor = System.Drawing.Color.Green;
                yasaklamaTarihiLabel.Text = "-";
                Program.yonetimPaneliSelectedUye.yasakliMi = false;
                Program.updateMember(Program.yonetimPaneliSelectedUye);

            }
            else {
                Program.yonetimPaneliSelectedUye.yasakliMi = true;
                Program.yonetimPaneliSelectedUye.yasaklamaTarihi = DateTime.Now;
                yasaklamaLabel.Text = "Yasaklı";
                yasaklamaLabel.ForeColor = System.Drawing.Color.Red;
                yasaklamaTarihiLabel.Text = Program.yonetimPaneliSelectedUye.yasaklamaTarihi.ToString(UyeConfigurations.dateFormat);
                Program.updateMember(Program.yonetimPaneliSelectedUye);
            }
        }

        public void cancelButton(object sender, EventArgs e)
        {
            Program.yonetimPaneliSelectedUye.aciklama = aciklamaTbox.Text.Trim();

            Program.yonetimPaneliSelectedUye.hedefler = hedeflerCbox.Checked;
            Program.yonetimPaneliSelectedUye.kiloHedef = kiloHedefCbox.Checked;

            int girilenHedef;
            if (kiloHedefCbox.Checked && int.TryParse(kiloHedefTbox.Text.Trim(), out girilenHedef))
            {
                Program.yonetimPaneliSelectedUye.kiloHedefSayi = girilenHedef;
            }
            else
            {
                // Checkbox seçili değilse veya hatalı giriş varsa 0 yap
                Program.yonetimPaneliSelectedUye.kiloHedefSayi = 0;
            }

            // 3. Diğer Hedef Checkboxları
            Program.yonetimPaneliSelectedUye.vucutGelistirme = vucutCbox.Checked;
            Program.yonetimPaneliSelectedUye.kolGelistirme = kolCbox.Checked;
            Program.yonetimPaneliSelectedUye.gogusGelistirme = gogusCbox.Checked;
            Program.yonetimPaneliSelectedUye.karinGelistirme = karinCbox.Checked;
            Program.yonetimPaneliSelectedUye.bacakGelistirme = bacakCbox.Checked;
            Program.yonetimPaneliSelectedUye.kondisyonArttirma = kondisyonCbox.Checked;

            Program.updateMember(Program.yonetimPaneliSelectedUye);
            this.Close();
        }

        private void OdemeDurumunuGuncelle(bool odendiOlsun)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen listeden bir ödeme seçiniz.");
                return;
            }

            ListViewItem secilenSatir = listView1.SelectedItems[0];
            int odemeNo = Convert.ToInt32(secilenSatir.Text); 

            try
            {
                if (SQLIslemleri.globalConnection.State != ConnectionState.Open)
                    SQLIslemleri.globalConnection.Open();

                string sql = "UPDATE odeme_bilgileri SET odendi_mi = @durum WHERE odeme_no = @id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, SQLIslemleri.globalConnection))
                {
                    cmd.Parameters.AddWithValue("@durum", odendiOlsun ? 1 : 0); // 1: Ödendi, 0: Ödenmedi
                    cmd.Parameters.AddWithValue("@id", odemeNo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
                return; 
            }

            var ilgiliOdeme = Program.yonetimPaneliSelectedUye.odemeGecmisi.FirstOrDefault(x => x.odemeNo == odemeNo);
            if (ilgiliOdeme != null)
            {
                ilgiliOdeme.odendiMi = odendiOlsun;
            }

            var ilgiliOdeme2 = Program.uyeler.FirstOrDefault(x=> x.uyeNumarasi == Program.yonetimPaneliSelectedUye.uyeNumarasi).odemeGecmisi.FirstOrDefault(x => x.odemeNo == odemeNo);
            if (ilgiliOdeme2 != null)
            {
                ilgiliOdeme2.odendiMi = odendiOlsun;
            }

            if (odendiOlsun)
            {
                secilenSatir.BackColor = Color.LightGreen;
                secilenSatir.SubItems[6].Text = "Evet";
            }
            else
            {
                secilenSatir.BackColor = Color.LightSalmon;
                secilenSatir.SubItems[6].Text = "Hayır";
            }
        }

        public void odendiOlarakIsaretle(object sender, EventArgs e)
        {
            OdemeDurumunuGuncelle(true);
        }

        public void odenmediOlarakIsaretle(object sender, EventArgs e)
        {
            OdemeDurumunuGuncelle(false); 
        }

        public string yasHesaplama(DateTime dateOfBirth)
        {
            DateTime nowTime = DateTime.Now;
            int yas = 0;
            if (nowTime.Month < dateOfBirth.Month) yas = (nowTime.Year - dateOfBirth.Year) - 1;
            else if (nowTime.Month > dateOfBirth.Month) yas = nowTime.Year - dateOfBirth.Year;
            else if (nowTime.Month == dateOfBirth.Month)
            {
                if (nowTime.Day >= dateOfBirth.Day) yas = nowTime.Year - dateOfBirth.Year;
                else yas = (nowTime.Year - dateOfBirth.Year) - 1;
            }
            return $"{yas} yaşında";
        }

        public void kiloHedefChecked(object sender, EventArgs e)
        {
            if (kiloHedefCbox.Checked) kiloHedefTbox.Enabled = true;
            else kiloHedefTbox.Enabled = false;
        }

        private void uyeBilgileri_Resize(object sender, EventArgs e)
        {
            adresLabel.MaximumSize = new System.Drawing.Size(groupBox1.Width - 100, 0);
        }
    }
}
