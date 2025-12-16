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

namespace Login
{
    public partial class FormTrangAdmin : Form
    {
        private int maNguoiDung;
        private string vaiTro;
        private string connectionString;
        public FormTrangAdmin(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel_Body.Controls.Clear();
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


// phần chuyển trang qua lại của admin
        private void btn_QLHD_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLHopDong(maNguoiDung, vaiTro, connectionString));
            label6.Text = btn_QLHD.Text;
        }

        private void btn_QLKT_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLKeToan(maNguoiDung, vaiTro, connectionString));
            label6.Text = btn_QLKT.Text;
        }

        private void btn_QLGS_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLGiamSat(maNguoiDung, vaiTro, connectionString));
            label6.Text = btn_QLGS.Text;
        }

        private void btn_QLTK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLTaiKhoan(maNguoiDung, vaiTro, connectionString));
            label6.Text = btn_QLTK.Text;
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 loginForm = new Form1();
            loginForm.ShowDialog();
            
            this.Close();
        }

        private void btn_QLNBH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLNBH(maNguoiDung, vaiTro, connectionString));
            label6.Text = btn_QLNBH.Text;
        }
    }
}
