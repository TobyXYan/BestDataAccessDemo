﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositoryLib.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Modify(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
