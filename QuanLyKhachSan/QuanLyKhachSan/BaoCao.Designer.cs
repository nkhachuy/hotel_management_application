namespace QuanLyKhachSan
{
    partial class BaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.SP_ThongKeHoaDonReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QL_KHACHSANDataSet2 = new QuanLyKhachSan.QL_KHACHSANDataSet2();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SP_ThongKeHoaDonReportTableAdapter = new QuanLyKhachSan.QL_KHACHSANDataSet2TableAdapters.SP_ThongKeHoaDonReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.SP_ThongKeHoaDonReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QL_KHACHSANDataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // SP_ThongKeHoaDonReportBindingSource
            // 
            this.SP_ThongKeHoaDonReportBindingSource.DataMember = "SP_ThongKeHoaDonReport";
            this.SP_ThongKeHoaDonReportBindingSource.DataSource = this.QL_KHACHSANDataSet2;
            // 
            // QL_KHACHSANDataSet2
            // 
            this.QL_KHACHSANDataSet2.DataSetName = "QL_KHACHSANDataSet2";
            this.QL_KHACHSANDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.SP_ThongKeHoaDonReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKhachSan.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(659, 626);
            this.reportViewer1.TabIndex = 0;
            // 
            // SP_ThongKeHoaDonReportTableAdapter
            // 
            this.SP_ThongKeHoaDonReportTableAdapter.ClearBeforeFill = true;
            // 
            // BaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 626);
            this.Controls.Add(this.reportViewer1);
            this.Name = "BaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaoCao";
            this.Load += new System.EventHandler(this.BaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SP_ThongKeHoaDonReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QL_KHACHSANDataSet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_ThongKeHoaDonReportBindingSource;
        private QL_KHACHSANDataSet2 QL_KHACHSANDataSet2;
        private QL_KHACHSANDataSet2TableAdapters.SP_ThongKeHoaDonReportTableAdapter SP_ThongKeHoaDonReportTableAdapter;
    }
}