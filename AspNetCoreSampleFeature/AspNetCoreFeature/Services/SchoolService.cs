using AspNetCoreFeature.Models;
using AspNetCoreFeature.Models.Response;

namespace AspNetCoreFeature.Services;

public class SchoolService : ISchoolService
{
    private List<School> _schools = new List<School>();

    /// <summary>
    /// 新增學校
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<bool> AddSchoolAsync(string id, string name)
    {
        if (_schools.Any(item => item.Id == id || item.Name == name))
        {
            return Task.FromResult(false);
        }
        _schools.Add(new School(id, name));
        return Task.FromResult(true);
    }

    /// <summary>
    /// 取得學校
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<bool> UpdateSchoolAsync(string id, string name)
    {
        var school = _schools.FirstOrDefault(item => item.Id == id);
        if (school == null)
        {
            return Task.FromResult(false);
        }
        school.SetName(name);
        return Task.FromResult(true);
    }

    /// <summary>
    /// 學校清單
    /// </summary>
    /// <returns></returns>
    public Task<List<School>> GetSchoolsAsync()
    {
        return Task.FromResult(_schools);
    }

    /// <summary>
    /// 刪除學校
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> DeleteSchoolAsync(string id)
    {
        var school = _schools.FirstOrDefault(item => item.Id == id);
        if (school == null)
        {
            return Task.FromResult(false);
        }
        _schools.Remove(school);
        return Task.FromResult(true);
        
    }

    /// <summary>
    /// 取得學校
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<School?> GetSchoolAsync(string id)
    {
        var school = _schools.FirstOrDefault(item => item.Id == id);
        return Task.FromResult(school);
    }

    /// <summary>
    /// 新增學生
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="name"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    public async Task<bool> AddStudentAsync(string studentId, string name, string schoolId)
    {
        var school = await this.GetSchoolAsync(schoolId);
        if (school == null)
        {
            return false;
        }

        school.AddStudent(new Student(studentId, name));
        return true;
    }

    /// <summary>
    /// 更新學生
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="name"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    public async Task<bool> UpdateStudentAsync(string studentId, string name, string schoolId)
    {
        var school = await this.GetSchoolAsync(schoolId);
        if (school == null)
        {
            return false;
        }
        school.UpdateStudent(studentId, name);
        return true;
    }

    /// <summary>
    /// 刪除學生
    /// </summary>
    /// <param name="id"></param>
    /// <param name="schoolId"></param>
    /// <returns></returns>
    public Task<bool> DeleteStudentAsync(string id, string schoolId)
    {
        var school = _schools.FirstOrDefault(item => item.Id == schoolId);
        if (school == null)
        {
            return Task.FromResult(false);
        }
        school.DeleteStudent(id);
        return Task.FromResult(true);
    }
    
    public Task<bool> AddStudentToClassRoomAsync(string id, string classRoomId, string studentId)
    {
        var school = _schools.FirstOrDefault(item => item.Id == id);
        if (school == null)
        {
            return Task.FromResult(false);
        }

        var student = school.GetStudent(studentId);
        if (student == null)
        {
            return Task.FromResult(false);
        }

        var classRoom = school.GetClassRoom(classRoomId);
        if (classRoom == null)
        {
            return Task.FromResult(false);
        }
        var result = classRoom.AddStudent(student);
        return Task.FromResult(result);
    }

    public Task<bool> DeleteStudentFromClassRoomAsync(string id, string classRoomId, string studentId)
    {
        var school = _schools.FirstOrDefault(item => item.Id == id);
        if (school == null)
        {
            return Task.FromResult(false);
        }

        var classRoom = school.GetClassRoom(classRoomId);
        if (classRoom == null)
        {
            return Task.FromResult(false);
        }
        var result = classRoom.DeleteStudent(studentId);
        return Task.FromResult(result);
    }
    
    public async Task<List<Student>> GetStudentsFromClassRoomAsync(string id, string schoolId)
    {
        var school = await this.GetSchoolAsync(schoolId);
        if (school == null)
        {
            return new List<Student>();
        }
        var classRoom = school.GetClassRoom(id);
        if (classRoom == null)
        {
            return new List<Student>();
        }
        return classRoom.GetStudents();
    }
}