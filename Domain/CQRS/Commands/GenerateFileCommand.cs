using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entites.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domain.CQRS.Commands
{
    public class GenerateFileCommand : IRequest<IActionResult>
    {
        public ExportConfigPageTable Request { get; }

        public GenerateFileCommand(ExportConfigPageTable request)
        {
            Request = request;
        }
    }
}