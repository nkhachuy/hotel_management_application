using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Phong
    {
        string maPhong;
        string tenPhong;
        string tinhTrang;
        string maLoaiPhong;
        string maTang;

        public string MaPhong 
        {   
            get { return maPhong; }
            set { maPhong = value; }
        }

        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; }
        }

        public string TinhTrang
        {
            get { return tinhTrang; }
            set { tinhTrang = value; }
        }

        public string MaLoaiPhong
        {
            get { return maLoaiPhong; }
            set { maLoaiPhong = value; }
        }
        
        public string MaTang
        {
            get { return maTang; }
            set { maTang = value; }
        }

        public Phong(string maPhong, string tenPhong, string tinhTrang, string maLoaiPhong, string maTang)
        {
            this.MaPhong = maPhong;
            this.TenPhong = tenPhong;
            this.TinhTrang = tinhTrang;
            this.MaLoaiPhong = maLoaiPhong;
            this.MaTang = maTang;
        }

        public Phong(DataRow row)
        {
            this.MaPhong = row["MAPHONG"].ToString();
            this.TenPhong = row["TENPHONG"].ToString();
            this.TinhTrang = row["TINHTRANG"].ToString();  
            this.MaLoaiPhong = row["MALP"].ToString();
            this.MaTang = row["MATANG"].ToString();
        }    
    }
}
