namespace AspNetCoreFeature.Models;

public class ClassRoom
{
    public string Id { get; private set; }
    
    public string Name { get; private set; }

    private Dictionary<string, decimal> _gradeScore = new Dictionary<string, decimal>();
    
    private List<Student> _students = new List<Student>();

    public ClassRoom(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public void SetName(string name)
    {
        this.Name = name;
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

    public bool DeleteStudent(string studentId)
    {
        var student = _students.FirstOrDefault(item => item.Id == studentId);
        if(student == null)
        {
            return false;
        }
        _students.Remove(student);
        return true;
    }

    public List<Student> GetStudents()
    {
        return this._students;
    }
}