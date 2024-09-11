using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class TaiKhoan
    {
        string tenHienThi;
        string userName;
        string passWord;
        string quyen;
        string maNV;

        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string Quyen { get => quyen; set => quyen = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public string TenHienThi { get => tenHienThi; set => tenHienThi = value; }

        public TaiKhoan(string tenHienThi, string userName, string passWord, string quyen, string maNV)
        {
            this.TenHienThi = tenHienThi;
            this.UserName = userName;
            this.PassWord = passWord;
            this.Quyen = quyen;
            this.MaNV = maNV;
        }

        public TaiKhoan(DataRow row)
        {
            this.TenHienThi = row["TENHIENTHI"].ToString();
            this.UserName = row["USERNAME"].ToString();
            this.PassWord = row["PASSWD"].ToString();
            this.Quyen = row["QUYEN"].ToString();
            this.MaNV = row["MANV"].ToString();
        }
    }
}
