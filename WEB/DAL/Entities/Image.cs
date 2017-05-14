using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Image:BaseEntity
    {
        public string Url { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string AestheticsStatus { get; set; }
        public string AestheticsPersent { get; set; }
        public bool IsForSale { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
