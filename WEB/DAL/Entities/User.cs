
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
