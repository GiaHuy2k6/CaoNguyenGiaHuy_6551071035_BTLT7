namespace QuanLyHoiVien
{
    partial class FormHoiVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle        = new System.Windows.Forms.Label();
            // --- Input labels ---
            this.lblHoTen        = new System.Windows.Forms.Label();
            this.lblSDT          = new System.Windows.Forms.Label();
            this.lblEmail        = new System.Windows.Forms.Label();
            this.lblNgaySinh     = new System.Windows.Forms.Label();
            this.lblHang         = new System.Windows.Forms.Label();
            // --- Input controls ---
            this.txtHoTen        = new System.Windows.Forms.TextBox();
            this.txtSDT          = new System.Windows.Forms.TextBox();
            this.txtEmail        = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh     = new System.Windows.Forms.DateTimePicker();
            this.cboHang         = new System.Windows.Forms.ComboBox();
            this.chkTrangThai    = new System.Windows.Forms.CheckBox();
            // --- GroupBox Giới tính ---
            this.grpGioiTinh     = new System.Windows.Forms.GroupBox();
            this.rdoNam          = new System.Windows.Forms.RadioButton();
            this.rdoNu           = new System.Windows.Forms.RadioButton();
            // --- Buttons ---
            this.btnThem         = new System.Windows.Forms.Button();
            this.btnSua          = new System.Windows.Forms.Button();
            this.btnXoa          = new System.Windows.Forms.Button();
            this.btnLamMoi       = new System.Windows.Forms.Button();
            this.btnTimKiem      = new System.Windows.Forms.Button();
            // --- Search bar ---
            this.lblTimHoTen     = new System.Windows.Forms.Label();
            this.txtTimHoTen     = new System.Windows.Forms.TextBox();
            this.lblTimHang      = new System.Windows.Forms.Label();
            this.cboTimHang      = new System.Windows.Forms.ComboBox();
            // --- DataGridView ---
            this.dgvHoiVien      = new System.Windows.Forms.DataGridView();
            // --- Panel header ---
            this.pnlHeader       = new System.Windows.Forms.Panel();
            this.pnlMain         = new System.Windows.Forms.Panel();
            this.pnlButtons      = new System.Windows.Forms.Panel();
            this.pnlSearch       = new System.Windows.Forms.Panel();

            this.grpGioiTinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvHoiVien).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();

            // ===================== FORM =====================
            this.Text            = "Quản Lý Hội Viên Phòng Gym FitZone";
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Size            = new System.Drawing.Size(1000, 680);
            this.MinimumSize     = new System.Drawing.Size(980, 650);
            this.BackColor       = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5f);

            // ===================== PANEL HEADER =====================
            this.pnlHeader.Dock        = DockStyle.Top;
            this.pnlHeader.Height      = 60;
            this.pnlHeader.BackColor   = System.Drawing.Color.FromArgb(30, 30, 60);
            this.pnlHeader.Controls.Add(this.lblTitle);

            this.lblTitle.Text      = "🏋️  QUẢN LÝ HỘI VIÊN — PHÒNG GYM FITZONE";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock      = DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ===================== PANEL MAIN (left input + right buttons) =====================
            this.pnlMain.Dock      = DockStyle.Top;
            this.pnlMain.Height    = 300;
            this.pnlMain.Padding   = new Padding(12, 12, 12, 8);
            this.pnlMain.BackColor = System.Drawing.Color.White;
            int lx = 20, ly = 20, lw = 130, cy = 20, cx = 160, cw = 220, ch = 28, gap = 38;

            // Họ tên
            this.lblHoTen.Text = "Họ tên"; SetLabel(lblHoTen, lx, ly, lw);
            this.txtHoTen.Location = new System.Drawing.Point(cx, cy); this.txtHoTen.Size = new System.Drawing.Size(cw, ch);
            this.txtHoTen.BorderStyle = BorderStyle.FixedSingle;

            // SDT
            ly += gap; cy += gap;
            this.lblSDT.Text = "Số điện thoại"; SetLabel(lblSDT, lx, ly, lw);
            this.txtSDT.Location = new System.Drawing.Point(cx, cy); this.txtSDT.Size = new System.Drawing.Size(cw, ch);
            this.txtSDT.BorderStyle = BorderStyle.FixedSingle;

            // Email
            ly += gap; cy += gap;
            this.lblEmail.Text = "Email"; SetLabel(lblEmail, lx, ly, lw);
            this.txtEmail.Location = new System.Drawing.Point(cx, cy); this.txtEmail.Size = new System.Drawing.Size(cw, ch);
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;

            // Ngày sinh
            ly += gap; cy += gap;
            this.lblNgaySinh.Text = "Ngày sinh"; SetLabel(lblNgaySinh, lx, ly, lw);
            this.dtpNgaySinh.Location = new System.Drawing.Point(cx, cy); this.dtpNgaySinh.Size = new System.Drawing.Size(cw, ch);
            this.dtpNgaySinh.Format   = DateTimePickerFormat.Short;

            // Hạng thành viên
            ly += gap; cy += gap;
            this.lblHang.Text = "Hạng thành viên"; SetLabel(lblHang, lx, ly, lw);
            this.cboHang.Location = new System.Drawing.Point(cx, cy); this.cboHang.Size = new System.Drawing.Size(cw, ch);
            this.cboHang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHang.Items.AddRange(new object[] { "Basic", "VIP", "Premium" });
            this.cboHang.SelectedIndex = 0;

            // CheckBox Trạng thái
            ly += gap; cy += gap;
            this.chkTrangThai.Text     = "Đang hoạt động";
            this.chkTrangThai.Location = new System.Drawing.Point(cx, cy);
            this.chkTrangThai.Size     = new System.Drawing.Size(160, 24);
            this.chkTrangThai.Checked  = true;
            this.chkTrangThai.Font     = new System.Drawing.Font("Segoe UI", 9.5f);

            // GroupBox Giới tính
            this.grpGioiTinh.Text     = "Giới tính";
            this.grpGioiTinh.Location = new System.Drawing.Point(420, 16);
            this.grpGioiTinh.Size     = new System.Drawing.Size(160, 60);
            this.grpGioiTinh.Font     = new System.Drawing.Font("Segoe UI", 9.5f);

            this.rdoNam.Text     = "Nam"; this.rdoNam.Location = new System.Drawing.Point(10, 28);
            this.rdoNam.Size     = new System.Drawing.Size(55, 20); this.rdoNam.Checked = true;
            this.rdoNu.Text      = "Nữ";  this.rdoNu.Location  = new System.Drawing.Point(75, 28);
            this.rdoNu.Size      = new System.Drawing.Size(55, 20);
            this.grpGioiTinh.Controls.AddRange(new Control[] { rdoNam, rdoNu });

            // Add controls to pnlMain
            this.pnlMain.Controls.AddRange(new Control[] {
                lblHoTen, txtHoTen, lblSDT, txtSDT, lblEmail, txtEmail,
                lblNgaySinh, dtpNgaySinh, lblHang, cboHang, chkTrangThai,
                grpGioiTinh
            });

            // ===================== PANEL BUTTONS =====================
            this.pnlButtons.Dock      = DockStyle.Right;
            this.pnlButtons.Width     = 160;
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Padding   = new Padding(12, 20, 12, 8);

            SetButton(btnThem,    "Thêm",    System.Drawing.Color.FromArgb(39, 174, 96), 10, 20);
            SetButton(btnSua,     "Sửa",     System.Drawing.Color.FromArgb(41, 128, 185), 10, 70);
            SetButton(btnXoa,     "Xóa",     System.Drawing.Color.FromArgb(192, 57, 43), 10, 120);
            SetButton(btnLamMoi,  "Làm mới", System.Drawing.Color.FromArgb(127, 140, 141), 10, 170);

            this.pnlButtons.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });
            this.pnlMain.Controls.Add(this.pnlButtons);

            // ===================== PANEL SEARCH =====================
            this.pnlSearch.Dock      = DockStyle.Top;
            this.pnlSearch.Height    = 50;
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlSearch.Padding   = new Padding(12, 10, 12, 6);

            this.lblTimHoTen.Text     = "Họ tên:";
            this.lblTimHoTen.Location = new System.Drawing.Point(12, 16);
            this.lblTimHoTen.Size     = new System.Drawing.Size(55, 24);
            this.lblTimHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtTimHoTen.Location = new System.Drawing.Point(72, 14);
            this.txtTimHoTen.Size     = new System.Drawing.Size(200, 28);
            this.txtTimHoTen.BorderStyle = BorderStyle.FixedSingle;

            this.lblTimHang.Text      = "Hạng TV:";
            this.lblTimHang.Location  = new System.Drawing.Point(290, 16);
            this.lblTimHang.Size      = new System.Drawing.Size(65, 24);
            this.lblTimHang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboTimHang.Location      = new System.Drawing.Point(360, 14);
            this.cboTimHang.Size          = new System.Drawing.Size(150, 28);
            this.cboTimHang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTimHang.Items.AddRange(new object[] { "(Tất cả)", "Basic", "VIP", "Premium" });
            this.cboTimHang.SelectedIndex = 0;

            SetButton(btnTimKiem, "Tìm kiếm", System.Drawing.Color.FromArgb(52, 73, 94), 530, 10);
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);

            this.pnlSearch.Controls.AddRange(new Control[] {
                lblTimHoTen, txtTimHoTen, lblTimHang, cboTimHang, btnTimKiem
            });

            // ===================== DATAGRIDVIEW =====================
            this.dgvHoiVien.Dock                  = DockStyle.Fill;
            this.dgvHoiVien.AllowUserToAddRows     = false;
            this.dgvHoiVien.AllowUserToDeleteRows  = false;
            this.dgvHoiVien.ReadOnly               = true;
            this.dgvHoiVien.SelectionMode          = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoiVien.MultiSelect            = false;
            this.dgvHoiVien.AutoSizeColumnsMode    = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoiVien.RowHeadersVisible      = false;
            this.dgvHoiVien.BackgroundColor        = System.Drawing.Color.White;
            this.dgvHoiVien.BorderStyle            = BorderStyle.None;
            this.dgvHoiVien.GridColor              = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvHoiVien.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.dgvHoiVien.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 30, 60);
            this.dgvHoiVien.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHoiVien.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.dgvHoiVien.ColumnHeadersHeight    = 36;
            this.dgvHoiVien.EnableHeadersVisualStyles = false;
            this.dgvHoiVien.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);

            // ===================== WIRE UP =====================
            this.Controls.Add(this.dgvHoiVien);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);

            this.grpGioiTinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvHoiVien).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ---- Helper methods ----
        private void SetLabel(Label lbl, int x, int y, int w)
        {
            lbl.Location  = new System.Drawing.Point(x, y + 4);
            lbl.Size      = new System.Drawing.Size(w, 24);
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lbl.Font      = new System.Drawing.Font("Segoe UI", 9.5f);
        }

        private void SetButton(Button btn, string text, System.Drawing.Color color, int x, int y)
        {
            btn.Text      = text;
            btn.Location  = new System.Drawing.Point(x, y);
            btn.Size      = new System.Drawing.Size(118, 36);
            btn.BackColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btn.Cursor    = Cursors.Hand;
        }

        #endregion

        // ---- Controls ----
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHoTen, lblSDT, lblEmail, lblNgaySinh, lblHang;
        private System.Windows.Forms.TextBox txtHoTen, txtSDT, txtEmail;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboHang;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.GroupBox grpGioiTinh;
        private System.Windows.Forms.RadioButton rdoNam, rdoNu;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem;
        private System.Windows.Forms.Label lblTimHoTen, lblTimHang;
        private System.Windows.Forms.TextBox txtTimHoTen;
        private System.Windows.Forms.ComboBox cboTimHang;
        private System.Windows.Forms.DataGridView dgvHoiVien;
        private System.Windows.Forms.Panel pnlHeader, pnlMain, pnlButtons, pnlSearch;
    }
}
