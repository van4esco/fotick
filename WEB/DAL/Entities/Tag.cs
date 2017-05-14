
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Tag:BaseEntity
    {
        public string Text { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
