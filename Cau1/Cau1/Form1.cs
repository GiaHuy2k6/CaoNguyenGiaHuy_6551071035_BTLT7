using Cau1.Models;
using Microsoft.EntityFrameworkCore;

namespace Cau1
{
    public partial class Form1 : Form
    {
        private readonly NhaSachTriThucDBContext _context;
        private List<TheLoaiSach> _allData = new();

        public Form1(NhaSachTriThucDBContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadDataGridAsync();

            // Subscribe to events
            dgvTheLoai.SelectionChanged += DgvTheLoai_SelectionChanged;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            // Real-time search
            txtTimKiem.TextChanged += async (s, ev) => await PerformSearch();
        }

        private async Task LoadDataGridAsync()
        {
            try
            {
                _allData = await _context.TheLoaiSaches.ToListAsync();
                RefreshDataGrid(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGrid(List<TheLoaiSach> data)
        {
            dgvTheLoai.DataSource = null;
            dgvTheLoai.DataSource = data;

            // Rename column headers to Vietnamese
            if (dgvTheLoai.Columns.Contains("MaTl"))
                dgvTheLoai.Columns["MaTl"].HeaderText = "Mã TL";
            if (dgvTheLoai.Columns.Contains("TenTheLoai"))
                dgvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
            if (dgvTheLoai.Columns.Contains("MoTa"))
                dgvTheLoai.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvTheLoai.Columns.Contains("SoLuongSach"))
                dgvTheLoai.Columns["SoLuongSach"].HeaderText = "Số lượng sách";
            if (dgvTheLoai.Columns.Contains("NgayTao"))
                dgvTheLoai.Columns["NgayTao"].HeaderText = "Ngày tạo";

            // Auto-adjust column widths
            foreach (DataGridViewColumn column in dgvTheLoai.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void DgvTheLoai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTheLoai.SelectedRows.Count > 0)
            {
                var selectedRow = dgvTheLoai.SelectedRows[0];

                txtMaTL.Text = selectedRow.Cells["MaTl"].Value?.ToString() ?? "";
                txtTenTL.Text = selectedRow.Cells["TenTheLoai"].Value?.ToString() ?? "";
                txtMoTa.Text = selectedRow.Cells["MoTa"].Value?.ToString() ?? "";
                lblNgayTao.Text = selectedRow.Cells["NgayTao"].Value?.ToString() ?? "";
            }
        }

        private async void BtnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            // Check for duplicate name
            if (_allData.Any(x => x.TenTheLoai.Equals(txtTenTL.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var newCategory = new TheLoaiSach
                {
                    TenTheLoai = txtTenTL.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    NgayTao = DateTime.Now
                };

                _context.TheLoaiSaches.Add(newCategory);
                await _context.SaveChangesAsync();

                MessageBox.Show("Thêm thể loại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadDataGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTL.Text))
            {
                MessageBox.Show("Vui lòng chọn thể loại để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                if (!int.TryParse(txtMaTL.Text, out int maTL))
                {
                    MessageBox.Show("Mã thể loại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var category = await _context.TheLoaiSaches.FindAsync(maTL);
                if (category == null)
                {
                    MessageBox.Show("Thể loại không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check for duplicate name (excluding current item)
                if (_allData.Any(x => x.MaTl != maTL && x.TenTheLoai.Equals(txtTenTL.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Tên thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                category.TenTheLoai = txtTenTL.Text.Trim();
                category.MoTa = txtMoTa.Text.Trim();

                _context.TheLoaiSaches.Update(category);
                await _context.SaveChangesAsync();

                MessageBox.Show("Cập nhật thể loại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadDataGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTL.Text))
            {
                MessageBox.Show("Vui lòng chọn thể loại để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa thể loại này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                if (!int.TryParse(txtMaTL.Text, out int maTL))
                {
                    MessageBox.Show("Mã thể loại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var category = await _context.TheLoaiSaches.FindAsync(maTL);
                if (category == null)
                {
                    MessageBox.Show("Thể loại không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _context.TheLoaiSaches.Remove(category);
                await _context.SaveChangesAsync();

                MessageBox.Show("Xóa thể loại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadDataGridAsync();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    "Không thể xóa thể loại này vì nó đang được sách khác tham chiếu!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnTimKiem_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }

        private async Task PerformSearch()
        {
            try
            {
                string searchText = txtTimKiem.Text.Trim();

                if (string.IsNullOrEmpty(searchText))
                {
                    await LoadDataGridAsync();
                    return;
                }

                var filteredData = _allData
                    .Where(x => x.TenTheLoai.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                RefreshDataGrid(filteredData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtTimKiem.Clear();
            await LoadDataGridAsync();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenTL.Text))
            {
                MessageBox.Show("Tên thể loại không được bỏ trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTL.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtMaTL.Clear();
            txtTenTL.Clear();
            txtMoTa.Clear();
            lblNgayTao.Text = "";
            dgvTheLoai.ClearSelection();
        }
    }
}
