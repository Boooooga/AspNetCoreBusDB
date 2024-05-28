using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Search;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Z.EntityFramework.Plus;

namespace Dal
{
	public abstract class BaseDal<TDbContext, TDbObject, TEntity, TObjectId, TSearchParams, TConvertParams>
		where TDbContext: DbContext, new()
		where TDbObject: class, new()
		where TEntity: class
		where TSearchParams: BaseSearchParams
		where TConvertParams: class
	{
		protected TDbContext _context;

		protected abstract bool RequiresUpdatesAfterObjectSaving { get; }

		protected BaseDal()
		{
		}

		protected internal BaseDal(TDbContext context)
		{
			_context = context;
		}

		public virtual Task<TObjectId> AddOrUpdateAsync(TEntity entity)
		{
			return AddOrUpdateAsync(entity, true);
		}

		internal virtual async Task<TObjectId> AddOrUpdateAsync(TEntity entity, bool forceSave)
		{
			if (_context == null)
				forceSave = true;
			var data = GetContext();
			try
			{
				var objects = data.Set<TDbObject>();
				var dbObject = await objects.FirstOrDefaultAsync(GetCheckDbObjectIdExpression(GetIdByEntity(entity)));
				var exists = dbObject != null;
				if (!exists)
				{
					dbObject = new TDbObject();
				}
				await UpdateBeforeSavingAsync(data, entity, dbObject, exists);
				if (!exists)
				{
					objects.Add(dbObject);
				}
				if (RequiresUpdatesAfterObjectSaving || forceSave)
				{
					await data.SaveChangesAsync();
				}
				if (RequiresUpdatesAfterObjectSaving)
				{
					await UpdateAfterSavingAsync(data, entity, dbObject, exists);
					if (forceSave)
					{
						await data.SaveChangesAsync();
					}
				}
				return GetIdByDbObject(dbObject);
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		public virtual Task<IList<TObjectId>> AddOrUpdateAsync(IList<TEntity> entities)
		{
			return AddOrUpdateAsync(entities, true);
		}

		internal virtual async Task<IList<TObjectId>> AddOrUpdateAsync(IList<TEntity> entities, bool forceSave)
		{
			if (_context == null)
				forceSave = true;
			var data = GetContext();
			try
			{
				var entitiesIdArray = entities.Select(GetIdByEntity).ToArray();
				var dbSet = data.Set<TDbObject>();
				var dbObjectsDictionary = await dbSet.Where(GetCheckDbObjectIdInArrayExpression(entitiesIdArray)).ToDictionaryAsync(GetIdByDbObjectExpression().Compile());
				var existingSet = new HashSet<TObjectId>();
				var dbObjects = new List<TDbObject>();
				var addedObjects = new List<TDbObject>();
				foreach (var entity in entities)
				{
					var id = GetIdByEntity(entity);
					var exists = dbObjectsDictionary.ContainsKey(id);
					var dbObject = exists ? dbObjectsDictionary[id] : new TDbObject();
					if (exists)
						existingSet.Add(id);
					await UpdateBeforeSavingAsync(data, entity, dbObject, exists);
					dbObjects.Add(dbObject);
					if (!exists)
					{
						addedObjects.Add(dbObject);
					}
				}
				dbSet.AddRange(addedObjects);
				if (RequiresUpdatesAfterObjectSaving || forceSave)
				{
					await data.SaveChangesAsync();
				}
				if (RequiresUpdatesAfterObjectSaving)
				{
					for (var i = 0; i < dbObjects.Count; i++)
					{
						var dbObject = dbObjects[i];
						var id = GetIdByDbObject(dbObject);
						await UpdateAfterSavingAsync(data, entities[i], dbObject, existingSet.Contains(id));
					}
				}
				if (forceSave)
				{
					await data.SaveChangesAsync();
				}
				return dbObjects.Select(GetIdByDbObject).ToList();
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		public virtual async Task<TEntity> GetAsync(TObjectId id, TConvertParams convertParams = null)
		{
			var data = GetContext();
			try
			{
				var dbObjects = data.Set<TDbObject>().Where(GetCheckDbObjectIdExpression(id)).Take(1);
				return (await BuildEntitiesListAsync(data, dbObjects, convertParams, true)).FirstOrDefault();
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		public virtual Task<bool> ExistsAsync(TObjectId id)
		{
			return ExistsAsync(GetCheckDbObjectIdExpression(id));
		}

		public virtual async Task<bool> ExistsAsync(TSearchParams searchParams)
		{
			var data = GetContext();
			try
			{
				var objects = data.Set<TDbObject>().AsNoTracking();
				return await (await BuildDbQueryAsync(data, objects, searchParams)).AnyAsync();
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		internal virtual async Task<bool> ExistsAsync(Expression<Func<TDbObject, bool>> predicate)
		{
			var data = GetContext();
			try
			{
				return await data.Set<TDbObject>().Where(predicate).AnyAsync();
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		public virtual Task<bool> DeleteAsync(TObjectId id)
		{
			return DeleteAsync(GetCheckDbObjectIdExpression(id));
		}

		internal virtual async Task<bool> DeleteAsync(Expression<Func<TDbObject, bool>> predicate)
		{
			var data = GetContext();
			try
			{
				return await data.Set<TDbObject>().Where(predicate).DeleteAsync() > 0;
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		public virtual async Task<SearchResult<TEntity>> GetAsync(TSearchParams searchParams, TConvertParams convertParams = null)
		{
			var data = GetContext();
			try
			{
				var objects = data.Set<TDbObject>().AsNoTracking();
				objects = await BuildDbQueryAsync(data, objects, searchParams);
				var visitor = new OrderedQueryableVisitor();
				visitor.Visit(objects.Expression);
				objects = visitor.IsOrdered
					? (objects as IOrderedQueryable<TDbObject>).ThenBy(GetIdByDbObjectExpression())
					: objects.OrderBy(GetIdByDbObjectExpression());
				var result = new SearchResult<TEntity>
				{
					Total = await objects.CountAsync(),
					RequestedObjectsCount = searchParams.ObjectsCount,
					RequestedStartIndex = searchParams.StartIndex,
					Objects = new List<TEntity>()
				};
				if (searchParams.ObjectsCount == 0)
				{
					return result;
				}
				objects = objects.Skip(searchParams.StartIndex);
				if (searchParams.ObjectsCount != null)
					objects = objects.Take(searchParams.ObjectsCount.Value);
				result.Objects = await BuildEntitiesListAsync(data, objects, convertParams, false);
				return result;
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		internal virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TDbObject, bool>> predicate, TConvertParams convertParams = null)
		{
			var data = GetContext();
			try
			{
				return await BuildEntitiesListAsync(data, data.Set<TDbObject>().Where(predicate), convertParams, false);
			}
			finally
			{
				await DisposeContextAsync(data);
			}
		}

		protected abstract Task UpdateBeforeSavingAsync(TDbContext context, TEntity entity, TDbObject dbObject, bool exists);

		protected virtual Task UpdateAfterSavingAsync(TDbContext context, TEntity entity, TDbObject dbObject, bool exists)
		{
			return Task.CompletedTask;
		}

		protected abstract Task<IQueryable<TDbObject>> BuildDbQueryAsync(TDbContext context, IQueryable<TDbObject> dbObjects, TSearchParams searchParams);

		protected abstract Task<IList<TEntity>> BuildEntitiesListAsync(TDbContext context, IQueryable<TDbObject> dbObjects, TConvertParams convertParams, bool isFull);

		protected abstract Expression<Func<TDbObject, TObjectId>> GetIdByDbObjectExpression();

		protected MemberInfo GetDbObjectIdMember()
		{
			return (GetIdByDbObjectExpression().Body as MemberExpression)?.Member;
		}

		protected TObjectId GetIdByDbObject(TDbObject dbObject)
		{
			return GetIdByDbObjectExpression().Compile()(dbObject);
		}

		protected Expression<Func<TDbObject, bool>> GetCheckDbObjectIdExpression(TObjectId objectId)
		{
			var p = Expression.Parameter(typeof(TDbObject));
			return Expression.Lambda<Func<TDbObject, bool>>(Expression.Equal(
				Expression.MakeMemberAccess(p, GetDbObjectIdMember()),
				Expression.Property(Expression.Constant(new { Id = objectId }), "Id")),
			p);
		}

		protected Expression<Func<TDbObject, bool>> GetCheckDbObjectIdInArrayExpression(TObjectId[] arr)
		{
			var p = Expression.Parameter(typeof(TDbObject));
			Expression<Func<TObjectId, bool>> expr = item => arr.Contains(item);
			var method = (expr.Body as MethodCallExpression)?.Method;
			return Expression.Lambda<Func<TDbObject, bool>>(Expression.Call(null, method,
				Expression.Property(Expression.Constant(new { Arr = arr }), "Arr"),
				Expression.MakeMemberAccess(p, GetDbObjectIdMember())), p);
		}

		protected abstract Expression<Func<TEntity, TObjectId>> GetIdByEntityExpression();

		protected TObjectId GetIdByEntity(TEntity entity)
		{
			return GetIdByEntityExpression().Compile()(entity);
		}

		protected TDbContext GetContext()
		{
			if (_context != null)
				return _context;
			var result = new TDbContext();
			return result;
		}

		protected async Task<bool> DisposeContextAsync(TDbContext context)
		{
			if (ReferenceEquals(context, _context)) return false;
			await context.DisposeAsync();
			return true;
		}
	}
}
