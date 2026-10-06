using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Cau4.Models;

namespace Cau4
{
    public partial class Form2 : Form
    {
        private AnKhangClinicDbContext db = new AnKhangClinicDbContext();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvBacSi.DataSource = db.BacSis.Select(x => new
            {
                x.MaBs,
                x.HoTen,
                x.ChuyenKhoa,
                SDT = x.Sdt
            }).ToList();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Không được bỏ trống Họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var bacSi = new BacSi
            {
                HoTen = txtHoTen.Text,
                ChuyenKhoa = txtChuyenKhoa.Text,
                Sdt = txtSDT.Text
            };
            
            db.BacSis.Add(bacSi);
            db.SaveChanges();
            MessageBox.Show("Thêm thành công!");
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvBacSi.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Không được bỏ trống Họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int maBS = (int)dgvBacSi.SelectedRows[0].Cells["MaBs"].Value;
                var bacSi = db.BacSis.Find(maBS);
                if (bacSi != null)
                {
                    bacSi.HoTen = txtHoTen.Text;
                    bacSi.ChuyenKhoa = txtChuyenKhoa.Text;
                    bacSi.Sdt = txtSDT.Text;

                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvBacSi.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa? Thao tác này có thể lỗi nếu bác sĩ đã có lịch khám.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int maBS = (int)dgvBacSi.SelectedRows[0].Cells["MaBs"].Value;
                    var bacSi = db.BacSis.Find(maBS);
                    if (bacSi != null)
                    {
                        try
                        {
                            db.BacSis.Remove(bacSi);
                            db.SaveChanges();
                            MessageBox.Show("Xóa thành công!");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể xóa bác sĩ này. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtChuyenKhoa.Clear();
            txtSDT.Clear();
        }

        private void dgvBacSi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBacSi.Rows[e.RowIndex];
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtChuyenKhoa.Text = row.Cells["ChuyenKhoa"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
            }
        }
    }
}
