using System;

namespace Fotick.Api.DAL.Entities
{
    public class ImageTag:BaseEntity
    {
        public Guid ImageId { get; set; }
        public Guid TagId { get; set; }
    }
}
