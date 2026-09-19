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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            FormKategori f = new FormKategori();
            f.ShowDialog();
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            FormUrun f = new FormUrun();
            f.ShowDialog();
        }

        private void btnIletisim_Click(object sender, EventArgs e)
        {
            Iletisim frm = new Iletisim();
            frm.Show();
        }

        private void btnHakkimizda_Click(object sender, EventArgs e)
        {
            Hakkımızda hakkımızda = new Hakkımızda();
            hakkımızda.Show();
        }
    }
}
