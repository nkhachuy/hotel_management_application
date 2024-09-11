using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;
        public static TaiKhoanDAO Instance
        {
            get { if (instance == null) 
                    instance = new TaiKhoanDAO(); 
                return instance; }
            private set { instance = value; }
        }
        
        public TaiKhoanDAO() { }

        public bool Login(string userName, string passWord)
        {
            string query = "SP_LOGIN @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName, passWord});
            if (result.Rows.Count > 0)
                return true; 
            return false;
        }

        public TaiKhoan GetMaNVByUserName(string userName)
        {
            TaiKhoan tk = null;
            string query = "SELECT * FROM TAIKHOAN WHERE USERNAME = @userName";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });
            if (data.Rows.Count > 0)
            {
                tk = new TaiKhoan(data.Rows[0]);
            }

            return tk;
        }

        public TaiKhoan GetTaiKhoanByUserName(string userName)
        {
            string query = "SELECT * FROM TAIKHOAN WHERE USERNAME = @userName";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });
            foreach (DataRow item in data.Rows)
            {
                return new TaiKhoan(item);
            }
            return null;
        }

        public bool UpdateTaiKhoan(string tenHienThi, string userName, string passWord, string newPassWord)
        {
            string query = "EXEC SP_UpdateTaiKhoan @tenHienThi , @userName , @passWord , @newPassWord";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenHienThi, userName, passWord, newPassWord });
            return result > 0;
        } 
        
        public bool DeleteTaiKhoanByMaNV(string maNV)
        {
            string query = "DELETE TAIKHOAN WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        public bool UpdateNullMaNV(string maNV)
        {
            string query = "UPDATE TAIKHOAN SET MANV = NULL WHERE MANV = @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return result > 0;
        }

        //
        public DataTable GetDanhSachTaiKhoan()
        {
            string query = "EXEC SP_TaiKhoan";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool themTaiKhoan(string tenHienThi, string username, string passwd, string quyen, string maNV)
        {
            string query = "EXEC SP_ThemTaiKhoan  @tenHienThi , @username , @passwd , @quyen , @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenHienThi, username, passwd, quyen, maNV });
            return result > 0;
        }

        public bool xoaTaiKhoan(int id)
        {
            string query = "DELETE FROM TAIKHOAN WHERE IDTK = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }

        public bool suaTaiKhoan(int idTaiKhoan, string tenHienThi, string username, string passwd, string quyen, string maNV)
        {
            string query = "EXEC SP_SuaTaiKhoan @idTaiKhoan , @tenHienThi , @username , @passws , @quyen , @maNV";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTaiKhoan, tenHienThi, username, passwd, quyen, maNV });
            return result > 0;
        }

        public DataTable TimKiemTaiKhoanTheoTen(string tenTaiKhoan)
        {
            string query = "EXEC SP_TimKiemTaiKhoanTheoTen @tenTaiKhoan";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { tenTaiKhoan });
        }
    }
}
