using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Domain.CQRS.Queries
{
    public class GetTemplateNamesQuery : IRequest<IEnumerable<string>>
    {
        
    }
}