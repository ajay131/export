using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Commands;
using Domain.CQRS.Queries;
using Domain.Data.DbContexts;
using Domain.DTO;
using Domain.Entites.Model;
using Infrastructure.Service;
using Infrastructure.Service.ConfigurationPage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly ExportDbContext _context;
        private readonly IConfigScreen _ConfigScreenservices;
        private readonly IParameterMappingScreen _ParameterMappingScreenservices;
        private readonly IExportScreen _ExportScreenservices;

        private readonly IMediator _mediator;


        public ExportController(ExportDbContext context, IConfigScreen ConfigScreenservices, IParameterMappingScreen ParameterMappingScreenservices, IExportScreen ExportScreenservices, IMediator mediator)
        {
            _ConfigScreenservices = ConfigScreenservices;
            _context = context;
            _ParameterMappingScreenservices = ParameterMappingScreenservices;
            _ExportScreenservices = ExportScreenservices;
            _mediator = mediator;


        }

        // [HttpGet("ProductListDropdown")]
        // public async Task<ActionResult<List<string>>> GetProductTypesList()
        // {
        //     var productTypes = await _ConfigScreenservices.GetProductTypesList();
        //     return Ok(productTypes);
        // }

        // [HttpGet("ExportTypeDropdown")]
        // public async Task<ActionResult<List<string>>> GetExportTypes()
        // {
        //     var exportTypes = await _ConfigScreenservices.GetExportTypesList();
        //     return Ok(exportTypes);
        // }

        // [HttpGet("ExportFormatDropdown")]
        // public async Task<ActionResult<List<string>>> GetExportFormat()
        // {
        //     var exportFormat = await _ConfigScreenservices.GetExportFormatAsync();
        //     return Ok(exportFormat);
        // }


        // [HttpGet("EmailTemplateDropdown")]
        // public async Task<ActionResult<List<string>>> GetEmailTemplate()
        // {
        //     var emailTemplate = await _ConfigScreenservices.GetEmailTemplate();
        //     return Ok(emailTemplate);
        // }

        // [HttpGet("FilterRecordDropdown")]
        // public async Task<ActionResult<List<string>>> GetFilterRecord()
        // {
        //     var filterRecord = await _ConfigScreenservices.GetFilterRecord();
        //     return Ok(filterRecord);
        // }

        [HttpGet("demand-terminal-headers")]
        public async Task<IActionResult> GetDemandTerminalHeaders()
        {
            string[] headers = await _mediator.Send(new GetDemandTerminalHeadersQuery());
            return Ok(headers);
        }

        [HttpPost("combined-export-data")]
        public async Task<ActionResult<string>> PostCombinedExportData(ExportDataDto exportData)
        {
            var result = await _mediator.Send(new PostCombinedExportDataCommand(exportData));
            return Ok(result);
        }

        [HttpGet("templateNames")]
        public async Task<ActionResult<IEnumerable<string>>> GetActionResultAsync()
        {
            var templateNames = await _mediator.Send(new GetTemplateNamesQuery());
            return Ok(templateNames);



        }

        // [HttpGet("ProductTypes")]
        // public ActionResult<IEnumerable<string>> GetProductTypes()
        // {
        //     return  _ExportScreenservices.GetProductTypes();
        // }

        [HttpPost("generate-file")]
        public async Task<IActionResult> GenerateFile(ExportConfigPageTable request)
        {
            // Post method now sends the command to MediatR which handles the execution
            return await _mediator.Send(new GenerateFileCommand(request));
        }

        [HttpGet("All-Dropdown")]
        public async Task<ActionResult<Dictionary<string, List<object>>>> GetAllDropdownData()
        {
            // var query = new GetAllDropdownDataQuery();
            // var result = await _mediator.Send(query);
            // return Ok(result);

            return await _mediator.Send(new GetAllDropdownDataQuery());
        }
    }
}