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
    public partial class FormQLNBH: Form
    {
        private string connectionString;
        private int maNguoiDung;
        private string vaiTro;
        private bool isThem = false;
        private int maNBHChon = -1;


        public FormQLNBH(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;
        }

        private void FormQLNBH_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NGUOIBAOHIEM", conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNBH.DataSource = dt;
            }
        }

        private void ClearForm()
        {
            txtHoTen.Clear();
            lbGioiTinh.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Today;
            txtDCThT.Clear();
            txtDCTT.Clear();
            txtSDT.Clear();
            txtCQCT.Clear();
            txtLSB.Clear();
            maNBHChon = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isThem = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maNBHChon == -1)
            {
                MessageBox.Show("Vui lòng chọn người được bảo hiểm cần xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SP_XOA_NGUOI_BAO_HIEM", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                cmd.Parameters.AddWithValue("@MaNBH", maNBHChon);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công.");
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            string gioiTinh = lbGioiTinh.SelectedItem?.ToString() ?? "";
            DateTime ngaySinh = dtpNgaySinh.Value;
            string diaChiThuongTru = txtDCThT.Text;
            string diaChiTamTru = txtDCTT.Text;
            string diaChiLienLac = txtSDT.Text;
            string coQuan = txtCQCT.Text;
            string lichSuBenh = txtLSB.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                if (isThem)
                {
                    cmd.CommandText = "SP_THEM_NGUOI_BAO_HIEM";
                    cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@DiaChiThuongTru", diaChiThuongTru);
                    cmd.Parameters.AddWithValue("@DiaChiTamTru", diaChiTamTru);
                    cmd.Parameters.AddWithValue("@DiaChiLienLac", diaChiLienLac);
                    cmd.Parameters.AddWithValue("@CoQuanCongTac", coQuan);
                    cmd.Parameters.AddWithValue("@LichSuBenh", lichSuBenh);
                }
                else
                {
                    if (maNBHChon == -1)
                    {
                        MessageBox.Show("Chưa chọn người cần sửa.");
                        return;
                    }

                    cmd.CommandText = "SP_CAPNHAT_NGUOI_BAO_HIEM";
                    cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                    cmd.Parameters.AddWithValue("@MaNBH", maNBHChon);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@DiaChiThuongTru", diaChiThuongTru);
                    cmd.Parameters.AddWithValue("@DiaChiTamTru", diaChiTamTru);
                    cmd.Parameters.AddWithValue("@DiaChiLienLac", diaChiLienLac);
                    cmd.Parameters.AddWithValue("@CoQuanCongTac", coQuan);
                    cmd.Parameters.AddWithValue("@LichSuBenh", lichSuBenh);
                }

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(isThem ? "Thêm thành công." : "Cập nhật thành công.");
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dgvNBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNBH.Rows[e.RowIndex];
                maNBHChon = Convert.ToInt32(row.Cells["MaNBH"].Value);
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                lbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtDCThT.Text = row.Cells["DiaChiThuongTru"].Value.ToString();
                txtDCTT.Text = row.Cells["DiaChiTamTru"].Value.ToString();
                txtSDT.Text = row.Cells["DiaChiLienLac"].Value.ToString();
                txtCQCT.Text = row.Cells["CoQuanCongTac"].Value.ToString();
                txtLSB.Text = row.Cells["LichSuBenh"].Value.ToString();
            }
        }

        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
