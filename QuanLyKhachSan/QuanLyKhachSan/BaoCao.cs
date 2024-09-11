using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class BaoCao : Form
    {
        private Admin adminForm;

        public BaoCao(Admin admin)
        {
            InitializeComponent();
            adminForm = admin;
        }

        private void BaoCao_Load(object sender, EventArgs e)
        {
            DateTime FromDate = adminForm.SelectedFromDate.Date;
            DateTime ToDate = adminForm.SelectedToDate.Date;
            // TODO: This line of code loads data into the 'QL_KHACHSANDataSet2.SP_ThongKeHoaDonReport' table. You can move, or remove it, as needed.
            this.SP_ThongKeHoaDonReportTableAdapter.Fill(this.QL_KHACHSANDataSet2.SP_ThongKeHoaDonReport, FromDate, ToDate);

            this.reportViewer1.RefreshReport();
        }
    }
}
