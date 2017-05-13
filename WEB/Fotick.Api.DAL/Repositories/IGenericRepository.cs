using Dapper;
using Fotick.Api.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Fotick.Api.DAL.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> FirstOrDefault();
        Task<T> FindById(Guid id);
        Task<int> Add(T entity);
        Task<int> Delete(Guid id);
        Task<int> Update(T entity);
    }

    public abstract class GenerciRpository<T>:IGenericRepository<T> where T:BaseEntity
    {
        private string _connectionString;
        public abstract string TableName { get; }
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public GenerciRpository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<T>($"SELECT * FROM {TableName} ORDER BY added_date ");
            }
        }

        public Task<T> FirstOrDefault()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {TableName} ORDER BY added_date LIMIT 1");
            }
        }

        public Task<T> FindById(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE id = @Id", new
                {
                    Id = id
                });
            }
        }

        public abstract Task<int> Add(T entity);

        public Task<int> Delete(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"DELET FROM {TableName}  WHERE id = @Id",
                            new
                            {
                                Id = id
                            });
            }
        }

        public abstract Task<int> Update(T entity);
    }
}
