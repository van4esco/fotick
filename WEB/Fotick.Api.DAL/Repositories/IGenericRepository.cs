using Dapper;
using Fotick.Api.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Fotick.Api.DAL.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        IEnumerable<T> GetAll();
        T FirstOrDefault();
        T FindById(Guid id);
        int Add(T entity);
        int Delete(Guid id);
        int Update(T entity);
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

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>($"SELECT * FROM {TableName} ORDER BY added_date ");
            }
        }

        public  T FirstOrDefault()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return  dbConnection.QueryFirstOrDefault<T>($"SELECT * FROM {TableName} ORDER BY added_date LIMIT 1");
            }
        }

        public T FindById(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<T>($"SELECT * FROM {TableName} WHERE id = @Id", new
                {
                    Id = id
                });
            }
        }

        public abstract int Add(T entity);

        public int Delete(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"DELET FROM {TableName}  WHERE id = @Id",
                            new
                            {
                                Id = id
                            });
            }
        }

        public abstract int Update(T entity);
    }
}
