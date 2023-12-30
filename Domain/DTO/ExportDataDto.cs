using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entites.Model;

namespace Domain.DTO
{
    public class ExportDataDto
    {
        public ExportConfigPageTable? ConfigData { get; set; }
        public List<ExportParameterSelectionPage>? ParameterData { get; set; }
    }
}