using AspNetCoreFeature.Extension;
using AspNetCoreSample.Models.Enum;
using AspNetCoreSample.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFeature.ActionFilter;
public class ApiResponseActionFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult && context.ModelState.IsValid)
        {
            var apiResponseStatus = objectResult.StatusCode == StatusCodes.Status200OK
                ? ApiResponseStatus.Success
                : ApiResponseStatus.Fail;
            
            var data = objectResult.StatusCode == StatusCodes.Status200OK ? objectResult.Value : null;
            var error = objectResult.StatusCode == StatusCodes.Status400BadRequest ? objectResult.Value : null;
            var apiResponse = new ApiResponse<object>
            {
                Status = apiResponseStatus,
                Message = apiResponseStatus.GetDescription(),
                Data = data,
                Errors = new List<string> { error.ToString() }
            };
            var result = new ObjectResult(apiResponse)
            {
                StatusCode = objectResult.StatusCode
            };
            context.Result = result;
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        return;
    }
}