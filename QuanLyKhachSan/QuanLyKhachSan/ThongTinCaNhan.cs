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
    public partial class ThongTinCaNhan : Form
    {
        private TaiKhoan taiKhoan;        

        public TaiKhoan TaiKhoan
        {
            get { return taiKhoan; }
            set { taiKhoan = value; changeTaiKhoan(taiKhoan); }
        }
        public ThongTinCaNhan(TaiKhoan tk)
        {
            
            InitializeComponent();
            this.TaiKhoan = tk;
        }

        void changeTaiKhoan(TaiKhoan tk)
        {
            txtTenHienThi.Text = tk.TenHienThi;
            txtTenDangNhap.Text = tk.UserName;            
        }

        void UpdateTaiKhoan()
        {
            string tenHienThi = txtTenHienThi.Text;
            string userName = txtTenDangNhap.Text;
            string passWord = txtMatKhau.Text;
            string newPassWord = txtMatKhauMoi.Text;
            string reenterPassWord = txtNhapLaiMatKhau.Text;
            if (!newPassWord.Equals(reenterPassWord))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu trùng với mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
            else
            {
                if (TaiKhoanDAO.Instance.UpdateTaiKhoan(tenHienThi, userName, passWord, newPassWord))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!");
                    if (capNhatTaiKhoan != null)
                    {
                        capNhatTaiKhoan(this, new TaiKhoanEvent(TaiKhoanDAO.Instance.GetTaiKhoanByUserName(userName)));
                    }                    
                }   
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại, vui lòng nhập tài khoản hoặc mật khẩu chính xác!");
                }    
            }    
        }

        private event EventHandler<TaiKhoanEvent> capNhatTaiKhoan;
        public event EventHandler<TaiKhoanEvent> CapNhatTaiKhoan
        {
            add { capNhatTaiKhoan += value; }
            remove { capNhatTaiKhoan -= value; }
        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            UpdateTaiKhoan();
        }
    }

    public class TaiKhoanEvent:EventArgs
    {
        private TaiKhoan taiKhoan;

        public TaiKhoan TaiKhoan { get => taiKhoan; set => taiKhoan = value; }

        public TaiKhoanEvent(TaiKhoan taiKhoan)
        {
            this.taiKhoan = taiKhoan;
        }
    }
}
