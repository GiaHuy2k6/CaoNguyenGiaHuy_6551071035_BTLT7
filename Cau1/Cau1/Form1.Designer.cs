namespace Cau1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtMoTa = new TextBox();
            label2 = new Label();
            label3 = new Label();
            lblNgayTaoLabel = new Label();
            txtMaTL = new TextBox();
            txtTenTL = new TextBox();
            txtTimKiem = new TextBox();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnThem = new Button();
            btnTimKiem = new Button();
            dgvTheLoai = new DataGridView();
            lblNgayTao = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTheLoai).BeginInit();
            SuspendLayout();
            // 
            // label1 - Mã thể loại
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 18);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã thể loại";
            // 
            // txtMaTL
            // 
            txtMaTL.Location = new Point(130, 15);
            txtMaTL.Name = "txtMaTL";
            txtMaTL.ReadOnly = true;
            txtMaTL.Size = new Size(160, 27);
            txtMaTL.TabIndex = 1;
            txtMaTL.BackColor = SystemColors.Control;
            // 
            // label2 - Tên thể loại
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 55);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 2;
            label2.Text = "Tên thể loại";
            // 
            // txtTenTL
            // 
            txtTenTL.Location = new Point(130, 52);
            txtTenTL.Name = "txtTenTL";
            txtTenTL.Size = new Size(250, 27);
            txtTenTL.TabIndex = 3;
            // 
            // label3 - Mô tả
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 92);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 4;
            label3.Text = "Mô tả";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(130, 89);
            txtMoTa.Multiline = true;
            txtMoTa.ScrollBars = ScrollBars.Vertical;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(250, 90);
            txtMoTa.TabIndex = 5;
            // 
            // lblNgayTaoLabel - static label "Ngày tạo:"
            // 
            lblNgayTaoLabel.AutoSize = true;
            lblNgayTaoLabel.Location = new Point(20, 192);
            lblNgayTaoLabel.Name = "lblNgayTaoLabel";
            lblNgayTaoLabel.Size = new Size(63, 20);
            lblNgayTaoLabel.TabIndex = 16;
            lblNgayTaoLabel.Text = "Ngày tạo:";
            // 
            // lblNgayTao - dynamic value label
            // 
            lblNgayTao.AutoSize = true;
            lblNgayTao.Location = new Point(130, 192);
            lblNgayTao.Name = "lblNgayTao";
            lblNgayTao.Size = new Size(70, 20);
            lblNgayTao.TabIndex = 6;
            lblNgayTao.Text = "";
            lblNgayTao.ForeColor = Color.DarkBlue;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(20, 232);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(294, 27);
            txtTimKiem.TabIndex = 7;
            txtTimKiem.PlaceholderText = "Nhập tên thể loại để tìm kiếm...";
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(324, 231);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 8;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(430, 15);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 9;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(534, 15);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 10;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(638, 15);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(638, 55);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 12;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // dgvTheLoai
            // 
            dgvTheLoai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTheLoai.Location = new Point(20, 272);
            dgvTheLoai.MultiSelect = false;
            dgvTheLoai.Name = "dgvTheLoai";
            dgvTheLoai.ReadOnly = true;
            dgvTheLoai.RowHeadersWidth = 51;
            dgvTheLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTheLoai.Size = new Size(760, 200);
            dgvTheLoai.TabIndex = 13;
            dgvTheLoai.AllowUserToAddRows = false;
            dgvTheLoai.AllowUserToDeleteRows = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 490);
            Controls.Add(lblNgayTao);
            Controls.Add(lblNgayTaoLabel);
            Controls.Add(dgvTheLoai);
            Controls.Add(btnTimKiem);
            Controls.Add(btnThem);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(txtTimKiem);
            Controls.Add(txtTenTL);
            Controls.Add(txtMaTL);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtMoTa);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Quản Lý Thể Loại Sách - Tri Thức Books";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 530);
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTheLoai).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtMoTa;
        private Label label2;
        private Label label3;
        private Label lblNgayTaoLabel;
        private TextBox txtMaTL;
        private TextBox txtTenTL;
        private TextBox txtTimKiem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnThem;
        private Button btnTimKiem;
        private DataGridView dgvTheLoai;
        private Label lblNgayTao;
    }
}
