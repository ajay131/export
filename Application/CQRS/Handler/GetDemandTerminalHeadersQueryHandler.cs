using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Domain.CQRS.Queries;
using MediatR;

namespace Application.CQRS.Handler
{
    public class GetDemandTerminalHeadersQueryHandler : IRequestHandler<GetDemandTerminalHeadersQuery, string[]>
    {
        private readonly IParameterMappingScreen _ParameterMappingScreenservices;

        public GetDemandTerminalHeadersQueryHandler(IParameterMappingScreen ParameterMappingScreenservices)
        {
            _ParameterMappingScreenservices = ParameterMappingScreenservices;
        }

        public Task<string[]> Handle(GetDemandTerminalHeadersQuery request, CancellationToken cancellationToken)
        {
            return  _ParameterMappingScreenservices.GetDemandTerminalHeaders(); 
        }


    }
}