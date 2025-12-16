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
    public partial class QLKeToan: Form
    {
        private string connectionString;
        private int maNguoiDung;
        private string vaiTro;
        bool isThem = false;
        int maThanhToanDangChon = -1;

        public QLKeToan(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;
        }

        private void ClearForm()
        {
            txtMaHD.Clear();
            txtSoTien.Clear();
            lbPTTT.ClearSelected();
            lbTrangThai.ClearSelected();
            dtpNgayTT.Value = DateTime.Now;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM THANHTOAN", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvThanhToan.DataSource = dt;
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvThanhToan.CurrentRow != null)
            {
                isThem = false;
                maThanhToanDangChon = Convert.ToInt32(dgvThanhToan.CurrentRow.Cells["MaThanhToan"].Value);

                // Load dữ liệu lên form
                txtMaHD.Text = dgvThanhToan.CurrentRow.Cells["MaHD"].Value.ToString();
                txtSoTien.Text = dgvThanhToan.CurrentRow.Cells["SoTien"].Value.ToString();
                dtpNgayTT.Value = Convert.ToDateTime(dgvThanhToan.CurrentRow.Cells["NgayThanhToan"].Value);
                lbPTTT.SelectedItem = dgvThanhToan.CurrentRow.Cells["PhuongThucThanhToan"].Value.ToString();
                lbTrangThai.SelectedItem = dgvThanhToan.CurrentRow.Cells["TrangThai"].Value.ToString();

            }
        }

        private void QLKeToan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int maHD = int.Parse(txtMaHD.Text);
            decimal soTien = decimal.Parse(txtSoTien.Text);
            string phuongThucTT = lbPTTT.SelectedItem?.ToString() ?? "";
            string trangThai = lbTrangThai.SelectedItem?.ToString() ?? "";
            DateTime ngayTT = dtpNgayTT.Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };

                if (isThem)
                {
                    cmd.CommandText = "SP_THEM_THANHTOAN";
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@NgayThanhToan", ngayTT);
                    cmd.Parameters.AddWithValue("@SoTien", soTien);
                    cmd.Parameters.AddWithValue("@PhuongThucThanhToan", phuongThucTT);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                }
                else
                {
                    cmd.CommandText = "SP_CAPNHAT_THANHTOAN";
                    cmd.Parameters.AddWithValue("@MaThanhToan", maThanhToanDangChon);
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@NgayThanhToan", ngayTT);
                    cmd.Parameters.AddWithValue("@SoTien", soTien);
                    cmd.Parameters.AddWithValue("@PhuongThucThanhToan", phuongThucTT);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lưu dữ liệu thành công!");
                    LoadData(); // Hàm load lại DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThanhToan.CurrentRow != null)
            {
                int maThanhToan = Convert.ToInt32(dgvThanhToan.CurrentRow.Cells["MaThanhToan"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SP_XOA_THANHTOAN", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaThanhToan", maThanhToan);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
