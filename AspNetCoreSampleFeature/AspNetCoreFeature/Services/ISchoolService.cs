using AspNetCoreFeature.Models;
using AspNetCoreFeature.Models.Response;

namespace AspNetCoreFeature.Services;

public interface ISchoolService
{
    /// <summary>
    /// 新增學校
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> AddSchoolAsync(string id, string name);

    /// <summary>
    /// 更新學校
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> UpdateSchoolAsync(string id, string name);
    
    /// <summary>
    /// 學校清單
    /// </summary>
    /// <returns></returns>
    Task<List<School>> GetSchoolsAsync();
    
    /// <summary>
    /// 刪除學校
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteSchoolAsync(string id);

    /// <summary>
    /// 取得學校
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<School?> GetSchoolAsync(string id);

    /// <summary>
    /// 新增學生
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="name"></param>
    /// <param name="schoolId"></param>
    Task<bool> AddStudentAsync(string studentId, string name, string schoolId);

    /// <summary>
    /// 更新學生
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="name"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    Task<bool> UpdateStudentAsync(string studentId, string name, string schoolId);

    /// <summary>
    /// 刪除學生
    /// </summary>
    /// <param name="id"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    Task<bool> DeleteStudentAsync(string id, string schoolId);

    /// <summary>
    /// 把學生加入課程
    /// </summary>
    /// <param name="id"></param>
    /// <param name="classRoomId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<bool> AddStudentToClassRoomAsync(string id, string classRoomId, string studentId);

    /// <summary>
    /// 從課程移除學生
    /// </summary>
    /// <param name="id"></param>
    /// <param name="classRoomId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<bool> DeleteStudentFromClassRoomAsync(string id, string classRoomId, string studentId);
    
    /// <summary>
    /// 取得班級中的學生
    /// </summary>
    /// <param name="id"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    Task<List<Student>> GetStudentsFromClassRoomAsync(string id, string schoolId);
}