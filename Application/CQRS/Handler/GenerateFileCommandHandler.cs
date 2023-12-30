using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Commands;
using Domain.Data.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.CQRS.Handler
{
    public class GenerateFileCommandHandler : IRequestHandler<GenerateFileCommand, IActionResult>
    {
        private readonly ExportDbContext _context;
        private readonly IExportScreen _exportScreen; // Interface for your export services

        public GenerateFileCommandHandler(ExportDbContext context, IExportScreen exportScreen)
        {
            _context = context;
            _exportScreen = exportScreen;
        }

        public async Task<IActionResult> Handle(GenerateFileCommand command, CancellationToken cancellationToken)
        {
            return  await _exportScreen.GenerateFile(command.Request);
        }
    }
}