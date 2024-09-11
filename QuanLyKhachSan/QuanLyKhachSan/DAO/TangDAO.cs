using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class TangDAO
    {
        private static TangDAO instance; 
        public static TangDAO Instance
        {
            get { if (instance == null) instance = new TangDAO(); return TangDAO.instance; }
            private set { TangDAO.instance = value; }
        }

        public List<Tang> GetListTang()
        {
            string query = "SELECT * FROM TANG";
            List<Tang> list = new List<Tang>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Tang cv = new Tang(row);
                list.Add(cv);
            }
            return list;
        }

        public Tang GetTangByTenTang(string tenTang)
        {
            Tang cv = null;
            string query = "SELECT * FROM TANG WHERE TENTANG = @tenTang";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenTang });
            foreach (DataRow row in data.Rows)
            {
                cv = new Tang(row);
                return cv;
            }
            return cv;
        }
    }
}
