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
    public partial class uyeEklemeForm : Form
    {
        Uye duzenlenecekUye = null;

        public void duzenlenecekUyeKopyala(Uye u) {
            if (duzenlenecekUye == null)
            {
                duzenlenecekUye = new Uye();
            }

            u.kopyala(duzenlenecekUye);
            enBuyukId = duzenlenecekUye.uyeNumarasi;
            UyeNoLabel.Text = $"Üye Numarası: {enBuyukId}";
            bilgileriFormaYaz();
        }

        public int enBuyukId = SQLIslemleri.getLastNumberOfMembers() + 1;
        public uyeEklemeForm()
        {
            InitializeComponent();
            UyeNoLabel.Text = $"Üye Numarası: {enBuyukId}";
            var listeHazirligi = Program.paketler
             .Where(p => p != null && p.aktifMi) 
             .Select(x => new
             {
                 
                 GorunenMetin = $"{x.paketIsmi} - {x.gun} Gün - {x.paketFiyati} TL",

                 Deger = x.paketNo
             })
             .ToList();

            paketLbox.DataSource = listeHazirligi;
            paketLbox.DisplayMember = "GorunenMetin"; 
            paketLbox.ValueMember = "Deger";          
        }

        Uye eklenecekUye = new Uye();
         
        private void uyeEklemeFunctionu()
        {
            //paket bilgisi
            if (paketLbox.SelectedIndex == -1 || paketLbox.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir paket seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Fonksiyonu durdur, kaydetme yapma
            }
            eklenecekUye.paketNo = Convert.ToInt32(paketLbox.SelectedValue);
            // uyelik bilgileri
            if (isimTbox.Text == ""){
                MessageBox.Show("İsim alanı boş bırakılamaz!");
                return;
            } else eklenecekUye.isim = isimTbox.Text;
            
            if (cinsiyetLbox.SelectedIndex == null) { 
                MessageBox.Show("Cinsiyet alanı boş bırakılamaz!");
                return;
            } else eklenecekUye.cinsiyet = cinsiyetLbox.SelectedIndex;

            if (telefonTbox.Text == "") {
                MessageBox.Show("Telefon numarası alanı boş bırakılamaz!");
                return;
            } else eklenecekUye.telefonNumarasi = telefonTbox.Text;

            if (dogumDtime.Value.Year == DateTime.Now.Year){
                MessageBox.Show("Lütfen geçerli bir doğum tarihi giriniz!");
                return;
            } else eklenecekUye.dogumTarihi = dogumDtime.Value;

            if (adresTbox.Text == ""){
                MessageBox.Show("Adres bölümü boş bırakılamaz!");
                return;
            } else eklenecekUye.adres = adresTbox.Text;

            if (boyTbox.Text != "")
            {
                double boy = 0;
                try {
                    boy = Int32.Parse(boyTbox.Text);
                }
                catch {
                    MessageBox.Show("Lütfen geçerli bir boy bilgisi giriniz.");
                    return;
                }
                eklenecekUye.Boy = boy;
            }
            
            if (kiloTbox.Text != "")
            {
                double kilo = 0;
                try
                {
                    kilo = Convert.ToDouble(kiloTbox.Text);
                }
                catch
                {
                    MessageBox.Show("Lütfen geçerli bir kilo bilgisi giriniz.");
                    return;
                }
                eklenecekUye.Kilo = kilo;
            }
            eklenecekUye.kronikHastalikVarMi = hastalikCbox.Checked;
            eklenecekUye.kronikHastalikAciklama = hastalikTbox.Text.Trim();
            eklenecekUye.aciklama = aciklamaTbox.Text.Trim();

            // hedefler
            eklenecekUye.hedefler = hastalikCbox.Checked;
            eklenecekUye.kiloHedef = kondisyonCbox.Checked;
            if (kiloHedefTbox.Text.Length > 0)
            {
                try
                {
                    eklenecekUye.kiloHedefSayi = Int32.Parse(kiloHedefTbox.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("Lütfen uygun bir kilo hedefi değeri giriniz.");
                    return;
                }
            }
            eklenecekUye.vucutGelistirme = vucutCbox.Checked;
            eklenecekUye.kolGelistirme = kolCbox.Checked;
            eklenecekUye.gogusGelistirme = gogusCbox.Checked;
            eklenecekUye.karinGelistirme = karinCbox.Checked;
            eklenecekUye.bacakGelistirme = bacakCbox.Checked;
            eklenecekUye.kondisyonArttirma = kondisyonCbox.Checked;
            
            // acil durum kisisi
            eklenecekUye.acilKisiIsim = acilİsimTbox.Text.Trim();
            eklenecekUye.acilKisiNumara = acilTelefonTbox.Text.Trim();
            eklenecekUye.acilKisiAdres = acilAdresTbox.Text.Trim();

            eklenecekUye.uyelikTarihi = DateTime.Now;

            Program.addMember(eklenecekUye);
            this.Close();
        }

        private void bilgileriFormaYaz()
        {
            if (duzenlenecekUye == null) return;

            isimTbox.Text = duzenlenecekUye.isim;
            telefonTbox.Text = duzenlenecekUye.telefonNumarasi;
            adresTbox.Text = duzenlenecekUye.adres;

            if (duzenlenecekUye.cinsiyet >= 0 && duzenlenecekUye.cinsiyet < cinsiyetLbox.Items.Count)
            {
                cinsiyetLbox.SelectedIndex = duzenlenecekUye.cinsiyet;
            }

            if (duzenlenecekUye.paketNo > 0)
            {
                paketLbox.SelectedValue = duzenlenecekUye.paketNo;
            }

            if (duzenlenecekUye.dogumTarihi > DateTime.MinValue)
            {
                dogumDtime.Value = duzenlenecekUye.dogumTarihi;
            }

            boyTbox.Text = duzenlenecekUye.Boy > 0 ? duzenlenecekUye.Boy.ToString() : "";
            kiloTbox.Text = duzenlenecekUye.Kilo > 0 ? duzenlenecekUye.Kilo.ToString() : "";

            hastalikCbox.Checked = duzenlenecekUye.kronikHastalikVarMi;
            hastalikTbox.Text = duzenlenecekUye.kronikHastalikAciklama;
            aciklamaTbox.Text = duzenlenecekUye.aciklama;

            kondisyonCbox.Checked = duzenlenecekUye.kondisyonArttirma || duzenlenecekUye.kiloHedef;

            kiloHedefTbox.Text = duzenlenecekUye.kiloHedefSayi > 0 ? duzenlenecekUye.kiloHedefSayi.ToString() : "";

            vucutCbox.Checked = duzenlenecekUye.vucutGelistirme;
            kolCbox.Checked = duzenlenecekUye.kolGelistirme;
            gogusCbox.Checked = duzenlenecekUye.gogusGelistirme;
            karinCbox.Checked = duzenlenecekUye.karinGelistirme;
            bacakCbox.Checked = duzenlenecekUye.bacakGelistirme;

            acilİsimTbox.Text = duzenlenecekUye.acilKisiIsim;
            acilTelefonTbox.Text = duzenlenecekUye.acilKisiNumara;
            acilAdresTbox.Text = duzenlenecekUye.acilKisiAdres;
        }

        private void iptalButtonu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uyeEklemeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return) {
                uyeEklemeFunctionu();
            }
        }

        private void uyeDuzenlemeFunctionu()
        {
            if (paketLbox.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir paket seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(isimTbox.Text))
            {
                MessageBox.Show("İsim alanı boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cinsiyetLbox.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            duzenlenecekUye.paketNo = Convert.ToInt32(paketLbox.SelectedValue);
            duzenlenecekUye.isim = isimTbox.Text.Trim();
            duzenlenecekUye.cinsiyet = cinsiyetLbox.SelectedIndex;
            duzenlenecekUye.telefonNumarasi = telefonTbox.Text.Trim();
            duzenlenecekUye.adres = adresTbox.Text.Trim();
            duzenlenecekUye.dogumTarihi = dogumDtime.Value;
            duzenlenecekUye.aciklama = aciklamaTbox.Text.Trim();

            duzenlenecekUye.kronikHastalikVarMi = hastalikCbox.Checked;
            duzenlenecekUye.kronikHastalikAciklama = hastalikTbox.Text.Trim();

            double kilo;
            if (double.TryParse(kiloTbox.Text, out kilo))
                duzenlenecekUye.Kilo = kilo;
            else
                duzenlenecekUye.Kilo = 0; 

            double boy;
            if (double.TryParse(boyTbox.Text, out boy))
                duzenlenecekUye.Boy = boy;
            else
                duzenlenecekUye.Boy = 0;

            duzenlenecekUye.vucutGelistirme = vucutCbox.Checked;
            duzenlenecekUye.kolGelistirme = kolCbox.Checked;
            duzenlenecekUye.gogusGelistirme = gogusCbox.Checked;
            duzenlenecekUye.karinGelistirme = karinCbox.Checked;
            duzenlenecekUye.bacakGelistirme = bacakCbox.Checked;

            duzenlenecekUye.kondisyonArttirma = kondisyonCbox.Checked;

            int hedefKiloSayi;
            if (int.TryParse(kiloHedefTbox.Text, out hedefKiloSayi))
            {
                duzenlenecekUye.kiloHedefSayi = hedefKiloSayi;
                duzenlenecekUye.kiloHedef = true;
            }
            else
            {
                duzenlenecekUye.kiloHedefSayi = 0;
                duzenlenecekUye.kiloHedef = false;
            }

            duzenlenecekUye.hedefler = (duzenlenecekUye.vucutGelistirme ||
                                        duzenlenecekUye.kiloHedef ||
                                        duzenlenecekUye.kondisyonArttirma);

            duzenlenecekUye.acilKisiIsim = acilİsimTbox.Text.Trim();
            duzenlenecekUye.acilKisiNumara = acilTelefonTbox.Text.Trim();
            duzenlenecekUye.acilKisiAdres = acilAdresTbox.Text.Trim();


            try
            {
                Program.updateMember(duzenlenecekUye);

                MessageBox.Show("Üye bilgileri başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close(); // Formu kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uyeEklemeButtonu_Click(object sender, EventArgs e)
        {
            if (duzenlenecekUye != null)
                uyeDuzenlemeFunctionu();
            
            else
                uyeEklemeFunctionu();
        }

        private void dogumDtime_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateOfBirth = dogumDtime.Value;
            DateTime nowTime = DateTime.Now;
            int yas = 0;
            if (nowTime.Month < dateOfBirth.Month) yas = (nowTime.Year - dateOfBirth.Year) - 1;
            else if (nowTime.Month > dateOfBirth.Month) yas = nowTime.Year - dateOfBirth.Year;
            else if (nowTime.Month == dateOfBirth.Month){
                if (nowTime.Day >= dateOfBirth.Day) yas = nowTime.Year - dateOfBirth.Year;
                else yas = (nowTime.Year - dateOfBirth.Year) - 1;
            }
            yasLabel.Text = $"{yas} yaşında";
        }

        private void kiloHedefCbox_CheckedChanged(object sender, EventArgs e)
        {
                kiloHedefTbox.Enabled = kiloHedefCbox.Checked;
        }

        private void hastalikCbox_CheckedChanged(object sender, EventArgs e)
        {
            hastalikTbox.Enabled = (hastalikCbox.Checked);   
        }

        private void uyeEklemeForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}

