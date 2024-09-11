using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class Admin : Form
    {
        private Admin ad;
        public Admin Ad
        {
            get { return ad; }
            set { ad = value; }
        }
        public DateTime SelectedFromDate
        {
            get { return dtpkFromDate.Value; }
        }

        public DateTime SelectedToDate
        {
            get { return dtpkToDate.Value; }
        }
        BindingSource nhanVienList = new BindingSource();
        BindingSource chucVuList = new BindingSource();
        BindingSource khachHangList = new BindingSource();
        BindingSource phongList = new BindingSource();
        BindingSource loaiPhongList = new BindingSource();
        BindingSource dichVuList = new BindingSource();
        BindingSource chiTietDichVuList = new BindingSource();
        BindingSource hoaDonList = new BindingSource();
        BindingSource taiKhoanList = new BindingSource();
        BindingSource datPhongList = new BindingSource();
        public Admin()
        {
            InitializeComponent();
            loadDanhThu();
        }

        #region Methods DoanhThu
        void loadDanhThu()
        {
            loadDateTimePicker();
            LoadListHoaDonByDate(dtpkFromDate.Value, dtpkToDate.Value);
            dtgvHoaDon.ReadOnly = true;
            dtgvHoaDon.AllowUserToAddRows = false;
            //dtgvHoaDon.RowHeadersVisible = false;
        }
        private void dtgvDoanhThu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = dtgvHoaDon.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    dtgvHoaDon.DoDragDrop(dtgvHoaDon.Rows[info.RowIndex], DragDropEffects.None);
                }
            }
        }
        private void loadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        private void LoadListHoaDonByDate(DateTime tuNgay, DateTime denNgay)
        {
            dtgvHoaDon.DataSource = HoaDonDAO.Instance.GetListHoaDonByDate(tuNgay, denNgay);
        }
        #endregion

        #region Events DoanhThu
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadListHoaDonByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        #endregion

        #region Methods NhanVien
        void loadNhanVien()
        {
            dtgvNhanVien.DataSource = nhanVienList;
            loadListNhanVien();
            loadComboboxChucVu(cboChucVu);
            dtgvNhanVien.ReadOnly = true;
            dtgvNhanVien.AllowUserToAddRows = false;
            

            if (txtMaNhanVien.DataBindings.Count == 0)
            {
                nhanVienBingding();
                txtTimKiem.GotFocus += TxtTimKiem_GotFocus;
                txtTimKiem.LostFocus += TxtTimKiem_LostFocus;
                nhanVienList.BindingComplete += nhanVienList_BindingComplete;
            }

            SetPlaceholderText();
        }
        
        private void nhanVienList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            nhanVienList.BindingComplete -= nhanVienList_BindingComplete;
        }

        private void SetPlaceholderText()
        {
            txtTimKiem.Text = "Nhập tên nhân viên cần tìm...";
            txtTimKiem.ForeColor = Color.Gray;
        }

        private void loadComboboxChucVu(ComboBox cb)
        {
            cb.DataSource = ChucVuDAO.Instance.GetListChucVu();
            cb.DisplayMember = "TenCV";
            cb.ValueMember = "TenCV";
        }

        private void loadListNhanVien()
        {
            nhanVienList.DataSource = NhanVienDAO.Instance.GetListNhanVien();
        }

        void nhanVienBingding()
        {
            txtMaNhanVien.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "MÃ NHÂN VIÊN", true, DataSourceUpdateMode.Never));
            txtTenNhanVien.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "HỌ TÊN", true, DataSourceUpdateMode.Never));
            mskNgaySinh.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "NGÀY SINH", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "ĐỊA CHỈ", true, DataSourceUpdateMode.Never));
            txtSoDienThoai.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "SỐ ĐIỆN THOẠI", true, DataSourceUpdateMode.Never));
            cboChucVu.DataBindings.Add(new Binding("SelectedValue", dtgvNhanVien.DataSource, "CHỨC VỤ", true, DataSourceUpdateMode.Never));
        }

        DataTable TimKiemNhanVienTheoTen(string tenNV)
        {
            return NhanVienDAO.Instance.TimKiemNhanVienByTen(tenNV);
        }
        #endregion

        #region Events Nhanvien
        private void TxtTimKiem_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                SetPlaceholderText();
            }
        }

        private void TxtTimKiem_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên nhân viên cần tìm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            loadListNhanVien();
            loadNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text;
            string tenNV = txtTenNhanVien.Text;
            string gioiTinh = cboGioiTinh.SelectedItem.ToString();
            DateTime ngaySinh = DateTime.Parse(mskNgaySinh.Text);
            string diaChi = txtDiaChi.Text;
            string sdt = txtSoDienThoai.Text;
            string maCV = ChucVuDAO.Instance.GetChucVuByTenChucVu(cboChucVu.SelectedValue.ToString()).MaCV;

            if (NhanVienDAO.Instance.InsertNhanVien(maNV, tenNV, gioiTinh, ngaySinh, diaChi, sdt, maCV))
            {
                loadListNhanVien();
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại do có thể mã nhân viên bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (dtgvNhanVien.SelectedCells.Count > 0)
            {
                string maNV = (string)dtgvNhanVien.SelectedCells[0].OwningRow.Cells["MÃ NHÂN VIÊN"].Value;
                NhanVien nv = NhanVienDAO.Instance.GetNhanVienByMaNV(maNV);
                if (nv.GioiTinh.Trim().ToLower() == "nam")
                {
                    cboGioiTinh.SelectedIndex = 0;
                }
                else
                {
                    cboGioiTinh.SelectedIndex = 1;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNVCu = (string)dtgvNhanVien.SelectedCells[0].OwningRow.Cells["MÃ NHÂN VIÊN"].Value;
            string maNV = txtMaNhanVien.Text;
            string tenNV = txtTenNhanVien.Text;
            string gioiTinh = cboGioiTinh.SelectedItem.ToString();
            DateTime ngaySinh = DateTime.Parse(mskNgaySinh.Text);
            string diaChi = txtDiaChi.Text;
            string sdt = txtSoDienThoai.Text;
            string maCV = ChucVuDAO.Instance.GetChucVuByTenChucVu(cboChucVu.SelectedValue.ToString()).MaCV;
            if (NhanVienDAO.Instance.UpdateNhanVien(maNV, tenNV, gioiTinh, ngaySinh, diaChi, sdt, maCV, maNVCu))
            {
                loadListNhanVien();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại có thể do mã nhân viên bạn sửa đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = (string)dtgvNhanVien.SelectedCells[0].OwningRow.Cells["MÃ NHÂN VIÊN"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (NhanVienDAO.Instance.DeleteNhanVien(maNV))
                {
                    loadListNhanVien();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNV = txtTimKiem.Text;
            DataTable data = TimKiemNhanVienTheoTen(tenNV);
            if (data.Rows.Count > 0)
            {
                nhanVienList.DataSource = TimKiemNhanVienTheoTen(tenNV);
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên có tên là: '" + tenNV + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods ChucVu
        void loadChucVu()
        {
            dtgvChucVu.DataSource = chucVuList;
            loadDanhSachChucVu();
            chucVuBingding();
            SetPlaceholderTextChucVu();
            //dtgvChucVu.RowHeadersVisible = false;
            txtTimKiem_CV.GotFocus += TxtTimKiem_CV_GotFocus;
            txtTimKiem_CV.LostFocus += TxtTimKiem_CV_LostFocus;
            dtgvChucVu.AllowUserToAddRows = false;
            //dtgvChucVu.MouseDown += dtgvChucVu_MouseDown;
            dtgvChucVu.ReadOnly = true;
            
        }
        //private void dtgvChucVu_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvChucVu.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvChucVu.DoDragDrop(dtgvChucVu.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void SetPlaceholderTextChucVu()
        {
            txtTimKiem_CV.Text = "Nhập tên chức vụ cần tìm...";
            txtTimKiem_CV.ForeColor = Color.Gray;
        }


        void loadDanhSachChucVu()
        {
            chucVuList.DataSource = ChucVuDAO.Instance.GetDanhSachChucVu();
        }

        void chucVuBingding()
        {
            txtMaCV_CV.DataBindings.Add(new Binding("Text", dtgvChucVu.DataSource, "MÃ CHỨC VỤ", true, DataSourceUpdateMode.Never));
            txtTenCV_CV.DataBindings.Add(new Binding("Text", dtgvChucVu.DataSource, "TÊN CHỨC VỤ", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Events ChucVu
        private void TxtTimKiem_CV_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem_CV.Text))
            {
                SetPlaceholderTextChucVu();
            }
        }

        private void TxtTimKiem_CV_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem_CV.Text == "Nhập tên chức vụ cần tìm...")
            {
                txtTimKiem_CV.Text = "";
                txtTimKiem_CV.ForeColor = Color.Black;
            }
        }
        private void btnThemCV_Click(object sender, EventArgs e)
        {
            string maCV = txtMaCV_CV.Text;
            string tenCV = txtTenCV_CV.Text;
            if (ChucVuDAO.Instance.ThemChucVu(maCV, tenCV))
            {
                loadDanhSachChucVu();
                MessageBox.Show("Thêm chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm chức vụ thất bại do mã chức vụ hoặc tên chức vụ đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCV_Click(object sender, EventArgs e)
        {
            string maCV = txtMaCV_CV.Text;
            if (ChucVuDAO.Instance.XoaChucVu(maCV))
            {
                loadDanhSachChucVu();
                MessageBox.Show("Xóa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xóa chức vụ thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaCV_Click(object sender, EventArgs e)
        {
            string maCVCu = (string)dtgvChucVu.SelectedCells[0].OwningRow.Cells["MÃ CHỨC VỤ"].Value;
            string maCV = txtMaCV_CV.Text;
            string tenCV = txtTenCV_CV.Text;
            if (ChucVuDAO.Instance.CapNhatChucVu(maCV, tenCV, maCVCu))
            {
                loadDanhSachChucVu();
                MessageBox.Show("Sửa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa chức vụ thất bại do mã chức vụ hoặc tên chức vụ đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemCV_Click(object sender, EventArgs e)
        {
            loadDanhSachChucVu();
            loadChucVu();
        }

        private void btnTimKiemCV_Click(object sender, EventArgs e)
        {
            string tenCV = txtTimKiem_CV.Text;
            DataTable data = ChucVuDAO.Instance.TimKiemChucVuTheoTen(tenCV);
            if (data.Rows.Count > 0)
            {
                chucVuList.DataSource = ChucVuDAO.Instance.TimKiemChucVuTheoTen(tenCV);
            }
            else
            {
                MessageBox.Show("Không tìm thấy chức vụ có tên là: '" + tenCV + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods KhachHang
        void loadKhachHang()
        {
            dtgvKhachHang.DataSource = khachHangList;
            loadDanhSachKhachHang();
            //dtgvKhachHang.RowHeadersVisible = false;
            dtgvKhachHang.AllowUserToAddRows = false;
            dtgvKhachHang.ReadOnly = true;

            //dtgvKhachHang.MouseDown += dtgvKhachHang_MouseDown;

            if (txtMaKH_KH.DataBindings.Count == 0)
            {
                khachHangBinding();
                txtTimKiem_KH.GotFocus += TxtTimKiem_KH_GotFocus;
                txtTimKiem_KH.LostFocus += TxtTimKiem_KH_LostFocus;
                khachHangList.BindingComplete += KhachHangList_BindingComplete;
            }

            SetPlaceholderTextKhachHang();
        }
        //private void dtgvKhachHang_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvKhachHang.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvKhachHang.DoDragDrop(dtgvKhachHang.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        private void KhachHangList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            khachHangList.BindingComplete -= KhachHangList_BindingComplete;
        }

        void SetPlaceholderTextKhachHang()
        {
            txtTimKiem_KH.Text = "Nhập tên khách hàng cần tìm...";
            txtTimKiem_KH.ForeColor = Color.Gray;
        }

        private void TxtTimKiem_KH_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem_KH.Text))
            {
                SetPlaceholderTextKhachHang();
            }
        }

        private void TxtTimKiem_KH_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem_KH.Text == "Nhập tên khách hàng cần tìm...")
            {
                txtTimKiem_KH.Text = "";
                txtTimKiem_KH.ForeColor = Color.Black;
            }
        }

        void loadDanhSachKhachHang()
        {
            khachHangList.DataSource = KhachHangDAO.Instance.GetDanhSachKhachHang();
        }

        void khachHangBinding()
        {
            txtMaKH_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "MÃ KHÁCH HÀNG", true, DataSourceUpdateMode.Never));
            txtTenKH_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "TÊN KHÁCH HÀNG", true, DataSourceUpdateMode.Never));
            txtQuocTich_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "QUỐC TỊCH", true, DataSourceUpdateMode.Never));
            txtCCCD_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "CCCD", true, DataSourceUpdateMode.Never));
            txtDiaChi_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "ĐỊA CHỈ", true, DataSourceUpdateMode.Never));
            txtSDT_KH.DataBindings.Add(new Binding("Text", dtgvKhachHang.DataSource, "SỐ ĐIỆN THOẠI", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Events KhachHang
        private void txtMaKH_KH_TextChanged(object sender, EventArgs e)
        {
            if (dtgvKhachHang.SelectedCells.Count > 0)
            {
                string maKH = (string)dtgvKhachHang.SelectedCells[0].OwningRow.Cells["MÃ KHÁCH HÀNG"].Value;
                KhachHang kh = KhachHangDAO.Instance.GetKhachHangByMaKH(maKH);
                if (kh.GioiTinh.Trim().ToLower() == "nam")
                {
                    cboGioiTinh_KH.SelectedIndex = 0;
                }
                else
                {
                    cboGioiTinh_KH.SelectedIndex = 1;
                }
            }
        }

        private void btnTimKIem_KH_Click(object sender, EventArgs e)
        {
            string tenKH = txtTimKiem_KH.Text;
            DataTable data = KhachHangDAO.Instance.TimKiemKhachHangTheoTen(tenKH);
            if (data.Rows.Count > 0)
            {
                khachHangList.DataSource = KhachHangDAO.Instance.TimKiemKhachHangTheoTen(tenKH);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng có tên là: '" + tenKH + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_KH_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH_KH.Text;
            string tenKH = txtTenKH_KH.Text;
            string diaChi = txtDiaChi_KH.Text;
            string gioiTinh = cboGioiTinh_KH.SelectedItem.ToString();
            string cccd = txtCCCD_KH.Text;
            string quocTich = txtQuocTich_KH.Text;
            string sdt = txtSDT_KH.Text;
            if (KhachHangDAO.Instance.ThemKhachHang(maKH, tenKH, diaChi, gioiTinh, cccd, quocTich, sdt))
            {
                loadDanhSachKhachHang();
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại do có thể mã khách hàng đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_KH_Click(object sender, EventArgs e)
        {
            string maKH = (string)dtgvKhachHang.SelectedCells[0].OwningRow.Cells["MÃ KHÁCH HÀNG"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (KhachHangDAO.Instance.XoaKhachHang(maKH))
                {
                    loadDanhSachKhachHang();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_KH_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH_KH.Text;
            string tenKH = txtTenKH_KH.Text;
            string diaChi = txtDiaChi_KH.Text;
            string gioiTinh = cboGioiTinh_KH.SelectedItem.ToString();
            string cccd = txtCCCD_KH.Text;
            string quocTich = txtQuocTich_KH.Text;
            string sdt = txtSDT_KH.Text;
            string maKHCU = (string)dtgvKhachHang.SelectedCells[0].OwningRow.Cells["MÃ KHÁCH HÀNG"].Value;

            if (KhachHangDAO.Instance.SuaKhachHang(maKH, tenKH, diaChi, gioiTinh, cccd, sdt, quocTich, maKHCU))
            {
                loadDanhSachKhachHang();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại có thể do mã khách hàng bạn sửa đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXem_KH_Click(object sender, EventArgs e)
        {
            loadDanhSachKhachHang();
            loadKhachHang();
        }

        #endregion

        #region Methods Phong
        void loadPhong()
        {
            dtgvPhong.DataSource = phongList;
            loadDanhSachPhong();
            loadComboboxLoaiPhong(cboLoaiPhong_P);
            loadComboboxTang(cboTang_P);
            //dtgvPhong.RowHeadersVisible = false;
            dtgvPhong.AllowUserToAddRows = false;
            dtgvPhong.ReadOnly = true;

            //dtgvPhong.MouseDown += dtgvPhong_MouseDown;

            if (txtMaPH_P.DataBindings.Count == 0)
            {
                phongBinding();
                txtTimKiem_P.GotFocus += txtTimKiem_P_GotFocus;
                txtTimKiem_P.LostFocus += txtTimKiem_P_LostFocus;
                phongList.BindingComplete += phongList_BindingComplete;
            }

            SetPlaceholderTextPhong();
        }
        //private void dtgvPhong_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvPhong.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvPhong.DoDragDrop(dtgvPhong.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadDanhSachPhong()
        {
            phongList.DataSource = PhongDAO.Instance.GetDanhSachPhong();
        }

        void loadComboboxLoaiPhong(ComboBox cb)
        {
            cb.DataSource = LoaiPhongDAO.Instance.GetListLoaiPhong();
            cb.DisplayMember = "TenLoaiPhong";
            cb.ValueMember = "TenLoaiPhong";
        }

        void loadComboboxTang(ComboBox cb)
        {
            cb.DataSource = TangDAO.Instance.GetListTang();
            cb.DisplayMember = "TENTANG";
            cb.ValueMember = "TENTANG";
        }

        void phongBinding()
        {
            txtMaPH_P.DataBindings.Add(new Binding("Text", dtgvPhong.DataSource, "MÃ PHÒNG", true, DataSourceUpdateMode.Never));
            txtTenPhong_P.DataBindings.Add(new Binding("Text", dtgvPhong.DataSource, "TÊN PHÒNG", true, DataSourceUpdateMode.Never));
            txtTinhTrang_P.DataBindings.Add(new Binding("Text", dtgvPhong.DataSource, "TÌNH TRẠNG", true, DataSourceUpdateMode.Never));
            cboLoaiPhong_P.DataBindings.Add(new Binding("SelectedValue", dtgvPhong.DataSource, "LOẠI PHÒNG", true, DataSourceUpdateMode.Never));
            cboTang_P.DataBindings.Add(new Binding("SelectedValue", dtgvPhong.DataSource, "TẦNG", true, DataSourceUpdateMode.Never));
        }

        private void SetPlaceholderTextPhong()
        {
            txtTimKiem_P.Text = "Nhập tên phòng cần tìm...";
            txtTimKiem_P.ForeColor = Color.Gray;
        }
        #endregion

        #region Events Phong
        private void phongList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            phongList.BindingComplete -= phongList_BindingComplete;
        }

        private void txtTimKiem_P_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem_P.Text))
            {
                SetPlaceholderTextPhong();
            }
        }

        private void txtTimKiem_P_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem_P.Text == "Nhập tên phòng cần tìm...")
            {
                txtTimKiem_P.Text = "";
                txtTimKiem_P.ForeColor = Color.Black;
            }
        }
        private void btnTimKiem_P_Click(object sender, EventArgs e)
        {
            string tenPhong = txtTimKiem_P.Text;
            DataTable data = PhongDAO.Instance.TimKiemPhongTheoTen(tenPhong);
            if (data.Rows.Count > 0)
            {
                phongList.DataSource = PhongDAO.Instance.TimKiemPhongTheoTen(tenPhong);
            }
            else
            {
                MessageBox.Show("Không tìm thấy phòng có tên là: '" + tenPhong + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_P_Click(object sender, EventArgs e)
        {
            string maPH = txtMaPH_P.Text;
            string tenPH = txtTenPhong_P.Text;
            string tinhTrang = txtTinhTrang_P.Text;
            string maLP = LoaiPhongDAO.Instance.GetLoaiPhongByTenLoaiPhong(cboLoaiPhong_P.SelectedValue.ToString()).MaLoaiPhong;
            string maTang = TangDAO.Instance.GetTangByTenTang(cboTang_P.SelectedValue.ToString()).MaTang;
            if (PhongDAO.Instance.ThemPhong(maPH, tenPH, tinhTrang, maLP, maTang))
            {
                loadDanhSachPhong();
                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại do có thể mã phòng bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_P_Click(object sender, EventArgs e)
        {
            string maPH = (string)dtgvPhong.SelectedCells[0].OwningRow.Cells["MÃ PHÒNG"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (PhongDAO.Instance.XoaPhong(maPH))
                {
                    loadDanhSachKhachHang();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_P_Click(object sender, EventArgs e)
        {
            string maPH = txtMaPH_P.Text;
            string tenPH = txtTenPhong_P.Text;
            string tinhTrang = txtTinhTrang_P.Text;
            string maLP = LoaiPhongDAO.Instance.GetLoaiPhongByTenLoaiPhong(cboLoaiPhong_P.SelectedValue.ToString()).MaLoaiPhong;
            string maTang = TangDAO.Instance.GetTangByTenTang(cboTang_P.SelectedValue.ToString()).MaTang;
            string maPHCU = (string)dtgvPhong.SelectedCells[0].OwningRow.Cells["MÃ PHÒNG"].Value;
            if (PhongDAO.Instance.SuaPhong(maPH, tenPH, tinhTrang, maLP, maTang, maPHCU))
            {
                loadDanhSachPhong();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại do có thể mã phòng đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnXem_P_Click(object sender, EventArgs e)
        {
            loadPhong();
        }
        #endregion

        #region Methods LoaiPhong
        void loadLoaiPhong()
        {
            dtgvLoaiPhong.DataSource = loaiPhongList;
            loadDanhSachLoaiPhong();
            //dtgvLoaiPhong.RowHeadersVisible = false;
            dtgvLoaiPhong.AllowUserToAddRows = false;
            dtgvLoaiPhong.ReadOnly = true;

            //dtgvLoaiPhong.MouseDown += dtgvLoaiPhong_MouseDown;

            if (txt_Ma_LP.DataBindings.Count == 0)
            {
                loaiPhongBinding();
                txt_TimKiem_LP.GotFocus += txtTimKiem_LP_GotFocus;
                txt_TimKiem_LP.LostFocus += txtTimKiem_LP_LostFocus;
                loaiPhongList.BindingComplete += loaiPhongList_BindingComplete;
            }

            SetPlaceholderTextLoaiPhong();
        }
        //private void dtgvLoaiPhong_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvLoaiPhong.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvLoaiPhong.DoDragDrop(dtgvLoaiPhong.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadDanhSachLoaiPhong()
        {
            loaiPhongList.DataSource = LoaiPhongDAO.Instance.GetDanhSachLoaiPhong();
        }

        void loaiPhongBinding()
        {
            txt_Ma_LP.DataBindings.Add(new Binding("Text", dtgvLoaiPhong.DataSource, "MÃ LOẠI PHÒNG", true, DataSourceUpdateMode.Never));
            txt_Ten_LP.DataBindings.Add(new Binding("Text", dtgvLoaiPhong.DataSource, "TÊN LOẠI PHÒNG", true, DataSourceUpdateMode.Never));
            txt_DonGia.DataBindings.Add(new Binding("Text", dtgvLoaiPhong.DataSource, "ĐƠN GIÁ", true, DataSourceUpdateMode.Never));
            txt_SoNguoi.DataBindings.Add(new Binding("Text", dtgvLoaiPhong.DataSource, "SỐ NGƯỜI", true, DataSourceUpdateMode.Never));
            txt_SoLuong.DataBindings.Add(new Binding("Text", dtgvLoaiPhong.DataSource, "SỐ LƯỢNG", true, DataSourceUpdateMode.Never));
        }

        private void SetPlaceholderTextLoaiPhong()
        {
            txt_TimKiem_LP.Text = "Nhập tên loại phòng cần tìm...";
            txt_TimKiem_LP.ForeColor = Color.Gray;
        }
        #endregion

        #region Events LoaiPhong
        private void loaiPhongList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            loaiPhongList.BindingComplete -= loaiPhongList_BindingComplete;
        }

        private void txtTimKiem_LP_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_LP.Text))
            {
                SetPlaceholderTextLoaiPhong();
            }
        }

        private void txtTimKiem_LP_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_LP.Text == "Nhập tên loại phòng cần tìm...")
            {
                txt_TimKiem_LP.Text = "";
                txt_TimKiem_LP.ForeColor = Color.Black;
            }
        }

        private void btn_TimKiem_LP_Click(object sender, EventArgs e)
        {
            string tenLoaiPhong = txt_TimKiem_LP.Text;
            DataTable data = LoaiPhongDAO.Instance.TimKiemLoaiPhongTheoTen(tenLoaiPhong);
            if (data.Rows.Count > 0)
            {
                loaiPhongList.DataSource = LoaiPhongDAO.Instance.TimKiemLoaiPhongTheoTen(tenLoaiPhong);
            }
            else
            {
                MessageBox.Show("Không tìm thấy loại phòng có tên là: '" + tenLoaiPhong + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Them_LP_Click(object sender, EventArgs e)
        {
            string maLoaiPhong = txt_Ma_LP.Text;
            string tenLoaiPhong = txt_Ten_LP.Text;
            float donGia = float.Parse(txt_DonGia.Text);
            int soNguoi = int.Parse(txt_SoNguoi.Text);
            int soLuong = int.Parse(txt_SoLuong.Text);
            if (LoaiPhongDAO.Instance.ThemLoaiPhong(maLoaiPhong, tenLoaiPhong, donGia, soNguoi, soLuong))
            {
                loadDanhSachLoaiPhong();
                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại do có thể mã phòng bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_LP_Click(object sender, EventArgs e)
        {
            string maLoaiPhong = (string)dtgvLoaiPhong.SelectedCells[0].OwningRow.Cells["MÃ LOẠI PHÒNG"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (LoaiPhongDAO.Instance.XoaLoaiPhong(maLoaiPhong))
                {
                    loadDanhSachLoaiPhong();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Sua_LP_Click(object sender, EventArgs e)
        {
            string maLoaiPhong = txt_Ma_LP.Text;
            string tenLoaiPhong = txt_Ten_LP.Text;
            float donGia = float.Parse(txt_DonGia.Text);
            int soNguoi = int.Parse(txt_SoNguoi.Text);
            int soLuong = int.Parse(txt_SoLuong.Text);
            string maLoaiPhongCu = (string)dtgvPhong.SelectedCells[0].OwningRow.Cells["MÃ LOẠI PHÒNG"].Value;
            if (LoaiPhongDAO.Instance.SuaLoaiPhong(maLoaiPhong, tenLoaiPhong, donGia, soNguoi, soLuong, maLoaiPhongCu))
            {
                loadDanhSachLoaiPhong();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại do có thể mã loại phòng đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Xem_LP_Click(object sender, EventArgs e)
        {
            loadLoaiPhong();
        }

        #endregion

        #region Methods DichVu
        void loadDichVu()
        {
            dtgvDichVu.DataSource = dichVuList;
            //dtgvDichVu.RowHeadersVisible = false;
            loadDanhSachDichVu();
            dtgvDichVu.AllowUserToAddRows = false;
            dtgvDichVu.ReadOnly = true;

            //dtgvDichVu.MouseDown += dtgvDichVu_MouseDown;

            if (txt_Ma_DV.DataBindings.Count == 0)
            {
                dichVuBinding();
                txt_TimKiem_DV.GotFocus += txtTimKiem_DV_GotFocus;
                txt_TimKiem_DV.LostFocus += txtTimKiem_DV_LostFocus;
                dichVuList.BindingComplete += dichVuList_BindingComplete;
            }

            SetPlaceholderTextDichVu();
        }
        //private void dtgvDichVu_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvDichVu.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvDichVu.DoDragDrop(dtgvDichVu.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadDanhSachDichVu()
        {
            dichVuList.DataSource = DichVuDAO.Instance.GetListDichVu();
        }

        void dichVuBinding()
        {
            txt_Ma_DV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "MÃ DỊCH VỤ", true, DataSourceUpdateMode.Never));
            txt_Ten_DV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "TÊN DỊCH VỤ", true, DataSourceUpdateMode.Never));
            txt_DonGia_DV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "GIÁ DỊCH VỤ", true, DataSourceUpdateMode.Never));
        }

        private void SetPlaceholderTextDichVu()
        {
            txt_TimKiem_DV.Text = "Nhập tên dịch vụ cần tìm...";
            txt_TimKiem_DV.ForeColor = Color.Gray;
        }
        #endregion

        #region Events DichVu
        private void dichVuList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            dichVuList.BindingComplete -= dichVuList_BindingComplete;
        }
        private void txtTimKiem_DV_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_DV.Text))
            {
                SetPlaceholderTextDichVu();
            }
        }

        private void txtTimKiem_DV_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_DV.Text == "Nhập tên dịch vụ cần tìm...")
            {
                txt_TimKiem_DV.Text = "";
                txt_TimKiem_DV.ForeColor = Color.Black;
            }
        }

        private void btn_TimKiem_DV_Click(object sender, EventArgs e)
        {
            string tenDichVu = txt_TimKiem_DV.Text;
            DataTable data = DichVuDAO.Instance.TimKiemDichVuTheoTen(tenDichVu);
            if (data.Rows.Count > 0)
            {
                dichVuList.DataSource = DichVuDAO.Instance.TimKiemDichVuTheoTen(tenDichVu);
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ có tên là: '" + tenDichVu + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Them_DV_Click(object sender, EventArgs e)
        {
            string maDichVu = txt_Ma_DV.Text;
            string tenDichVu = txt_Ten_DV.Text;
            float giaDichVu = float.Parse(txt_DonGia_DV.Text);
            if (DichVuDAO.Instance.themDichVu(maDichVu, tenDichVu, giaDichVu))
            {
                loadDanhSachDichVu();
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại do có thể mã dịch vụ bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_DV_Click(object sender, EventArgs e)
        {
            string maDichVu = (string)dtgvDichVu.SelectedCells[0].OwningRow.Cells["MÃ DỊCH VỤ"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (DichVuDAO.Instance.xoaDichVu(maDichVu))
                {
                    loadDanhSachDichVu();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Sua_DV_Click(object sender, EventArgs e)
        {
            string maDichVu = txt_Ma_DV.Text;
            string tenDichVu = txt_Ten_DV.Text;
            float giaDichVu = float.Parse(txt_DonGia_DV.Text);
            string maDichVuCu = (string)dtgvDichVu.SelectedCells[0].OwningRow.Cells["MÃ DỊCH VỤ"].Value;
            if (DichVuDAO.Instance.suaDichVu(maDichVu, tenDichVu, giaDichVu, maDichVuCu))
            {
                loadDanhSachDichVu();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại do có thể mã dịch vụ đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Xem_DV_Click(object sender, EventArgs e)
        {
            loadDichVu();
        }
        #endregion

        #region Methods ChiTietDichVu
        void loadChiTietDichVu()
        {
            dtgvChiTietDV.DataSource = chiTietDichVuList;
            //dtgvChiTietDV.RowHeadersVisible = false;
            loadDanhSachChiTietDichVu();
            loadComboboxCTDVMaDV(cb_MaDV_CTDV);
            loadComboboxCTDVMaPhong(cb_MaP_CTDV);
            dtgvChiTietDV.AllowUserToAddRows = false;
            dtgvKhachHang.ReadOnly = true;

            //dtgvChiTietDV.MouseDown += dtgvChiTietDichVu_MouseDown;

            if (cb_MaDV_CTDV.DataBindings.Count == 0)
            {
                chiTietDichVuBinding();
                txt_TimKiem_CTDV.GotFocus += txtTimKiem_CTDV_GotFocus;
                txt_TimKiem_CTDV.LostFocus += txtTimKiem_CTDV_LostFocus;
                chiTietDichVuList.BindingComplete += chiTietDichVuList_BindingComplete;
            }

            SetPlaceholderTextChiTietDichVu();
        }
        //private void dtgvChiTietDichVu_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvChiTietDV.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvChiTietDV.DoDragDrop(dtgvChiTietDV.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadDanhSachChiTietDichVu()
        {
            chiTietDichVuList.DataSource = ChiTietDichVuDAO.Instance.GetList_ChiTietDichVu();
        }
        void loadComboboxCTDVMaDV(ComboBox cb)
        {
            cb.DataSource = DichVuDAO.Instance.GetListDichVu();
            cb.DisplayMember = "TÊN DỊCH VỤ";
            cb.ValueMember = "TÊN DỊCH VỤ";
        }

        void loadComboboxCTDVMaPhong(ComboBox cb)
        {
            cb.DataSource = PhongDAO.Instance.GetDanhSachPhong();
            cb.DisplayMember = "TÊN PHÒNG";
            cb.ValueMember = "TÊN PHÒNG";
        }
        void chiTietDichVuBinding()
        {
            cb_MaDV_CTDV.DataBindings.Add(new Binding("SelectedValue", dtgvChiTietDV.DataSource, "TÊN DỊCH VỤ", true, DataSourceUpdateMode.Never));
            cb_MaP_CTDV.DataBindings.Add(new Binding("SelectedValue", dtgvChiTietDV.DataSource, "TÊN PHÒNG", true, DataSourceUpdateMode.Never));
        }

        private void SetPlaceholderTextChiTietDichVu()
        {
            txt_TimKiem_CTDV.Text = "Nhập tên phòng cần tìm...";
            txt_TimKiem_CTDV.ForeColor = Color.Gray;
        }
        #endregion

        #region Events ChiTietDichVu
        private void chiTietDichVuList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            chiTietDichVuList.BindingComplete -= chiTietDichVuList_BindingComplete;
        }
        private void txtTimKiem_CTDV_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_CTDV.Text))
            {
                SetPlaceholderTextChiTietDichVu();
            }
        }

        private void txtTimKiem_CTDV_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_CTDV.Text == "Nhập tên phòng cần tìm...")
            {
                txt_TimKiem_CTDV.Text = "";
                txt_TimKiem_CTDV.ForeColor = Color.Black;
            }
        }

        private void btn_TimKiem_CTDV_Click(object sender, EventArgs e)
        {
            string tenPhong = txt_TimKiem_CTDV.Text;
            DataTable data = ChiTietDichVuDAO.Instance.TimKiemChiTietDichVuTheoTenPhong(tenPhong);
            if (data.Rows.Count > 0)
            {
                chiTietDichVuList.DataSource = ChiTietDichVuDAO.Instance.TimKiemChiTietDichVuTheoTenPhong(tenPhong);
            }
            else
            {
                MessageBox.Show("Không tìm thấy phòng có tên là: '" + tenPhong + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Them_CTDV_Click(object sender, EventArgs e)
        {
            string maDichVu = DichVuDAO.Instance.GetDichVuByTenDV(cb_MaDV_CTDV.SelectedValue.ToString()).MaDichVu;
            string maPhong = PhongDAO.Instance.GetByTenPhong(cb_MaP_CTDV.SelectedValue.ToString()).MaPhong;
            if (ChiTietDichVuDAO.Instance.themChiTietDichVu(maPhong, maDichVu))
            {
                loadDanhSachChiTietDichVu();
                MessageBox.Show("Thêm chi tiết dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm chi tiết dịch vụ thất bại do có thể mã dịch vụ bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_CTDV_Click(object sender, EventArgs e)
        {
            string tenDichVu = (string)dtgvChiTietDV.SelectedCells[0].OwningRow.Cells["TÊN DỊCH VỤ"].Value;
            string tenPhong = (string)dtgvChiTietDV.SelectedCells[0].OwningRow.Cells["TÊN PHÒNG"].Value;
            string maPhong = PhongDAO.Instance.GetByTenPhong(tenPhong).MaPhong;
            string maDichVu = DichVuDAO.Instance.GetDichVuByTenDV(tenDichVu).MaDichVu;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (ChiTietDichVuDAO.Instance.xoaChiTietDichVu(maDichVu, maPhong))
                {
                    loadDanhSachChiTietDichVu();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Sua_CTDV_Click(object sender, EventArgs e)
        {
            string maDichVu = DichVuDAO.Instance.GetDichVuByTenDV(cb_MaDV_CTDV.SelectedValue.ToString()).MaDichVu;
            string maPhong = PhongDAO.Instance.GetByTenPhong(cb_MaP_CTDV.SelectedValue.ToString()).MaPhong;
            string tendv = (string)dtgvChiTietDV.SelectedCells[0].OwningRow.Cells["Tên Dịch Vụ"].Value;
            string tenphong = (string)dtgvChiTietDV.SelectedCells[0].OwningRow.Cells["Tên Phòng"].Value;
            string maDichVuCu = DichVuDAO.Instance.GetDichVuByTenDV(tendv).MaDichVu;
            string maPhongCu = PhongDAO.Instance.GetByTenPhong(tenphong).MaPhong;
            if (ChiTietDichVuDAO.Instance.suaChiTietDichVu(maPhong, maDichVu, maDichVuCu, maPhongCu))
            {
                loadDanhSachChiTietDichVu();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại do có thể mã dịch đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Xem_CTDV_Click(object sender, EventArgs e)
        {
            loadChiTietDichVu();
        }
        #endregion

        //HÓA ĐƠN
        #region Methods HoaDon
        void loadHoaDon()
        {
            dtgv_HoaDon.DataSource = hoaDonList;
            //dtgv_HoaDon.RowHeadersVisible = false;
            loadDanhSachHoaDon();
            loadComboboxMaNV(cbMaNV);
            loadComboboxMaPhong(cbMaPhong);
            dtgv_HoaDon.AllowUserToAddRows = false;
            dtgv_HoaDon.ReadOnly = true;

            //dtgv_HoaDon.MouseDown += dtgv_HoaDon_MouseDown;

            if (txt_ID_HD.DataBindings.Count == 0)
            {
                hoaDonBinDing();
                txt_TimKiem_HD.GotFocus += TxtTimKiem_HD_GotFocus;
                txt_TimKiem_HD.LostFocus += TxtTimKiem_HD_LostFocus;
                hoaDonList.BindingComplete += hoaDonList_BindingComplete;
            }

            SetPlaceholderTextHoaDon();
        }
        //private void dtgv_HoaDon_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgv_HoaDon.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgv_HoaDon.DoDragDrop(dtgv_HoaDon.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        private void hoaDonList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            hoaDonList.BindingComplete -= hoaDonList_BindingComplete;
        }

        private void SetPlaceholderTextHoaDon()
        {
            txt_TimKiem_HD.Text = "Nhập ID hóa đơn cần tìm...";
            txt_TimKiem_HD.ForeColor = Color.Gray;
        }

        private void loadComboboxMaNV(ComboBox cb)
        {
            cb.DataSource = NhanVienDAO.Instance.GetListNhanVien();
            cb.DisplayMember = "HỌ TÊN";
            cb.ValueMember = "HỌ TÊN";
        }

        private void loadComboboxMaPhong(ComboBox cb)
        {
            cb.DataSource = PhongDAO.Instance.GetDanhSachPhong();
            cb.DisplayMember = "TÊN PHÒNG";
            cb.ValueMember = "TÊN PHÒNG";
        }

        private void loadDanhSachHoaDon()
        {
            hoaDonList.DataSource = HoaDonDAO.Instance.GetListHoaDon();
        }

        void hoaDonBinDing()
        {
            txt_ID_HD.DataBindings.Add(new Binding("Text", dtgv_HoaDon.DataSource, "ID", true, DataSourceUpdateMode.Never));
            msk_NgayTT_HD.DataBindings.Add(new Binding("Text", dtgv_HoaDon.DataSource, "NGÀY THANH TOÁN", true, DataSourceUpdateMode.Never));
            txt_TongTien_HD.DataBindings.Add(new Binding("Text", dtgv_HoaDon.DataSource, "TỔNG TIỀN", true, DataSourceUpdateMode.Never));
            txt_GiamGia_HD.DataBindings.Add(new Binding("Text", dtgv_HoaDon.DataSource, "GIẢM GIÁ", true, DataSourceUpdateMode.Never));
            txt_ThanhTien_HD.DataBindings.Add(new Binding("Text", dtgv_HoaDon.DataSource, "THÀNH TIỀN", true, DataSourceUpdateMode.Never));
            cbMaNV.DataBindings.Add(new Binding("SelectedValue", dtgv_HoaDon.DataSource, "NHÂN VIÊN THANH TOÁN", true, DataSourceUpdateMode.Never));
            cbMaPhong.DataBindings.Add(new Binding("SelectedValue", dtgv_HoaDon.DataSource, "TÊN PHÒNG", true, DataSourceUpdateMode.Never));
        }

        DataTable TimKiemHoaDonTheoID(string idHoaDon)
        {
            return HoaDonDAO.Instance.TimKiemHoaDonByID(idHoaDon);
        }
        #endregion

        #region Events HoaDon
        private void TxtTimKiem_HD_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_HD.Text))
            {
                SetPlaceholderTextHoaDon();
            }
        }

        private void TxtTimKiem_HD_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_HD.Text == "Nhập ID hóa đơn cần tìm...")
            {
                txt_TimKiem_HD.Text = "";
                txt_TimKiem_HD.ForeColor = Color.Black;
            }
        }

        private void btnXem_HD_Click(object sender, EventArgs e)
        {
            loadHoaDon();
        }

        private void btnThem_HD_Click(object sender, EventArgs e)
        {
            string idHoaDon = txt_ID_HD.Text;
            DateTime ngayThanhToan = DateTime.Parse(msk_NgayTT_HD.Text);
            float tongTien = float.Parse(txt_TongTien_HD.Text);
            float giamGia = float.Parse(txt_GiamGia_HD.Text);
            float thanhTien = float.Parse(txt_ThanhTien_HD.Text);
            string maNhanVien = NhanVienDAO.Instance.GetNhanVienByTenNV(cbMaNV.SelectedValue.ToString()).MaNV;
            string maPhong = PhongDAO.Instance.GetByTenPhong(cbMaPhong.SelectedValue.ToString()).MaPhong;


            if (HoaDonDAO.Instance.themHoaDon(ngayThanhToan, tongTien, giamGia, thanhTien, maNhanVien, maPhong))
            {
                loadDanhSachHoaDon();
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại do có thể mã nhân viên bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_HD_Click(object sender, EventArgs e)
        {
            int idHoaDon = int.Parse(txt_ID_HD.Text);
            DateTime ngayThanhToan = DateTime.Parse(msk_NgayTT_HD.Text);
            float tongTien = float.Parse(txt_TongTien_HD.Text);
            float giamGia = float.Parse(txt_GiamGia_HD.Text);
            float thanhTien = float.Parse(txt_ThanhTien_HD.Text);
            string maNhanVien = NhanVienDAO.Instance.GetNhanVienByTenNV(cbMaNV.SelectedValue.ToString()).MaNV;
            string maPhong = PhongDAO.Instance.GetByTenPhong(cbMaPhong.SelectedValue.ToString()).MaPhong;
            int idHoaDonCu = (int)dtgv_HoaDon.SelectedCells[0].OwningRow.Cells["ID"].Value;


            if (HoaDonDAO.Instance.suaHoaDon(idHoaDon, ngayThanhToan, tongTien, giamGia, thanhTien, maNhanVien, maPhong, idHoaDonCu))
            {
                loadDanhSachHoaDon();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại có thể do ID bạn sửa đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_HD_Click(object sender, EventArgs e)
        {
            int idHoaDon = (int)dtgv_HoaDon.SelectedCells[0].OwningRow.Cells["ID"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (HoaDonDAO.Instance.xoaHoaDon(idHoaDon))
                {
                    loadDanhSachHoaDon();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnTimKiem_HD_Click(object sender, EventArgs e)
        {
            string idHoaDon = txt_TimKiem_HD.Text;
            DataTable data = TimKiemHoaDonTheoID(idHoaDon);
            if (data.Rows.Count > 0)
            {
                hoaDonList.DataSource = TimKiemHoaDonTheoID(idHoaDon);
            }
            else
            {
                MessageBox.Show("Không tìm thấy ID có tên là: '" + idHoaDon + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        //TÀI KHOẢN
        #region Methods TaiKhoan
        void loadTaiKhoan()
        {
            dtgvTaiKhoan.DataSource = taiKhoanList;
            //dtgvTaiKhoan.RowHeadersVisible = false;
            loadDanhSachTaiKhoan();
            loadComboboxNhanVien(cbMaNV_TK);
            dtgvTaiKhoan.AllowUserToAddRows = false;
            dtgvTaiKhoan.ReadOnly = true;

            //dtgvTaiKhoan.MouseDown += dtgvTaiKhoan_MouseDown;

            if (txt_ID_TK.DataBindings.Count == 0)
            {
                taiKhoanBinding();
                txt_TimKiem_TK.GotFocus += TxtTimKiem_TK_GotFocus;
                txt_TimKiem_TK.LostFocus += TxtTimKiem_TK_LostFocus;
                taiKhoanList.BindingComplete += TaiKhoanList_BindingComplete;
            }

            SetPlaceholderTextTaiKhoan();
        }
        //private void dtgvTaiKhoan_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgvTaiKhoan.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgvTaiKhoan.DoDragDrop(dtgvTaiKhoan.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadComboboxNhanVien(ComboBox cb)
        {
            cb.DataSource = NhanVienDAO.Instance.GetListNhanVien();
            cb.DisplayMember = "HỌ TÊN";
            cb.ValueMember = "HỌ TÊN";
        }
        private void TaiKhoanList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            taiKhoanList.BindingComplete -= TaiKhoanList_BindingComplete;
        }

        void SetPlaceholderTextTaiKhoan()
        {
            txt_TimKiem_TK.Text = "Nhập tên tài khoản cần tìm...";
            txt_TimKiem_TK.ForeColor = Color.Gray;
        }

        private void TxtTimKiem_TK_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_TK.Text))
            {
                SetPlaceholderTextTaiKhoan();
            }
        }

        private void TxtTimKiem_TK_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_TK.Text == "Nhập tên tài khoản cần tìm...")
            {
                txt_TimKiem_TK.Text = "";
                txt_TimKiem_TK.ForeColor = Color.Black;
            }
        }

        void loadDanhSachTaiKhoan()
        {
            taiKhoanList.DataSource = TaiKhoanDAO.Instance.GetDanhSachTaiKhoan();
        }

        void taiKhoanBinding()
        {
            txt_ID_TK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "ID TÀI KHOẢN", true, DataSourceUpdateMode.Never));
            txt_TenHienThi_TK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TÊN HIỂN THỊ", true, DataSourceUpdateMode.Never));
            txt_Username_TK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "USERNAME", true, DataSourceUpdateMode.Never));
            txt_Password_TK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "PASSWORD", true, DataSourceUpdateMode.Never));
            //txt_Quyen_TK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "QUYỀN", true, DataSourceUpdateMode.Never));
            cbMaNV_TK.DataBindings.Add(new Binding("SelectedValue", dtgvTaiKhoan.DataSource, "NHÂN VIÊN SỞ HỮU", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Events TaiKhoan
        private void btnTimKIem_TK_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txt_TimKiem_TK.Text;
            DataTable data = TaiKhoanDAO.Instance.TimKiemTaiKhoanTheoTen(tenTaiKhoan);
            if (data.Rows.Count > 0)
            {
                taiKhoanList.DataSource = TaiKhoanDAO.Instance.TimKiemTaiKhoanTheoTen(tenTaiKhoan);
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản có tên là: '" + tenTaiKhoan + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Them_TK_Click(object sender, EventArgs e)
        {    
            string tenHienThi = txt_TenHienThi_TK.Text;
            string username = txt_Username_TK.Text;
            string passwd = txt_Password_TK.Text;
            string quyen = cboQuyen.Text;
            string maNhanVien = NhanVienDAO.Instance.GetNhanVienByTenNV(cbMaNV_TK.SelectedValue.ToString()).MaNV;
            if (TaiKhoanDAO.Instance.themTaiKhoan(tenHienThi, username, passwd, quyen, maNhanVien))
            {
                loadDanhSachTaiKhoan();
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại do có thể mã khách hàng đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_TK_Click(object sender, EventArgs e)
        {
            int idTaiKhoan = int.Parse(txt_ID_TK.Text);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TaiKhoanDAO.Instance.xoaTaiKhoan(idTaiKhoan))
                {
                    loadDanhSachTaiKhoan();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Sua_TK_Click(object sender, EventArgs e)
        {
            int idTaiKhoan = int.Parse(txt_ID_TK.Text);
            string tenHienThi = txt_TenHienThi_TK.Text;
            string username = txt_Username_TK.Text;
            string passwd = txt_Password_TK.Text;
            string quyen = cboQuyen.Text;
            string maNhanVien = NhanVienDAO.Instance.GetNhanVienByTenNV(cbMaNV_TK.SelectedValue.ToString()).MaNV;


            if (TaiKhoanDAO.Instance.suaTaiKhoan(idTaiKhoan, tenHienThi, username, passwd, quyen, maNhanVien))
            {
                loadDanhSachTaiKhoan();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại có thể do mã khách hàng bạn sửa đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xem_TK_Click(object sender, EventArgs e)
        {
            loadDanhSachTaiKhoan();
            loadTaiKhoan();
        }

        private void txt_ID_TK_TextChanged(object sender, EventArgs e)
        {
            if (dtgvTaiKhoan.SelectedCells.Count > 0)
            {
                string userName = (string)dtgvTaiKhoan.SelectedCells[0].OwningRow.Cells["USERNAME"].Value;
                TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByUserName(userName);
                if (tk.Quyen.Trim().ToLower() == "user")
                {
                    cboGioiTinh.SelectedIndex = 0;
                }
                else
                {
                    cboGioiTinh.SelectedIndex = 1;
                }
            }
        }


        #endregion

        //ĐẶT PHÒNG
        #region Methods DatPhong
        void loadDatPhong()
        {
            dtgv_DatPhong.DataSource = datPhongList;
            //dtgv_DatPhong.RowHeadersVisible = false;
            loadDanhSachDatPhong();
            //loadComboboxTenKH(cb_MaKH_DP);
            loadComboboxMaNVDP(cb_MaNV_DP);
            loadComboboxMaPhongDP(cb_MaPhong_DP);
            loadComboboxKhachHang(cboKhachHang);
            dtgv_DatPhong.AllowUserToAddRows = false;
            dtgv_DatPhong.ReadOnly = true;

            //dtgv_DatPhong.MouseDown += dtgv_DatPhong_MouseDown;

            if (txtMaDP.DataBindings.Count == 0)
            {
                datPhongBinding();
                txt_TimKiem_DP.GotFocus += txtTimKiem_DP_GotFocus;
                txt_TimKiem_DP.LostFocus += txtTimKiem_DP_LostFocus;
                datPhongList.BindingComplete += datPhongList_BindingComplete;
            }

            SetPlaceholderTextDatPhong();
        }
        //private void dtgv_DatPhong_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DataGridView.HitTestInfo info = dtgv_DatPhong.HitTest(e.X, e.Y);
        //        if (info.RowIndex >= 0)
        //        {
        //            dtgv_DatPhong.DoDragDrop(dtgv_DatPhong.Rows[info.RowIndex], DragDropEffects.None);
        //        }
        //    }
        //}
        void loadDanhSachDatPhong()
        {
            datPhongList.DataSource = DatPhongDAO.Instance.GetListDatPhong();
        }
        //void loadComboboxTenKH(ComboBox cb)
        //{
        //    cb.DataSource = DatPhongDAO.Instance.GetListDatPhong();
        //    cb.DisplayMember = "TÊN KHÁCH HÀNG";
        //    cb.ValueMember = "TÊN KHÁCH HÀNG";
        //}
        void loadComboboxMaNVDP(ComboBox cb)
        {
            cb.DataSource = NhanVienDAO.Instance.GetListNhanVien();
            cb.DisplayMember = "HỌ TÊN";
            cb.ValueMember = "HỌ TÊN";
        }
        void loadComboboxMaPhongDP(ComboBox cb)
        {
            cb.DataSource = PhongDAO.Instance.GetDanhSachPhong();
            cb.DisplayMember = "TÊN PHÒNG";
            cb.ValueMember = "TÊN PHÒNG";
        }

        void loadComboboxKhachHang(ComboBox cb)
        {
            cb.DataSource = KhachHangDAO.Instance.GetDanhSachKhachHang();
            cb.DisplayMember = "TÊN KHÁCH HÀNG";
            cb.ValueMember = "TÊN KHÁCH HÀNG";
        }

        void datPhongBinding()
        {
            txtMaDP.DataBindings.Add(new Binding("Text", dtgv_DatPhong.DataSource, "MÃ ĐẶT PHÒNG", true, DataSourceUpdateMode.Never));
            cboKhachHang.DataBindings.Add(new Binding("SelectedValue", dtgv_DatPhong.DataSource, "TÊN KHÁCH HÀNG", true, DataSourceUpdateMode.Never));
            cb_MaNV_DP.DataBindings.Add(new Binding("SelectedValue", dtgv_DatPhong.DataSource, "NHÂN VIÊN TIẾP NHẬN", true, DataSourceUpdateMode.Never));
            cb_MaPhong_DP.DataBindings.Add(new Binding("SelectedValue", dtgv_DatPhong.DataSource, "TÊN PHÒNG", true, DataSourceUpdateMode.Never));
            msk_NgayDatPhong_DP.DataBindings.Add(new Binding("Text", dtgv_DatPhong.DataSource, "NGÀY ĐẶT PHÒNG", true, DataSourceUpdateMode.Never));
            msk_NgayNhanPhong_DP.DataBindings.Add(new Binding("Text", dtgv_DatPhong.DataSource, "NGÀY NHẬN PHÒNG", true, DataSourceUpdateMode.Never));
            msk_NgayTraPhong_DP.DataBindings.Add(new Binding("Text", dtgv_DatPhong.DataSource, "NGÀY TRẢ PHÒNG", true, DataSourceUpdateMode.Never));
        }

        private void SetPlaceholderTextDatPhong()
        {
            txt_TimKiem_DP.Text = "Nhập khách hàng cần tìm...";
            txt_TimKiem_DP.ForeColor = Color.Gray;
        }
        #endregion
        #region Events DatPhong
        private void datPhongList_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            datPhongList.BindingComplete -= datPhongList_BindingComplete;
        }
        private void txtTimKiem_DP_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem_DP.Text))
            {
                SetPlaceholderTextDatPhong();
            }
        }

        private void txtTimKiem_DP_GotFocus(object sender, EventArgs e)
        {
            if (txt_TimKiem_DP.Text == "Nhập khách hàng cần tìm...")
            {
                txt_TimKiem_DP.Text = "";
                txt_TimKiem_DP.ForeColor = Color.Black;
            }
        }

        private void btn_TimKiem_DP_Click(object sender, EventArgs e)
        {
            string tenKH = txt_TimKiem_DP.Text;
            DataTable data = DatPhongDAO.Instance.TimKiemDatPhongTheoTenKH(tenKH);
            if (data.Rows.Count > 0)
            {
                datPhongList.DataSource = DatPhongDAO.Instance.TimKiemDatPhongTheoTenKH(tenKH);
            }
            else

            {
                MessageBox.Show("Không tìm khách hàng có tên là: '" + tenKH + "' trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Them_DP_Click(object sender, EventArgs e)
        {
            string maDP = txtMaDP.Text;
            string maKH = KhachHangDAO.Instance.GetKhachHangByTenKH(cboKhachHang.SelectedValue.ToString()).MaKH;
            //string maKH = KhachHangDAO.Instance.GetKhachHangByMaKH(cb_MaKH_DP.SelectedValue.ToString()).TenKH;
            string maNV = NhanVienDAO.Instance.GetNhanVienByTenNV(cb_MaNV_DP.SelectedValue.ToString()).MaNV;
            DateTime ngayDatPhong = DateTime.Parse(msk_NgayDatPhong_DP.Text);
            DateTime ngayNhanPhong = DateTime.Parse(msk_NgayNhanPhong_DP.Text);
            DateTime ngayTraPhong = DateTime.Parse(msk_NgayTraPhong_DP.Text);
            string maP = PhongDAO.Instance.GetByTenPhong(cb_MaPhong_DP.SelectedValue.ToString()).MaPhong;

            if (DatPhongDAO.Instance.themDatPhong(maDP, maKH, maNV, ngayDatPhong, ngayNhanPhong, ngayTraPhong, maP))
            {
                loadDanhSachDatPhong();
                MessageBox.Show("Thêm đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm đặt phòng thất bại do có thể mã đặt phòng bạn thêm đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_DP_Click(object sender, EventArgs e)
        {
            string maDP = (string)dtgv_DatPhong.SelectedCells[0].OwningRow.Cells["MÃ ĐẶT PHÒNG"].Value;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (DatPhongDAO.Instance.xoaDatPhong(maDP))
                {
                    loadDanhSachDatPhong();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_Sua_DP_Click(object sender, EventArgs e)
        {
            string maDPCu = (string)dtgv_DatPhong.SelectedCells[0].OwningRow.Cells["MÃ ĐẶT PHÒNG"].Value;
            string maDP = txtMaDP.Text;
            string maKH = KhachHangDAO.Instance.GetKhachHangByTenKH(cboKhachHang.SelectedValue.ToString()).MaKH;
            //string maKH = KhachHangDAO.Instance.GetKhachHangByMaKH(cb_MaKH_DP.SelectedValue.ToString()).TenKH;
            string maNV = NhanVienDAO.Instance.GetNhanVienByTenNV(cb_MaNV_DP.SelectedValue.ToString()).MaNV;
            DateTime ngayDatPhong = DateTime.Parse(msk_NgayDatPhong_DP.Text);
            DateTime ngayNhanPhong = DateTime.Parse(msk_NgayNhanPhong_DP.Text);
            DateTime ngayTraPhong = DateTime.Parse(msk_NgayTraPhong_DP.Text);
            string maP = PhongDAO.Instance.GetByTenPhong(cb_MaPhong_DP.SelectedValue.ToString()).MaPhong;

            if (DatPhongDAO.Instance.suaDatPhong(maDP, maKH, maNV, ngayDatPhong, ngayNhanPhong, ngayTraPhong, maP, maDPCu))
            {
                loadDanhSachDatPhong();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thất bại có thể do mã đặt phòng bạn sửa đã tồn tại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xem_DP_Click(object sender, EventArgs e)
        {
            loadDatPhong();
        }
        #endregion
        private void txt_Username_TK_TextChanged(object sender, EventArgs e)
        {
            if (dtgvTaiKhoan.SelectedCells.Count > 0)
            {
                string userName = (string)dtgvTaiKhoan.SelectedCells[0].OwningRow.Cells["USERNAME"].Value;
                TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByUserName(userName);
                if (tk.Quyen.Trim().ToLower() == "user")
                {
                    cboQuyen.SelectedIndex = 0;
                }
                else
                {
                    cboQuyen.SelectedIndex = 1;
                }
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'QL_KHACHSANDataSet.SP_ThongKeHoaDonReport' table. You can move, or remove it, as needed.
            //this.SP_ThongKeHoaDonReportTableAdapter.Fill(this.QL_KHACHSANDataSet.SP_ThongKeHoaDonReport, dtpkFromDate.Value, dtpkToDate.Value);

            //this.reportViewer1.RefreshReport();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            BaoCao f = new BaoCao(this);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }


    }
}
