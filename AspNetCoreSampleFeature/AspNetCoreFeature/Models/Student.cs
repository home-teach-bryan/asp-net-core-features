namespace AspNetCoreFeature.Models;

public class Student
{
    /// <summary>
    /// 學生編號
    /// </summary>
    public string Id { get; private set; }
    
    /// <summary>
    /// 學生名稱
    /// </summary>
    public string Name { get; private set; }


    public Student(string id, string name)
    {
        Id = id;
        Name = name;
    }
    public void SetName(string name)
    {
        this.Name = name;
    }
}