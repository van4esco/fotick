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

        public override int Add(Tag entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"INSERT INTO {TableName} (id,text,added_date) VALUES(@Id,@Text,@Date)",
                        new
                        {
                            Id = entity.Id,
                            Text = entity.Text,
                            Date = entity.AddedDate
                        });
            }
        }

        public Tag GetByText(string text)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Tag>($"SELECT * FROM {TableName} WHERE text = @Text", new
                {
                    Text = text
                });
            }
        }

        public override int Update(Tag entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"UPDATE {TableName} SET text = @Text WHERE id = @Id",
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
        Tag GetByText(string text);
    }
}
