using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class ReportData
    {
        public List<ReportDataTable> DataTables { get; set; }
        public string ReportName { get; set; }
        public List<ReportParameter> Parameters { get; set; }
        public Language Language { get; set; }
        public FormatType FormatType { get; set; }

    }

    public class ReportDataTable
    {
        public List<object> Data { get; set; } = new List<object>();
        public string ClassName { get; set; }
    }

    public class ReportParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string SubReportName { get; set; }
    }

    public enum Language { Arabic = 0, English = 1 }
    public enum FormatType
    {
        PDF = 1,
        Excel = 2
    }
}
