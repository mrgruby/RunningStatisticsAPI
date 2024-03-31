using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRunningStatRepository : IBaseRepository<RunningStatConverted>
    {
        IEnumerable<RunningStatConverted> GetSpecific(Expression<Func<RunningStatConverted, bool>> predicate);
        Task<ServiceResponse<string>> ParseGarminJsonFile(string fileName);
    }
}
