﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using TreinaWeb.MinhaApi.Api.HATEOAS.Helpers;

namespace TreinaWeb.MinhaApi.Api.Filters
{
    public class FillResponseWithHATEOSAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //application/hal+json
            if (actionExecutedContext.Response.IsSuccessStatusCode && 
                actionExecutedContext.Request.Headers.SelectMany(s=> s.Value).Any(a=> a.Contains("hal")))
            {
                ObjectContent responseContent = actionExecutedContext.Response.Content as ObjectContent;
                object respondeValue = responseContent.Value;
                RestResourceBuilder.BuildResource(respondeValue, actionExecutedContext.Request);
            }
        }
    }
}