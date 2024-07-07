using System.Net;
using AspNetCoreFeature.Models.Request;
using AspNetCoreFeature.Models.Response;
using AspNetCoreFeature.Services;
using AspNetCoreFeature.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFeature.Controllers;

/// <summary>
/// 學校相關API
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SchoolController : ControllerBase
{
    private readonly ISchoolService _schoolService;

    /// <summary>
    /// 學校控制器
    /// </summary>
    /// <param name="schoolService"></param>
    public SchoolController(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    /// <summary>
    /// 新增學校
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddSchool([FromBody] AddSchoolRequest request)
    {
        var result = await _schoolService.AddSchoolAsync(request.Id, request.Name);
        return Ok(new
        {
            Status = result
        });
    }

    /// <summary>
    /// 更新學校
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateSchool([FromRoute] string id, [FromBody] UpdateSchoolRequest request)
    {
        var result = await _schoolService.UpdateSchoolAsync(id, request.Name);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 刪除學校
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteSchool([FromRoute] string id)
    {
        var result = await _schoolService.DeleteSchoolAsync(id);
        return Ok(new
        {
            Status = result
        });
    }

    /// <summary>
    /// 取得學校清單
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetSchools()
    {
        var schools = await _schoolService.GetSchoolsAsync();
        var result = schools.Select(item => new GetSchoolResponse
        {
            Id = item.Id,
            Name = item.Name
        });
        return Ok(new
        {
            Status = true,
            Data = result,
        });
    }
    
    /// <summary>
    /// 取得學校
    /// </summary>
    /// <param name="id">學校id</param>
    /// <returns>學校資料</returns>
    [HttpGet]
    [Route("{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<GetSchoolResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSchool([FromRoute] string id)
    {
        var school = await _schoolService.GetSchoolAsync(id);
        if (school == null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<GetSchoolResponse>
        {
            Status = true,
            Message = "成功",
            Result = new GetSchoolResponse
            {
                Id = school.Id,
                Name = school.Name
            }
        });
    }
    /// <summary>
    /// 新增學生到班級
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("{id}/classrooms/{classRoomId}/student")]
    public async Task<IActionResult> AddStudentToClassRoom([FromRoute] string id, [FromRoute] string classRoomId, [FromBody] AddStudentClassRoomRequest request)
    {
        var result = await _schoolService.AddStudentToClassRoomAsync(id, classRoomId, request.StudentId);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 從班級移除學生
    /// </summary>
    /// <param name="id"></param>
    /// <param name="classRoomId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}/classrooms/{classRoomId}/student/{studentId}")]
    public async Task<IActionResult> DeleteStudentFromClassRoom([FromRoute] string id, [FromRoute] string classRoomId, [FromRoute] string studentId)
    {
        var result = await _schoolService.DeleteStudentFromClassRoomAsync(id, classRoomId, studentId);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 取得班級學生
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="classRoomId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{schoolId}/classroom/{classRoomId}/student")]
    public async Task<IActionResult> GetStudentsFromClassRoom([FromRoute]string schoolId,[FromRoute] string classRoomId)
    {
        var students = await _schoolService.GetStudentsFromClassRoomAsync(classRoomId, schoolId);
        return Ok(new
        {
            Status = true,
            Data = students.Select(item => new GetStudentResponse
            {
                Id = item.Id,
                Name = item.Name
            })
        });
    }
    
}
