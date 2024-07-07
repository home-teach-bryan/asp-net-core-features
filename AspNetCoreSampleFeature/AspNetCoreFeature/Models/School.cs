namespace AspNetCoreFeature.Models;

public class School
{
    public string Id { get; private set; }
    public string Name { get; private set; }

    private List<ClassRoom> _classRooms = new List<ClassRoom>();
    
    private List<Student> _students = new List<Student>();


    public School(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public List<ClassRoom>? GetClassRooms()
    {
        return this._classRooms;
    }

    public bool AddClassRoom(ClassRoom classRoom)
    {
        if (this._classRooms.Any(item => item.Id == classRoom.Id))
        {
            return false;
        }
        _classRooms.Add(classRoom);
        return true;
    }

    public ClassRoom? GetClassRoom(string id)
    {
        return _classRooms.FirstOrDefault(item => item.Id == id);
    }

    public void RemoveClassRoom(ClassRoom classRoom)
    {
        _classRooms.Remove(classRoom);
    }

    public bool AddStudent(Student student)
    {
        if (_students.Any(item => item.Id == student.Id || item.Name == student.Name))
        {
            return false;
        }

        _students.Add(student);
        return true;
    }

    public Student? GetStudent(string studentId)
    {
        return _students.FirstOrDefault(item => item.Id == studentId);

    } 

    public bool UpdateStudent(string studentId, string name)
    {
        var student = _students.FirstOrDefault(item => item.Id == studentId);
        if (student == null)
        {
            return false;
        }
        student.SetName(name);
        return true;
    }
    
    public bool DeleteStudent(string studentId)
    {
        var student = _students.FirstOrDefault(item => item.Id == studentId);
        if (student == null)
        {
            return false;
        }
        _students.Remove(student);
        return true;
    }
}