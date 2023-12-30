using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Queries;
using MediatR;

namespace Application.CQRS.Handler
{
    public class GetAllDropdownDataQueryHandler : IRequestHandler<GetAllDropdownDataQuery, Dictionary<string, List<object>>>
    {
        private readonly IConfigScreen _ConfigScreenservices;
        
        public GetAllDropdownDataQueryHandler(IConfigScreen ConfigScreenservices)
        {
            _ConfigScreenservices = ConfigScreenservices;
        }
        
        public async Task<Dictionary<string, List<object>>> Handle(GetAllDropdownDataQuery request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Insidie handler");
            return await _ConfigScreenservices.GetAllDropdownData();
        }
    }
}