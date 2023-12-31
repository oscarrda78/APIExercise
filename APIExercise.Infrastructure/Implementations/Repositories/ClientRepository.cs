﻿using APIExercise.Core.Entities;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APIExercise.Infrastructure.Implementations.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }
    }
}