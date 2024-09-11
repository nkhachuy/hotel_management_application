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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string passWord = txtPassword.Text;
            if (Login(userName, passWord))
            {
                TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByUserName(userName);
                Home f = new Home(tk);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }   
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }             
        }

        bool Login(string userName, string passWord)
        {
            return TaiKhoanDAO.Instance.Login(userName, passWord);
        }

        public string GetMaNV()
        {
            string userName = txtUsername.Text;
            string maNV = TaiKhoanDAO.Instance.GetMaNVByUserName(userName).MaNV;
            return maNV;
        }

    }
}
