using Fotick.Api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotick.Api.BLL.Contracts
{
    public interface ITagsManager
    {
        Task<IEnumerable<Tag>> GetTags(string url);
    }
}
