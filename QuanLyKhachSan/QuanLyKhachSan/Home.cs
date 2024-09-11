using DevExpress.XtraPrinting.Export;
using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class Home : Form
    {
        private TaiKhoan taiKhoan;
        public TaiKhoan TaiKhoan
        {
            get { return taiKhoan; }
            set { taiKhoan = value; changeTaiKhoan(taiKhoan.Quyen); }
        }
        public Home(TaiKhoan tk)
        {
            InitializeComponent();
            this.TaiKhoan = tk;
            loadPhong();
            loadDichVu();
            loadComboboxPhong();
            cboGiamGia.SelectedIndex = 0;  
            btnChuyenPhong.Enabled = false;
            btnThanhToan.Enabled = false;
            btnGiamGia.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        #region Event
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan f = new ThongTinCaNhan(taiKhoan);
            f.CapNhatTaiKhoan += f_CapNhatTaiKhoan;
            f.ShowDialog();            
        }

        private void f_CapNhatTaiKhoan(object sender, TaiKhoanEvent e)
        {
            thôngTinCáNhânToolStripMenuItem.Text = e.TaiKhoan.TenHienThi;
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {    
            tongTien = 0;
            string maPhong = ((sender as Button).Tag as Phong).MaPhong;
            string tinhTrang = ((sender as Button).Tag as Phong).TinhTrang;
            switch (tinhTrang)
            {
                case "Trống":
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnThanhToan.Enabled = false;
                        btnChuyenPhong.Enabled = false;
                        btnGiamGia.Enabled = false;
                        break;
                    }
                case "Đang sử dụng":
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnThanhToan.Enabled = true;
                        btnChuyenPhong.Enabled = true;
                        btnGiamGia.Enabled = true;
                        break;
                    }
                case "Đã đặt":
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnThanhToan.Enabled = false;
                        btnChuyenPhong.Enabled = true;
                        break;
                    }
            }
            ltvDichVu.Tag = (sender as Button).Tag;            
            ShowPhong(maPhong);
            showDichVu(maPhong);
            tongTien = 0;
        }
        #endregion

        #region Method
        float giamGia = 0;
        float tt = 0;
        float tongTien = 0;
        void changeTaiKhoan(string quyen)
        {
            adminToolStripMenuItem.Enabled = quyen == "admin";
            thôngTinTàiKhoảnToolStripMenuItem.Text = taiKhoan.TenHienThi.ToUpper();
        }
        void loadPhong()
        {
            List<Phong> listPhong = PhongDAO.Instance.loadListPhong();
            foreach (Phong item in listPhong)
            {
                Button btn = new Button() { Width = PhongDAO.Width, Height = PhongDAO.Height};
                btn.Text = item.TenPhong + "\n" + item.TinhTrang;
                btn.Click += btn_Click;
                btn.Tag = item;
                
                switch (item.TinhTrang)
                {
                    case "Trống":
                        {
                            btn.BackColor = Color.White;                            
                            break;
                        }
                    case "Đã đặt":
                        {
                            btn.BackColor = Color.LightGoldenrodYellow;
                            break;
                        }
                    case "Đang sử dụng":
                        {
                            btn.BackColor = Color.SpringGreen;
                            break;
                        }
                }
                flpPhong.Controls.Add(btn);
            }    
        }        

        private void ShowPhong(string maPhong)
        {            
            ThongTinPhong phong = ThongTinPhongDAO.Instance.GetThongTinPhong(maPhong);
            float tienPhong = 0;
            if (phong != null)
            {
                ltvPhong.Items.Clear(); // Xóa dữ liệu cũ trước khi thêm mới

                // Thêm thông tin phòng vào ListView
                ListViewItem item = new ListViewItem(phong.TenPhong.ToString());                
                item.SubItems.Add(phong.TenLoaiPhong.ToString());
                item.SubItems.Add(phong.TenTang.ToString());
                item.SubItems.Add(phong.Gia.ToString());
                tienPhong += phong.Gia;
                ltvPhong.Items.Add(item);
            }
            tongTien += tienPhong;            
        }

        private void showDichVu(string maPhong)
        {
            ltvDichVu.Items.Clear();
            List<ThongTinDichVu> listCTDV = ThongTinDichVuDAO.Instance.GetThongTinDichVu(maPhong);
            float tienDichVu = 0;
            string maDichVu = (cboDichVu.SelectedItem as ThongTinDichVu).MaDichVu;
            foreach (ThongTinDichVu item in listCTDV)
            {
                ListViewItem ltvItem = new ListViewItem(item.TenDichVu.ToString());
                ltvItem.SubItems.Add(item.GiaDichVu.ToString());
                tienDichVu += item.GiaDichVu;
                ltvDichVu.Items.Add(ltvItem);             
            }
            tongTien += tienDichVu;
            tt = tongTien;
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTongTien.Text = tongTien.ToString("C", culture);
        }

        private void loadDichVu()
        {
            List<ThongTinDichVu> dichVu = ThongTinDichVuDAO.Instance.loadDichVu();
            cboDichVu.DataSource = dichVu;
            cboDichVu.DisplayMember = "TenDichVu";
            cboDichVu.ValueMember = "MaDichVu";
        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDichVu = cboDichVu.Text;
            ThongTinDichVu selectedDichVu = ThongTinDichVuDAO.Instance.GetDichVuByName(tenDichVu);
            string maDichVu = null;
            ComboBox cb = sender as ComboBox;
            // Hiển thị giá lên TextBox
            if (selectedDichVu != null)
            {
                ThongTinDichVu selected = cb.SelectedItem as ThongTinDichVu;
                maDichVu = selected.MaDichVu;
                txtGia.Text = selectedDichVu.GiaDichVu.ToString("C");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Phong phong = ltvDichVu.Tag as Phong;
            string maDichVu = (cboDichVu.SelectedItem as ThongTinDichVu).MaDichVu;
            string maPhong = PhongDAO.Instance.GetMaPhong(phong.MaPhong);
            if (DichVuDAO.Instance.kiemTraDichVuDaTonTai(maPhong, maDichVu) == true)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DichVuDAO.Instance.DeleteDichVu(maPhong, maDichVu);
                    showDichVu(maPhong);
                }                 
            }
            else
            {
                MessageBox.Show("Không thể xóa vì dịch vụ không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Phong phong = ltvDichVu.Tag as Phong;
            string maDichVu = (cboDichVu.SelectedItem as ThongTinDichVu).MaDichVu;
            string maPhong = PhongDAO.Instance.GetMaPhong(phong.MaPhong);
            if (DichVuDAO.Instance.kiemTraDichVuDaTonTai(maPhong, maDichVu) == false)
            {
                btnThanhToan.Enabled = true;
                btnGiamGia.Enabled = true;
                DichVuDAO.Instance.InsertDichVu(maPhong, maDichVu);
                ShowPhong(maPhong);
                showDichVu(maPhong);                
                if (phong.TinhTrang == "Trống" || phong.TinhTrang == "Đã đặt")
                {                    
                    flpPhong.Controls.Clear();
                    loadPhong();
                    tongTien = 0;
                }                 
            }
            else
            {
                MessageBox.Show("Dịch vụ đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Phong phong = ltvDichVu.Tag as Phong;
            DangNhap dn = new DangNhap();
            float thanhTien = float.Parse(txtTongTien.Text.Split(',')[0]);
            DateTime ngayThanhToan = DateTime.Now;
            string maNV = dn.GetMaNV();
            string maPhong = PhongDAO.Instance.GetMaPhong(phong.MaPhong);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn thanh toán cho phòng " + phong.TenPhong + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                HoaDonDAO.Instance.InsertHoaDon(ngayThanhToan, tt, giamGia, thanhTien, maNV, maPhong);
                showDichVu(maPhong);
                flpPhong.Controls.Clear();
                loadPhong();
                btnThanhToan.Enabled = false;
                MessageBox.Show("Thanh toán thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGiamGia_Click(object sender, EventArgs e)
        {
            Phong phong = ltvDichVu.Tag as Phong;
            float thanhTien = 0;
            float tongTien = float.Parse(txtTongTien.Text.Split(',')[0]);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn giảm giá cho phòng " + phong.TenPhong + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                switch(cboGiamGia.SelectedItem.ToString())
                {
                    case "5%":
                        {
                            giamGia = (float)(tongTien * 0.05);
                            thanhTien = tongTien - giamGia;
                            txtTongTien.Text = thanhTien.ToString("C");
                            MessageBox.Show("Đã giảm giá thành công 5% cho phong " + phong.TenPhong + "\nSố tiền còn lại phải thanh toán là " + txtTongTien.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case "10%":
                        {
                            giamGia = (float)(tongTien * 0.1);
                            thanhTien = tongTien - giamGia;
                            txtTongTien.Text = thanhTien.ToString("C");
                            MessageBox.Show("Đã giảm giá thành công 10% cho phong " + phong.TenPhong + "\nSố tiền còn lại phải thanh toán là " + txtTongTien.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case "20%":
                        {
                            giamGia = (float)(tongTien * 0.2);
                            thanhTien = tongTien - giamGia;
                            txtTongTien.Text = thanhTien.ToString("C");
                            MessageBox.Show("Đã giảm giá thành công 20% cho phong " + phong.TenPhong + "\nSố tiền còn lại phải thanh toán là " + txtTongTien.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case "25%":
                        {
                            giamGia = (float)(tongTien * 0.25);
                            thanhTien = tongTien - giamGia;
                            txtTongTien.Text = thanhTien.ToString("C");
                            MessageBox.Show("Đã giảm giá thành công 25% cho phong " + phong.TenPhong + "\nSố tiền còn lại phải thanh toán là " + txtTongTien.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case "50%":
                        {
                            giamGia = (float)(tongTien * 0.5);
                            thanhTien = tongTien - giamGia;
                            txtTongTien.Text = thanhTien.ToString("C");
                            MessageBox.Show("Đã giảm giá thành công 50% cho phong " + phong.TenPhong + "\nSố tiền còn lại phải thanh toán là " + txtTongTien.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }                
                }    
                
            }             
        }

        private void loadComboboxPhong()
        {
            List<Phong> phong = PhongDAO.Instance.loadPhongTrong();
            cboPhong.DataSource = phong;
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
        }

        private void btnChuyenPhong_Click(object sender, EventArgs e)
        {
            Phong phong = ltvDichVu.Tag as Phong;
            string maPhongMoi = (cboPhong.SelectedItem as Phong).MaPhong;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn chuyển phòng từ phòng " + phong.TenPhong + " sang phòng " + cboPhong.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PhongDAO.Instance.chuyenPhong(phong.MaPhong, maPhongMoi);
                flpPhong.Controls.Clear();                
                loadPhong();
                ShowPhong(phong.MaPhong);
                showDichVu(phong.MaPhong);
                loadComboboxPhong();
                btnThanhToan.Enabled = false;
                btnGiamGia.Enabled = false;
                btnChuyenPhong.Enabled = false;
                MessageBox.Show("Chuyển phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

    }
}
