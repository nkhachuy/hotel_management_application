using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class DatPhong
    {
        string maDP;
        string maKH;        
        string maNV;
        DateTime ngayDatPhong;
        DateTime ngayNhanPhong;
        DateTime ngayTraPhong;
        string maPhong;

        public string MaDP { get => maDP; set => maDP = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public DateTime NgayDatPhong { get => ngayDatPhong; set => ngayDatPhong = value; }
        public DateTime NgayNhanPhong { get => ngayNhanPhong; set => ngayNhanPhong = value; }
        public DateTime NgayTraPhong { get => ngayTraPhong; set => ngayTraPhong = value; }
        public string MaPhong { get => maPhong; set => maPhong = value; }

        public DatPhong(string maDP, string maKH, string maNV, DateTime ngayDatPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong, string maPhong)
        {
            MaDP = maDP;
            MaKH = maKH;
            MaNV = maNV;
            NgayDatPhong = ngayDatPhong;
            NgayNhanPhong = ngayNhanPhong;
            NgayTraPhong = ngayTraPhong;
            MaPhong = maPhong;            
        }

        public DatPhong(DataRow row)
        {
            MaDP = row["MAPHONG"].ToString();
            MaKH = row["MAKH"].ToString();
            MaNV = row["MANV"].ToString();
            NgayDatPhong = DateTime.Parse(row["NGAYDATPHONG"].ToString());
            NgayNhanPhong = DateTime.Parse(row["NGAYNHANPHONG"].ToString());
            NgayTraPhong = DateTime.Parse(row["NGAYTRAPHONG"].ToString());
            MaPhong = row["MAPHONG"].ToString();
        }
        
    }
}
