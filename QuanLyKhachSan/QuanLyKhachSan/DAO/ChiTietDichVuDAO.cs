using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyKhachSan.DAO
{
    class ChiTietDichVuDAO
    {
        private static ChiTietDichVuDAO instance;
        public static ChiTietDichVuDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChiTietDichVuDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private ChiTietDichVuDAO() { }

        public List<ChiTietDichVu> GetList_ChiTietDichVu(string maPhong)
        {
            List<ChiTietDichVu> list = new List<ChiTietDichVu>();
            string query = "SELECT * FROM CHITIETDICHVU WHERE MAPHONG = @maPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong });
            foreach (DataRow item in data.Rows)
            {
                ChiTietDichVu ctdv = new ChiTietDichVu(item);
                list.Add(ctdv);
            }
            return list;
        }       


        public string GetMaPhong(string maPhong)
        {
            string query = "SELECT * FROM CHITIETDICHVU WHERE MAPHONG = @maPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong });
            if (data.Rows.Count > 0)
            {
                ChiTietDichVu ctdv = new ChiTietDichVu(data.Rows[0]);
                return ctdv.MaPhong;
            }
            return null;
        }

        public ChiTietDichVu GetCTDVByMaDV(string maDichVu)
        {
            string query = "SELECT * FROM CHITIETDICHVU WHERE MADV = @maDichVu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maDichVu });

            if (data.Rows.Count > 0)
            {
                ChiTietDichVu ctdv = new ChiTietDichVu(data.Rows[0]);
                return ctdv;
            }

            return null;
        }


        public string GetMaDichVu(string maDichVu)
        {
            string query = "SELECT * FROM CHITIETDICHVU WHERE MADV = @maDichVu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maDichVu });
            if (data.Rows.Count > 0)
            {
                ChiTietDichVu ctdv = new ChiTietDichVu(data.Rows[0]);
                return ctdv.MaDV;
            }
            return null;
        }
        public bool kiemTraChiTietDichVuDaTonTai(string maPhong, string maDichVu)
        {
            string query = "SELECT * FROM dbo.CHITIETDICHVU WHERE MAPHONG = @maPhong AND MADV = @maDichVu";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong, maDichVu });
            if (result.Rows.Count > 0)
                return true;
            return false;
        }
        public DataTable GetList_ChiTietDichVu()
        {
            string query = "EXEC SP_ChiTietDichVu";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool themChiTietDichVu(string maPhong, string maDichVu)
        {
            string query = "EXEC SP_ThemChiTietDichVu @maPhong , @maDichVu ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maPhong, maDichVu });
            return result > 0;
        }

        public bool xoaChiTietDichVu(string maDichVu, string maPhong)
        {
            string query = "DELETE FROM CHITIETDICHVU WHERE MADV = @maDV AND MAPHONG = @maPhong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDichVu, maPhong });
            return result > 0;
        }

        public bool suaChiTietDichVu(string maPhong, string maDichVu, string maDichVuCu, string maPhongCu)
        {
            string query = "EXEC SP_SuaChiTietDichVu  @maPhong , @maDichVu , @maDichVuCu , @maPhongCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maPhong, maDichVu, maDichVuCu, maPhongCu });
            return result > 0;
        }

        public DataTable TimKiemChiTietDichVuTheoTenPhong(string tenPhong)
        {
            string query = "EXEC SP_TimKiemChiTietDichVuTheoTenPhong @maPhong";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenPhong });
        }


    }
}
