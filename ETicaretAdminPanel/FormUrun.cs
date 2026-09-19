using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETicaretAdminPanel
{
    public partial class FormUrun : Form
    {
        int seciliUrunId = -1;
        public FormUrun()
        {
            InitializeComponent();
        }
        void KategoriDoldur()
        {
            var dt = DB.calistir("SELECT KategoriId, KategoriAdi FROM KATEGORILER ORDER BY KategoriAdi");
            cmbKategori.DisplayMember = "KategoriAdi";
            cmbKategori.ValueMember = "KategoriId";
            cmbKategori.DataSource = dt;
            cmbKategori.SelectedIndex = -1;
        }

        void Listele()
        {
            dgvUrun.DataSource = DB.calistir(
                @"SELECT u.UrunId, k.KategoriAdi, u.UrunAdi, u.Fiyat, u.Stok, u.Aciklama, u.Aktif
              FROM URUNLER u
              JOIN KATEGORILER k ON u.KategoriId = k.KategoriId
              ORDER BY u.UrunId DESC"
            );

            dgvUrun.ClearSelection();
            seciliUrunId = -1;
            txtUrunAdi.Text = "";
            txtFiyat.Text = "";
            txtStok.Text = "";
            txtAciklama.Text = "";
            cmbKategori.SelectedIndex = -1;
        }


        private void FormUrun_Load(object sender, EventArgs e)
        {
            KategoriDoldur();
            Listele();

        }

        private void dgvUrun_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            seciliUrunId = Convert.ToInt32(dgvUrun.Rows[e.RowIndex].Cells["UrunId"].Value);
            txtUrunAdi.Text = dgvUrun.Rows[e.RowIndex].Cells["UrunAdi"].Value.ToString();
            txtFiyat.Text = dgvUrun.Rows[e.RowIndex].Cells["Fiyat"].Value.ToString();
            txtStok.Text = dgvUrun.Rows[e.RowIndex].Cells["Stok"].Value.ToString();
            txtAciklama.Text = dgvUrun.Rows[e.RowIndex].Cells["Aciklama"].Value.ToString();


            string katAdi = dgvUrun.Rows[e.RowIndex].Cells["KategoriAdi"].Value.ToString();
            cmbKategori.Text = katAdi;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cmbKategori.SelectedValue == null)
            {
                MessageBox.Show("Kategori seç.");
                return;
            }

            string ad = txtUrunAdi.Text.Trim();
            if (ad == "") { MessageBox.Show("Ürün adı boş olamaz."); return; }

            string fiyat = txtFiyat.Text.Trim();
            string stok = txtStok.Text.Trim();
            string aciklama = txtAciklama.Text.Trim();

            DB.calistir(
                "INSERT INTO URUNLER (KategoriId, UrunAdi, Fiyat, Stok, Aciklama) VALUES (" +
                cmbKategori.SelectedValue + ", '" + ad + "', " + fiyat + ", " + stok + ", '" + aciklama + "')"
            );

            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliUrunId == -1)
            {
                MessageBox.Show("Listeden ürün seç.");
                return;
            }

            if (cmbKategori.SelectedValue == null)
            {
                MessageBox.Show("Kategori seç.");
                return;
            }

            string ad = txtUrunAdi.Text.Trim();
            string aciklama = txtAciklama.Text.Trim();

            if (ad == "")
            {
                MessageBox.Show("Ürün adı boş olamaz.");
                return;
            }

            if (!decimal.TryParse(txtFiyat.Text.Trim().Replace('.', ','), out decimal fiyat))
            {
                MessageBox.Show("Fiyat sayısal olmalı.");
                return;
            }

            if (!int.TryParse(txtStok.Text.Trim(), out int stok))
            {
                MessageBox.Show("Stok sayısal olmalı.");
                return;
            }

            string sql = @"UPDATE URUNLER
                   SET KategoriId = @kid,
                       UrunAdi    = @ad,
                       Fiyat      = @fiyat,
                       Stok       = @stok,
                       Aciklama   = @aciklama
                   WHERE UrunId   = @id";

            int etkilenen = DB.komutCalistir(sql,
                new SqlParameter("@kid", cmbKategori.SelectedValue),
                new SqlParameter("@ad", ad),
                new SqlParameter("@fiyat", fiyat),
                new SqlParameter("@stok", stok),
                new SqlParameter("@aciklama", aciklama),
                new SqlParameter("@id", seciliUrunId)
            );

            

            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliUrunId == -1) { MessageBox.Show("Listeden ürün seç."); return; }

            var cevap = MessageBox.Show("Silmek istediğine emin misin?", "Onay", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                DB.calistir("DELETE FROM URUNLER WHERE UrunId=" + seciliUrunId);
                Listele();
            }
        }
    }
        
}
