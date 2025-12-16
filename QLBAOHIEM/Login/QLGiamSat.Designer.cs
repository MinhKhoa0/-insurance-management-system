namespace Login
{
    partial class QLGiamSat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvNguoiBaoHiem = new System.Windows.Forms.DataGridView();
            this.dgvBaoHiem = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvHopDong = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.Thoát = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiBaoHiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoHiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNguoiBaoHiem
            // 
            this.dgvNguoiBaoHiem.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvNguoiBaoHiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNguoiBaoHiem.Location = new System.Drawing.Point(12, 200);
            this.dgvNguoiBaoHiem.Name = "dgvNguoiBaoHiem";
            this.dgvNguoiBaoHiem.RowHeadersWidth = 51;
            this.dgvNguoiBaoHiem.RowTemplate.Height = 24;
            this.dgvNguoiBaoHiem.Size = new System.Drawing.Size(1159, 184);
            this.dgvNguoiBaoHiem.TabIndex = 13;
            // 
            // dgvBaoHiem
            // 
            this.dgvBaoHiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoHiem.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvBaoHiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoHiem.Location = new System.Drawing.Point(12, 48);
            this.dgvBaoHiem.Name = "dgvBaoHiem";
            this.dgvBaoHiem.RowHeadersWidth = 51;
            this.dgvBaoHiem.RowTemplate.Height = 24;
            this.dgvBaoHiem.Size = new System.Drawing.Size(1159, 80);
            this.dgvBaoHiem.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(483, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 36);
            this.label1.TabIndex = 17;
            this.label1.Text = "DANH MỤC BẢO HIỂM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(556, 36);
            this.label2.TabIndex = 18;
            this.label2.Text = "DANH MỤC NGƯỜI ĐƯỢC BẢO HIỂM";
            // 
            // dgvHopDong
            // 
            this.dgvHopDong.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvHopDong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHopDong.Location = new System.Drawing.Point(12, 449);
            this.dgvHopDong.Name = "dgvHopDong";
            this.dgvHopDong.RowHeadersWidth = 51;
            this.dgvHopDong.RowTemplate.Height = 24;
            this.dgvHopDong.Size = new System.Drawing.Size(1159, 191);
            this.dgvHopDong.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(417, 400);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(360, 36);
            this.label3.TabIndex = 20;
            this.label3.Text = "DANH MỤC HỢP ĐỒNG";
            // 
            // Thoát
            // 
            this.Thoát.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Thoát.Location = new System.Drawing.Point(540, 646);
            this.Thoát.Name = "Thoát";
            this.Thoát.Size = new System.Drawing.Size(94, 30);
            this.Thoát.TabIndex = 21;
            this.Thoát.Text = "Thoát";
            this.Thoát.UseVisualStyleBackColor = true;
            this.Thoát.Click += new System.EventHandler(this.Thoát_Click);
            // 
            // QLGiamSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 686);
            this.Controls.Add(this.Thoát);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvHopDong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvBaoHiem);
            this.Controls.Add(this.dgvNguoiBaoHiem);
            this.Name = "QLGiamSat";
            this.Text = "QLGiamSat";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiBaoHiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoHiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNguoiBaoHiem;
        private System.Windows.Forms.DataGridView dgvBaoHiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvHopDong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Thoát;
    }
}