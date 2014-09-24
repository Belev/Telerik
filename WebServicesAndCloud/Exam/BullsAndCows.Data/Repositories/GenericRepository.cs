﻿namespace Exam.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IBullsAndCowsContext context;
        private IDbSet<T> set;

        public GenericRepository()
            :this(new BullsAndCowsContext())
        {

        }

        public GenericRepository(IBullsAndCowsContext context)
        {
            this.context = context;
            this.set = this.context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}