using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class PhongDAO
    {
        private static PhongDAO instance;
        public static PhongDAO Instance
        {
            get { if  (instance == null)
                    instance = new PhongDAO();
                return PhongDAO.instance;
            }
            private set { PhongDAO.instance = value; }
        }
        public PhongDAO() { }

        public static int Width = 95;
        public static int Height = 95;

        public List<Phong> loadListPhong()
        {
            List<Phong> listPhong = new List<Phong>();
            string query = "SP_LayDanhSachPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Phong phong = new Phong(item);
                listPhong.Add(phong);
            }
            return listPhong;
        }

        public Phong GetPhongByMaPhong(string maPhong)
        {
            string query = "SELECT * FROM PHONG WHERE MAPHONG = @maPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maPhong });

            if (data.Rows.Count > 0)
            {
                Phong phong = new Phong(data.Rows[0]);
                return phong;
            }

            return null;
        }

        public Phong GetByTenPhong(string tenPhong)
        {
            string query = "SELECT * FROM PHONG WHERE TENPHONG = @tenPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenPhong });

            if (data.Rows.Count > 0)
            {
               Phong phong = new Phong(data.Rows[0]);
                return phong;
            }
            return null;
        }

        public string GetMaPhong(string maPhong)
        {
            string query = "SELECT * FROM PHONG WHERE MAPHONG = @maPhong ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {maPhong});
            if (data.Rows.Count > 0)
            {
                Phong phong = new Phong(data.Rows[0]);
                return phong.MaPhong;
            }
            return null;
        }

        public List<Phong> loadPhongTrong()
        {
            List<Phong> list = new List<Phong>();
            string query = "SELECT * FROM PHONG WHERE TINHTRANG = N'TRỐNG'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Phong phong = new Phong(row);
                list.Add(phong);
            }
            return list;
        }

        public void chuyenPhong(string maPhongCu, string maPhongMoi)
        {
            string query = "EXEC SP_ChuyenPhong @maPhongCu , @maPhongMoi";
            DataProvider.Instance.ExecuteScalar(query, new object[] { maPhongCu, maPhongMoi });
        }

        public DataTable GetDanhSachPhong()
        {
            string query = "EXEC SP_Phong";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDanhSachPhongTrong()
        {
            string query = "EXEC SP_PhongTrong";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool ThemPhong(string maPhong, string tenPhong, string tinhTrang, string maLP, string maTang)
        {
            string query = "EXEC SP_ThemPhong @maPhong , @tenPhong , @tinhTrang , @maLP , @maTang";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maPhong, tenPhong, tinhTrang, maLP, maTang });
            return result > 0;
        }

        public bool XoaPhong(string maPhong)
        {
            string query = "DELETE dbo.PHONG WHERE MAPHONG = @maPhong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maPhong });
            return result > 0;
        }

        public bool SuaPhong(string maPhong, string tenPhong, string tinhTrang, string maLP, string maTang, string maPhongCu)
        {
            string query = "EXEC SP_SuaPhong @maPhong , @tenPhong , @tinhTrang , @maLP , @maTang , @maPhongCu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maPhong, tenPhong, tinhTrang, maLP, maTang, maPhongCu });
            return result > 0;
        }

        public DataTable TimKiemPhongTheoTen(string tenPhong)
        {
            string query = "EXEC SP_TimKiemPhongTheoTen @tenPhong";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenPhong });
        }

        public Phong GetPhongByTenPhong(string tenPhong) 
        {
            string query = "SELECT * FROM PHONG WHERE TENPHONG = @tenPhong";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenPhong });

            if (data.Rows.Count > 0)
            {
                Phong phong = new Phong(data.Rows[0]);
                return phong;
            }
            return null;
        }
    }
}
