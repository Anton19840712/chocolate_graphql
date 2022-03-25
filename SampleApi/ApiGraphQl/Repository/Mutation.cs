﻿using ApiGraphQl.Context;
using ApiGraphQl.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Threading.Tasks;

namespace ApiGraphQl.Repository
{
    public class Mutation
    {
        [UseDbContext(typeof(PersonContext))]
        public async Task<AddPersonPayload> AddAuthorAsync(
            AddPersonInput input,
            [ScopedService] PersonContext dbContext)
            {
            var person = new Person
            {
                Name = input.Name,

                Score = input.Score
            };

            dbContext.Persons?.Add(person);

            await dbContext.SaveChangesAsync();

            return new AddPersonPayload(person);
        }
    }
}