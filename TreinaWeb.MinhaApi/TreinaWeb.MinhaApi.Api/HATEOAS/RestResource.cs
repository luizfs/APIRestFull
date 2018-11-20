using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreinaWeb.MinhaApi.Api.HATEOAS
{
    public class RestResource
    {
        public List<RestLink> Links { get; set; } = new List<RestLink>();
    }
}