using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IReportService
    {
        Task<string> ExportReport(ReportData reportData);
    }
}
