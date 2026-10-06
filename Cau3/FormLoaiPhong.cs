using System;
using System.Linq;
using System.Windows.Forms;
using Cau3_App.Models;

namespace Cau3_App
{
    public partial class FormLoaiPhong : Form
    {
        private SunriseHomestayContext db = new SunriseHomestayContext();
        
        private TextBox txtTenLoai = new TextBox();
        private TextBox txtGiaMoiDem = new TextBox();
        private TextBox txtMoTa = new TextBox();
        private Button btnThem = new Button();
        private Button btnSua = new Button();
        private Button btnXoa = new Button();
        private Button btnLamMoi = new Button();
        private DataGridView dgvLoaiPhong = new DataGridView();
        private int selectedId = -1;

        public FormLoaiPhong()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Loại phòng";
            this.Size = new System.Drawing.Size(600, 400);

            Label lbl1 = new Label() { Text = "Tên loại:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            txtTenLoai.Location = new System.Drawing.Point(100, 20);
            
            Label lbl2 = new Label() { Text = "Giá/đêm:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            txtGiaMoiDem.Location = new System.Drawing.Point(100, 60);

            Label lbl3 = new Label() { Text = "Mô tả:", Location = new System.Drawing.Point(250, 20), AutoSize = true };
            txtMoTa.Location = new System.Drawing.Point(300, 20);
            txtMoTa.Multiline = true;
            txtMoTa.Size = new System.Drawing.Size(250, 60);

            btnThem.Text = "Thêm"; btnThem.Location = new System.Drawing.Point(20, 100);
            btnThem.Click += BtnThem_Click;

            btnSua.Text = "Sửa"; btnSua.Location = new System.Drawing.Point(100, 100);
            btnSua.Click += BtnSua_Click;

            btnXoa.Text = "Xóa"; btnXoa.Location = new System.Drawing.Point(180, 100);
            btnXoa.Click += BtnXoa_Click;

            btnLamMoi.Text = "Làm mới"; btnLamMoi.Location = new System.Drawing.Point(260, 100);
            btnLamMoi.Click += (s, e) => ResetFields();

            dgvLoaiPhong.Location = new System.Drawing.Point(20, 140);
            dgvLoaiPhong.Size = new System.Drawing.Size(530, 200);
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.CellClick += DgvLoaiPhong_CellClick;

            this.Controls.Add(lbl1); this.Controls.Add(txtTenLoai);
            this.Controls.Add(lbl2); this.Controls.Add(txtGiaMoiDem);
            this.Controls.Add(lbl3); this.Controls.Add(txtMoTa);
            this.Controls.Add(btnThem); this.Controls.Add(btnSua);
            this.Controls.Add(btnXoa); this.Controls.Add(btnLamMoi);
            this.Controls.Add(dgvLoaiPhong);

            this.Load += FormLoaiPhong_Load;
        }

        private void FormLoaiPhong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvLoaiPhong.DataSource = db.LoaiPhongs.Select(x => new {
                x.MaLoai, x.TenLoai, x.GiaMoiDem, x.MoTa
            }).ToList();
        }

        private void ResetFields()
        {
            txtTenLoai.Text = "";
            txtGiaMoiDem.Text = "";
            txtMoTa.Text = "";
            selectedId = -1;
        }

        private void DgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvLoaiPhong.Rows[e.RowIndex];
                selectedId = (int)row.Cells["MaLoai"].Value;
                txtTenLoai.Text = row.Cells["TenLoai"].Value?.ToString();
                txtGiaMoiDem.Text = row.Cells["GiaMoiDem"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var lp = new LoaiPhong
            {
                TenLoai = txtTenLoai.Text,
                GiaMoiDem = decimal.TryParse(txtGiaMoiDem.Text, out decimal gia) ? gia : 0,
                MoTa = txtMoTa.Text
            };
            db.LoaiPhongs.Add(lp);
            db.SaveChanges();
            LoadData();
            ResetFields();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedId != -1)
            {
                var lp = db.LoaiPhongs.Find(selectedId);
                if (lp != null)
                {
                    lp.TenLoai = txtTenLoai.Text;
                    lp.GiaMoiDem = decimal.TryParse(txtGiaMoiDem.Text, out decimal gia) ? gia : 0;
                    lp.MoTa = txtMoTa.Text;
                    db.SaveChanges();
                    LoadData();
                    ResetFields();
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId != -1)
            {
                var lp = db.LoaiPhongs.Find(selectedId);
                if (lp != null)
                {
                    db.LoaiPhongs.Remove(lp);
                    db.SaveChanges();
                    LoadData();
                    ResetFields();
                }
            }
        }
    }
}
