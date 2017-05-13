using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Fotick.Api.DAL.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;
namespace Fotick.Api.DAL.Repositories
{
    public class UserRepository :GenerciRpository<User>, IUserRepository
    {
        private const string _tableName = "dbo.users";

        public override string TableName { get => "dbo.Users"; }

        public UserRepository(IConfiguration configuration):base(configuration)
        {
        }
        public override Task<int> Add(User entity)
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

        public override Task<int> Update(User entity)
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
