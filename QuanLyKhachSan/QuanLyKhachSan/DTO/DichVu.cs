using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class DichVu
    {
        string tenDichVu;
        string maDichVu;

        public string MaDichVu
        {
            get { return maDichVu; }
            set { maDichVu = value; }
        }

        public string TenDichVu { get => tenDichVu; set => tenDichVu = value; }

        public DichVu(string maPhong, string maDichVu)
        {
            TenDichVu = tenDichVu;
            MaDichVu = maDichVu;            
        }

        public DichVu(DataRow row)
        {
            TenDichVu = row["TENDV"].ToString();
            MaDichVu = row["MADV"].ToString();
        }  
    }
}
