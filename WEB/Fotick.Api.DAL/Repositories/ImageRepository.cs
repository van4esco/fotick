using Dapper;
using Fotick.Api.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
                return dbConnection.Execute($"INSERT INTO {TableName} (id,url,user_id,aesthetics_status,aesthetics_persent,added_date,is_for_sale) VALUES(@Id,@Url,@UserId,@AestheticsStatus,@AestheticsPersent,@Date,@IsForSale)",
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
                return dbConnection.Query<Tag>($"SELECT * FROM dbo.Tags as t JOIN dbo.ImageTags as i ON i.tag_id = t.id WHERE i.image_id = @id", new
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
                return dbConnection.Execute($"UPDATE {TableName} SET url = @Url,is_for_sale = @IsForSale, aesthetics_status = @AestheticsStatus,aesthetics_persent = @AestheticsPersent WHERE id = @Id",
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
                return  dbConnection.Execute($"INSERT INTO dbo.ImageTags (id,image_id,tag_id,added_date) VALUES(@Id,@ImageId,@TagId,@Date)",
                        new
                        {
                            Id = tag.Id,
                            Date = tag.AddedDate,
                            ImageId = id,
                            TagId = tag.Id
                        });
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
    }

    public interface IImageRepository:IGenericRepository<Image>
    {
        IEnumerable<Tag> GetImageTags(Guid id);
        int AddTag(Guid id, string text);
        Image FindByUrl(string url);
    }
}
