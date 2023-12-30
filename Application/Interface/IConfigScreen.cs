using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entites.Model;

namespace Application.Interface
{
    public interface IConfigScreen
    {
        Task<List<Product_Type>> GetProductTypesList();
        Task<List<Export_Type>> GetExportTypesList();
        Task<List<Export_Format>> GetExportFormatAsync();
        Task<List<Filter_Records_by_Processor>> GetFilterRecord();
        Task<List<Email_Template>> GetEmailTemplate();
        Task<Dictionary<string, List<object>>> GetAllDropdownData();
    }
}