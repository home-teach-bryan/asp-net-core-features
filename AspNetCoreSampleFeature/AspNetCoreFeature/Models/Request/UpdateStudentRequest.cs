using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

public class UpdateStudentRequest
{
    /// <summary>
    /// 學生名字
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(10, ErrorMessage = "學生名字最長為10個字元")]
    public string Name { get; set; }
}