using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau4.Models;

namespace Cau4
{
    public partial class Form1 : Form
    {
        private AnKhangClinicDbContext db = new AnKhangClinicDbContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboboxes();
            LoadData();
        }

        private void LoadComboboxes()
        {
            var bacSis = db.BacSis.ToList();
            
            cboBacSi.DataSource = new List<BacSi>(bacSis);
            cboBacSi.DisplayMember = "HoTen";
            cboBacSi.ValueMember = "MaBs";
            cboBacSi.Format += (s, e) => 
            {
                if (e.ListItem is BacSi bs)
                {
                    e.Value = $"{bs.HoTen} - {bs.ChuyenKhoa}";
                }
            };
            
            var filterBacSis = new List<BacSi> { new BacSi { MaBs = 0, HoTen = "Tất cả Bác sĩ" } };
            filterBacSis.AddRange(bacSis);
            cboFilterBacSi.DataSource = filterBacSis;
            cboFilterBacSi.DisplayMember = "HoTen";
            cboFilterBacSi.ValueMember = "MaBs";

            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            var query = db.LichKhams.Include(x => x.MaBsNavigation).AsQueryable();

            int filterBacSiId = (int)cboFilterBacSi.SelectedValue;
            if (filterBacSiId != 0)
            {
                query = query.Where(x => x.MaBs == filterBacSiId);
            }

            var tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value.Date);
            var denNgay = DateOnly.FromDateTime(dtpDenNgay.Value.Date);
            
            query = query.Where(x => x.NgayKham >= tuNgay && x.NgayKham <= denNgay);

            var list = query.ToList().Select(x => new
            {
                x.MaLich,
                x.TenBenhNhan,
                SDT = x.Sdt,
                NgayKham = x.NgayKham?.ToString("dd/MM/yyyy"),
                x.GioKham,
                BacSi = x.MaBsNavigation != null ? $"{x.MaBsNavigation.HoTen} - {x.MaBsNavigation.ChuyenKhoa}" : "",
                ChuyenKhoa = x.MaBsNavigation?.ChuyenKhoa,
                x.TrangThai
            }).ToList();

            dgvLichKham.DataSource = list;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenBenhNhan.Text))
            {
                MessageBox.Show("Không được bỏ trống Tên bệnh nhân.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboBacSi.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn Bác sĩ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpNgayKham.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Không cho đặt lịch khám vào ngày trong quá khứ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lichKham = new LichKham
            {
                TenBenhNhan = txtTenBenhNhan.Text,
                Sdt = txtSDT.Text,
                NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value),
                GioKham = dtpGioKham.Value.ToString("HH:mm"),
                MaBs = (int)cboBacSi.SelectedValue,
                TrangThai = cboTrangThai.Text
            };

            db.LichKhams.Add(lichKham);
            db.SaveChanges();
            MessageBox.Show("Thêm thành công!");
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLichKham.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtTenBenhNhan.Text))
                {
                    MessageBox.Show("Không được bỏ trống Tên bệnh nhân.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cboBacSi.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn Bác sĩ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int maLich = (int)dgvLichKham.SelectedRows[0].Cells["MaLich"].Value;
                var lichKham = db.LichKhams.Find(maLich);
                if (lichKham != null)
                {
                    lichKham.TenBenhNhan = txtTenBenhNhan.Text;
                    lichKham.Sdt = txtSDT.Text;
                    lichKham.NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value);
                    lichKham.GioKham = dtpGioKham.Value.ToString("HH:mm");
                    lichKham.MaBs = (int)cboBacSi.SelectedValue;
                    lichKham.TrangThai = cboTrangThai.Text;

                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichKham.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int maLich = (int)dgvLichKham.SelectedRows[0].Cells["MaLich"].Value;
                    var lichKham = db.LichKhams.Find(maLich);
                    if (lichKham != null)
                    {
                        db.LichKhams.Remove(lichKham);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenBenhNhan.Clear();
            txtSDT.Clear();
            dtpNgayKham.Value = DateTime.Now;
            dtpGioKham.Value = DateTime.Now;
            if (cboBacSi.Items.Count > 0) cboBacSi.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            cboFilterBacSi.SelectedIndex = 0;
            LoadData();
        }

        private void dgvLichKham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLichKham.Rows[e.RowIndex];
                txtTenBenhNhan.Text = row.Cells["TenBenhNhan"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                
                string ngayKhamStr = row.Cells["NgayKham"].Value?.ToString();
                if (DateTime.TryParseExact(ngayKhamStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                {
                    dtpNgayKham.Value = dt;
                }

                string gioKhamStr = row.Cells["GioKham"].Value?.ToString();
                if (DateTime.TryParseExact(gioKhamStr, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime time))
                {
                    dtpGioKham.Value = time;
                }

                cboTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
                
                string bs = row.Cells["BacSi"].Value?.ToString();
                if (!string.IsNullOrEmpty(bs))
                {
                    foreach (var item in cboBacSi.Items)
                    {
                        if (item is BacSi bacSi)
                        {
                            string itemText = $"{bacSi.HoTen} - {bacSi.ChuyenKhoa}";
                            if (itemText == bs)
                            {
                                cboBacSi.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void quảnLýBácSĩToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            LoadComboboxes();
            LoadData();
        }
    }
}
