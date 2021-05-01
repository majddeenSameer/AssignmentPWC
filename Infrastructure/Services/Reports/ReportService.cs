using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Utilities;

namespace Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private IConfiguration _configuration;
        public ReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ExportReport(ReportData reportData)
        {
            string baseUri = _configuration["Report:BaseUri"];

            if (string.IsNullOrEmpty(baseUri))
                throw new KeyNotFoundException("Report:BaseUri key not found");

            var data64 = await RestApiHelper.PostAsync<string, ReportData>($"{baseUri}api/Report/print", reportData, enableCamelCase: false);


            return data64;
        }
    }
}
