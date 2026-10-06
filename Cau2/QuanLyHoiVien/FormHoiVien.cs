using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyHoiVien.Data;
using QuanLyHoiVien.Models;

namespace QuanLyHoiVien
{
    public partial class FormHoiVien : Form
    {
        private readonly AppDbContext _context = new AppDbContext();
        private int _selectedMaHV = 0; // 0 = không có hội viên nào được chọn

        public FormHoiVien()
        {
            InitializeComponent();

            // Đảm bảo CSDL tồn tại
            try { _context.Database.EnsureCreated(); }
            catch { /* Bảng có thể đã được tạo từ script SQL */ }

            // Đăng ký sự kiện
            btnThem.Click    += BtnThem_Click;
            btnSua.Click     += BtnSua_Click;
            btnXoa.Click     += BtnXoa_Click;
            btnLamMoi.Click  += BtnLamMoi_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvHoiVien.SelectionChanged += DgvHoiVien_SelectionChanged;

            LoadDanhSach();
        }

        // ===================================================
        //  LOAD DANH SÁCH
        // ===================================================
        private void LoadDanhSach(List<HoiVien>? data = null)
        {
            var ds = data ?? _context.HoiViens.AsNoTracking().OrderBy(h => h.MaHV).ToList();

            // Hiển thị dữ liệu đã định dạng lên DGV
            var display = ds.Select(h => new
            {
                h.MaHV,
                h.HoTen,
                GioiTinh      = h.GioiTinh ? "Nam" : "Nữ",
                NgaySinh      = h.NgaySinh.HasValue ? h.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                h.SDT,
                h.Email,
                h.HangThanhVien,
                TrangThai     = h.TrangThai ? "Đang hoạt" : "Tạm ngưng"
            }).ToList();

            dgvHoiVien.DataSource = display;

            // Đặt tên cột cho đẹp
            if (dgvHoiVien.Columns.Count > 0)
            {
                dgvHoiVien.Columns["MaHV"].HeaderText          = "Mã HV";
                dgvHoiVien.Columns["HoTen"].HeaderText         = "Họ tên";
                dgvHoiVien.Columns["GioiTinh"].HeaderText      = "Giới tính";
                dgvHoiVien.Columns["NgaySinh"].HeaderText      = "Ngày sinh";
                dgvHoiVien.Columns["SDT"].HeaderText           = "SĐT";
                dgvHoiVien.Columns["Email"].HeaderText         = "Email";
                dgvHoiVien.Columns["HangThanhVien"].HeaderText = "Hạng thành viên";
                dgvHoiVien.Columns["TrangThai"].HeaderText     = "Trạng thái";

                dgvHoiVien.Columns["MaHV"].FillWeight          = 50;
                dgvHoiVien.Columns["HoTen"].FillWeight         = 140;
                dgvHoiVien.Columns["GioiTinh"].FillWeight      = 65;
                dgvHoiVien.Columns["NgaySinh"].FillWeight      = 90;
                dgvHoiVien.Columns["SDT"].FillWeight           = 90;
                dgvHoiVien.Columns["Email"].FillWeight         = 130;
                dgvHoiVien.Columns["HangThanhVien"].FillWeight = 100;
                dgvHoiVien.Columns["TrangThai"].FillWeight     = 80;
            }
        }

        // ===================================================
        //  ĐIỀN DỮ LIỆU TỪ DGV LÊN CONTROLS
        // ===================================================
        private void DgvHoiVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow == null) return;

            int maHV = Convert.ToInt32(dgvHoiVien.CurrentRow.Cells["MaHV"].Value);
            var hv = _context.HoiViens.Find(maHV);
            if (hv == null) return;

