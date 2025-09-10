﻿using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Querying;

namespace VerticalSliceBlazorArchitecture.DataAccess.Querying
{
    [UsedImplicitly]
    public class QueryService(
        IAppDbContextFactory appDbContextFactory)
        : IQueryService
    {
        public async Task<bool> AnyAsync<TResult>(IQuerySpecification<TResult> spec)
        {
            return await PrepareQuery(spec).AnyAsync();
        }

        public async Task<IReadOnlyCollection<TResult>> QueryAsync<TResult>(IQuerySpecification<TResult> spec)
        {
            var qry = PrepareQuery(spec);
            var sql = qry.ToQueryString();

            Debug.WriteLine(sql);

            return await qry.ToListAsync();
        }

        public async Task<TResult> QuerySingleAsync<TResult>(IQuerySpecification<TResult> spec)
        {
            return await PrepareQuery(spec).SingleAsync();
        }

        public async Task<TResult?> QuerySingleOrDefaultAsync<TResult>(IQuerySpecification<TResult> spec)
        {
            return await PrepareQuery(spec).SingleOrDefaultAsync();
        }

        private IQueryable<TResult> PrepareQuery<TResult>(IQuerySpecification<TResult> spec)
        {
            var appDbContext = appDbContextFactory.Create();
            var query = spec.Apply(appDbContext);

            return query;
        }
    }
}