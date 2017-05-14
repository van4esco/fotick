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
        public override int Add(User entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"INSERT INTO {_tableName} (id,login,userName,addedDate) VALUES(@Id,@Login,@Name,@Date)",
                        new
                        {
                            Id = entity.Id,
                            Login = entity.Login,
                            Name = entity.UserName,
                            Date = entity.AddedDate
                        });
            }
        }

        public override int Update(User entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"UPDATE {_tableName} SET name = @Name, login = @Login WHERE id = @Id",
                        new
                        {
                            Name = entity.UserName,
                            Login = entity.Login,
                            Id = entity.Id
                        });
            }
        }

        public IEnumerable<Image> GetUserImages(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Image>($"SELECT * FROM dbo.Images WHERE userId = @Id ORDER BY aestheticsPersent", new
                {
                    Id = id
                });
            }
        }

        public User FindByUserName(string userName)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<User>($"SELECT * FROM {TableName} WHERE userName = @UserName", new
                {
                    UserName = userName
                });
            }
        }
    }

    public interface IUserRepository:IGenericRepository<User>
    {
        IEnumerable<Image> GetUserImages(Guid id);
        User FindByUserName(string userName);
    }
}
