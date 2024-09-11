using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class ThongTinDichVu
    {
        string maDichVu;
        string tenDichVu;
        float giaDichVu;

        public string MaDichVu
        {
            get { return maDichVu; }
            set { maDichVu = value; }
        }    
        public string TenDichVu
        {
            get { return tenDichVu; }
            set { tenDichVu = value; }
        }

        public float GiaDichVu
        {
            get { return giaDichVu; }
            set { giaDichVu = value;}
        }

        public ThongTinDichVu(string maDichVu, string tenDichVu, float giaDichVu = 0)
        {
            this.MaDichVu = maDichVu;
            this.TenDichVu = tenDichVu;
            this.GiaDichVu = giaDichVu;
        }

        public ThongTinDichVu(DataRow row)
        {
            this.MaDichVu = row["MADV"].ToString();
            this.TenDichVu = row["TENDV"].ToString();
            this.GiaDichVu = float.Parse(row["GIADV"].ToString());
        }

    }    
    
}
