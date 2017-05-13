using Fotick.Api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Fotick.Api.DAL.Repositories
{
    public class TagRepository : GenerciRpository<Tag>, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override string TableName => "dbo.Tags";

        public override Task<int> Add(Tag entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"INSERT INTO {TableName} (id,text,added_date) VALUES(@Id,@Text,@Date)",
                        new
                        {
                            Id = entity.Id,
                            Text = entity.Text,
                            Date = entity.AddedDate
                        });
            }
        }

        public Task<Tag> GetByText(string text)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefaultAsync<Tag>($"SELECT * FROM {TableName} WHERE text = @Text", new
                {
                    Id = text
                });
            }
        }

        public override Task<int> Update(Tag entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync($"UPDATE {TableName} SET text = @Text WHERE id = @Id",
                        new
                        {
                            Url = entity.Text,
                            Id = entity.Id
                        });
            }
        }
    }

    public interface ITagRepository:IGenericRepository<Tag>
    {
        Task<Tag> GetByText(string text);
    }
}
