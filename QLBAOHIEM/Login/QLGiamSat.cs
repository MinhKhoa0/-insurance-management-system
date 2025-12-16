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
    public partial class QLGiamSat: Form
    {
        private int maNguoiDung;
        private string vaiTro;
        private string connectionString;
        public QLGiamSat(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;

            if (vaiTro != "ADMIN" && vaiTro != "GIAM_SAT")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadDanhMuc();
        }


        private void LoadDanhMuc()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Bảo hiểm
                    SqlDataAdapter daBH = new SqlDataAdapter("SELECT * FROM LOAIBAOHIEM", conn);
                    DataTable dtBH = new DataTable();
                    daBH.Fill(dtBH);
                    dgvBaoHiem.DataSource = dtBH;

                    // Người được bảo hiểm
                    SqlDataAdapter daNDBH = new SqlDataAdapter("SELECT * FROM NGUOIBAOHIEM", conn);
                    DataTable dtNDBH = new DataTable();
                    daNDBH.Fill(dtNDBH);
                    dgvNguoiBaoHiem.DataSource = dtNDBH;

                    // Hợp đồng
                    SqlDataAdapter daHD = new SqlDataAdapter("SELECT * FROM HOPDONGBH", conn);
                    DataTable dtHD = new DataTable();
                    daHD.Fill(dtHD);
                    dgvHopDong.DataSource = dtHD;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
