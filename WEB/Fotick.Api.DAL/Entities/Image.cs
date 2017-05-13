using System;

namespace Fotick.Api.DAL.Entities
{
    public class Image:BaseEntity
    {
        public string Url { get; set; }
        public Guid UserId { get; set; }
        public string AestheticsStatus { get; set; }
        public string AestheticsPersent { get; set; }
    }
}
