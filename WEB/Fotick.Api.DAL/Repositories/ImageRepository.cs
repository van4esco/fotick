using Dapper;
using Fotick.Api.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fotick.Api.DAL.Repositories
{
    public class ImageRepository : GenerciRpository<Image>, IImageRepository
    {

        public override string TableName => "dbo.Images";
        private readonly ITagRepository _tagRepository;



        public ImageRepository(IConfiguration configuration, ITagRepository tagRepository) :base(configuration)
        {
            _tagRepository = tagRepository;
        }

        public override int Add(Image entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"INSERT INTO {TableName} (id,url,userId,aestheticsStatus,aestheticsPersent,addedDate,isForSale) VALUES(@Id,@Url,@UserId,@AestheticsStatus,@AestheticsPersent,@Date,@IsForSale)",
                        new
                        {
                            Id = entity.Id,
                            Url = entity.Url,
                            AestheticsStatus = entity.AestheticsStatus,
                            AestheticsPersent = entity.AestheticsPersent,
                            Date = entity.AddedDate,
                            IsForSale = entity.IsForSale,
                            UserId = entity.UserId
                        });
            }
        }

        public IEnumerable<Tag> GetImageTags(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Tag>($"SELECT * FROM dbo.Tags as t JOIN dbo.ImageTags as i ON i.tags_id = t.id WHERE i.image_id = @id", new
                {
                    Id = id
                });
            }
        }


        public override int Update(Image entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute($"UPDATE {TableName} SET url = @Url,isForSale = @IsForSale, aestheticsStatus = @AestheticsStatus,aestheticsPersent = @AestheticsPersent WHERE id = @Id",
                        new
                        {
                            Url = entity.Url,
                            AestheticsStatus = entity.AestheticsStatus,
                            AestheticsPersent = entity.AestheticsPersent,
                            Id = entity.Id,
                            IsForSale = entity.IsForSale
                        });
            }
        }

        public  int AddTag(Guid id, string text)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var tag =  _tagRepository.GetByText(text); 
                if(tag==null){
                    tag = new Tag() { Text = text };
                     _tagRepository.Add(tag);
                }
                return  dbConnection.Execute($"INSERT INTO dbo.ImageTags (id,image_id,tags_id,added_date) VALUES(@Id,@ImageId,@TagId,@Date)",
                        new
                        {
                            Id = Guid.NewGuid(),
                            Date = tag.AddedDate,
                            ImageId = id,
                            TagId = tag.Id
                        });
            }
        }

        public int AddTags(Guid id,IEnumerable<Tag> tags){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var imgtags = tags.Select(p => new ImageTag { 
                    ImageId = id,
                    TagId = p.Id
                });
                return dbConnection.Execute($"INSERT INTO dbo.ImageTags (id,image_id,tags_id,added_date) VALUES(@Id,@ImageId,@TagId,@AddedDAte)", imgtags);
            }
        }

        public Image FindByUrl(string url)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Image>($"SELECT * FROM {TableName} WHERE url = @Url", new
                {
                    Url = url
                });
            }
        }

        public IEnumerable<Image> SearchByTag(string tag)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Image>($"" +
                $"SELECT * FROM dbo.Images as i " +
                    $"JOIN dbo.ImageTags as it ON it.image_id = i.id " +
                    $"JOIN dbo.Tags as t ON t.id = it.tags_id"+
                    "WHERE t.text LIKE @Tag OR @Tag LIKE t.text", new
                {
                    Tag = tag
                });
            }
        }
    }

    public interface IImageRepository:IGenericRepository<Image>
    {
        IEnumerable<Tag> GetImageTags(Guid id);
        int AddTag(Guid id, string text);
        Image FindByUrl(string url);
        int AddTags(Guid id, IEnumerable<Tag> tags);
        IEnumerable<Image> SearchByTag(string tag);
    }
}
