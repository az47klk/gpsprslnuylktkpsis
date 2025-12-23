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
    public partial class girisEkrani : Form
    {
        public girisEkrani()
        {
            InitializeComponent();
           
        }

        public void basariliGiris(String name) {
            durumlar(true);
            symbolLabel.Text = "✓";
            symbolLabel.ForeColor = Color.Chartreuse;
            nameLabel.Text = name;
            ErrorLabel.Visible = false;
            UyariLabel.Visible = false;
            nameLabel.Location = new Point(this.Size.Width/2 - nameLabel.Size.Width/2, nameLabel.Location.Y);
        }

        public void uyariliGiris(String name, String causeOf)
        {
            durumlar(true);
            symbolLabel.Text = "✓";
            symbolLabel.ForeColor = Color.Yellow;
            nameLabel.Text = name;
            UyariLabel.Text = causeOf;
            ErrorLabel.Visible = false;
            UyariLabel.Visible = true;
            nameLabel.Location = new Point(this.Size.Width / 2 - nameLabel.Size.Width / 2, nameLabel.Location.Y);
            UyariLabel.Location = new Point(this.Size.Width / 2 - UyariLabel.Size.Width / 2, UyariLabel.Location.Y);
        }

        public void basarisizGiris(String name, String causeOf)
        {
            durumlar(true);
            symbolLabel.Text = "X";
            symbolLabel.ForeColor = Color.Red;
            nameLabel.Text = name;
            UyariLabel.Visible = false;
            ErrorLabel.Visible = true;
            ErrorLabel.Text = causeOf;
            nameLabel.Location = new Point(this.Size.Width/2 - nameLabel.Size.Width/2, nameLabel.Location.Y);
            ErrorLabel.Location = new Point(this.Size.Width/2 - ErrorLabel.Size.Width/2,  ErrorLabel.Location.Y);
        }

        public void guleGule(String name, String causeOf)
        {
            durumlar(true);
            symbolLabel.Text = "☹";
            symbolLabel.ForeColor = Color.Red;
            nameLabel.Text = name;
            UyariLabel.Visible = false;
            ErrorLabel.Visible = true;
            ErrorLabel.Text = causeOf;
            nameLabel.Location = new Point(this.Size.Width / 2 - nameLabel.Size.Width / 2, nameLabel.Location.Y);
            ErrorLabel.Location = new Point(this.Size.Width / 2 - ErrorLabel.Size.Width / 2, ErrorLabel.Location.Y);
        }

        public void durumlar(bool d) {
            symbolLabel.Visible = d; nameLabel.Visible = d; UyariLabel.Visible=d; ErrorLabel.Visible = d;
        }

        private void UyeleriComboboxaDoldur()
        {
            // Önce ComboBox'ı temizleyelim
            uyeSec.DataSource = null;
            uyeSec.Items.Clear();

            // LINQ ile listeyi istediğimiz formata (Display ve Value) çeviriyoruz
            var comboListesi = Program.uyeler
                .Where(u => u.paketNo != 0) // (İsteğe bağlı) Sadece aktif/paketli üyeleri getir
                .Select(u => new
                {
                    // Ekranda görünecek yazı: "15 - Ahmet Yılmaz"
                    GorunenMetin = $"{u.uyeNumarasi} - {u.isim}",

                    // Arka planda tutulacak değer (ID)
                    Deger = u.uyeNumarasi
                })
                .ToList();

            // Bağlama işlemi
            uyeSec.DataSource = comboListesi;
            uyeSec.DisplayMember = "GorunenMetin"; // Kullanıcının gördüğü
            uyeSec.ValueMember = "Deger";          // Seçilince alacağın ID
        }

        private void girisEkrani_Load(object sender, EventArgs e)
        {
            UyeleriComboboxaDoldur();
            durumlar(false);
        }

        public async void newFunc()
        {
            aaa();
            await Task.Delay(7000);
            durumlar(false);
        }

        public void aaa()
        {
          
            // 1. SEÇİM KONTROLLERİ (Standart)
            if (uyeSec.SelectedIndex == -1 || uyeSec.SelectedValue == null) return;

            int secilenID;
            if (!int.TryParse(uyeSec.SelectedValue.ToString(), out secilenID)) return;

            Uye secilenUye = Program.uyeler.FirstOrDefault(u => u.uyeNumarasi == secilenID);
            if (secilenUye == null) return;

            if (secilenUye.uyeIcerideMi)
            {
                // 1. Veritabanını Güncelle (Dışarıda = 0)
                IcerideDurumunuGuncelle(secilenUye.uyeNumarasi, false);

                // 2. RAM'i Güncelle
                secilenUye.uyeIcerideMi = false;

                // 3. Ekrana Yaz (Güle Güle)
                guleGule(secilenUye.isim, "Çıkış Yapıldı. İyi günler!");
                return; // İşlem bitti, fonksiyondan çık.
            }

            else
            {
                // A) YASAKLI MI KONTROLÜ (En önce buna bakılır)
                if (secilenUye.yasakliMi)
                {
                    string sebep = string.IsNullOrEmpty(secilenUye.yasaklamaAciklamasi)
                                   ? "Giriş Yasaklanmıştır!"
                                   : secilenUye.yasaklamaAciklamasi;

                    basarisizGiris(secilenUye.isim, sebep);
                    return;
                }

                // --- Buraya geldiyse içeri girecek demektir. Önce durumu güncelleyelim ---

                // 1. Veritabanını Güncelle (İçeride = 1)
                IcerideDurumunuGuncelle(secilenUye.uyeNumarasi, true);

                // 2. RAM'i Güncelle
                secilenUye.uyeIcerideMi = true;


                // B) BORÇ KONTROLÜ (İçeri girdi ama uyarı verecek miyiz?)
                var gecikmisBorc = secilenUye.odemeGecmisi?
                    .FirstOrDefault(x => x.odendiMi == false && x.sonOdemeTarihi.Date < DateTime.Today);

                if (gecikmisBorc != null)
                {
                    string uyariMesaji = $"Ödenmemiş Borç!\n" +
                                         $"Tarih: {gecikmisBorc.sonOdemeTarihi.ToShortDateString()}\n" +
                                         $"Tutar: {gecikmisBorc.odenecekUcret} TL";

                    // Sarı ekran veriyoruz
                    uyariliGiris(secilenUye.isim, uyariMesaji);
                }
                else
                {
                    // C) TERTEMİZ GİRİŞ
                    // Yeşil ekran veriyoruz
                    basariliGiris(secilenUye.isim);
                }
            }
        }

        private void uyeSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            newFunc();
        }

        private void IcerideDurumunuGuncelle(int uyeID, bool icerideMi)
        {
            try
            {
                if (SQLIslemleri.globalConnection.State != ConnectionState.Open)
                    SQLIslemleri.globalConnection.Open();

                string sql = "UPDATE uyeler SET iceride_mi = @durum WHERE no = @id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, SQLIslemleri.globalConnection))
                {
                    cmd.Parameters.AddWithValue("@durum", icerideMi ? 1 : 0);
                    cmd.Parameters.AddWithValue("@id", uyeID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durum güncellenirken hata: " + ex.Message);
            }
        }
    }
}
