using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_projesi
{
    internal static class Program
    {
        public static Uye yonetimPaneliSelectedUye = new Uye();

        public static SQLiteConnection globalConnection;
        public static string dbPath = ".\\gym.db";

        public static List<Uye> uyeler = new List<Uye>();
        public static List<Paket> paketler = new List<Paket>();

        public static void addMember(Uye _member)
        {
            if (_member == null) return;
            
            _member.uyeNumarasi = SQLIslemleri.addMember(_member);
            uyeler.Add(_member);
        }

        public static void deleteMember(Uye _member)
        {
            if (_member == null) return;
            Program.uyeler.RemoveAll(x => x.uyeNumarasi == _member.uyeNumarasi);
            SQLIslemleri.deleteMember(_member.uyeNumarasi);
        }
        
        public static void deleteMember(int _memberNo)
        {
            // Önce veritabanından silmeyi dene
            bool silindiMi = SQLIslemleri.deleteMember(_memberNo);

            if (silindiMi)
            {
                uyeler.RemoveAll(x => x.uyeNumarasi == _memberNo);
            }
            else
            {
                MessageBox.Show("Üye veritabanında bulunamadı (Zaten silinmiş olabilir).");
                uyeler.RemoveAll(x => x.uyeNumarasi == _memberNo); 
            }
        }

        public static void updateMember(Uye _member)
        {
            int bulunanUye = uyeler.FindIndex(x => x.uyeNumarasi == _member.uyeNumarasi);
            if (bulunanUye != -1)
            {
                uyeler[bulunanUye] = _member;
            }
            SQLIslemleri.updateMember(_member);
        }

        public static void DemoVerileriYukle()
        {
            // Bağlantı kontrolü
            if (SQLIslemleri.globalConnection.State != System.Data.ConnectionState.Open)
                SQLIslemleri.globalConnection.Open();

            using (var transaction = SQLIslemleri.globalConnection.BeginTransaction())
            {
                try
                {
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(SQLIslemleri.globalConnection))
                    {
                        cmd.Transaction = transaction;

                        cmd.CommandText = @"
                    DELETE FROM uyeler; 
                    DELETE FROM sqlite_sequence WHERE name='uyeler';
                    DELETE FROM odeme_bilgileri;
                    DELETE FROM sqlite_sequence WHERE name='odeme_bilgileri';
                ";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                    INSERT INTO uyeler (
                        iceride_mi, paket_no, isim, cinsiyet, telefon_numarasi, adres, 
                        dogum_tarihi, uyelik_tarihi, aciklama, 
                        kronik_hastalik_status, kronik_hastalik_aciklama, 
                        kilo, boy, vucut_yag_orani, hedefler, kilo_hedef, kilo_hedefi_sayi, 
                        vucut_gelistirme, kol_gelistirme, gogus_gelistirme, karin_gelistirme, bacak_gelistirme, kondisyon_arttirma,
                        acil_kisi_isim, acil_kisi_numara, acil_kisi_adres,
                        yasakli_mi, yasaklama_aciklamasi, yasaklama_tarihi,
                        uyelik_dondurma, uyelik_dondurma_baslangic, uyelik_dondurma_bitis
                    ) VALUES 
                    (0, 1, 'Ahmet Yılmaz', 0, '5551000001', 'Merkez Mah.', '1995-05-20', '2024-01-01', 'Düzenli', 0, '', 80, 180, 15, 1, 1, 85, 1, 1, 1, 1, 1, 0, 'Baba', '5559990001', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 2, 'Mehmet Öztürk', 0, '5551000002', 'Yıldız Cad.', '1990-02-10', '2024-02-01', 'Gecikmeli ödüyor', 0, '', 95, 175, 25, 1, 1, 85, 0, 0, 0, 1, 0, 1, 'Eş', '5559990002', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Ayşe Kaya', 1, '5551000003', 'Güneş Sok.', '1998-08-15', '2024-03-01', 'Pilates', 0, '', 55, 165, 20, 1, 0, 0, 0, 0, 0, 1, 1, 1, 'Anne', '5559990003', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Caner Demir', 0, '5551000004', 'Karanfil Sok.', '1992-11-11', '2024-01-10', 'Sorunlu üye', 0, '', 88, 182, 12, 1, 1, 90, 1, 1, 1, 0, 0, 0, 'Abi', '5559990004', 'İş', 1, 'Kavga çıkardı', '2025-10-01', 0, NULL, NULL),
                    (0, 3, 'Elif Çelik', 1, '5551000005', 'Kampüs Yolu', '2003-04-23', '2025-09-01', 'Öğrenci', 0, '', 50, 160, 18, 0, 0, 0, 0, 0, 0, 0, 0, 1, 'Baba', '5559990005', 'Memleket', 0, '', NULL, 0, NULL, NULL),
                    (0, 2, 'Volkan Korkmaz', 0, '5551000006', 'Sanayi Mah.', '1985-07-30', '2023-01-01', 'Eski üye', 1, 'Diz ağrısı', 105, 178, 30, 1, 1, 90, 0, 0, 0, 0, 0, 1, 'Oğul', '5559990006', 'Dükkan', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Zeynep Arslan', 1, '5551000007', 'Lale Apt.', '1996-09-09', '2024-06-15', 'Kardiyo', 0, '', 60, 170, 22, 1, 0, 0, 0, 0, 0, 1, 1, 0, 'Kardeş', '5559990007', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Burak Yıldız', 0, '5551000008', 'Sahil Sitesi', '1999-01-01', '2025-01-01', 'Askerde', 0, '', 75, 180, 10, 1, 1, 80, 1, 1, 1, 1, 0, 0, 'Anne', '5559990008', 'Ev', 0, '', NULL, 1, '2025-12-01', '2026-06-01'),
                    (0, 2, 'Selin Aydın', 1, '5551000009', 'Moda Cad.', '1994-05-05', '2024-05-05', 'Akşam geliyor', 0, '', 58, 168, 21, 0, 0, 0, 0, 0, 0, 1, 1, 1, 'Eş', '5559990009', 'Ofis', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Kerem Beyaz', 0, '5551000010', 'Yeni Mahalle', '2000-12-12', '2025-12-18', 'Yeni kayıt', 0, '', 70, 175, 15, 1, 1, 75, 1, 0, 1, 0, 0, 0, 'Baba', '5559990010', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Hakan Şahin', 0, '5551000011', 'Çarşı', '1988-08-08', '2024-08-01', 'Telefonu açmıyor', 0, '', 90, 185, 20, 1, 0, 0, 1, 1, 1, 0, 0, 0, 'Arkadaş', '5559990011', '-', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Gizem Koç', 1, '5551000012', 'Plaza 2', '1997-02-14', '2024-02-14', 'Sabah grubu', 0, '', 52, 163, 19, 0, 0, 0, 0, 0, 0, 1, 1, 1, 'Anne', '5559990012', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Cemal Taş', 0, '5551000013', 'Arka Sokak', '1980-10-10', '2023-01-01', 'Hırsızlık girişimi', 0, '', 80, 170, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 'Yok', '0000000000', '-', 1, 'Hırsızlık', '2024-01-01', 0, NULL, NULL),
                    (0, 3, 'Deniz Mavi', 1, '5551000014', 'Sahil', '2001-06-01', '2025-06-01', 'Yüzücü', 0, '', 65, 172, 24, 1, 1, 60, 0, 0, 0, 1, 1, 1, 'Baba', '5559990014', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Emre Kara', 0, '5551000015', 'Spor Cad.', '1993-03-03', '2023-03-03', 'Yarışmacı', 0, '', 95, 180, 8, 1, 0, 0, 1, 1, 1, 1, 1, 0, 'Antrenör', '5559990015', 'Salon', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Leyla Gül', 1, '5551000016', 'Gül Apt.', '1999-09-19', '2024-09-01', 'Unutkan', 0, '', 58, 165, 22, 0, 0, 0, 0, 0, 0, 1, 0, 1, 'Eş', '5559990016', 'İş', 0, '', NULL, 0, NULL, NULL),
                    (0, 2, 'Sinan Tekin', 0, '5551000017', 'Tekin Plaza', '1982-12-01', '2022-01-01', 'VIP', 0, '', 85, 178, 20, 1, 0, 0, 1, 1, 1, 1, 1, 1, 'Sekreter', '5559990017', 'Ofis', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Buse Yener', 1, '5551000018', 'Okul Sok.', '2004-04-04', '2025-10-01', 'Öğrenci', 0, '', 50, 160, 18, 0, 0, 0, 0, 0, 0, 0, 1, 1, 'Anne', '5559990018', 'Ev', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Onur Kurt', 0, '5551000019', 'Dağ Yolu', '1991-01-20', '2024-01-20', 'Crossfit', 0, '', 78, 176, 15, 1, 1, 85, 1, 1, 0, 1, 0, 1, 'Baba', '5559990019', 'Köy', 0, '', NULL, 0, NULL, NULL),
                    (0, 1, 'Yağmur Şen', 1, '5551000020', 'Şen Sok.', '1995-05-05', '2024-11-01', 'Ay sonu öder', 0, '', 55, 168, 20, 0, 0, 0, 0, 0, 0, 1, 0, 1, 'Kardeş', '5559990020', 'Ev', 0, '', NULL, 0, NULL, NULL);
                ";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                    INSERT INTO odeme_bilgileri (uye_no, paket_no, donem_baslangici, donem_bitisi, gun, odeme_miktari, odendi_mi) VALUES
                    (1, 1, '2025-12-01', '2026-01-01', 30, '1500.00', 1),
                    (2, 2, '2025-11-15', '2025-12-15', 30, '2000.00', 0),
                    (3, 1, '2025-12-05', '2026-01-05', 30, '1500.00', 1),
                    (5, 3, '2025-09-01', '2026-06-01', 270, '5000.00', 1),
                    (6, 2, '2025-11-01', '2025-12-01', 30, '2000.00', 0),
                    (7, 1, '2025-12-15', '2026-01-15', 30, '1500.00', 1),
                    (9, 2, '2025-12-01', '2026-01-01', 30, '2000.00', 1),
                    (10, 1, '2025-12-18', '2026-01-18', 30, '1500.00', 1),
                    (11, 1, '2025-11-20', '2025-12-20', 30, '1500.00', 0),
                    (12, 1, '2025-12-14', '2026-01-14', 30, '1500.00', 1),
                    (14, 3, '2025-06-01', '2026-06-01', 365, '8000.00', 1),
                    (15, 1, '2025-12-01', '2026-01-01', 30, '1500.00', 1),
                    (16, 1, '2025-11-10', '2025-12-10', 30, '1500.00', 0),
                    (17, 2, '2025-12-01', '2026-12-01', 365, '10000.00', 1),
                    (18, 1, '2025-12-01', '2026-01-01', 30, '1500.00', 1),
                    (19, 1, '2025-12-10', '2026-01-10', 30, '1500.00', 1),
                    (20, 1, '2025-11-18', '2025-12-18', 30, '1500.00', 0);
                ";
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Windows.Forms.MessageBox.Show("Demo verileri yüklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        public static void odemeKontrolu()
        {
            if (SQLIslemleri.globalConnection.State != System.Data.ConnectionState.Open)
                SQLIslemleri.globalConnection.Open();

            foreach (Uye u in Program.uyeler.ToList())
            {
                if (u.paketNo == 0) continue;        
                if (u.yasakliMi) continue;            
                if (u.uyelikDondurma) continue;      

                Paket paketi = Program.paketler.FirstOrDefault(p => p.paketNo == u.paketNo);

                if (paketi == null || paketi.gun <= 0) continue;

                DateTime sonBitisTarihi;

                if (u.odemeGecmisi == null || u.odemeGecmisi.Count == 0)
                {
                    sonBitisTarihi = u.uyelikTarihi;

                    if (sonBitisTarihi.Year < 2000)
                    {
                        sonBitisTarihi = DateTime.Today;
                    }
                }
                else
                {
                    sonBitisTarihi = u.odemeGecmisi.Max(x => x.DonemBitisi);
                }

                bool degisiklikYapildi = false;
                int sayac = 0;

                while (sonBitisTarihi.Date < DateTime.Today)
                {
                    sayac++;

                    if (sayac > 50)
                    {
                        break;
                    }

                    odeme yeniOdeme = new odeme();
                    yeniOdeme.odemeNo = 0; 
                    yeniOdeme.paket = paketi.paketNo;
                    yeniOdeme.odenecekUcret = paketi.paketFiyati;
                    yeniOdeme.gun = paketi.gun;
                    yeniOdeme.odendiMi = false;

                    yeniOdeme.DonemBaslangici = sonBitisTarihi;
                    yeniOdeme.DonemBitisi = sonBitisTarihi.AddDays(paketi.gun);

                    yeniOdeme.sonOdemeTarihi = yeniOdeme.DonemBitisi;

                    string sql = @"INSERT INTO odeme_bilgileri 
                           (uye_no, paket_no, donem_baslangici, donem_bitisi, gun, odeme_miktari, odendi_mi) 
                           VALUES 
                           (@uyeNo, @paketNo, @bas, @bit, @gun, @miktar, 0);
                           SELECT last_insert_rowid();";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, SQLIslemleri.globalConnection))
                    {
                        cmd.Parameters.AddWithValue("@uyeNo", u.uyeNumarasi);
                        cmd.Parameters.AddWithValue("@paketNo", yeniOdeme.paket);
                        cmd.Parameters.AddWithValue("@bas", yeniOdeme.DonemBaslangici);
                        cmd.Parameters.AddWithValue("@bit", yeniOdeme.DonemBitisi);
                        cmd.Parameters.AddWithValue("@gun", yeniOdeme.gun);

                        cmd.Parameters.AddWithValue("@miktar", yeniOdeme.odenecekUcret.ToString(System.Globalization.CultureInfo.InvariantCulture));

                        long yeniID = (long)cmd.ExecuteScalar();
                        yeniOdeme.odemeNo = (int)yeniID;
                    }

                    if (u.odemeGecmisi == null) u.odemeGecmisi = new List<odeme>();
                    u.odemeGecmisi.Add(yeniOdeme);

                    sonBitisTarihi = yeniOdeme.DonemBitisi;
                    degisiklikYapildi = true;
                }

                if (degisiklikYapildi)
                {
                    Program.updateMember(u);
                }
            }
        }

        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        [STAThread]
        static void Main()
        {
            SQLIslemleri.startSqlIslemleri();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new yonetimPaneli());
        }
    }

    public static class SQLIslemleri
    {
        public static SQLiteConnection globalConnection;
        public static string dbPath = ".\\gym.db";

        public static DateTime stringToDateTime(string dateTimeString)
        {
            string dateTimeFormat = "yyyy-MM-dd";

            DateTime t = new DateTime();
            if (!DateTime.TryParseExact(dateTimeString, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out t))
            {
                Debug.WriteLine("Tarih parse hatasi!");
            }
            return t;
        }
        public static int b(bool a) { return a ? 1 : 0; }

        public static void startSqlIslemleri()
        {
            //SQLIslemleri.createSqliteFileIfNotExist();
            globalConnection = new SQLiteConnection("Data Source=gym.db;Version=3");
            try
            {
                globalConnection.Open();
            }
            catch
            {
                MessageBox.Show("Bir sorun oluştu. Lütfen daha sonra tekrar deneyin.");
                Application.Exit();
            }
            Program.paketler = getAllPackageTypes();
            Program.uyeler = getMembers();


        }

        public static void createSqliteFileIfNotExist()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (SQLiteConnection _tmp_connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                _tmp_connection.Open();
                // 1. ÜYELER TABLOSU
                string createUyelerTableQuery = @"CREATE TABLE IF NOT EXISTS uyeler (no INTEGER PRIMARY KEY AUTOINCREMENT, iceride_mi INTEGER, paket_no INTEGER, isim TEXT, cinsiyet INTEGER, telefon_numarasi TEXT, adres TEXT, dogum_tarihi TEXT, aciklama TEXT, kronik_hastalik_status INTEGER, kronik_hastalik_aciklama TEXT, kilo TEXT, boy TEXT, vucut_yag_orani TEXT, hedefler INTEGER, kilo_hedef INTEGER, kilo_hedefi_sayi TEXT, vucut_gelistirme INTEGER, kol_gelistirme INTEGER, gogus_gelistirme INTEGER, karin_gelistirme INTEGER, bacak_gelistirme INTEGER, kondisyon_arttirma INTEGER, acil_kisi_isim TEXT, acil_kisi_numara TEXT, acil_kisi_adres TEXT, uyelik_tarihi TEXT, yasakli_mi INTEGER, yasaklama_aciklamasi TEXT, yasaklama_tarihi TEXT, uyelik_dondurma INTEGER, uyelik_dondurma_baslangic TEXT, uyelik_dondurma_bitis TEXT);";
                // 2. PAKETLER TABLOSU
                string createPaketlerTableQuery = "CREATE TABLE \"paketler\" (\"paket_no\"INTEGER UNIQUE,\"paket_ismi\"\tTEXT,\"paket_fiyati\"TEXT,\"aktif_mi\"INTEGER,\"yonlendirme\"INTEGER, \"gun\" INTEGER,PRIMARY KEY(\"paket_no\"));";
                // 3. ÖDEME BİLGİLERİ TABLOSU
                string createOdemeTableQuery = "CREATE TABLE IF NOT EXISTS odeme_bilgileri (\"odeme_no\" INTEGER UNIQUE, \"uye_no\" INTEGER, \"paket_no\" INTEGER, \"donem_baslangici\" TEXT, \"donem_bitisi\" TEXT, \"gun\" INTEGER, \"son_odeme_tarihi\" TEXT, \"odendi_mi\" INTEGER, PRIMARY KEY(\"odeme_no\" AUTOINCREMENT));";

                using (SQLiteCommand cmd = new SQLiteCommand(createUyelerTableQuery, _tmp_connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(createPaketlerTableQuery, _tmp_connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(createOdemeTableQuery, _tmp_connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int getLastNumberOfMembers()
        {
            string commandTemplate = "SELECT IFNULL(MAX(no), 0) FROM uyeler";
            Object sonuc;
            using (SQLiteCommand command = new SQLiteCommand(commandTemplate, globalConnection))
            {
                sonuc = command.ExecuteScalar();
            }
            int dondurulecekDeger = 0;
            if (sonuc != null && sonuc != DBNull.Value)
            {
                try
                {
                    dondurulecekDeger = Convert.ToInt32(sonuc);
                }
                catch
                {
                    return -1;
                }
            }
            return dondurulecekDeger;
        }

        public static List<Uye> getMembers()
        {
            List<Uye> uyeListesi = new List<Uye>();

            // Bağlantı kontrolü
            if (globalConnection.State != System.Data.ConnectionState.Open)
                globalConnection.Open();

            string sql = "SELECT * FROM uyeler";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, globalConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Uye u = new Uye();

                        // Güvenli Integer Çevrimleri
                        u.uyeNumarasi = Convert.ToInt32(reader["no"]);
                        u.uyeIcerideMi = Convert.ToInt32(reader["iceride_mi"]) == 1; // 1 ise True
                        u.paketNo = Convert.ToInt32(reader["paket_no"]);
                        u.isim = reader["isim"].ToString();
                        u.cinsiyet = Convert.ToInt32(reader["cinsiyet"]);
                        u.telefonNumarasi = reader["telefon_numarasi"].ToString();
                        u.adres = reader["adres"].ToString();

                        // Tarihleri Güvenli Çekme (0001 Hatasına Karşı)
                        u.dogumTarihi = GuvenliTarihCevir(reader["dogum_tarihi"]);
                        u.uyelikTarihi = GuvenliTarihCevir(reader["uyelik_tarihi"]);

                        u.aciklama = reader["aciklama"].ToString();
                        u.kronikHastalikVarMi = Convert.ToInt32(reader["kronik_hastalik_status"]) == 1;
                        u.kronikHastalikAciklama = reader["kronik_hastalik_aciklama"].ToString();

                        // Sayısal Veriler (Boş gelme ihtimaline karşı 0 atama)
                        u.Kilo = reader["kilo"] != DBNull.Value ? Convert.ToDouble(reader["kilo"]) : 0;
                        u.Boy = reader["boy"] != DBNull.Value ? Convert.ToDouble(reader["boy"]) : 0;
                        u.vucutYagOrani = reader["vucut_yag_orani"] != DBNull.Value ? Convert.ToDouble(reader["vucut_yag_orani"]) : 0;

                        // Hedefler (SQLite'da bool yok, int 1/0 tutuyorsun muhtemelen)
                        u.hedefler = Convert.ToInt32(reader["hedefler"]) == 1;
                        u.kiloHedef = Convert.ToInt32(reader["kilo_hedef"]) == 1;
                        u.kiloHedefSayi = reader["kilo_hedefi_sayi"] != DBNull.Value ? Convert.ToInt32(reader["kilo_hedefi_sayi"]) : 0;

                        u.vucutGelistirme = Convert.ToInt32(reader["vucut_gelistirme"]) == 1;
                        u.kolGelistirme = Convert.ToInt32(reader["kol_gelistirme"]) == 1;
                        u.gogusGelistirme = Convert.ToInt32(reader["gogus_gelistirme"]) == 1;
                        u.karinGelistirme = Convert.ToInt32(reader["karin_gelistirme"]) == 1;
                        u.bacakGelistirme = Convert.ToInt32(reader["bacak_gelistirme"]) == 1;
                        u.kondisyonArttirma = Convert.ToInt32(reader["kondisyon_arttirma"]) == 1;

                        u.acilKisiIsim = reader["acil_kisi_isim"].ToString();
                        u.acilKisiNumara = reader["acil_kisi_numara"].ToString();
                        u.acilKisiAdres = reader["acil_kisi_adres"].ToString();

                        u.yasakliMi = Convert.ToInt32(reader["yasakli_mi"]) == 1;
                        u.yasaklamaAciklamasi = reader["yasaklama_aciklamasi"].ToString();

                        u.yasaklamaTarihi = GuvenliTarihCevir(reader["yasaklama_tarihi"]);

                        u.uyelikDondurma = Convert.ToInt32(reader["uyelik_dondurma"]) == 1;
                        u.uyelikDondurmaBaslangic = GuvenliTarihCevir(reader["uyelik_dondurma_baslangic"]);
                        u.uyelikDondurmaBitis = GuvenliTarihCevir(reader["uyelik_dondurma_bitis"]);

                       
                        uyeListesi.Add(u);
                    }
                } 
            }

            foreach (Uye u in uyeListesi)
            {
                // Ödeme listesini başlat
                u.odemeGecmisi = new List<odeme>();

                string odemeSql = "SELECT * FROM odeme_bilgileri WHERE uye_no=@uye_no";

                using (SQLiteCommand odemeCmd = new SQLiteCommand(odemeSql, globalConnection))
                {
                    odemeCmd.Parameters.AddWithValue("@uye_no", u.uyeNumarasi);

                    using (SQLiteDataReader odemeReader = odemeCmd.ExecuteReader())
                    {
                        while (odemeReader.Read())
                        {
                            odeme o = new odeme();
                            o.odemeNo = Convert.ToInt32(odemeReader["odeme_no"]);
                            o.paket = Convert.ToInt32(odemeReader["paket_no"]);

                            // Tarihleri Helper ile Çek
                            o.DonemBaslangici = GuvenliTarihCevir(odemeReader["donem_baslangici"]);
                            o.DonemBitisi = GuvenliTarihCevir(odemeReader["donem_bitisi"]);

                            // Gun null gelebilir mi? Kontrol edelim.
                            o.gun = odemeReader["gun"] != DBNull.Value ? Convert.ToInt32(odemeReader["gun"]) : 30;

                            // PARA BİRİMİ DÜZELTMESİ (Invariant Culture)
                            string paraStr = odemeReader["odeme_miktari"].ToString();
                            double para;
                            // Nokta/Virgül farketmeksizin okumaya çalışır
                            if (double.TryParse(paraStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out para))
                                o.odenecekUcret = para;
                            else
                                o.odenecekUcret = 0;

                            o.odendiMi = Convert.ToInt32(odemeReader["odendi_mi"]) == 1;

                            // Son ödeme tarihi yoksa dönem bitişini verelim
                            o.sonOdemeTarihi = o.DonemBitisi;

                            u.odemeGecmisi.Add(o);
                        }
                    }
                }
            }

            return uyeListesi;
        }

        private static DateTime GuvenliTarihCevir(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value) return DateTime.MinValue;

            DateTime sonuc;
            if (DateTime.TryParse(dbValue.ToString(), out sonuc))
            {
                if (sonuc.Year < 2000) return DateTime.MinValue;
                return sonuc;
            }

            return DateTime.MinValue;
        }

        private static string TarihFormatla(DateTime dt)
        {
            if (dt == DateTime.MinValue)
            {
                return "2000-01-01"; 
            }

            return dt.ToString("yyyy-MM-dd");
        }

        public static int addMember(Uye _member)
        {
            string sql = @"INSERT INTO uyeler (iceride_mi, paket_no,isim, cinsiyet, telefon_numarasi, adres, dogum_tarihi, aciklama, kronik_hastalik_status, kronik_hastalik_aciklama, kilo, boy, vucut_yag_orani, hedefler, kilo_hedef, kilo_hedefi_sayi, 
                        vucut_gelistirme, kol_gelistirme,  gogus_gelistirme, karin_gelistirme, bacak_gelistirme, kondisyon_arttirma, acil_kisi_isim, acil_kisi_numara, acil_kisi_adres, uyelik_tarihi,  yasakli_mi, yasaklama_aciklamasi,
                        yasaklama_tarihi, uyelik_dondurma,  uyelik_dondurma_baslangic, uyelik_dondurma_bitis) 
                        VALUES (@iceride_mi, @paket_no,@isim, @cinsiyet, @telefon_numarasi, @adres, @dogum_tarihi, @aciklama, @kronik_hastalik_status, @kronik_hastalik_aciklama, @kilo, @boy, @vucut_yag_orani, @hedefler, @kilo_hedef,
                        @kilo_hedefi_sayi, @vucut_gelistirme, @kol_gelistirme,  @gogus_gelistirme, @karin_gelistirme, @bacak_gelistirme, @kondisyon_arttirma, @acil_kisi_isim, @acil_kisi_numara, @acil_kisi_adres, @uyelik_tarihi,  
                        @yasakli_mi, @yasaklama_aciklamasi, @yasaklama_tarihi, @uyelik_dondurma,  @uyelik_dondurma_baslangic, @uyelik_dondurma_bitis);";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, globalConnection))
            {
                cmd.Parameters.AddWithValue("@iceride_mi", b(_member.uyeIcerideMi));
                cmd.Parameters.AddWithValue("@paket_no", _member.paketNo);
                cmd.Parameters.AddWithValue("@isim", _member.isim);
                cmd.Parameters.AddWithValue("@cinsiyet", _member.cinsiyet);
                cmd.Parameters.AddWithValue("@telefon_numarasi", _member.telefonNumarasi);
                cmd.Parameters.AddWithValue("@adres", _member.adres);

                // --- TARİH KONTROLLERİ BURADA YAPILIYOR ---
                cmd.Parameters.AddWithValue("@dogum_tarihi", TarihFormatla(_member.dogumTarihi));

                cmd.Parameters.AddWithValue("@aciklama", _member.aciklama);
                cmd.Parameters.AddWithValue("@kronik_hastalik_status", b(_member.kronikHastalikVarMi));
                cmd.Parameters.AddWithValue("@kronik_hastalik_aciklama", _member.kronikHastalikAciklama);
                cmd.Parameters.AddWithValue("@kilo", _member.Kilo);
                cmd.Parameters.AddWithValue("@boy", _member.Boy);
                cmd.Parameters.AddWithValue("@vucut_yag_orani", _member.vucutYagOrani);
                cmd.Parameters.AddWithValue("@hedefler", b(_member.hedefler));
                cmd.Parameters.AddWithValue("@kilo_hedef", b(_member.kiloHedef));
                cmd.Parameters.AddWithValue("@kilo_hedefi_sayi", _member.kiloHedefSayi);
                cmd.Parameters.AddWithValue("@vucut_gelistirme", b(_member.vucutGelistirme));
                cmd.Parameters.AddWithValue("@kol_gelistirme", b(_member.kolGelistirme));
                cmd.Parameters.AddWithValue("@gogus_gelistirme", b(_member.gogusGelistirme));
                cmd.Parameters.AddWithValue("@karin_gelistirme", b(_member.karinGelistirme));
                cmd.Parameters.AddWithValue("@bacak_gelistirme", b(_member.bacakGelistirme));
                cmd.Parameters.AddWithValue("@kondisyon_arttirma", b(_member.kondisyonArttirma));
                cmd.Parameters.AddWithValue("@acil_kisi_isim", _member.acilKisiIsim);
                cmd.Parameters.AddWithValue("@acil_kisi_numara", _member.acilKisiNumara);
                cmd.Parameters.AddWithValue("@acil_kisi_adres", _member.acilKisiAdres);

                // --- TARİH KONTROLLERİ ---
                cmd.Parameters.AddWithValue("@uyelik_tarihi", TarihFormatla(_member.uyelikTarihi));

                cmd.Parameters.AddWithValue("@yasakli_mi", b(_member.yasakliMi));
                cmd.Parameters.AddWithValue("@yasaklama_aciklamasi", _member.yasaklamaAciklamasi);

                // --- TARİH KONTROLLERİ ---
                cmd.Parameters.AddWithValue("@yasaklama_tarihi", TarihFormatla(_member.yasaklamaTarihi));

                cmd.Parameters.AddWithValue("@uyelik_dondurma", b(_member.uyelikDondurma));

                // --- TARİH KONTROLLERİ ---
                cmd.Parameters.AddWithValue("@uyelik_dondurma_baslangic", TarihFormatla(_member.uyelikDondurmaBaslangic));
                cmd.Parameters.AddWithValue("@uyelik_dondurma_bitis", TarihFormatla(_member.uyelikDondurmaBitis));

                // Kaydı işle
                cmd.ExecuteNonQuery();

                // Yeni oluşan ID'yi döndür
                cmd.CommandText = "SELECT last_insert_rowid()";
                long yeniId = (long)cmd.ExecuteScalar();
                return Convert.ToInt32(yeniId);
            }
        }

        public static bool updateMember(Uye _member)
        {
            // DÜZELTME 1: WHERE öncesine boşluk eklendi.
            // DÜZELTME 2: kilo_hedef_sayi -> kilo_hedefi_sayi yapıldı (Insert kodunla uyumlu olsun diye)
            string sql = @"UPDATE uyeler SET 
            iceride_mi = @iceride_mi, paket_no = @paket_no, isim = @isim, cinsiyet = @cinsiyet, telefon_numarasi = @telefon_numarasi, adres = @adres, dogum_tarihi = @dogum_tarihi, aciklama = @aciklama, kronik_hastalik_status = @kronik_hastalik_status,
            kronik_hastalik_aciklama = @kronik_hastalik_aciklama, kilo = @kilo, boy = @boy, vucut_yag_orani = @vucut_yag_orani, hedefler = @hedefler, kilo_hedef = @kilo_hedef, kilo_hedefi_sayi = @kilo_hedefi_sayi, vucut_gelistirme = @vucut_gelistirme,
            kol_gelistirme = @kol_gelistirme, gogus_gelistirme = @gogus_gelistirme, karin_gelistirme = @karin_gelistirme, bacak_gelistirme = @bacak_gelistirme, kondisyon_arttirma = @kondisyon_arttirma, acil_kisi_isim = @acil_kisi_isim, 
            acil_kisi_numara = @acil_kisi_numara, acil_kisi_adres = @acil_kisi_adres, uyelik_tarihi = @uyelik_tarihi, yasakli_mi = @yasakli_mi, yasaklama_aciklamasi = @yasaklama_aciklamasi, yasaklama_tarihi = @yasaklama_tarihi, uyelik_dondurma = @uyelik_dondurma, 
            uyelik_dondurma_baslangic = @uyelik_dondurma_baslangic, uyelik_dondurma_bitis = @uyelik_dondurma_bitis 
            WHERE No = @no";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, globalConnection))
            {
                cmd.Parameters.AddWithValue("@no", _member.uyeNumarasi);

                cmd.Parameters.AddWithValue("@iceride_mi", b(_member.uyeIcerideMi));
                cmd.Parameters.AddWithValue("@paket_no", _member.paketNo);
                cmd.Parameters.AddWithValue("@isim", _member.isim);
                cmd.Parameters.AddWithValue("@cinsiyet", _member.cinsiyet);
                cmd.Parameters.AddWithValue("@telefon_numarasi", _member.telefonNumarasi);
                cmd.Parameters.AddWithValue("@adres", _member.adres);

                // DÜZELTME 3: TarihFormatla fonksiyonu kullanıldı
                cmd.Parameters.AddWithValue("@dogum_tarihi", TarihFormatla(_member.dogumTarihi));

                cmd.Parameters.AddWithValue("@aciklama", _member.aciklama);
                cmd.Parameters.AddWithValue("@kronik_hastalik_status", b(_member.kronikHastalikVarMi));
                cmd.Parameters.AddWithValue("@kronik_hastalik_aciklama", _member.kronikHastalikAciklama);
                cmd.Parameters.AddWithValue("@kilo", _member.Kilo);
                cmd.Parameters.AddWithValue("@boy", _member.Boy);
                cmd.Parameters.AddWithValue("@vucut_yag_orani", _member.vucutYagOrani);

                // DÜZELTME 4: Boolean 'b' wrapper eklendi
                cmd.Parameters.AddWithValue("@hedefler", b(_member.hedefler));
                cmd.Parameters.AddWithValue("@kilo_hedef", b(_member.kiloHedef));

                // DÜZELTME 5: Parametre ismi SQL ile eşitlendi (@kilo_hedefi_sayi)
                cmd.Parameters.AddWithValue("@kilo_hedefi_sayi", _member.kiloHedefSayi);

                cmd.Parameters.AddWithValue("@vucut_gelistirme", b(_member.vucutGelistirme));
                cmd.Parameters.AddWithValue("@kol_gelistirme", b(_member.kolGelistirme));
                cmd.Parameters.AddWithValue("@gogus_gelistirme", b(_member.gogusGelistirme));
                cmd.Parameters.AddWithValue("@karin_gelistirme", b(_member.karinGelistirme));
                cmd.Parameters.AddWithValue("@bacak_gelistirme", b(_member.bacakGelistirme));
                cmd.Parameters.AddWithValue("@kondisyon_arttirma", b(_member.kondisyonArttirma));
                cmd.Parameters.AddWithValue("@acil_kisi_isim", _member.acilKisiIsim);
                cmd.Parameters.AddWithValue("@acil_kisi_numara", _member.acilKisiNumara);
                cmd.Parameters.AddWithValue("@acil_kisi_adres", _member.acilKisiAdres);

                // Tarih formatlama
                cmd.Parameters.AddWithValue("@uyelik_tarihi", TarihFormatla(_member.uyelikTarihi));

                cmd.Parameters.AddWithValue("@yasakli_mi", b(_member.yasakliMi));
                cmd.Parameters.AddWithValue("@yasaklama_aciklamasi", _member.yasaklamaAciklamasi);
                cmd.Parameters.AddWithValue("@yasaklama_tarihi", TarihFormatla(_member.yasaklamaTarihi));

                // DÜZELTME 4: Boolean 'b' wrapper eklendi
                cmd.Parameters.AddWithValue("@uyelik_dondurma", b(_member.uyelikDondurma));

                // DÜZELTME 3: Tarih formatlama eklendi (Direkt nesne vermek yerine)
                cmd.Parameters.AddWithValue("@uyelik_dondurma_baslangic", TarihFormatla(_member.uyelikDondurmaBaslangic));
                cmd.Parameters.AddWithValue("@uyelik_dondurma_bitis", TarihFormatla(_member.uyelikDondurmaBitis));

                // Etkilenen kayıt sayısını kontrol et
                int sonuc = cmd.ExecuteNonQuery();

                return sonuc > 0;
            }
        }

        public static bool deleteMember(int _memberNo)
        {
            string sql = "DELETE FROM uyeler WHERE no = @no;";
            if (globalConnection.State != System.Data.ConnectionState.Open) return false;
            
            using (SQLiteCommand cmd = new SQLiteCommand(sql, globalConnection))
            {
                cmd.Parameters.AddWithValue("@no", _memberNo);
                int etkilenenSatir = cmd.ExecuteNonQuery();
                return etkilenenSatir > 0;
            }
        }

        public static Uye getMember(int id)
        {
            Uye m = null;
            string sql = "SELECT * FROM uyeler WHERE no = @no";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, globalConnection))
            {
                cmd.Parameters.AddWithValue("@no", id);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        m = new Uye();

                        m.uyeNumarasi = Convert.ToInt32(reader["no"]);

                        m.uyeIcerideMi = Convert.ToBoolean(reader["iceride_mi"]);
                        m.paketNo = Convert.ToInt32(reader["paket_no"]);
                        m.isim = reader["isim"].ToString();
                        m.cinsiyet = Convert.ToInt32(reader["cinsiyet"]);
                        m.telefonNumarasi = reader["telefon_numarasi"].ToString();
                        m.adres = reader["adres"].ToString();

                        m.dogumTarihi = Convert.ToDateTime(reader["dogum_tarihi"]);
                        m.uyelikTarihi = Convert.ToDateTime(reader["uyelik_tarihi"]);

                        m.aciklama = reader["aciklama"].ToString();
                        m.kronikHastalikVarMi = Convert.ToBoolean(reader["kronik_hastalik_status"]);
                        m.kronikHastalikAciklama = reader["kronik_hastalik_aciklama"].ToString();

                        m.Kilo = Convert.ToDouble(reader["kilo"]);
                        m.Boy = Convert.ToDouble(reader["boy"]);
                        m.vucutYagOrani = Convert.ToDouble(reader["vucut_yag_orani"]);

                        m.hedefler = Convert.ToBoolean(reader["hedefler"]);
                        m.kiloHedef = Convert.ToBoolean(reader["kilo_hedef"]);
                        m.kiloHedefSayi = Convert.ToInt32(reader["kilo_hedef_sayi"]);

                        m.vucutGelistirme = Convert.ToBoolean(reader["vucut_gelistirme"]);
                        m.kolGelistirme = Convert.ToBoolean(reader["kol_gelistirme"]);
                        m.gogusGelistirme = Convert.ToBoolean(reader["gogus_gelistirme"]);
                        m.karinGelistirme = Convert.ToBoolean(reader["karin_gelistirme"]);
                        m.bacakGelistirme = Convert.ToBoolean(reader["bacak_gelistirme"]);
                        m.kondisyonArttirma = Convert.ToBoolean(reader["kondisyon_arttirma"]);

                        m.acilKisiIsim = reader["acil_kisi_isim"].ToString();
                        m.acilKisiNumara = reader["acil_kisi_numara"].ToString();
                        m.acilKisiAdres = reader["acil_kisi_adres"].ToString();

                        m.yasakliMi = Convert.ToBoolean(reader["yasakli_mi"]);
                        m.yasaklamaAciklamasi = reader["yasaklama_aciklamasi"].ToString();

                        if (reader["yasaklama_tarihi"] != DBNull.Value)
                            m.yasaklamaTarihi = Convert.ToDateTime(reader["yasaklama_tarihi"]);

                        m.uyelikDondurma = Convert.ToBoolean(reader["uyelik_dondurma"]);

                        if (reader["uyelik_dondurma_baslangic"] != DBNull.Value)
                            m.uyelikDondurmaBaslangic = Convert.ToDateTime(reader["uyelik_dondurma_baslangic"]);

                        if (reader["uyelik_dondurma_bitis"] != DBNull.Value)
                            m.uyelikDondurmaBitis = Convert.ToDateTime(reader["uyelik_dondurma_bitis"]);
                        m.empty = false;

                        // odeme listini olustur
                        string odemeCommandTemplate = "SELECT * FROM odeme_bilgileri WHERE no=@uye_no";
                        using (SQLiteCommand odemeCmd = new SQLiteCommand(odemeCommandTemplate, globalConnection))
                        {
                            odemeCmd.Parameters.AddWithValue("@uye_no", reader["uye_no"]);
                            using (SQLiteDataReader odemeReader = odemeCmd.ExecuteReader())
                            {
                                while (odemeReader.Read())
                                {
                                    odeme o = new odeme();
                                    o.odemeNo = Convert.ToInt32(odemeReader["odeme_no"]);
                                    o.paket = Convert.ToInt32(odemeReader["paket_no"]);
                                    o.DonemBaslangici = stringToDateTime(odemeReader["donem_baslangici"].ToString());
                                    o.DonemBitisi = stringToDateTime(odemeReader["donem_bitisi"].ToString());
                                    o.gun = Convert.ToInt32(odemeReader["gun"]);
                                    o.odenecekUcret = Convert.ToDouble(odemeReader["odeme_miktari"]);
                                    o.odendiMi = (Convert.ToInt32(odemeReader["odendi_mi"]) == 1) ? true : false;
                                    m.odemeGecmisi.Add(o);
                                }
                            }
                        }
                    }
                }
            }
            return m;
        }

        public static void addPaymentInfo(Uye u, bool odendiMi = false, DateTime baslangicTarihi = new DateTime(), int paketNo = -1)
        {
            odeme o = new odeme();
            if (paketNo == -1)
                paketNo = u.paketNo;
            else u.paketNo = paketNo;

            if (Program.paketler[Program.paketler.FindIndex(x => x.paketNo == u.paketNo)].aktifMi == false)
                paketNo = Program.paketler[Program.paketler.FindIndex(x => x.paketNo == u.paketNo)].yonlendirme;
            else
                paketNo = u.paketNo;

            o.paket = paketNo;
            o.DonemBaslangici = DateTime.Now;
            o.gun = Program.paketler[Program.paketler.FindIndex(x => x.paketNo == paketNo)].gun;
            o.DonemBitisi = o.DonemBaslangici.AddDays(o.gun);
            o.odenecekUcret = Program.paketler[Program.paketler.FindIndex(x => x.paketNo == paketNo)].paketFiyati;
            o.odendiMi = odendiMi;

            string addPaymentInfoCommandTemplate = "INSERT INTO odeme_bilgileri (uye_no, paket_no, donem_baslanngici, donem_bitisi, gun, odeme_miktari, odendi_mi) VALUES (@uye_no, @paket_no, @donem_baslangici, @donem_bitisi, @gun, @odeme_miktari, @odendi_mi);";
            using (SQLiteCommand addPaymentInfoCommand = new SQLiteCommand(addPaymentInfoCommandTemplate, globalConnection))
            {
                addPaymentInfoCommand.Parameters.AddWithValue("@uye_no", u.uyeNumarasi);
                addPaymentInfoCommand.Parameters.AddWithValue("@paket_no", o.paket);
                addPaymentInfoCommand.Parameters.AddWithValue("@donem_baslangici", o.DonemBaslangici.ToString("yyyy-MM-dd"));
                addPaymentInfoCommand.Parameters.AddWithValue("@donem_bitisi", o.DonemBitisi.ToString("yyyy-MM-dd"));
                addPaymentInfoCommand.Parameters.AddWithValue("@gun", o.gun);
                addPaymentInfoCommand.Parameters.AddWithValue("@odeme_miktari", o.odenecekUcret);
                addPaymentInfoCommand.Parameters.AddWithValue("@odendi_mi", o.odendiMi);
                addPaymentInfoCommand.ExecuteNonQuery();
            }
            u.odemeGecmisi.Add(o);
        }

        public static List<Uye> searchInMembers(string _keyword)
        {
            List<Uye> results = new List<Uye>();
            results.AddRange(Program.uyeler.Where(x => x.uyeNumarasi.ToString().Contains(_keyword)).ToList());
            results.AddRange(Program.uyeler.Where(x => x.isim.Contains(_keyword)).ToList());
            results.AddRange(Program.uyeler.Where(x => x.adres.Contains(_keyword)).ToList());

            return results;
        }

        public static List<Paket> getAllPackageTypes()
        {
            List<Paket> tmp_paketler = new List<Paket>();

            string paketler_tmp_template = "SELECT * FROM paketler";
            using (SQLiteCommand cmd = new SQLiteCommand(paketler_tmp_template, globalConnection))
            {
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Paket tmp_pkt = new Paket();
                    tmp_pkt.paketNo = Convert.ToInt32(rdr["paket_no"]);
                    tmp_pkt.paketIsmi = rdr["paket_ismi"].ToString();
                    tmp_pkt.paketFiyati = Convert.ToDouble(rdr["paket_fiyati"], System.Globalization.CultureInfo.InvariantCulture);
                    tmp_pkt.aktifMi = (Convert.ToInt32(rdr["aktif_mi"]) == 1) ? true : false;
                    tmp_pkt.yonlendirme = Convert.ToInt32(rdr["yonlendirme"]);
                    tmp_pkt.gun = Convert.ToInt32(rdr["gun"]);
                    tmp_paketler.Add(tmp_pkt);
                }
            }
            return tmp_paketler;
        }

        public static int getLastNumberOfPackets()
        {
            string commandTemplate = "SELECT IFNULL(MAX(paket_no), 0) FROM paketler";
            Object sonuc;
            using (SQLiteCommand command = new SQLiteCommand(commandTemplate, globalConnection))
            {
                sonuc = command.ExecuteScalar();
            }
            int dondurulecekDeger = 0;
            if (sonuc != null && sonuc != DBNull.Value)
            {
                try
                {
                    dondurulecekDeger = Convert.ToInt32(sonuc);
                }
                catch
                {
                    return -1;
                }
            }
            return dondurulecekDeger;
        }

    }

        public static class UyeConfigurations
        {
            public const int CINSIYET_ERKEK = 0;
            public const int CINSIYET_KADIN = 1;

            public const int ODEME_YAPILMADI = 0;
            public const int ODEME_YAPILDI = 1;

            public static string dateFormat = "dd.MM.yyyy";

            public static Uye EMPTY_MEMBER = new Uye();
            static UyeConfigurations()
            {
                EMPTY_MEMBER.empty = true;
            }
        }

        public class Uye
        {
            public int uyeNumarasi;
            public bool uyeIcerideMi = false;
            // uyelik bilgileri
            public int paketNo;
            public string isim;
            public int cinsiyet;
            public string telefonNumarasi;
            public string adres;
            public DateTime dogumTarihi;
            public string aciklama;
            public bool kronikHastalikVarMi = false;
            public string kronikHastalikAciklama;

            public double Kilo;
            public double Boy;
            public double vucutYagOrani;

            //uye hedefleri
            public bool hedefler = false;
            public bool kiloHedef = false;
            public int kiloHedefSayi;

            public bool vucutGelistirme = false;
            public bool kolGelistirme = false;
            public bool gogusGelistirme = false;
            public bool karinGelistirme = false;
            public bool bacakGelistirme = false;
            public bool kondisyonArttirma = false;

            //acil durum kisisi
            public string acilKisiIsim { get; set; }
            public string acilKisiNumara { get; set; }
            public string acilKisiAdres { get; set; }

            // uyelik bilgileri
            public DateTime uyelikTarihi;
            public bool yasakliMi;
            public string yasaklamaAciklamasi;
            public DateTime yasaklamaTarihi;
            public bool uyelikDondurma;
            public DateTime uyelikDondurmaBaslangic;
            public DateTime uyelikDondurmaBitis;
            public List<odeme> odemeGecmisi = new List<odeme>();

            public bool empty = true;

            public void kopyala(Uye hedefUye)
        {
            // Eğer hedef kutu yoksa (null ise) işlem yapamayız
            if (hedefUye == null) return;

            // --- TEMEL BİLGİLER (Ben -> Hedef) ---
            hedefUye.uyeNumarasi = this.uyeNumarasi;
            hedefUye.uyeIcerideMi = this.uyeIcerideMi;

            hedefUye.paketNo = this.paketNo;
            hedefUye.isim = this.isim;
            hedefUye.cinsiyet = this.cinsiyet;
            hedefUye.telefonNumarasi = this.telefonNumarasi;
            hedefUye.adres = this.adres;
            hedefUye.dogumTarihi = this.dogumTarihi;
            hedefUye.aciklama = this.aciklama;

            // --- SAĞLIK BİLGİLERİ ---
            hedefUye.kronikHastalikVarMi = this.kronikHastalikVarMi;
            hedefUye.kronikHastalikAciklama = this.kronikHastalikAciklama;
            hedefUye.Kilo = this.Kilo;
            hedefUye.Boy = this.Boy;
            hedefUye.vucutYagOrani = this.vucutYagOrani;

            // --- HEDEFLER ---
            hedefUye.hedefler = this.hedefler;
            hedefUye.kiloHedef = this.kiloHedef;
            hedefUye.kiloHedefSayi = this.kiloHedefSayi;
            hedefUye.vucutGelistirme = this.vucutGelistirme;
            hedefUye.kolGelistirme = this.kolGelistirme;
            hedefUye.gogusGelistirme = this.gogusGelistirme;
            hedefUye.karinGelistirme = this.karinGelistirme;
            hedefUye.bacakGelistirme = this.bacakGelistirme;
            hedefUye.kondisyonArttirma = this.kondisyonArttirma;

            // --- ACİL DURUM KİŞİSİ ---
            hedefUye.acilKisiIsim = this.acilKisiIsim;
            hedefUye.acilKisiNumara = this.acilKisiNumara;
            hedefUye.acilKisiAdres = this.acilKisiAdres;

            // --- ÜYELİK DURUMLARI ---
            hedefUye.uyelikTarihi = this.uyelikTarihi;
            hedefUye.yasakliMi = this.yasakliMi;
            hedefUye.yasaklamaAciklamasi = this.yasaklamaAciklamasi;
            hedefUye.yasaklamaTarihi = this.yasaklamaTarihi;
            hedefUye.uyelikDondurma = this.uyelikDondurma;
            hedefUye.uyelikDondurmaBaslangic = this.uyelikDondurmaBaslangic;
            hedefUye.uyelikDondurmaBitis = this.uyelikDondurmaBitis;

            hedefUye.empty = this.empty;

            if (this.odemeGecmisi != null)
            {
                hedefUye.odemeGecmisi = this.odemeGecmisi.Select(x => x.KopyaOlustur()).ToList();
            }
            else
            {
                hedefUye.odemeGecmisi = new List<odeme>();
            }
        }
        }

        public class odeme
        {
            public int odemeNo;
            public int paket;
            public double odenecekUcret;
            public DateTime DonemBaslangici;
            public DateTime DonemBitisi;
            public int gun;
            public DateTime sonOdemeTarihi;
            public bool odendiMi;

            public odeme KopyaOlustur()
            {
                return new odeme
                {
                    odemeNo = this.odemeNo,
                    paket = this.paket,
                    odenecekUcret = this.odenecekUcret,
                    DonemBaslangici = this.DonemBaslangici,
                    DonemBitisi = this.DonemBitisi,
                    gun = this.gun,
                    sonOdemeTarihi = this.sonOdemeTarihi,
                    odendiMi = this.odendiMi
                };
            }
         }

        public class Paket
        {
            public int paketNo;
            public string paketIsmi;
            public Double paketFiyati;
            public bool aktifMi;
            public int yonlendirme;
            public int gun;
        }
    }

