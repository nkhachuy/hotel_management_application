using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class LoaiPhong
    {
        string maLoaiPhong; 
        string tenLoaiPhong;
        float donGia;
        int soNguoi;
        int soLuong;

        public string MaLoaiPhong { get => maLoaiPhong; set => maLoaiPhong = value; }
        public string TenLoaiPhong { get => tenLoaiPhong; set => tenLoaiPhong = value; }
        public float DonGia { get => donGia; set => donGia = value; }
        public int SoNguoi { get => soNguoi; set => soNguoi = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }

        public LoaiPhong(string maLoaiPhong, string tenLoaiPhong, float donGia, int soNguoi, int soLuong) 
        {
            this.MaLoaiPhong = maLoaiPhong;
            this.TenLoaiPhong = tenLoaiPhong;
            this.DonGia = donGia;
            this.SoNguoi = soNguoi;
            this.SoLuong = soLuong;
        }

        public LoaiPhong(DataRow row)
        {
            this.MaLoaiPhong = row["MALP"].ToString();
            this.TenLoaiPhong = row["TENLP"].ToString();
            this.DonGia = float.Parse(row["DONGIA"].ToString());
            this.SoNguoi = int.Parse(row["SONGUOI"].ToString());
            this.SoLuong = int.Parse(row["SOLUONG"].ToString());
        }
    }
}
