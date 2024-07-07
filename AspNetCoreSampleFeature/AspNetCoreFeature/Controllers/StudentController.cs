using AspNetCoreFeature.Models.Request;
using AspNetCoreFeature.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFeature.Controllers;

[ApiController]
[Route("api/School/{schoolId}/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ISchoolService _schoolService;

    public StudentController(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    
    /// <summary>
    /// 新增學生
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddStudent([FromRoute] string schoolId, [FromBody] AddStudentRequest request)
    {
        var result = await _schoolService.AddStudentAsync(request.Id, request.Name, schoolId); 
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 更新學生
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateStudent([FromRoute] string schoolId, [FromRoute] string id, [FromBody] UpdateStudentRequest request)
    {
        var result = await _schoolService.UpdateStudentAsync(id, request.Name, schoolId);
        return Ok(new
        {
            Status = result
        });
    }
    
    
    /// <summary>
    ///  刪除學生
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute] string schoolId, [FromRoute] string id)
    {
        var result = await _schoolService.DeleteStudentAsync(id, schoolId);
        return Ok(new
        {
            Status = result
        });
    }
}