using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        protected DbSet<T> DBSet { get; set; }
        private DbContext Context { get; set; }

        public GenericRepository()
        {
            this.Context = new AssignmentContext();
            this.DBSet = this.Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.DBSet;
        }

        public T GetById(int Id)
        {
            return this.DBSet.Find(Id);
        }

        public T GetById(Guid Id)
        {
            return this.DBSet.Find(Id);
        }

        public IQueryable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DBSet.Where(predicate);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DBSet.Add(entity);
            }
            this.Context.SaveChanges();
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DBSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DBSet.Attach(entity);
                this.DBSet.Remove(entity);
            }
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
            this.Context.SaveChanges();
        }

        public void Detach(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
            this.Context.SaveChanges();
        }
    }
}
