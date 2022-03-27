﻿using GreenDonut;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.DataBaseContexts;
using Demo.Models.PersonModels.DalPersonsDto;

namespace Demo.Data
{
    public class PersonDataLoader : BatchDataLoader<int, PersonDalDto>
    {
        private readonly IDbContextFactory<PersonContext> _contextFactory;

        public PersonDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<PersonContext> contextFactory)
            : base(batchScheduler)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, PersonDalDto>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
                return await _contextFactory.CreateDbContext().Persons!
                    .Where(t => keys.Contains(t.Id))
                    .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}