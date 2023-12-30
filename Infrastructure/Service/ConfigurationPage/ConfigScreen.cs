using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Data.DbContexts;
using Domain.Entites.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.ConfigurationPage
{
    public class ConfigScreen : IConfigScreen
    {
        private readonly ExportDbContext _exportDbContext;
        public ConfigScreen(ExportDbContext exportDbContext)
        {
            _exportDbContext = exportDbContext;

        }
        public async Task<List<Product_Type>> GetProductTypesList()
        {
            var productTypes = await _exportDbContext.product_type
                .Select(pt => new Product_Type { productId = pt.productId, productType = pt.productType })
                .ToListAsync();

            return productTypes;
        }

        public async Task<List<Export_Type>> GetExportTypesList()
        {
            var exportTypes = await _exportDbContext.export_type
                .Select(pt => new Export_Type { exportId = pt.exportId, exportType = pt.exportType })
                .ToListAsync();

            return exportTypes;
        }

        public async Task<List<Export_Format>> GetExportFormatAsync()
        {
            var exportFormat = await _exportDbContext.export_format
            .Select(pt => new Export_Format { exportFormatId = pt.exportFormatId, exportFormat = pt.exportFormat })
                .ToListAsync();
            return exportFormat;
        }
        public async Task<List<Filter_Records_by_Processor>> GetFilterRecord()
        {
            var filterRecord = await _exportDbContext.filter_Records_by_Processor
            .Select(pt => new Filter_Records_by_Processor { recordId = pt.recordId, recordType = pt.recordType })
                .ToListAsync();
            return filterRecord;
        }
        public async Task<List<Email_Template>> GetEmailTemplate()
        {
            var emailTemplate = await _exportDbContext.email_template
            .Select(pt => new Email_Template { emailTemplateId = pt.emailTemplateId, emailTemplate = pt.emailTemplate })
                .ToListAsync();
            return emailTemplate;
        }

        public async Task<Dictionary<string, List<object>>> GetAllDropdownData()
        {
            var dropdownData = new Dictionary<string, List<object>>();

            // Add product types data
            var productTypes = await GetProductTypesList();
            dropdownData.Add("ProductTypes", productTypes.Cast<object>().ToList());

            // Add export types data
            var exportTypes = await GetExportTypesList();
            dropdownData.Add("ExportTypes", exportTypes.Cast<object>().ToList());

            // Add export format data
            var exportFormats = await GetExportFormatAsync();
            dropdownData.Add("ExportFormats", exportFormats.Cast<object>().ToList());

            // Add filter record data
            var filterRecords = await GetFilterRecord();
            dropdownData.Add("FilterRecords", filterRecords.Cast<object>().ToList());

            // Add email template data
            var emailTemplates = await GetEmailTemplate();
            dropdownData.Add("EmailTemplates", emailTemplates.Cast<object>().ToList());

            return dropdownData;
        }



    }
}