using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Tang
    {
        string maTang;
        string tenTang;

        public string MaTang { get => maTang; set => maTang = value; }
        public string TenTang { get => tenTang; set => tenTang = value; }

        public Tang(string maTang, string tenTang)
        {
            this.MaTang = maTang;
            this.TenTang = tenTang;
        }  
        
        public Tang(DataRow row) 
        {
            this.MaTang = row["MATANG"].ToString();
            this.TenTang = row["TENTANG"].ToString();
        }
    }
}
