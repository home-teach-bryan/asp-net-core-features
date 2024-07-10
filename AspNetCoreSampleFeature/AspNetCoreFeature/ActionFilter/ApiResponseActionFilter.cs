using AspNetCoreSample.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFeature.ActionFilter;
public class ApiResponseActionFilter : ActionFilterAttribute
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var apiResponse = new ApiResponse<object>
            {
                Status = true,
                Data = objectResult.Value,
            };
            context.Result = new ObjectResult(apiResponse);
        }
    }
}