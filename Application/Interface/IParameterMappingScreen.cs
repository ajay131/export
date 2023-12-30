using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;

namespace Application.Interface
{
    public interface IParameterMappingScreen
    {
         Task<string> PostCombinedExportData(ExportDataDto exportData);
          Task<string[]> GetDemandTerminalHeaders();
    }
}