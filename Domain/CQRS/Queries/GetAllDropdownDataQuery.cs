using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Domain.CQRS.Queries
{
    public class GetAllDropdownDataQuery : IRequest<Dictionary<string, List<object>>>
    {
        
    }
}