using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETicaretAdminPanel
{
    public partial class FormKategori : Form
    {
        int seciliKategoriId = -1;

        public FormKategori()
        {
            InitializeComponent();
        }
        void Listele()
        {
            dgvKategori.DataSource = DB.calistir(
                "SELECT KategoriId, KategoriAdi FROM KATEGORILER ORDER BY KategoriId DESC"
            );

            dgvKategori.ClearSelection();
            seciliKategoriId = -1;
            txtKategoriAdi.Text = "";
        }

        private void FormKategori_Load(object sender, EventArgs e)
        {
            Listele();
            dgvKategori.Dock = DockStyle.Top;
            dgvKategori.Height = 220;

            dgvKategori.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKategori.RowHeadersVisible = false;
            dgvKategori.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvKategori.DefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Regular);

            dgvKategori.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvKategori.RowTemplate.Height = 32;

            dgvKategori.Columns["KategoriId"].FillWeight = 30;
            dgvKategori.Columns["KategoriAdi"].FillWeight = 70;
        }

        private void dgvKategori_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            seciliKategoriId = Convert.ToInt32(dgvKategori.Rows[e.RowIndex].Cells["KategoriId"].Value);
            txtKategoriAdi.Text = dgvKategori.Rows[e.RowIndex].Cells["KategoriAdi"].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string ad = txtKategoriAdi.Text.Trim();
            if (ad == "")
            {
                MessageBox.Show("Kategori adı boş olamaz.");
                return;
            }

            DB.calistir("INSERT INTO KATEGORILER (KategoriAdi) VALUES ('" + ad + "')");
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliKategoriId == -1)
            {
                MessageBox.Show("Önce listeden kategori seç.");
                return;
            }

            string ad = txtKategoriAdi.Text.Trim();
            if (ad == "")
            {
                MessageBox.Show("Kategori adı boş olamaz.");
                return;
            }

            DB.calistir("UPDATE KATEGORILER SET KategoriAdi='" + ad + "' WHERE KategoriId=" + seciliKategoriId);
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliKategoriId == -1)
            {
                MessageBox.Show("Önce listeden kategori seç.");
                return;
            }

            var cevap = MessageBox.Show("Silmek istediğine emin misin?", "Onay", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                DB.calistir("DELETE FROM KATEGORILER WHERE KategoriId=" + seciliKategoriId);
                Listele();
            }
        }
    }
    
}
