using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Fotick.Api.DAL.Entities;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Fotick.Api.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        private const string _tableName = "dbo.users";
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Task<int> Add(User entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"INSERT INTO {_tableName} (id,login,user_name,added_date) VALUES(@Id,@Login,@Name,@Date)",
                        new
                        {
                            Id = entity.Id,
                            Login = entity.Login,
                            Name = entity.UserName,
                            Date = entity.AddedDate
                        });
            }
        }

        public Task<int> Delete(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"DELET FROM {_tableName}  WHERE id = @Id",
                            new
                            {
                                Id = id
                            });
            }
        }

        public Task<User> FindById(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM {_tableName} WHERE id = @Id",new { 
                    Id = id
                });
            }
        }

        public Task<User> FirstOrDefault()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM {_tableName} ORDER BY added_date LIMIT 1");
            }
        }

        public Task<IEnumerable<User>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<User>($"SELECT * FROM {_tableName} ORDER BY added_date ");
            }
        }

        public Task<int> Update(User entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"UPDATE {_tableName} SET name = @Name, login = @Login WHERE id = @Id",
                        new
                        {
                            Name = entity.UserName,
                            Login = entity.Login,
                            Id = entity.Id
                        });
            }
        }

        public Task<IEnumerable<Image>> GetUserImages(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Image>($"SELECT * FROM dbo.Images WHERE user_id = @Id ORDER BY aesthetics_persent", new
                {
                    Id = id
                });
            }
        }
    }

    public interface IUserRepository:IGenericRepository<User>
    {
        Task<IEnumerable<Image>> GetUserImages(Guid id);
    }
}
