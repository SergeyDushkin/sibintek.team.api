using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sibintek.sibmobile.core;

namespace sibintek.team.api
{
    public class PaginationResultFilter : IResultFilter
    {
        public PaginationResultFilter()
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = (Microsoft.AspNetCore.Mvc.ObjectResult)context.Result;

            if (result.DeclaredType == null)
                return;
                
            var interfaces = result.DeclaredType.GetInterfaces();

            if (!interfaces.Any(r => r.Name.IndexOf("IQueryable") > -1))
                return;

            var value = result.Value;

            var genericTypes = result.DeclaredType.GenericTypeArguments;
            
            var method = typeof(PaginationExtension).GetMethod(nameof(PaginationExtension.AsPagination));
            var generic = method.MakeGenericMethod(genericTypes);

            var pageFromRequest = context.HttpContext.Request.Headers["page"].FirstOrDefault();
            var limitFromRequest = context.HttpContext.Request.Headers["limit"].FirstOrDefault();

            int page = 1;
            int limit = 100;

            if (!string.IsNullOrEmpty(pageFromRequest))
            {
                int.TryParse(pageFromRequest, out page);
            }

            if (!string.IsNullOrEmpty(limitFromRequest))
            {
                int.TryParse(limitFromRequest, out limit);
            }

            var returnValue = (IPaged)generic.Invoke(this, new object[] { value, page, limit });
            var paginationValue = returnValue.GetType().GetProperty("Items").GetValue(returnValue, null);

            context.HttpContext.Response.Headers.Add("total", returnValue.TotalResults.ToString());
            context.HttpContext.Response.Headers.Add("pages", returnValue.TotalPages.ToString());
            context.HttpContext.Response.Headers.Add("limit", returnValue.ResultsPerPage.ToString());
            context.HttpContext.Response.Headers.Add("page", returnValue.CurrentPage.ToString());

            context.Result = new ObjectResult(paginationValue);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
