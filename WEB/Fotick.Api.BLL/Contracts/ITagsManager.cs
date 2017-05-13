using System;
using System.Collections.Generic;
using System.Text;

namespace Fotick.Api.BLL.Contracts
{
    public interface ITagsManager
    {
        Task<IEnumerable<Tag>> GetTags(string url);
    }
}
