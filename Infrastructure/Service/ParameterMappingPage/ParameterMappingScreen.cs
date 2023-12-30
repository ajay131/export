using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Data.DbContexts;
using Domain.DTO;
using Domain.Entites.Model;

namespace Infrastructure.Service.ParameterMappingPage
{
    public class ParameterMappingScreen : IParameterMappingScreen
    {
        private readonly ExportDbContext _context;
        public ParameterMappingScreen(ExportDbContext context)
        {
            _context = context;
        }


        public async Task<string[]> GetDemandTerminalHeaders()
        {

            PropertyInfo[] properties = typeof(DemandTerminal).GetProperties();
            string[] fields = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                fields[i] = properties[i].Name;
            }

            return  fields;
        }




        public async Task<string> PostCombinedExportData(ExportDataDto exportData)
        {
            var configData = exportData.ConfigData;
            configData.IsActive = true;
            configData.IsDeleted = false;
            configData.CreatedBy = Guid.NewGuid();
            configData.CreatedDate = DateTime.UtcNow;
            configData.UpdatedBy = Guid.NewGuid();
            configData.UpdatedDate = DateTime.UtcNow;
            configData.AuthorizedBy = Guid.NewGuid();
            configData.AuthorizedDate = DateTime.UtcNow;
            configData.Status = 1;
            configData.Sequence = 1;
            configData.Version = 1;
            configData.Description = "Example description";

            // Add ExportConfigPageTable data
            _context.ExportConfigPageTables.Add(exportData.ConfigData);

            
            // Save changes to get the ConfigId
            await _context.SaveChangesAsync();

            // The ConfigId should be updated in exportData.ConfigData after saving changes
            int configId = exportData.ConfigData.ConfigId;

            // Assign the ConfigId to the ParameterData
            exportData.ParameterData.ForEach(p => p.ConfigId = configId);

            // Add ExportParameterSelectionPage data
            _context.ExportParameterSelectionPages.AddRange(exportData.ParameterData);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return "Data added successfully";
        }
    }
}