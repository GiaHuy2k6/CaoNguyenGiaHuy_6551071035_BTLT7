using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau3_App.Models;

namespace Cau3_App
{
    public partial class Form1 : Form
    {
        private SunriseHomestayContext db;
        
        private TextBox txtSoPhong;
        private NumericUpDown nudTangSo;
        private ComboBox cboLoaiPhong;
        private ComboBox cboTinhTrang;
        private PictureBox picHinhAnh;
        private Button btnChonAnh;
        private Button btnThem, btnSua, btnXoa, btnLamMoi;
        private ComboBox cboLocLoaiPhong, cboLocTinhTrang;
        private Button btnTimKiem;
        private DataGridView dgvPhong;
        
        private string hinhAnhFileName = "";
        private int selectedPhongId = -1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản Lý Phòng - Sunrise Homestay";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Labels and Inputs
            int startX = 20;
            int startY = 20;

            Label lblSoPhong = new Label() { Text = "Số phòng", Location = new Point(startX, startY), AutoSize = true };
            txtSoPhong = new TextBox() { Location = new Point(startX, startY + 20), Width = 100 };

            Label lblTangSo = new Label() { Text = "Tầng số", Location = new Point(startX + 120, startY), AutoSize = true };
            nudTangSo = new NumericUpDown() { Location = new Point(startX + 120, startY + 20), Width = 80, Minimum = 1, Maximum = 100 };

            Label lblLoaiPhong = new Label() { Text = "Loại phòng", Location = new Point(startX + 220, startY), AutoSize = true };
            cboLoaiPhong = new ComboBox() { Location = new Point(startX + 220, startY + 20), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblTinhTrang = new Label() { Text = "Tình trạng", Location = new Point(startX + 360, startY), AutoSize = true };
            cboTinhTrang = new ComboBox() { Location = new Point(startX + 360, startY + 20), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTinhTrang.Items.AddRange(new string[] { "Trống", "Đang ở", "Đang dọn" });

            picHinhAnh = new PictureBox() { Location = new Point(startX + 500, startY), Size = new Size(100, 80), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
            btnChonAnh = new Button() { Text = "Chọn ảnh...", Location = new Point(startX + 500, startY + 85), Width = 100 };
            btnChonAnh.Click += BtnChonAnh_Click;

            // Buttons
            btnThem = new Button() { Text = "Thêm", Location = new Point(startX + 650, startY + 20) };
            btnThem.Click += BtnThem_Click;
            btnSua = new Button() { Text = "Sửa", Location = new Point(startX + 740, startY + 20) };
            btnSua.Click += BtnSua_Click;
            btnXoa = new Button() { Text = "Xóa", Location = new Point(startX + 830, startY + 20) };
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi = new Button() { Text = "Làm mới", Location = new Point(startX + 920, startY + 20) };
            btnLamMoi.Click += BtnLamMoi_Click;

            // Filter
            int filterY = startY + 120;
            cboLocLoaiPhong = new ComboBox() { Location = new Point(startX, filterY), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLocTinhTrang = new ComboBox() { Location = new Point(startX + 170, filterY), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLocTinhTrang.Items.AddRange(new string[] { "Tất cả", "Trống", "Đang ở", "Đang dọn" });
            cboLocTinhTrang.SelectedIndex = 0;
            btnTimKiem = new Button() { Text = "Tìm kiếm", Location = new Point(startX + 340, filterY - 2) };
            btnTimKiem.Click += BtnTimKiem_Click;

            // DataGridView
            dgvPhong = new DataGridView() { Location = new Point(startX, filterY + 30), Size = new Size(940, 350), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false, RowTemplate = { Height = 60 } };
            dgvPhong.CellClick += DgvPhong_CellClick;

            this.Controls.Add(lblSoPhong); this.Controls.Add(txtSoPhong);
            this.Controls.Add(lblTangSo); this.Controls.Add(nudTangSo);
            this.Controls.Add(lblLoaiPhong); this.Controls.Add(cboLoaiPhong);
            this.Controls.Add(lblTinhTrang); this.Controls.Add(cboTinhTrang);
            this.Controls.Add(picHinhAnh); this.Controls.Add(btnChonAnh);
            this.Controls.Add(btnThem); this.Controls.Add(btnSua);
            this.Controls.Add(btnXoa); this.Controls.Add(btnLamMoi);
            this.Controls.Add(cboLocLoaiPhong); this.Controls.Add(cboLocTinhTrang); this.Controls.Add(btnTimKiem);
            this.Controls.Add(dgvPhong);

            // Add a menu for opening LoaiPhong form
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem mnuQuanLy = new ToolStripMenuItem("Hệ thống");
            ToolStripMenuItem mnuLoaiPhong = new ToolStripMenuItem("Quản lý Loại phòng");
            mnuLoaiPhong.Click += (s, e) => { new FormLoaiPhong().ShowDialog(); LoadLoaiPhong(); };
            mnuQuanLy.DropDownItems.Add(mnuLoaiPhong);
            menu.Items.Add(mnuQuanLy);
            this.MainMenuStrip = menu;
            this.Controls.Add(menu);

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new SunriseHomestayContext();
            string imgDir = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(imgDir)) Directory.CreateDirectory(imgDir);

            LoadLoaiPhong();
            LoadData();
        }

        private void LoadLoaiPhong()
        {
            var dsLoaiPhong = db.LoaiPhongs.ToList();
            
            cboLoaiPhong.DataSource = dsLoaiPhong;
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";

            var dsLoc = dsLoaiPhong.ToList();
            dsLoc.Insert(0, new LoaiPhong { MaLoai = 0, TenLoai = "Tất cả" });
            cboLocLoaiPhong.DataSource = dsLoc;
            cboLocLoaiPhong.DisplayMember = "TenLoai";
            cboLocLoaiPhong.ValueMember = "MaLoai";
            cboLocLoaiPhong.SelectedIndex = 0;
        }

        private void LoadData(int filterLoaiPhong = 0, string filterTinhTrang = "Tất cả")
        {
            var query = db.Phongs.Include(x => x.MaLoaiNavigation).AsQueryable();
            if (filterLoaiPhong != 0)
                query = query.Where(x => x.MaLoai == filterLoaiPhong);
            if (filterTinhTrang != "Tất cả")
                query = query.Where(x => x.TinhTrang == filterTinhTrang);

            var list = query.ToList();

            dgvPhong.Columns.Clear();
            dgvPhong.AutoGenerateColumns = false;

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaPhong", DataPropertyName = "MaPhong", HeaderText = "Mã phòng" });
            
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Anh";
            imgCol.HeaderText = "Ảnh";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvPhong.Columns.Add(imgCol);

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoPhong", DataPropertyName = "SoPhong", HeaderText = "Số phòng" });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "TangSo", DataPropertyName = "TangSo", HeaderText = "Tầng" });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenLoai", HeaderText = "Loại phòng" });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiaMoiDem", HeaderText = "Giá/đêm" });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "TinhTrang", DataPropertyName = "TinhTrang", HeaderText = "Tình trạng" });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "HinhAnh", DataPropertyName = "HinhAnh", Visible = false });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaLoai", DataPropertyName = "MaLoai", Visible = false });

