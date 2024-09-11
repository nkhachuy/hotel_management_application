using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyKhachSan.DAO
{
    public class DatPhongDAO
    {
        private static DatPhongDAO instance;

        public static DatPhongDAO Instance 
        {
            get { if (instance == null) instance = new DatPhongDAO(); return DatPhongDAO.instance; }
            private set { DatPhongDAO.instance = value; }
        }

        
        public List<DatPhong> loadListDatPhong()
        {
            List<DatPhong> listDatPhong = new List<DatPhong>();
            string query = "SP_LayDanhDatPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DatPhong dp = new DatPhong(item);
                listDatPhong.Add(dp);
            }
            return listDatPhong;
        }
        private DatPhongDAO() { }
        public DataTable GetListDatPhong()
        {
            string query = "EXEC SP_DatPhong";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        //public KhachHang GetDatPhongMaKH(string maKH)
        //{
        //    string query = "SELECT * FROM KHACHHANG WHERE MAKH = @maKH";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maKH });

        //    if (data.Rows.Count > 0)
        //    {
        //        KhachHang kh = new KhachHang(data.Rows[0]);
        //        return kh;
        //    }
        //    return null;
        //}
        //public KhachHang GetDatPhongTenKH(string tenKH)
        //{
        //    string query = "SELECT * FROM KHACHHANG WHERE TENKH = @tenKH";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenKH });

        //    if (data.Rows.Count > 0)
        //    {
        //        KhachHang kh = new KhachHang(data.Rows[0]);
        //        return kh;
        //    }
        //    return null;
        //}
        //public NhanVien GetNhanVienMaNV(string maNV)
        //{
        //    string query = "SELECT * FROM NHANVIEN WHERE MANV = @maNV";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maNV });

        //    if (data.Rows.Count > 0)
        //    {
        //        NhanVien nv = new NhanVien(data.Rows[0]);
        //        return nv;
        //    }
        //    return null;
        //}
        //public NhanVien GetNhanVienTenNV(string tenNV)
        //{
        //    string query = "SELECT * FROM NHANVIEN WHERE TENNV = @tenNV";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenNV });

        //    if (data.Rows.Count > 0)
        //    {
        //        NhanVien nv = new NhanVien(data.Rows[0]);
        //        return nv;
        //    }
        //    return null;
        //}
        public bool DeleteDatPhongByMaNV(string maNV)
        {
            string query = "DELETE DBO.DATPHONG WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        public bool themDatPhong(string maDatPhong, string maKhachHang, string maNhanVien, DateTime ngayDatPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong, string maPhong)
        {
            string query = "EXEC SP_ThemDatPhong @maDatPhong , @maKhachHang , @maNhanVien , @ngayDatPhong , @ngayNhanPhong , @ngayTraPhong , @maPhong ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDatPhong, maKhachHang, maNhanVien, ngayDatPhong, ngayNhanPhong, ngayTraPhong, maPhong });
            return result > 0;
        }

        public bool xoaDatPhong(string maDatPhong)
        {
            string query = "DELETE FROM DATPHONG WHERE MADP = @maDP";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDatPhong });
            return result > 0;
        }

        public bool suaDatPhong(string maDatPhong, string maKhachHang, string maNhanVien, DateTime ngayDatPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong, string maPhong, string maDatPhongCu)
        {
            string query = "EXEC SP_SuaDatPhong  @maDatPhong , @maKhachHang , @maNhanVien , @ngayDatPhong , @ngayNhanPhong , @ngayTraPhong , @maPhong , @maDatPhongCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDatPhong, maKhachHang, maNhanVien, ngayDatPhong, ngayNhanPhong, ngayTraPhong, maPhong, maDatPhongCu });
            return result > 0;
        }
        public DataTable TimKiemDatPhongTheoTenKH(string tenKhachHang)
        {
            string query = "EXEC SP_TimKiemDatPhongTheoTenKH @tenKhachHang";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenKhachHang });
        }
    }
}
