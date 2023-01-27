using ApiSDemo.Data;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiSDemo.Repositories.Repos
{
	public class GenericRepo<T> : IGenericRepo<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepo(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public bool Create(T entity)
		{
			var  t =  _dbSet.Add(entity);
			return t != null;
		}

		public bool Delete(T entity)
		{
			var res = _dbSet.Remove(entity);
			return res != null;
		}

		public IEnumerable<T> GetAll()
		{
			return _dbSet;
		}

		public T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
	}
}