            dgvPhong.DataSource = list;

            foreach (DataGridViewRow row in dgvPhong.Rows)
            {
                var p = (Phong)row.DataBoundItem;
                row.Cells["TenLoai"].Value = p.MaLoaiNavigation?.TenLoai;
                row.Cells["GiaMoiDem"].Value = p.MaLoaiNavigation?.GiaMoiDem?.ToString("N0");

                if (!string.IsNullOrEmpty(p.HinhAnh))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", p.HinhAnh);
                    if (File.Exists(path))
                    {
                        try { row.Cells["Anh"].Value = Image.FromFile(path); } catch { }
                    }
                }
            }
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string destFolder = Path.Combine(Application.StartupPath, "Images");
                    string fileName = Path.GetFileName(ofd.FileName);
                    string destFile = Path.Combine(destFolder, fileName);
                    
                    if (!File.Exists(destFile))
                        File.Copy(ofd.FileName, destFile);

                    hinhAnhFileName = fileName;
                    picHinhAnh.Image = Image.FromFile(destFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi chọn ảnh: " + ex.Message);
                }
            }
        }

        private void DgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPhong.Rows[e.RowIndex];
                selectedPhongId = (int)row.Cells["MaPhong"].Value;
                txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString();
                nudTangSo.Value = Convert.ToDecimal(row.Cells["TangSo"].Value);
                cboLoaiPhong.SelectedValue = row.Cells["MaLoai"].Value;
                cboTinhTrang.SelectedItem = row.Cells["TinhTrang"].Value?.ToString();
                hinhAnhFileName = row.Cells["HinhAnh"].Value?.ToString() ?? "";
                
                picHinhAnh.Image = null;
                if (!string.IsNullOrEmpty(hinhAnhFileName))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", hinhAnhFileName);
                    if (File.Exists(path))
                    {
                        try { picHinhAnh.Image = Image.FromFile(path); } catch { }
                    }
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoPhong.Text = "";
            nudTangSo.Value = 1;
            if (cboLoaiPhong.Items.Count > 0) cboLoaiPhong.SelectedIndex = 0;
            if (cboTinhTrang.Items.Count > 0) cboTinhTrang.SelectedIndex = 0;
            picHinhAnh.Image = null;
            hinhAnhFileName = "";
            selectedPhongId = -1;
            cboLocLoaiPhong.SelectedIndex = 0;
            cboLocTinhTrang.SelectedIndex = 0;
            LoadData();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text)) { MessageBox.Show("Nhập số phòng"); return; }
            var p = new Phong
            {
                SoPhong = txtSoPhong.Text,
                TangSo = (int)nudTangSo.Value,
                MaLoai = (int)cboLoaiPhong.SelectedValue,
                TinhTrang = cboTinhTrang.SelectedItem.ToString(),
                HinhAnh = hinhAnhFileName
            };
            db.Phongs.Add(p);
            db.SaveChanges();
            LoadData((int)cboLocLoaiPhong.SelectedValue, cboLocTinhTrang.SelectedItem.ToString());
            BtnLamMoi_Click(null, null);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedPhongId == -1) return;
            var p = db.Phongs.Find(selectedPhongId);
            if (p != null)
            {
                p.SoPhong = txtSoPhong.Text;
                p.TangSo = (int)nudTangSo.Value;
                p.MaLoai = (int)cboLoaiPhong.SelectedValue;
                p.TinhTrang = cboTinhTrang.SelectedItem.ToString();
                p.HinhAnh = hinhAnhFileName;
                db.SaveChanges();
                LoadData((int)cboLocLoaiPhong.SelectedValue, cboLocTinhTrang.SelectedItem.ToString());
                BtnLamMoi_Click(null, null);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedPhongId == -1) return;
            if (MessageBox.Show("Bạn có chắc chắn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var p = db.Phongs.Find(selectedPhongId);
                if (p != null)
                {
                    db.Phongs.Remove(p);
                    db.SaveChanges();
                    LoadData((int)cboLocLoaiPhong.SelectedValue, cboLocTinhTrang.SelectedItem.ToString());
                    BtnLamMoi_Click(null, null);
                }
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            int loai = (int)cboLocLoaiPhong.SelectedValue;
            string tinh = cboLocTinhTrang.SelectedItem.ToString();
            LoadData(loai, tinh);
        }
    }
}
