using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace UI.Extensions.Middleware
{
	public static class StatusCodePagesExtensions
	{
        public static IApplicationBuilder UseStatusCodePagesWithReExecute(
		    this IApplicationBuilder app,
		    Func<StatusCodeContext, string> generatePath,
		    Func<StatusCodeContext, string> generateQuery = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseStatusCodePages(async context =>
            {
	            var newPath = generatePath(context);
                var newQueryString = generateQuery == null ? QueryString.Empty : new QueryString(generateQuery(context));

                var originalPath = context.HttpContext.Request.Path;
                var originalQueryString = context.HttpContext.Request.QueryString;
                // Store the original paths so the app can check it.
                context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
                {
	                OriginalPathBase = context.HttpContext.Request.PathBase.Value,
	                OriginalPath = originalPath.Value,
	                OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : null,
                });

                // An endpoint may have already been set. Since we're going to re-invoke the middleware pipeline we need to reset
                // the endpoint and route values to ensure things are re-calculated.
                context.HttpContext.SetEndpoint(endpoint: null);
                var routeValuesFeature = context.HttpContext.Features.Get<IRouteValuesFeature>();
                routeValuesFeature?.RouteValues?.Clear();

                context.HttpContext.Request.Path = newPath;
                context.HttpContext.Request.QueryString = newQueryString;
                try
                {
	                await context.Next(context.HttpContext);
                }
                finally
                {
	                context.HttpContext.Request.QueryString = originalQueryString;
	                context.HttpContext.Request.Path = originalPath;
	                context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);
                }
            });
        }
    }
}
