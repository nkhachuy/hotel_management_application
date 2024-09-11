using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class ChucVu
    {
        string maCV;
        string tenCV;

        public string MaCV { get => maCV; set => maCV = value; }
        public string TenCV { get => tenCV; set => tenCV = value; }

        public ChucVu(string maCV, string tenCV)
        {
            this.MaCV = maCV;
            this.TenCV = tenCV;
        }

        public ChucVu() { }

        public ChucVu(DataRow row)
        {
            this.MaCV = row["MaCV"].ToString();
            this.TenCV = row["TenCV"].ToString();
        }
    }
}
