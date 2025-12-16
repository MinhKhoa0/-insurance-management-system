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
    public partial class Form1 : Form
    {

        private string connectionString = "Server=DESKTOP-AQJG5SQ\\MAY1;Database=BAOHIEM;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.peye = new System.Windows.Forms.PictureBox();
            this.phide = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.peye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phide)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập: ";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Location = new System.Drawing.Point(231, 29);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(146, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu: ";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(231, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(146, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(128, 135);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(80, 26);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(260, 137);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // peye
            // 
            this.peye.Image = ((System.Drawing.Image)(resources.GetObject("peye.Image")));
            this.peye.Location = new System.Drawing.Point(383, 71);
            this.peye.Name = "peye";
            this.peye.Size = new System.Drawing.Size(28, 21);
            this.peye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.peye.TabIndex = 6;
            this.peye.TabStop = false;
            this.peye.Click += new System.EventHandler(this.peye_Click);
            // 
            // phide
            // 
            this.phide.Image = ((System.Drawing.Image)(resources.GetObject("phide.Image")));
            this.phide.Location = new System.Drawing.Point(383, 71);
            this.phide.Name = "phide";
            this.phide.Size = new System.Drawing.Size(28, 21);
            this.phide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.phide.TabIndex = 7;
            this.phide.TabStop = false;
            this.phide.Click += new System.EventHandler(this.phide_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(456, 184);
            this.Controls.Add(this.phide);
            this.Controls.Add(this.peye);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Màn hình đăng nhập";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SP_KIEMTRA_DANGNHAP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    cmd.Parameters.AddWithValue("@MatKhau", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string vaiTro = reader["VaiTro"].ToString().ToUpper();
                        int maNguoiDung = Convert.ToInt32(reader["MaNguoiDung"]);

                        // Xác định user SQL tương ứng với vai trò
                        string sqlUsername = "";

                        switch (vaiTro)
                        {
                            case "ADMIN":
                                sqlUsername = "user_admin";
                                break;
                            case "GIAM_SAT":
                                sqlUsername = "user_gs";
                                break;
                            case "KE_TOAN":
                                sqlUsername = "user_kt";
                                break;
                            case "LAP_HOP_DONG":
                                sqlUsername = "user_lhd";
                                break;
                            case "NGUOI_BAO_HIEM":
                                sqlUsername = "user_nbh";
                                break;
                            default:
                                throw new Exception("Vai trò không hợp lệ");
                        }


                        // Tạo chuỗi kết nối động
                        string dynamicConnString = $"Server=DESKTOP-AQJG5SQ\\MAY1;Database=BAOHIEM;User Id={sqlUsername};Password=MatKhau@123;Encrypt=True;TrustServerCertificate=True;";

                        MessageBox.Show("Đăng nhập thành công với vai trò: " + vaiTro);

                        this.Hide();

                        // Mở form tương ứng, truyền dynamicConnString
                        switch (vaiTro)
                        {
                            case "ADMIN":
                                FormTrangAdmin adminForm = new FormTrangAdmin(maNguoiDung, vaiTro, dynamicConnString);
                                adminForm.ShowDialog();
                                break;
                            case "GIAM_SAT":
                                QLGiamSat giamSatForm = new QLGiamSat(maNguoiDung, vaiTro, dynamicConnString);
                                giamSatForm.ShowDialog();
                                break;
                            case "KE_TOAN":
                                QLKeToan keToanForm = new QLKeToan(maNguoiDung, vaiTro, dynamicConnString);
                                keToanForm.ShowDialog();
                                break;
                            case "LAP_HOP_DONG":
                                QLHopDong hopDongForm = new QLHopDong(maNguoiDung, vaiTro, dynamicConnString);
                                hopDongForm.ShowDialog();
                                break;
                            case "NGUOI_BAO_HIEM":
                                FormQLNBH nbhForm = new FormQLNBH(maNguoiDung, vaiTro, dynamicConnString);
                                nbhForm.ShowDialog();
                                break;
                        }

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại: Không tìm thấy người dùng.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
                }
            }
        }



        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void phide_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                peye.BringToFront();
                txtPassword.PasswordChar = '\0';

            }
        }

        private void peye_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                phide.BringToFront();
                txtPassword.PasswordChar = '*';
            }
        }
    }
}