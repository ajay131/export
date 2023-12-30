using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Commands;
using Domain.Data.DbContexts;
using Domain.DTO;
using MediatR;

namespace Application.CQRS.Handler
{
    public class PostCombinedExportDataCommandHandler : IRequestHandler<PostCombinedExportDataCommand, string>
    {
        private readonly ExportDbContext _context;
        private readonly IParameterMappingScreen _ParameterMappingScreenservices;

        public PostCombinedExportDataCommandHandler(ExportDbContext context, IParameterMappingScreen ParameterMappingScreenservices)
        {
            _context = context;
            _ParameterMappingScreenservices = ParameterMappingScreenservices;
        }

        public async Task<string> Handle(PostCombinedExportDataCommand request, CancellationToken cancellationToken)
        {
            return await _ParameterMappingScreenservices.PostCombinedExportData(request.ExportData);
        }
    }
}