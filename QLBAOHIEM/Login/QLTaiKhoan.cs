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
    public partial class QLTaiKhoan: Form
    {
        private string connectionString;
        private int maNguoiDung;
        private string vaiTro;
        private bool isThem = false;
        private int selectedMaNguoiDung = -1;

        public QLTaiKhoan(int maNguoiDung, string vaiTro, string connStr)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            this.vaiTro = vaiTro;
            connectionString = connStr;
        }



        private void LoadData()
        {
            string sql = "SELECT MaNguoiDung, TenDangNhap, VaiTro FROM NGUOIDUNG";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            dgvNguoiDung.DataSource = dt;
            dgvNguoiDung.ClearSelection();

            ClearInputFields();

            selectedMaNguoiDung = -1;
            isThem = false;
        }

        private void ClearInputFields()
        {
            txtTenDN.Text = "";
            txtMatKhau.Text = "";
            lbVaiTro.SelectedIndex = -1;
        }






        private void QLTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadData();
            bool isAdmin = vaiTro.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);
            btnThem.Enabled = isAdmin;
            btnSua.Enabled = isAdmin;
            btnXoa.Enabled = isAdmin;
            btnLuu.Enabled = isAdmin;
            lbVaiTro.Enabled = isAdmin;
            txtMatKhau.Enabled = isAdmin;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            isThem = true;
            selectedMaNguoiDung = -1;

        }



        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNguoiDung.Rows[e.RowIndex];
                selectedMaNguoiDung = Convert.ToInt32(row.Cells["MaNguoiDung"].Value);
                txtTenDN.Text = row.Cells["TenDangNhap"].Value.ToString();
                string vaiTroRow = row.Cells["VaiTro"].Value.ToString();
                lbVaiTro.SelectedItem = vaiTroRow;

                txtMatKhau.Text = "";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaNguoiDung == -1)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_XOA_NGUOIDUNG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNguoiDungCanXoa", selectedMaNguoiDung);
                    cmd.Parameters.AddWithValue("@MaNguoiDung", this.maNguoiDung); // người thực hiện

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa người dùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi xóa người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaNguoiDung == -1)
            {
                MessageBox.Show("Vui lòng chọn người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDN.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isThem && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống khi thêm mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lbVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenDangNhap = txtTenDN.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTroMoi = lbVaiTro.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (isThem)
                {
                    cmd = new SqlCommand("SP_THEM_NGUOIDUNG", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@VaiTro", vaiTroMoi);
                    cmd.Parameters.AddWithValue("@MaNguoiDung", this.maNguoiDung); // người gọi thực hiện
                }
                else
                {
                    if (selectedMaNguoiDung == -1)
                    {
                        MessageBox.Show("Vui lòng chọn người dùng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmd = new SqlCommand("SP_CAPNHAT_NGUOIDUNG", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNguoiDungCanCapNhat", selectedMaNguoiDung);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@VaiTro", vaiTroMoi);
                    cmd.Parameters.AddWithValue("@MaNguoiDung", this.maNguoiDung); // người gọi thực hiện
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
