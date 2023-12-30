using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Application.Interface;
using Domain.Data.DbContexts;
using Domain.Entites.Model;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

// using 

using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace Infrastructure.Service.ExportPage
{
    public class ExportScreen : IExportScreen
    {
        private readonly ExportDbContext _context;


        public ExportScreen(ExportDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<string>> GetTemplateNamesAsync()
        {
            var templateNames = await _context.ExportConfigPageTables
                .Where(config => config.IsActive == true && config.IsDeleted == false)
                .Select(config => config.TemplateName)
                .ToListAsync();

            return templateNames;
        }
        // public ActionResult<IEnumerable<string>> GetProductTypes()
        // {
        //     var productTypes = _context.ExportConfigPageTables
        //         // .Where(config => config.IsActive == true && config.IsDeleted == false)
        //         .Select(config => config.ProductType)
        //         .Distinct()
        //         .ToList();

        //     return productTypes;
        // }


        public async Task<IActionResult> GenerateExcel([FromBody] ExportConfigPageTable request)
        {
            // Declare demandTerminalData outside the using block
            List<DemandTerminal> demandTerminalData;

            // Fetch data from DemandTerminal table
            demandTerminalData =  await _context.DemandTerminal.ToListAsync();

            // Create Excel package using EPPlus
            var workbook = new XLWorkbook();

            // Fetch export configuration including parameter selection for the specified template
            var exportConfigs = _context.ExportConfigPageTables
                .Join(
                    _context.ExportParameterSelectionPages,
                    config => config.ConfigId,
                    selection => selection.ConfigId,
                    (config, selection) => new { Config = config, Selection = selection }
                )
                .Where(joinResult => joinResult.Config.TemplateName == request.TemplateName)
                .ToList();

            // Fetch the first configuration from the list
            var exportConfig = exportConfigs.First();

            // Determine the number of blank lines to skip
            int blankLinesFromTop = exportConfig.Config.blankLines ?? 0;
            int dateSequence = exportConfig.Config.dateSequence ?? 0;
            int TransactionSequenceNoInput = exportConfig.Config.noOfTransactionSequence ?? 0;
            int TotalTransactionValueSequence = exportConfig.Config.totalTransactionValueSequence ?? 0;

            // Create Excel worksheet
            var worksheet = workbook.Worksheets.Add("ExportedData");


            // Set the row where Date will be placed
            var dateRow = blankLinesFromTop + 1;

            // Handle DateCheckbox and Date if IsHeader is true
            if (exportConfig.Config.IsHeader == true)
            {
                // Customize headers based on ExportConfigPageTable values
                if (exportConfig.Config.isIncludedDate == true)
                {
                    var dateColumn = dateSequence; // Get the next column
                    worksheet.Cell(dateRow, dateColumn).Value = "Date";
                    worksheet.Cell(dateRow + 1, dateColumn).Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (exportConfig.Config.isIncludedNoOfTransactions == true)
                {
                    var dateColumn = TransactionSequenceNoInput; // Get the next column
                    worksheet.Cell(dateRow, TransactionSequenceNoInput).Value = "No. of Transaction";
                    worksheet.Cell(dateRow + 1, TransactionSequenceNoInput).Value = _context.DemandTerminal.Count(dt => dt.ApplicationId != null);
                }
                if (exportConfig.Config.isIncludedTotalTransactionValue == true)
                {
                    var dateColumn = TotalTransactionValueSequence; // Get the next column
                    worksheet.Cell(dateRow, dateColumn).Value = "Total value of transaction";
                    worksheet.Cell(dateRow + 1, dateColumn).Value = _context.DemandTerminal.Sum(dt => dt.RepaymentAmount);
                }
            }

            // Adjust the starting row for headers based on blankLines value
            var headerStartRow = exportConfig.Config.IsHeader == true ? dateRow + 2 : blankLinesFromTop + 1;

            // Add headers based on ExportParameterSelectionPage
            for (var i = 1; i <= exportConfigs.Count; i++)
            {
                worksheet.Cell(headerStartRow, i).Value = exportConfigs[i - 1].Selection.transactionDataParameter;
            }

            // Add data to the Excel sheet, starting from the row after skipping blank lines
            for (var row = 1; row <= demandTerminalData.Count; row++)
            {
                var rowData = demandTerminalData[row - 1]; // Adjust the index

                var col = 1;

                foreach (var header in exportConfigs.Select(config => config.Selection.transactionDataParameter))
                {
                    // Get the property value using reflection
                    var property = typeof(DemandTerminal).GetProperty(header);
                    var value = property.GetValue(rowData);

                    // Convert the value to the appropriate type (e.g., string)
                    var formattedValue = Convert.ToString(value);

                    // Set the cell value
                    worksheet.Cell(headerStartRow + row, col).Value = formattedValue;

                    col++;
                }
            }

            // Save the workbook to a stream
            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            // Dispose of the workbook
            workbook.Dispose();

            // Return the Excel file
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"{request.TemplateName}_ExportedData.xlsx";
            stream.Position = 0;

            return new Microsoft.AspNetCore.Mvc.FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileName
            };


        }




        public  async Task< IActionResult> GenerateCsv(ExportConfigPageTable request)
        {
            // Fetch data from DemandTerminal table
            List<DemandTerminal> demandTerminalData = await _context.DemandTerminal.ToListAsync();

            // Create CSV content using StringBuilder
            StringBuilder csv = new StringBuilder();

            // Fetch export configuration including parameter selection for the specified template
            var exportConfigs = _context.ExportConfigPageTables
                .Join(
                    _context.ExportParameterSelectionPages,
                    config => config.ConfigId,
                    selection => selection.ConfigId,
                    (config, selection) => new { Config = config, Selection = selection }
                )
                .Where(joinResult => joinResult.Config.TemplateName == request.TemplateName)
                .ToList();

            // Fetch the first configuration from the list
            var exportConfig = exportConfigs.First();

            // Determine the number of blank lines to skip
            int blankLinesFromTop = exportConfig.Config.blankLines ?? 0;
            int dateSequence = exportConfig.Config.dateSequence ?? 0;
            int TransactionSequenceNoInput = exportConfig.Config.noOfTransactionSequence ?? 0;
            int TotalTransactionValueSequence = exportConfig.Config.totalTransactionValueSequence ?? 0;

            List<string> exportHeaders = new List<string>();

            for (int i = 0; i < blankLinesFromTop; i++)
            {
                csv.AppendLine();
            }

            // Fetch special headers order from ExportConfigPage_Table
            var specialConfig = _context.ExportConfigPageTables
                .Where(config => config.TemplateName == request.TemplateName)
                .FirstOrDefault();

            List<string> specialHeadersOrder = new List<string>();
            List<string> specialValues = new List<string>();

            if (specialConfig != null)
            {
                if (specialConfig.isIncludedDate == true)
                {
                    int dateColumn = dateSequence > 0 ? dateSequence : 1;
                    specialHeadersOrder.Add("Date");
                    specialValues.Add(DateTime.Now.ToString("dd-MM-yyyy"));

                }

                if (specialConfig.isIncludedNoOfTransactions == true)
                {
                    specialHeadersOrder.Add("No. of Transaction");
                    specialValues.Add(_context.DemandTerminal.Count(dt => dt.ApplicationId != null).ToString());

                }

                if (specialConfig.isIncludedTotalTransactionValue == true)
                {
                    specialHeadersOrder.Add("Total value of transaction");
                    specialValues.Add(_context.DemandTerminal.Sum(dt => dt.RepaymentAmount).ToString());


                }
            }

            var specialHeadersWithSequence = new List<(string Header, int Sequence, string Value)>();

            if (dateSequence > 0)
                specialHeadersWithSequence.Add(("Date", dateSequence, DateTime.Now.ToString("dd-MM-yyyy")));

            if (TransactionSequenceNoInput > 0)
                specialHeadersWithSequence.Add(("No. of Transaction", TransactionSequenceNoInput, _context.DemandTerminal.Count(dt => dt.ApplicationId != null).ToString()));

            if (TotalTransactionValueSequence > 0)
                specialHeadersWithSequence.Add(("Total value of transaction", TotalTransactionValueSequence, _context.DemandTerminal.Sum(dt => dt.RepaymentAmount).ToString()));

            // Order headers by sequence
            var orderedSpecialHeadersWithSequence = specialHeadersWithSequence.OrderBy(item => item.Sequence).ToList();

            // Extract headers and values based on order
            var orderedSpecialHeaders = orderedSpecialHeadersWithSequence.Select(item => item.Header).ToList();
            var orderedSpecialValues = orderedSpecialHeadersWithSequence.Select(item => item.Value).ToList();

            string delimiter = specialConfig.Delimiter;
            csv.AppendLine(string.Join(delimiter, orderedSpecialHeaders));
            csv.AppendLine(string.Join(delimiter, orderedSpecialValues));

            // Add data headers based on ExportParameterSelectionPage
            exportHeaders = exportConfigs
                .OrderBy(joinResult => joinResult.Selection.sequenceExportType)
                .Select(joinResult => joinResult.Selection.transactionDataParameter)
                .ToList();

            csv.AppendLine(string.Join(delimiter, exportHeaders));

            // Add data rows to the CSV
            foreach (var row in demandTerminalData)
            {
                
                // Order the properties based on the sequenceExportType
                var orderedProperties = exportConfigs
                    .OrderBy(joinResult => joinResult.Selection.sequenceExportType)
                    .Select(joinResult => typeof(DemandTerminal).GetProperty(joinResult.Selection.transactionDataParameter))
                    .ToList();
                var rowData = orderedProperties.Select(property =>
                {
                    var value = property?.GetValue(row);

                    System.Console.WriteLine("===value====>" + JsonConvert.SerializeObject(value) );
                     
                    return $"{value}";

                }).ToList();


                csv.AppendLine(string.Join(delimiter, rowData));
            }
            // Convert the StringBuilder to a MemoryStream
            var byteArray = Encoding.UTF8.GetBytes(csv.ToString());
            var stream = new MemoryStream(byteArray);

            // Return the CSV file
            var contentType = "text/csv";
            var fileName = $"{request.TemplateName}_ExportedData.csv";

            return new Microsoft.AspNetCore.Mvc.FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileName
            };
        }


        public async Task<IActionResult> GenerateFile(ExportConfigPageTable request)
        {
            var exportConfig = await _context.ExportConfigPageTables
                .FirstOrDefaultAsync(e => e.TemplateName == request.TemplateName);

            if (exportConfig == null)
            {
                return new BadRequestObjectResult("Export configuration not found");
            }

            if (exportConfig.ExportFormat == 2)
            {
                // Assuming GenerateExcel is an asynchronous method
                return await  GenerateExcel(request);
            }
            else if (exportConfig.ExportFormat == 1)
            {
                // Assuming GenerateCsv is an asynchronous method
                return await  GenerateCsv(request);
            }
            else
            {
                return new OkObjectResult("Unsupported export format");
            }
        }

    }
}