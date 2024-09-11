using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class NhanVien
    {
        string maNV;
        string tenNV;
        string gioiTinh;
        DateTime ngaySinh;
        string diaChi;
        string sDT;
        string maCV;

        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string MaCV { get => maCV; set => maCV = value; }

        public NhanVien(string maNV, string tenNV, string gioiTinh, DateTime ngaySinh, string diaChi, string sDT, string maCV)
        {
            MaNV = maNV;
            TenNV = tenNV;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            SDT = sDT;
            MaCV = maCV;
            MaNV = maNV;
            TenNV = tenNV;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            SDT = sDT;
            MaCV = maCV;
        }

        public NhanVien(DataRow row)
        {
            this.MaNV = row["MANV"].ToString();
            this.TenNV = row["TENNV"].ToString();
            this.GioiTinh = row["GIOITINH"].ToString();
            this.NgaySinh = (DateTime)row["NGAYSINH"];
            this.DiaChi = row["DIACHI"].ToString();
            this.SDT = row["SDT"].ToString();
            this.MaCV = row["MACV"].ToString();
        }
    }
}
