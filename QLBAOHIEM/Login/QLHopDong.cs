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
    public partial class QLHopDong: Form
    {
        private string connectionString;
        private bool isThemMoi = false;
        private int maNguoiDung;
        private string vaiTro;

        public QLHopDong(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HOPDONGBH";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvHopDong.DataSource = dt;
            }
        }
     
        private void QLHopDong_Load_1(object sender, EventArgs e)
        {
            if (vaiTro != "ADMIN" && vaiTro != "LAP_HOP_DONG")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadData();
            lbTrangThai.Items.Add("HIEU_LUC");
            lbTrangThai.Items.Add("HET_HAN");
            lbTrangThai.Items.Add("CHO_DUYET");
            lbTrangThai.SelectedIndex = 0;
        }

        private void ClearInput()
        {
            txtMaNBH.Clear();
            txtMaNLHD.Clear();
            txtMaLBH.Clear();
            lbTrangThai.SelectedIndex = 0;
            dtpNgayBD.Value = DateTime.Now;
            dtpNgayKT.Value = DateTime.Now;
        }


        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHopDong.Rows[e.RowIndex];

                txtMaNBH.Text = row.Cells["MaNBH"].Value.ToString();
                txtMaNLHD.Text = row.Cells["MaLapHopDong"].Value.ToString(); 
                txtMaLBH.Text = row.Cells["MaLoaiBH"].Value.ToString();

                string trangThai = row.Cells["TrangThai"].Value.ToString();
                int index = lbTrangThai.Items.IndexOf(trangThai);
                if (index >= 0) lbTrangThai.SelectedIndex = index;

                dtpNgayBD.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                dtpNgayKT.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                isThemMoi = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInput();
            isThemMoi = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isThemMoi = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHopDong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hợp đồng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                int maHD = Convert.ToInt32(dgvHopDong.CurrentRow.Cells["MaHD"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SP_XOA_HOPDONG", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@MaNguoiDung", this.maNguoiDung);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInput();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ form
                int maNBH = int.Parse(txtMaNBH.Text.Trim());
                string maLoaiBH = txtMaLBH.Text.Trim();  // Là string vì SP cập nhật dùng varchar(20)
                string trangThai = lbTrangThai.SelectedItem.ToString();
                DateTime ngayBD = dtpNgayBD.Value;
                DateTime ngayKT = dtpNgayKT.Value;
                int maLapHopDong = int.Parse(txtMaNLHD.Text.Trim());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (isThemMoi)
                    {
                        // Gọi SP thêm hợp đồng
                        cmd = new SqlCommand("SP_THEM_HOPDONGBH", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaNBH", maNBH);
                        cmd.Parameters.AddWithValue("@MaLoaiBH", maLoaiBH);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.Parameters.AddWithValue("@MaLapHopDong", maLapHopDong);
                    }
                    else
                    {
                        // Gọi SP cập nhật hợp đồng
                        cmd = new SqlCommand("SP_CAPNHAT_HOPDONG", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Lấy mã hợp đồng hiện tại từ dòng chọn
                        int maHD = Convert.ToInt32(dgvHopDong.CurrentRow.Cells["MaHD"].Value);

                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@MaLoaiBH", maLoaiBH);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(isThemMoi ? "Thêm hợp đồng thành công." : "Cập nhật hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ClearInput();
                isThemMoi = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
