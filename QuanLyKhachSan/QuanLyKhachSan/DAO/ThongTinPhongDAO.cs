using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DTO;

namespace QuanLyKhachSan.DAO
{
    public class ThongTinPhongDAO
    {
        private static ThongTinPhongDAO instance;

        public static ThongTinPhongDAO Instance
        {
            get { if (instance == null) 
                    instance = new ThongTinPhongDAO(); 
                return ThongTinPhongDAO.instance; }
            private set { ThongTinPhongDAO.instance = value; }
        }

        private ThongTinPhongDAO() { }

        public ThongTinPhong GetThongTinPhong(string maPhong)
        {
            
            string query = "SP_ThongTinPhong @maPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {maPhong});
            if (data.Rows.Count > 0)
            {
                ThongTinPhong phong = new ThongTinPhong(data.Rows[0]);
                return phong;
            }

            return null;
        }
    }
}
