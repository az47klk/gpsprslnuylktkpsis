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
    public partial class PaketDuzenle : Form
    {
        List<Paket> geciciPaketler = new List<Paket>();
        List<Paket> listHazirligi = new List<Paket>();
        Paket seciliPaket;
        int paketLastNum;

        public PaketDuzenle()
        {
            InitializeComponent();
        }

        private void refreshLbox(Paket seciliOlan = null)
        {
            var listeHazirligi = geciciPaketler
            .Where(p => p != null && p.aktifMi && (seciliOlan != null) ? p.paketNo != seciliOlan.paketNo : true)
            .Select(x => new
            {

                GorunenMetin = $"{x.paketIsmi} - {x.gun} Gün - {x.paketFiyati} TL",
                Deger = x.paketNo
            })
            .ToList();

            yonlendirmeCbox.DisplayMember = "GorunenMetin";
            yonlendirmeCbox.ValueMember = "Deger";
            yonlendirmeCbox.DataSource = listeHazirligi;
        }

        private void refreshMainList()
        {
            listView1.Items.Clear();
            foreach (Paket p in geciciPaketler)
            {
                ListViewItem i = new ListViewItem();
                i.Text = p.paketNo.ToString();
                i.SubItems.Add(p.paketIsmi);
                i.SubItems.Add(p.paketFiyati.ToString() + " TL");
                i.SubItems.Add(p.gun.ToString() + " Gün");
                i.SubItems.Add(p.aktifMi ? "Evet" : "Hayır");
                if (p.aktifMi == false)
                    i.SubItems.Add(geciciPaketler[geciciPaketler.FindIndex(x => x.paketNo == p.yonlendirme)].paketIsmi);
                else i.SubItems.Add("-");

                    listView1.Items.Add(i);
            }
            paketLastNum = SQLIslemleri.getLastNumberOfPackets() + 1;
        }

        private void PaketDuzenle_Load(object sender, EventArgs e)
        {
            geciciPaketler = Program.paketler.Select(x => new Paket()
            {
                paketNo = x.paketNo,
                paketIsmi = x.paketIsmi,
                paketFiyati = x.paketFiyati,
                gun = x.gun,
                aktifMi = x.aktifMi,
                yonlendirme = x.yonlendirme
            }).ToList();

            paketLastNum = SQLIslemleri.getLastNumberOfPackets() + 1;
            refreshMainList();

            seciliPaket = seciliPaketiBul();
            paketNoLbl.Text = paketLastNum.ToString();
            paketIsmiTbox.Text = "";
            fiyatTbox.Text = "";
            gunSayisiTbox.Text = "";
            aktifMiCbox.Checked = true;
            yonlendirmeCbox.SelectedValue = -1;
            refreshLbox();
            icKaydetBtn.Text = "Ekle";
        }

        private Paket seciliPaketiBul()
        {
            Paket tmpPaket = null;
            if (listView1.SelectedItems.Count == 1)
            {
                int indx = geciciPaketler.FindIndex(x => x.paketNo == Convert.ToInt32(listView1.SelectedItems[0].Text));
                if (indx != -1)
                    tmpPaket = geciciPaketler[indx];
            }
            return tmpPaket;
        }

        private void icKaydetBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                if (paketIsmiTbox.Text != null && paketIsmiTbox.Text != "")
                {
                    seciliPaket.paketIsmi = paketIsmiTbox.Text;
                }
                else
                {
                    MessageBox.Show("Lütfen uygun bir isim giriniz.");
                    return;
                }

                string temizFiyat = fiyatTbox.Text.Replace(".", ",");
                double ff;
                if (Double.TryParse(temizFiyat, out ff))
                {
                    seciliPaket.paketFiyati = ff;
                }
                else
                {
                    MessageBox.Show("Lütfen uygun bir fiyat giriniz.");
                    return;
                }

                if (!Int32.TryParse(gunSayisiTbox.Text, out seciliPaket.gun))
                {
                    MessageBox.Show("Lütfen uygun gün sayısı giriniz.");
                    return;
                }

                seciliPaket.aktifMi = aktifMiCbox.Checked;
                seciliPaket.yonlendirme = Int32.Parse(yonlendirmeCbox.SelectedValue.ToString());
            }
            else if (listView1.SelectedItems.Count == 0)
            {
                Paket tmpEklenecekPaket = new Paket();
                tmpEklenecekPaket.paketNo = paketLastNum;
                if (paketIsmiTbox.Text != null && paketIsmiTbox.Text != "")
                {
                    tmpEklenecekPaket.paketIsmi = paketIsmiTbox.Text;
                }
                else
                {
                    MessageBox.Show("Lütfen uygun bir isim giriniz.");
                    return;
                }

                string temizFiyat = fiyatTbox.Text.Replace(".", ",");
                double ff;
                if (Double.TryParse(temizFiyat, out ff))
                {
                    tmpEklenecekPaket.paketFiyati = ff;
                }
                else
                {
                    MessageBox.Show("Lütfen uygun bir fiyat giriniz.");
                    return;
                }

                if (!Int32.TryParse(gunSayisiTbox.Text, out tmpEklenecekPaket.gun))
                {
                    MessageBox.Show("Lütfen uygun gün sayısı giriniz.");
                    return;
                }

                tmpEklenecekPaket.aktifMi = aktifMiCbox.Checked;
                tmpEklenecekPaket.yonlendirme = Int32.Parse(yonlendirmeCbox.SelectedValue.ToString());
                geciciPaketler.Add(tmpEklenecekPaket);
            }
            refreshMainList();
        }

        private bool yonlendirmeVarMi(Paket p)
        {
            if(geciciPaketler.Count(x => x.aktifMi == false && x.yonlendirme == p.paketNo) > 0)
            {
                return true;
            }
            return false;
        }

        private void disKaydetBtn_Click(object sender, EventArgs e)
        {
            if (SQLIslemleri.globalConnection.State != System.Data.ConnectionState.Open)
                SQLIslemleri.globalConnection.Open();

            using (SQLiteTransaction trans = SQLIslemleri.globalConnection.BeginTransaction())
            {
                try
                {
                    HashSet<int> orijinalIDler = new HashSet<int>(Program.paketler.Select(x => x.paketNo));

                    var silinecekIDler = Program.paketler
                        .Select(p => p.paketNo)
                        .Except(geciciPaketler.Select(g => g.paketNo))
                        .ToList();

                    foreach (int id in silinecekIDler)
                    {
                        string silSQL = "DELETE FROM paketler WHERE paket_no = @no";
                        using (SQLiteCommand cmd = new SQLiteCommand(silSQL, SQLIslemleri.globalConnection, trans))
                        {
                            cmd.Parameters.AddWithValue("@no", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (Paket p in geciciPaketler)
                    {
                        // KİLİT NOKTA BURASI: ID > 0 kontrolü yerine listeye bakıyoruz.
                        if (orijinalIDler.Contains(p.paketNo))
                        {
                            string updSQL = @"UPDATE paketler SET 
                                    paket_ismi = @isim, 
                                    paket_fiyati = @fiyat, 
                                    aktif_mi = @aktif, 
                                    yonlendirme = @yonlendirme, 
                                    gun = @gun 
                                    WHERE paket_no = @no";

                            using (SQLiteCommand cmd = new SQLiteCommand(updSQL, SQLIslemleri.globalConnection, trans))
                            {
                                cmd.Parameters.AddWithValue("@isim", p.paketIsmi);
                                cmd.Parameters.AddWithValue("@fiyat", p.paketFiyati);
                                cmd.Parameters.AddWithValue("@aktif", p.aktifMi ? 1 : 0);
                                cmd.Parameters.AddWithValue("@yonlendirme", p.yonlendirme);
                                cmd.Parameters.AddWithValue("@gun", p.gun);
                                cmd.Parameters.AddWithValue("@no", p.paketNo); // Mevcut ID'yi kullan
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {

                            string insSQL = @"INSERT INTO paketler (paket_ismi, paket_fiyati, aktif_mi, yonlendirme, gun) 
                                    VALUES (@isim, @fiyat, @aktif, @yonlendirme, @gun);
                                    SELECT last_insert_rowid();";

                            using (SQLiteCommand cmd = new SQLiteCommand(insSQL, SQLIslemleri.globalConnection, trans))
                            {
                                cmd.Parameters.AddWithValue("@isim", p.paketIsmi);
                                cmd.Parameters.AddWithValue("@fiyat", p.paketFiyati);
                                cmd.Parameters.AddWithValue("@aktif", p.aktifMi ? 1 : 0);
                                cmd.Parameters.AddWithValue("@yonlendirme", p.yonlendirme);
                                cmd.Parameters.AddWithValue("@gun", p.gun);

                                long yeniID = (long)cmd.ExecuteScalar();
                                p.paketNo = (int)yeniID;
                            }
                        }
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                    return;
                }
            }

            Program.paketler.Clear();
            Program.paketler.AddRange(geciciPaketler);

            MessageBox.Show("Tüm değişiklikler başarıyla kaydedildi!");
            this.Close();
        }

        private void disIptalBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                seciliPaket = seciliPaketiBul();
                paketNoLbl.Text = seciliPaket.paketNo.ToString();
                paketIsmiTbox.Text = seciliPaket.paketIsmi.ToString();
                fiyatTbox.Text = seciliPaket.paketFiyati.ToString();
                gunSayisiTbox.Text = seciliPaket.gun.ToString();
                aktifMiCbox.Enabled = !yonlendirmeVarMi(seciliPaket);
                aktifMiCbox.Checked = seciliPaket.aktifMi;
                refreshLbox(seciliPaket);
                if (seciliPaket.aktifMi == false)
                    yonlendirmeCbox.SelectedValue = seciliPaket.yonlendirme;
                else yonlendirmeCbox.SelectedValue = -1;
                icKaydetBtn.Text = "Kaydet";
            }
            else {
                seciliPaket = seciliPaketiBul();
                paketNoLbl.Text = paketLastNum.ToString();
                paketIsmiTbox.Text = "";
                fiyatTbox.Text = "";
                gunSayisiTbox.Text = "";
                aktifMiCbox.Checked = true;
                yonlendirmeCbox.SelectedValue = -1;
                refreshLbox();
                icKaydetBtn.Text = "Ekle";
            }   
        }

        private void PaketDuzenle_Click(object sender, EventArgs e)
        {
            listView1.SelectedItems.Clear();
        }

        private void aktifMiCbox_CheckedChanged(object sender, EventArgs e)
        {
            if (aktifMiCbox.Checked) {
                yonlendirmeCbox.Enabled = false;
                
            }
            else {
                yonlendirmeCbox.Enabled = true;
                refreshLbox(seciliPaket);
            }
        }
    }
}
