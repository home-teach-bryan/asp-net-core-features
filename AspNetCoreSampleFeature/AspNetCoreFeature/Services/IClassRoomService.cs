using AspNetCoreFeature.Models;

namespace AspNetCoreFeature.Services;

public interface IClassRoomService
{
    Task<List<ClassRoom>> GetClassRoomsAsync(string schoolId);

    Task<bool> AddClassRoomAsync(string id, string name, string schoolId);

    Task<bool> UpdateClassRoomAsync(string id, string name, string schoolId);

    Task<bool> DeleteClassRoomAsync(string id, string schoolId);
    Task<ClassRoom?> GetClassRoomAsync(string id, string schoolId);

    
}