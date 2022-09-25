﻿using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Infrastructure.Database
{
    public interface ISession : IDisposable
    {
        T? Get<T>(Guid id) where T : Entity;
        IEnumerable<T> Get<T>() where T : Entity;
        Task AddAsync<T>(T entity) where T : Entity;
        Task AddAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task AddOrUpdateAsync<T>(T entity) where T : Entity;
        Task UpdateAsync<T>(T entity) where T : Entity;
        Task DeleteAsync<T>(T entity) where T : Entity;
        void SaveChanges();
    }
}
