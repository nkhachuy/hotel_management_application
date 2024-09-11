using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;

        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return instance; }
            set { instance = value; }
        }

        public DataTable GetDanhSachKhachHang()
        {
            string query = "EXEC SP_KhachHang";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public KhachHang GetKhachHangByMaKH(string maKH)
        {
            KhachHang kh = null;
            string query = "SELECT * FROM KHACHHANG WHERE MAKH = @maKH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maKH });
            foreach (DataRow item in data.Rows)
            {
                kh = new KhachHang(item);
            }
            return kh;
        }

        public KhachHang GetKhachHangByTenKH(string tenKH)
        {
            KhachHang kh = null;
            string query = "SELECT * FROM KHACHHANG WHERE TENKH = @tenKH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenKH });
            foreach (DataRow item in data.Rows)
            {
                kh = new KhachHang(item);
            }
            return kh;
        }

        public bool ThemKhachHang(string maKH, string tenKH, string diaChi, string gioiTinh, string CCCD, string sdt, string quocTich)
        {
            string query = "EXEC SP_ThemKhachHang @maKH , @tenKH , @diaChi , @gioiTinh , @CCCD , @sdt , @quocTich";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maKH, tenKH, diaChi, gioiTinh, CCCD, sdt, quocTich });
            return result > 0;
        }

        public bool XoaKhachHang(string maKH)
        {
            string query = "DELETE dbo.KHACHHANG WHERE MAKH = @maKH";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maKH });
            return result > 0;
        }

        public bool SuaKhachHang(string maKH, string tenKH, string diaChi, string gioiTinh, string CCCD, string sdt, string quocTich, string maKHCU)
        {
            string query = "EXEC SP_UpdateKhachHang @maKH , @tenKH , @diaChi , @gioiTinh , @CCCD , @sdt , @quocTich , @maKHCU";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maKH, tenKH, diaChi, gioiTinh, CCCD, sdt, quocTich, maKHCU });
            return result > 0;
        }  

        public DataTable TimKiemKhachHangTheoTen(string ten)
        {
            string query = "EXEC SP_TimKiemKhachHangTheoTen @ten";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { ten });
        }
    }
}
