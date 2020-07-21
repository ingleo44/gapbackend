using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllRecords();
        Task<T> GetAsync(int id);
        T Get(int id);
        IQueryable<T> Query();
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(int[] ids);
        Task<IEnumerable<IDictionary<string, object>>> ExecuteQuery(string query, object[] parameters);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {

        protected MedAppointmentsContext DbContext { get; set; }

        protected GenericRepository(MedAppointmentsContext dbContext)
        {
            DbContext = dbContext;
        }

        protected GenericRepository()
        {
        }

        public Task<List<T>> GetAllRecords()
        {
            return DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }


        public T Get(int id)
        {
            return DbContext.Find<T>(id);
        }


        public IQueryable<T> Query()
        {
            return DbContext.Set<T>();
        }

        public async Task<T> InsertAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return entity;
        }


        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(new[] {id});
        }

        public async Task DeleteAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var entity = DbContext.FindAsync<T>(id);
                DbContext.Remove(entity);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IDictionary<string, object>>> ExecuteQuery(string query, object[] parameters)
        {
            using (var command = DbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                if (parameters != null)
                    foreach (var prm in parameters)
                        command.Parameters.Add(prm);
                DbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList<string>();
                    var result = new List<IDictionary<string, object>>();
                    foreach (IDataRecord record in reader as IEnumerable)
                    {
                        IDictionary<string, object> expando;
                        expando = new ExpandoObject() as IDictionary<string, object>;
                        foreach (var name in names)
                            expando[name] = record[name];
                        result.Add(expando);
                    }

                    return result;
                }
            }
        }



    }


}