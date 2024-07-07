using AspNetCoreFeature.Models;

namespace AspNetCoreFeature.Services;

public class ClassRoomService : IClassRoomService
{
    private readonly ISchoolService _schoolService;

    public ClassRoomService(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }
    public async Task<List<ClassRoom>> GetClassRoomsAsync(string schoolId)
    {
        var school = await _schoolService.GetSchoolAsync(schoolId);
        if (school == null)
        {
            return new List<ClassRoom>();
        }
        return school.GetClassRooms(); 
    }

    public async Task<bool> AddClassRoomAsync(string id, string name, string schoolId)
    {
        var school = await _schoolService.GetSchoolAsync(schoolId);
        if (school == null)
        {
            return false;
        }
        var isSuccess = school.AddClassRoom(new ClassRoom(id, name));
        return isSuccess;
    }

    public async Task<bool> UpdateClassRoomAsync(string id, string name, string schoolId)
    {
         var school = await _schoolService.GetSchoolAsync(schoolId);
         if (school == null)
         {
             return false;
         }
         var classRoom = school.GetClassRoom(id);
         if (classRoom == null)
         {
             return false;
         }
         classRoom.SetName(name);
         return true;
    }

    public async Task<bool> DeleteClassRoomAsync(string id, string schoolId)
    {
         var school = await _schoolService.GetSchoolAsync(schoolId);
         if (school == null)
         {
             return false;
         }
         var classRoom = school.GetClassRoom(id);
         if (classRoom == null)
         {
             return false;
         }
         school.RemoveClassRoom(classRoom);
         return true;
    }

    public async Task<ClassRoom?> GetClassRoomAsync(string id, string schoolId)
    {
        var school = await _schoolService.GetSchoolAsync(schoolId);
        if (school != null)
        {
            return school.GetClassRoom(id);
        }
        return null;
    }

    
    
}