            _selectedMaHV        = hv.MaHV;
            txtHoTen.Text        = hv.HoTen;
            txtSDT.Text          = hv.SDT ?? "";
            txtEmail.Text        = hv.Email ?? "";
            dtpNgaySinh.Value    = hv.NgaySinh ?? DateTime.Today;
            cboHang.Text         = hv.HangThanhVien ?? "Basic";
            chkTrangThai.Checked = hv.TrangThai;
            rdoNam.Checked       = hv.GioiTinh;
            rdoNu.Checked        = !hv.GioiTinh;
        }

        // ===================================================
        //  VALIDATE
        // ===================================================
        private bool Validate(out string errorMsg)
        {
            errorMsg = "";

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { errorMsg = "Họ tên không được để trống."; return false; }

            string sdt = txtSDT.Text.Trim();
            if (!string.IsNullOrWhiteSpace(sdt))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{9,11}$"))
                { errorMsg = "Số điện thoại chỉ chứa chữ số và phải đủ 9-11 ký tự."; return false; }
            }

            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains('@'))
                { errorMsg = "Email phải chứa ký tự '@'."; return false; }
            }

            int tuoi = CalculateAge(dtpNgaySinh.Value);
            if (tuoi < 15)
            { errorMsg = "Tuổi hội viên phải từ 15 tuổi trở lên mới được đăng ký."; return false; }

            return true;
        }

        private int CalculateAge(DateTime ngaySinh)
        {
            var today = DateTime.Today;
            int age = today.Year - ngaySinh.Year;
            if (ngaySinh.Date > today.AddYears(-age)) age--;
            return age;
        }

        // ===================================================
        //  LẤY DỮ LIỆU TỪ CONTROLS -> OBJECT
        // ===================================================
        private HoiVien LayDuLieuForm()
        {
            return new HoiVien
            {
                HoTen         = txtHoTen.Text.Trim(),
                GioiTinh      = rdoNam.Checked,
                NgaySinh      = dtpNgaySinh.Value.Date,
                SDT           = txtSDT.Text.Trim(),
                Email         = txtEmail.Text.Trim(),
                HangThanhVien = cboHang.Text,
                TrangThai     = chkTrangThai.Checked
            };
        }

        // ===================================================
        //  THÊM
        // ===================================================
        private async void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!Validate(out string msg))
            { MessageBox.Show(msg, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var hv = LayDuLieuForm();
            hv.NgayDangKy = DateTime.Now;

            _context.HoiViens.Add(hv);
            await _context.SaveChangesAsync();

            MessageBox.Show($"Đã thêm hội viên '{hv.HoTen}' thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoiForm();
        }

        // ===================================================
        //  SỬA
        // ===================================================
        private async void BtnSua_Click(object? sender, EventArgs e)
        {
            if (_selectedMaHV == 0)
            { MessageBox.Show("Vui lòng chọn một hội viên trong danh sách để sửa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (!Validate(out string msg))
            { MessageBox.Show(msg, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var hv = await _context.HoiViens.FindAsync(_selectedMaHV);
            if (hv == null) { MessageBox.Show("Không tìm thấy hội viên!"); return; }

            hv.HoTen         = txtHoTen.Text.Trim();
            hv.GioiTinh      = rdoNam.Checked;
            hv.NgaySinh      = dtpNgaySinh.Value.Date;
            hv.SDT           = txtSDT.Text.Trim();
            hv.Email         = txtEmail.Text.Trim();
            hv.HangThanhVien = cboHang.Text;
            hv.TrangThai     = chkTrangThai.Checked;

            await _context.SaveChangesAsync();

            MessageBox.Show($"Đã cập nhật thông tin hội viên '{hv.HoTen}' thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoiForm();
        }

        // ===================================================
        //  XÓA — hiển thị xác nhận YesNo + thông báo kết quả
        // ===================================================
        private async void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (_selectedMaHV == 0)
            { MessageBox.Show("Vui lòng chọn một hội viên trong danh sách để xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            // Xác nhận YesNo
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa hội viên Mã HV = {_selectedMaHV} không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var hv = await _context.HoiViens.FindAsync(_selectedMaHV);
                if (hv != null)
                {
                    _context.HoiViens.Remove(hv);
                    await _context.SaveChangesAsync();

                    // Thông báo kết quả sau SaveChangesAsync
                    MessageBox.Show($"Đã xóa hội viên '{hv.HoTen}' thành công!", "Kết quả",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadDanhSach();
                LamMoiForm();
            }
        }

        // ===================================================
        //  LÀM MỚI
        // ===================================================
        private void BtnLamMoi_Click(object? sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            _selectedMaHV        = 0;
            txtHoTen.Text        = "";
            txtSDT.Text          = "";
            txtEmail.Text        = "";
            dtpNgaySinh.Value    = DateTime.Today;
            cboHang.SelectedIndex = 0;
            chkTrangThai.Checked = true;
            rdoNam.Checked       = true;
            dgvHoiVien.ClearSelection();
        }

        // ===================================================
        //  TÌM KIẾM — kết hợp 2 điều kiện (LINQ Where + &&)
        // ===================================================
        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            string tuKhoaHoTen = txtTimHoTen.Text.Trim();
            string hangTim     = cboTimHang.Text;

            var query = _context.HoiViens.AsNoTracking().AsQueryable();

            // Điều kiện 1: Tìm theo họ tên (Contains - gần đúng)
            if (!string.IsNullOrWhiteSpace(tuKhoaHoTen))
                query = query.Where(h => h.HoTen.Contains(tuKhoaHoTen));

            // Điều kiện 2: Tìm theo Hạng thành viên
            if (hangTim != "(Tất cả)" && !string.IsNullOrWhiteSpace(hangTim))
                query = query.Where(h => h.HangThanhVien == hangTim);

            var ketQua = query.OrderBy(h => h.MaHV).ToList();
            LoadDanhSach(ketQua);

            if (ketQua.Count == 0)
                MessageBox.Show("Không tìm thấy hội viên phù hợp.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
