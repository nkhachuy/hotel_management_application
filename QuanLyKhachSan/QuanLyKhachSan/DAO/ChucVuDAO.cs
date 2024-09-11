using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class ChucVuDAO
    {
        private static ChucVuDAO instance;
        public static ChucVuDAO Instance
        {
            get { if (instance == null) instance = new ChucVuDAO(); return ChucVuDAO.instance; }
            private set { instance = value; }
        }

        public List<ChucVu> GetListChucVu()
        {
            string query = "SELECT * FROM CHUCVU";
            List<ChucVu> list = new List<ChucVu>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows) 
            {
                ChucVu cv = new ChucVu(row);
                list.Add(cv);
            }
            return list;
        }

        public ChucVu GetChucVuByMaChucVu(string maChucVu)
        {
            ChucVu cv = null;
            string query = "SELECT * FROM CHUCVU WHERE MACV = @maChucVu";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maChucVu });
            foreach (DataRow row in data.Rows) 
            {
                cv = new ChucVu(row);
                return cv;
            }
            return cv;
        }

        public ChucVu GetChucVuByTenChucVu(string tenChucVu)
        {
            ChucVu cv = null;
            string query = "SELECT * FROM CHUCVU WHERE TENCV = @tenChucVu";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenChucVu });
            foreach (DataRow row in data.Rows)
            {
                cv = new ChucVu(row);
                return cv;
            }
            return cv;
        }

        public DataTable GetDanhSachChucVu()
        {
            string query = "SELECT MACV AS N'MÃ CHỨC VỤ', TENCV AS N'TÊN CHỨC VỤ' FROM CHUCVU";
            return DataProvider.Instance.ExecuteQuery(query);
        }   
        
        public bool ThemChucVu(string maChucVu, string tenChucVu)
        {
            string query = "EXEC SP_ThemChucVu @maChucVu , @tenChucVu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maChucVu, tenChucVu });
            return result > 0;
        }

        public bool XoaChucVu(string maChucVu)
        {
            string query = "DELETE dbo.CHUCVU WHERE MACV = @maChucVu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maChucVu });
            return result > 0;
        }

        public bool CapNhatChucVu(string maChucVu, string tenChucVu, string maChucVuCu)
        {
            string query = "EXEC SP_UpdateChucVu @maChucVu , @tenChucVu , @maChucVuCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maChucVu, tenChucVu, maChucVuCu });
            return result > 0;
        }

        public DataTable TimKiemChucVuTheoTen(string tenCV)
        {
            string query = "EXEC SP_TimKiemChucVuTheoTen @tenCV";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenCV });
        }    


    }
}
