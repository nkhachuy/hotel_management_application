using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class ThongTinDichVuDAO
    {
        private static ThongTinDichVuDAO instance;

        public static ThongTinDichVuDAO Instance
        {
            get { if  (instance == null)
                    instance = new ThongTinDichVuDAO();
                return ThongTinDichVuDAO.instance;
            }
            private set { ThongTinDichVuDAO.instance = value; }
        }

        private ThongTinDichVuDAO() { }

        public List<ThongTinDichVu> GetThongTinDichVu(string maPhong)
        {

            List<ThongTinDichVu> list = new List<ThongTinDichVu>();
            string query = "EXEC SP_ThongTinDichVu @maPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong });
            foreach (DataRow row in data.Rows) 
            {
                ThongTinDichVu dichVu = new ThongTinDichVu(row);
                list.Add(dichVu);
            }
            return list;
        }

        public List<ThongTinDichVu> loadDichVu()
        {
            List<ThongTinDichVu> list = new List<ThongTinDichVu>();
            string query = "SELECT * FROM DICHVU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ThongTinDichVu dichVu = new ThongTinDichVu(row);
                list.Add(dichVu);
            }
            return list;            
        }

        public ThongTinDichVu GetDichVuByName(string tenDichVu)
        {
            ThongTinDichVu dichVu = null;
            string query = "SELECT * FROM DICHVU WHERE TENDV = @tenDichVu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {tenDichVu});

            if (data.Rows.Count > 0)
            {
                dichVu = new ThongTinDichVu(data.Rows[0]);
            }

            return dichVu;
        }
        public ThongTinDichVu GetThongDichVu(string maDichVu)
        {

            string query = "SP_ThongTinDichVu @maDichVu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maDichVu });
            if (data.Rows.Count > 0)
            {
                ThongTinDichVu dv = new ThongTinDichVu(data.Rows[0]);
                return dv;
            }

            return null;
        }


    }
}
