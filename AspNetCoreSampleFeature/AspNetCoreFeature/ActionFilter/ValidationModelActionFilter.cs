using AspNetCoreFeature.Extension;
using AspNetCoreSample.Models.Enum;
using AspNetCoreSample.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFeature.ActionFilter;

public class ValidationModelActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var apiResponse = new ApiResponse<object>
            {
                Status = ApiResponseStatus.Fail,
                Message = ApiResponseStatus.Fail.GetDescription(),
                Errors = context.ModelState.SelectMany(item => item.Value.Errors.Select(item2 => item2.ErrorMessage)).ToList(),
                Data = null
            };
            context.Result = new ObjectResult(apiResponse);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}