using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using MediatR;

namespace Domain.CQRS.Commands
{
    public class PostCombinedExportDataCommand : IRequest<string>
    {
        public ExportDataDto ExportData { get; }

        public PostCombinedExportDataCommand(ExportDataDto exportData)
        {
            ExportData = exportData;
        }
    }
}