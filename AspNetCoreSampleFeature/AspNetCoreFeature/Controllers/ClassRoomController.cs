using AspNetCoreFeature.Models.Request;
using AspNetCoreFeature.Models.Response;
using AspNetCoreFeature.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFeature.Controllers;

[ApiController]
[Route("api/School/{schoolId}/[controller]")]
public class ClassRoomController : ControllerBase
{
    private readonly IClassRoomService _classRoomService;

    public ClassRoomController(IClassRoomService classRoomService)
    {
        _classRoomService = classRoomService;
    }
    
    /// <summary>
    /// 加入班級
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddClassRoom([FromRoute] string schoolId, [FromBody] AddClassRoomRequest request)
    {
        var result = await _classRoomService.AddClassRoomAsync(request.Id, request.Name, schoolId);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 更新班級
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateClassRoom([FromRoute] string schoolId, [FromRoute] string id, [FromBody] UpdateClassRoomRequest request)
    {
        var result = await _classRoomService.UpdateClassRoomAsync(id, request.Name, schoolId);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 刪除班級
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteClassRoom([FromRoute] string schoolId, [FromRoute] string id)
    {
        var result = await _classRoomService.DeleteClassRoomAsync(id, schoolId);
        return Ok(new
        {
            Status = result
        });
    }
    
    /// <summary>
    /// 取得學校中全部班級
    /// </summary>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetClassRooms([FromRoute] string schoolId)
    {
        var classRooms = await _classRoomService.GetClassRoomsAsync(schoolId);
        return Ok(new
        {
            Status = true,
            Data = classRooms.Select(item => new GetClassRoomResponse
            {
                Id = item.Id,
                Name = item.Name
            })
        });
    }
    
    /// <summary>
    /// 取得班級
    /// </summary>
    /// <param name="schoolId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetClassRoom([FromRoute] string schoolId, [FromRoute] string id)
    {
        var classRoom = await _classRoomService.GetClassRoomAsync(id, schoolId);
        if (classRoom == null)
        {
            return NotFound();
        }
        return Ok(new
        {
            Status = true,
            Data = new GetClassRoomResponse { Id = classRoom.Id, Name = classRoom.Name }
        });
    }

   
    
}