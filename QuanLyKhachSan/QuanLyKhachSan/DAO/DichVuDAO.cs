using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DTO;


namespace QuanLyKhachSan.DAO
{
    public class DichVuDAO
    {
        private static DichVuDAO instance;
        public static DichVuDAO Instance
        {
            get { if (instance == null) 
                    instance = new DichVuDAO(); 
                return instance; }
            private set { instance = value; }
        }

        private DichVuDAO() { }

        public List<DichVu> GetListChiTietDichVu(string maPhong)
        {
            List<DichVu> list = new List<DichVu>();
            string query = "SELECT * FROM CHITIETDICHVU WHERE MAPHONG = @maPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {maPhong});
            foreach (DataRow item in data.Rows)
            {
                DichVu ctdv = new DichVu(item);
                list.Add(ctdv);
            }
            return list;
        }
        public DichVu GetDichVuByTenDV(string tenDV)
        {
            string query = "SELECT * FROM DICHVU WHERE TENDV = @tenDV ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenDV });

            if (data.Rows.Count > 0 && data.Rows[0] != null)
            {
                DichVu dv = new DichVu(data.Rows[0]);
                return dv;
            }

            return null;
        }

        //public DichVu GetPhongByMaDV(string maDichVu)
        //{
        //    string query = "SELECT * FROM DICHVU WHERE MADV = @maDichVu ";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maDichVu });

        //    if (data.Rows.Count > 0 && data.Rows[0] != null)
        //    {
        //        DichVu dv = new DichVu(data.Rows[0]);
        //        return dv;
        //    }

        //    return null;
        //}

        
        public bool kiemTraDichVuDaTonTai(string maPhong, string maDichVu)
        {
            string query = "SELECT * FROM dbo.CHITIETDICHVU WHERE MAPHONG = @maPhong AND MADV = @maDichVu";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong , maDichVu });
            if (result.Rows.Count > 0)
                return true;
            return false;
        }

        public void InsertDichVu(String maPhong, string maDichVu)
        {
            string query = "EXEC SP_ThemDichVu @maPhong , @maDichVu";
            DataProvider.Instance.ExecuteScalar(query, new object[] {maPhong, maDichVu});
        }

        public void DeleteDichVu(String maPhong, string maDichVu)
        {
            string query = "DELETE FROM CHITIETDICHVU WHERE MAPHONG = @maPhong AND MADV = @maDichVu";
            DataProvider.Instance.ExecuteScalar(query, new object[] { maPhong, maDichVu });
        }
        public DataTable GetListDichVu()
        {
            string query = "EXEC SP_DichVu";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool themDichVu(string maDichVu, string tenDichVu, float giaDichVu)
        {
            string query = "EXEC SP_ThemDichVu_1 @maDichVu , @tenDichVu , @giaDichVu";
            //DataProvider.Instance.ExecuteScalar(query, new object[] { maDichVu, tenDichVu, giaDichVu });
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDichVu, tenDichVu, giaDichVu });
            return result > 0;

        }

        public bool xoaDichVu(string maDichVu)
        {
            string query = "DELETE FROM DICHVU WHERE MADV = @maDV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDichVu });
            return result > 0;
        }

        public bool suaDichVu(string maDichVu, string tenDichVu, float giaDichVu, string maDVCu)
        {
            string query = "EXEC SP_SuaDichVu @maDichVu , @tenDichVu , @giaDichVu , @maDVCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDichVu, tenDichVu, giaDichVu, maDVCu });
            return result > 0;
        }
        public DataTable TimKiemDichVuTheoTen(string tenDichVu)
        {
            string query = "EXEC SP_TimKiemDichVuTheoTen @tenDichVu";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenDichVu });
        }

        public DichVu GetMaDVByTenDV(string tenDV)
        {
            string query = "SELECT * FROM DICHVU WHERE TENDV = @tenDV";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenDV });

            if (data.Rows.Count > 0)
            {
                DichVu dv = new DichVu(data.Rows[0]);
                return dv;
            }
            return null;
        }
    }
}
