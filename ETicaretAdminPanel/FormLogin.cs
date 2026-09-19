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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string k = txtKullanici.Text.Trim();
            string s = txtSifre.Text.Trim();

            var dt = DB.calistir(
                "SELECT * FROM ADMINS WHERE KullaniciAdi='" + k +
                "' AND Sifre='" + s + "'"
            );

            if (dt.Rows.Count > 0) {
                lblBilgi.Visible = true;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı, lütfen tekrar deneyin!"); 
                    
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            FormMain frm = new FormMain();
            frm.Show();
            this.Hide();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
