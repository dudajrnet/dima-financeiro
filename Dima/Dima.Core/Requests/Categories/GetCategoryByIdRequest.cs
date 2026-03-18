using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories;

public class GetCategoryByIdRequest : BaseRequest
{
    public long? Id { get; set; }
}

