using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class HoaDonDAO
    {
        private static HoaDonDAO instance;
        public static HoaDonDAO Instance
        {
            get { if (instance == null) 
                    instance = new HoaDonDAO(); 
                return HoaDonDAO.instance; }
            private set { HoaDonDAO.instance = value; }
        }   
        
        public HoaDonDAO() { }

        public void InsertHoaDon(DateTime ngayThanhToan, float tongTien, float giamGia, float thanhTien, string maNV, string maPhong)
        {
            string query = "EXEC SP_ThemHoaDon @ngayThanhToan , @tongTien , @giamGia , @thanhTien , @maNV , @maPhong";
            DataProvider.Instance.ExecuteScalar(query, new object[] { ngayThanhToan, tongTien, giamGia, thanhTien, maNV, maPhong });
        }

        public DataTable GetListHoaDonByDate(DateTime tuNgay, DateTime denNgay)
        {
            string query = "EXEC SP_ThongKeHoaDon @tuNgay , @denNgay";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tuNgay, denNgay });
        }    

        public bool DeleteHoaDonByMaNV(string maNV)
        {
            string query = "DELETE FROM HOADON WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        public bool UpdateNullMaNV(string maNV)
        {
            string query = "UPDATE HOADON SET MANV = NULL WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        //

        public List<HoaDon> GetDanhSachHoaDon()
        {
            string query = "SELECT * FROM HOADON";
            List<HoaDon> list = new List<HoaDon>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                HoaDon hoaDon = new HoaDon(row);
                list.Add(hoaDon);
            }   
            return list;
        }
        public DataTable GetListHoaDon()
        {
            string query = "EXEC SP_HoaDon_1";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool themHoaDon(DateTime ngayThanhToan, float tongTien, float giamGia,  float thanhTien, string maNhanVien, string maPhong)
        {
            string query = "EXEC SP_Them_HoaDon @ngayThanhToan , @tongTien , @giamGia , @thanhTien , @maNhanVien , @maPhong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ngayThanhToan, tongTien, giamGia, thanhTien, maNhanVien, maPhong });
            return result > 0;
        }

        public bool suaHoaDon(int idHoaDon, DateTime ngayThanhToan, float tongTien, float giamGia, float thanhTien, string maNhanVien, string maPhong, int idHoaDonCu)
        {
            string query = "EXEC SP_SuaHoaDon @idHoaDon , @ngayThanhToan , @tongTien , @giamGia , @thanhTien , @maNhanVien , @maPhong , @idHoaDonCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idHoaDon, ngayThanhToan, tongTien, giamGia, thanhTien, maNhanVien, maPhong, idHoaDonCu });
            return result > 0;
        }

        public bool xoaHoaDon(int id)
        {
            string query = "DELETE dbo.HOADON WHERE IDHOADON = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }

        public HoaDon GetNhanVienIDHoaDon(string idHoaDon)
        {
            HoaDon hd = null;
            string query = "SELECT * FROM HOADON WHERE IDHOADON = @idHoaDon";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idHoaDon });
            foreach (DataRow item in data.Rows)
            {
                hd = new HoaDon(item);
            }
            return hd;
        }

        public DataTable TimKiemHoaDonByID(string idHoaDon)
        {
            string query = "EXEC SP_TimKiemHoaDonTheoID @idHoaDon";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { idHoaDon });
        }
    }
}
