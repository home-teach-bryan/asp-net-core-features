using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

public class AddStudentRequest
{
 
    /// <summary>
    /// 學生編號
    /// </summary>
    [Required]
    public string Id { get; set; }
    
    /// <summary>
    /// 學生名字
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(10, ErrorMessage = "學生名字最長為10個字元")]
    public string Name { get; set; }
}