using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Queries;
using Domain.Data.DbContexts;
using MediatR;

namespace Application.CQRS.Handler
{
    public class GetTemplateNamesQueryHandler : IRequestHandler<GetTemplateNamesQuery, IEnumerable<string>>
    {
        private readonly ExportDbContext _context;
        private readonly IExportScreen _ExportScreenservices;


        public GetTemplateNamesQueryHandler(ExportDbContext context, IExportScreen ExportScreenservices)
        {
            _context = context;
            _ExportScreenservices = ExportScreenservices;

        }


        public  Task<IEnumerable<string>> Handle(GetTemplateNamesQuery request, CancellationToken cancellationToken)
        {
            return  _ExportScreenservices.GetTemplateNamesAsync();

        }

    }
}