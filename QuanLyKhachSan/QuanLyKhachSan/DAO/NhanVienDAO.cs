using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null)
                    instance = new NhanVienDAO();
            return instance;
            }
            private set { instance = value; }            
        }

        private NhanVienDAO() { }

        //public List<NhanVien> GetListNhanVien()
        //{
        //    List<NhanVien> list = new List<NhanVien>();
        //    string query = "SELECT * FROM NHANVIEN";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query);
        //    foreach (DataRow item in data.Rows) 
        //    {
        //        NhanVien nhanvien = new NhanVien(item);
        //        list.Add(nhanvien);
        //    }
        //    return list;
        //}

        public DataTable GetListNhanVien()
        {
            string query = "EXEC SP_NhanVien";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool InsertNhanVien(string maNV, string hoTen, string gioiTinh, DateTime ngaySinh, string diaChi, string sdt, string maCV)
        {
            string query = "EXEC SP_ThemNhanVien @maNV , @hoTen , @gioiTinh , @ngaySinh , @diaChi , @sdt , @maCV";
            int result =  DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV, hoTen, gioiTinh, ngaySinh, diaChi, sdt, maCV });
            return result > 0;
        }

        public bool UpdateNhanVien(string maNV, string hoTen, string gioiTinh, DateTime ngaySinh, string diaChi, string sdt, string maCV, string maNVCu)
        {
            string query = "EXEC SP_SuaNhanVien @maNV , @hoTen , @gioiTinh , @ngaySinh , @diaChi , @sdt , @maCV , @maNVCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV, hoTen, gioiTinh, ngaySinh, diaChi, sdt, maCV, maNVCu });
            return result > 0;
        }

        public bool DeleteNhanVien(string maNV) 
        {
            string query = "DELETE dbo.NHANVIEN WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        public NhanVien GetNhanVienByMaNV(string maNV)
        {
            NhanVien nv = null;
            string query = "SELECT * FROM NHANVIEN WHERE MANV = @maNV";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maNV });
            foreach (DataRow item in data.Rows)
            {
                nv = new NhanVien(item);
            }
            return nv;
        }

        public NhanVien GetNhanVienByTenNV(string tenNV)
        {
            NhanVien nv = null;
            string query = "SELECT * FROM NHANVIEN WHERE TENNV = @tenNV";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenNV });
            foreach (DataRow item in data.Rows)
            {
                nv = new NhanVien(item);
            }
            return nv;
        }    

        public DataTable TimKiemNhanVienByTen(string ten)
        {
            string query = "EXEC SP_TimKiemNhanVienTheoTen @ten";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { ten });            
        }
    }
}
