using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ETicaretAdminPanel
{
    public partial class Iletisim : Form
    {
        public Iletisim()
        {
            InitializeComponent();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            string adSoyad = txtAdSoyad.Text.Trim();
            string eposta = txtEposta.Text.Trim();
            string mesaj = txtMesaj.Text.Trim();

            
            if (adSoyad == "" || eposta == "" || mesaj == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (!eposta.Contains("@") || !eposta.Contains("."))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(DB.baglanti);
                con.Open();

                string sql = @"INSERT INTO Mesajlar (AdSoyad, Eposta, Mesaj)
                       VALUES (@AdSoyad, @Eposta, @Mesaj)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@AdSoyad", adSoyad);
                cmd.Parameters.AddWithValue("@Eposta", eposta);
                cmd.Parameters.AddWithValue("@Mesaj", mesaj);

                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Mesajınız başarıyla gönderildi 😊", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtAdSoyad.Clear();
                txtEposta.Clear();
                txtMesaj.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu:\n" + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Iletisim_Load(object sender, EventArgs e)
        {

        }
    }
}
