using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class KhachHang
    {
        string maKH;
        string tenKH;
        string diaChi;
        string gioiTinh;
        string cCCD;
        string sDT;
        string quocTich;

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string CCCD { get => cCCD; set => cCCD = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string QuocTich { get => quocTich; set => quocTich = value; }

        public KhachHang(string maKH, string tenKH, string diaChi, string gioiTinh, string cccd, string sdt, string quocTich) 
        {
            this.MaKH = maKH;
            this.TenKH = tenKH;
            this.DiaChi = diaChi;
            this.GioiTinh = gioiTinh;
            this.CCCD = cccd;
            this.SDT = sdt;
            this.QuocTich = quocTich;
        }

        public KhachHang(DataRow row)
        {
            this.MaKH = row["MAKH"].ToString();
            this.TenKH = row["TENKH"].ToString();
            this.DiaChi = row["DIACHI"].ToString();
            this.GioiTinh = row["GIOITINH"].ToString();
            this.CCCD = row["CCCD"].ToString();
            this.SDT = row["SDT"].ToString();
            this.QuocTich = row["QUOCTICH"].ToString();
        }
    }
}
