using Infrastructure.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Utilities
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    }
}
