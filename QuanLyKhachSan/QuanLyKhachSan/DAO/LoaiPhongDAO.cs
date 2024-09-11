using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class LoaiPhongDAO
    {
        private static LoaiPhongDAO instance;

        public static LoaiPhongDAO Instance
        {
            get { if (instance == null) instance = new LoaiPhongDAO(); return LoaiPhongDAO.instance; }
            private set { LoaiPhongDAO.instance = value; }
        }

        public List<LoaiPhong> GetListLoaiPhong()
        {
            string query = "SELECT * FROM LOAIPHONG";
            List<LoaiPhong> list = new List<LoaiPhong>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                LoaiPhong cv = new LoaiPhong(row);
                list.Add(cv);
            }
            return list;
        }

        public LoaiPhong GetLoaiPhongByTenLoaiPhong(string tenLoaiPhong)
        {
            LoaiPhong cv = null;
            string query = "SELECT * FROM LOAIPHONG WHERE TENLP = @tenLoaiPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenLoaiPhong });
            foreach (DataRow row in data.Rows)
            {
                cv = new LoaiPhong(row);
                return cv;
            }
            return cv;
        }

        public string GetMaLoaiPhong(string maLoaiPhong)
        {
            string query = "SELECT * FROM LOAIPHONG WHERE MALP = @maLoaiPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maLoaiPhong });
            if (data.Rows.Count > 0)
            {
                LoaiPhong loaiPhong = new LoaiPhong(data.Rows[0]);
                return loaiPhong.MaLoaiPhong;
            }
            return null;
        }
        public void chuyenLoaiPhong(string maLoaiPhongCu, string maLoaiPhongMoi)
        {
            string query = "EXEC SP_ChuyenLoaiPhong @maLoaiPhongCu , @maLoaiPhongMoi";
            DataProvider.Instance.ExecuteScalar(query, new object[] { maLoaiPhongCu, maLoaiPhongMoi });
        }
        public DataTable GetDanhSachLoaiPhong()
        {
            string query = "EXEC SP_LoaiPhong";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool ThemLoaiPhong(string maLoaiPhong, string tenLoaiPhong, float donGia, int soNguoi, int soLuong)
        {
            string query = "EXEC SP_ThemLoaiPhong @maLoaiPhong , @tenLoaiPhong , @donGia , @soNguoi , @soLuong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maLoaiPhong, tenLoaiPhong, donGia, soNguoi, soLuong });
            return result > 0;
        }

        public bool XoaLoaiPhong(string maLoaiPhong)
        {
            string query = "DELETE dbo.LOAIPHONG WHERE MALP = @maLoaiPhong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maLoaiPhong });
            return result > 0;
        }

        public bool SuaLoaiPhong(string maLoaiPhong, string tenLoaiPhong, float donGia, int soNguoi, int soLuong, string maLoaiPhongCu)
        {
            string query = "EXEC SP_SuaLoaiPhong @maLoaiPhong , @tenLoaiPhong , @donGia , @soNguoi , @soLuong, @maPhongCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maLoaiPhong, tenLoaiPhong, donGia, soNguoi, soLuong, maLoaiPhongCu });
            return result > 0;
        }

        public DataTable TimKiemLoaiPhongTheoTen(string tenLoaiPhong)
        {
            string query = "EXEC SP_TimKiemLoaiPhongTheoTen @tenLoaiPhong";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenLoaiPhong });
        }
    }
}
