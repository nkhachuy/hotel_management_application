using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyKhachSan.DTO
{
    class ChiTietDichVu
    {
        string maPhong;
        string maDV;

        public string MaPhong { get => maPhong; set => maPhong = value; }
        public string MaDV { get => maDV; set => maDV = value; }

        public ChiTietDichVu(string maPhong, string maDV)
        {
            MaPhong = maPhong;
            MaDV = maDV;
        }
        public ChiTietDichVu(DataRow row)
        {
            MaPhong = row["MAPHONG"].ToString();
            MaDV = row["MADV"].ToString();
        }
    }
}
