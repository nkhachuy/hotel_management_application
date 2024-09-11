using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class ThongTinPhong
    {
        string tenPhong;
        string tenLoaiPhong;
        string tenTang;
        float gia;

        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; }
        }

        public string TenLoaiPhong
        {
            get { return tenLoaiPhong; }
            set { tenLoaiPhong = value;}
        }

        public string TenTang
        {
            get { return tenTang; }
            set { tenTang = value; }
        }

        public float Gia
        { 
            get { return gia; } 
            set {  gia = value; } 
        }

        public ThongTinPhong(string tenPhong, string tenLoaiPhong, string tenTang, float gia = 0)
        {
            this.TenPhong = tenPhong;
            this.TenLoaiPhong = tenLoaiPhong;
            this.TenTang = tenTang;
            this.Gia = gia;
        }

        public ThongTinPhong(DataRow row) 
        {
            this.TenPhong = row["TENPHONG"].ToString();
            this.TenLoaiPhong = row["TENLP"].ToString();
            this.TenTang = row["TENTANG"].ToString();
            this.Gia = float.Parse(row["DONGIA"].ToString());
        }
    }    
}
