﻿using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class BaseRepository<TKey, T> : IRepository<TKey, T> where T: class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context) 
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add<T>(entity);
            
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
           return _context.Set<T>().Any(expression);
        }

        public T Get(TKey key) => _context.Find<T>(key);

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
