using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class HoaDon
    {
        int idHoaDon;
        DateTime ngayThanhToan;
        float tongTien;
        float giamGia;
        float thanhTien;
        string maNhanVien;
        string maPhong;

        public int IdHoaDon
        {
            get { return idHoaDon; }
            set { idHoaDon = value; }
        }

        public DateTime NgayThanhToan
        {
            get { return ngayThanhToan;}
            set { ngayThanhToan = value; }
        }

        public float TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }

        public float GiamGia
        {
            get { return giamGia; }
            set { giamGia = value; }
        }

        public float ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }

        public string MaNhanVien
        {
            get { return maNhanVien; }
            set { maNhanVien = value;}
        }

        public string MaPhong
        {
            get { return maPhong; }
            set { maPhong = value; }
        }

        public HoaDon(int idHoaDon, DateTime ngayThanhToan, float tongTien, float giamGia, float thanhTien, string maNhanVien, string maPhong)
        {
            this.IdHoaDon = idHoaDon;
            this.NgayThanhToan = ngayThanhToan;
            this.TongTien = tongTien;
            this.GiamGia = giamGia;
            this.ThanhTien = thanhTien;
            this.MaNhanVien = maNhanVien;
            this.MaPhong = maPhong;
        }

        public HoaDon(DataRow row)
        {
            this.IdHoaDon = int.Parse(row["IDHOADON"].ToString());
            this.NgayThanhToan = DateTime.Parse(row["NGAYTHANHTOAN"].ToString());
            this.TongTien = float.Parse(row["TONGTIEN"].ToString());
            this.GiamGia = float.Parse(row["GIAMGIA"].ToString());
            this.ThanhTien = float.Parse(row["THANHTIEN"].ToString());
            this.MaNhanVien = row["MANV"].ToString();
            this.MaPhong = row["MAPHONG"].ToString();
        }
    }
}
