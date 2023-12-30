using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entites.Model;
using Microsoft.AspNetCore.Mvc;


namespace Application.Interface
{
  public interface IExportScreen
  {
    Task<IEnumerable<string>> GetTemplateNamesAsync();
    //Task<IActionResult> GenerateExcel([FromBody] ExportConfigPageTable request);

    //Task<IActionResult> GenerateCsv(ExportConfigPageTable request);

    Task<IActionResult> GenerateFile(ExportConfigPageTable request);
  }
}