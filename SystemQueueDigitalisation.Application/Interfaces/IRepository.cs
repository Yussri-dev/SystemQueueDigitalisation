﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task UpdateRangeAsync(List<T> entities);

    }
}
