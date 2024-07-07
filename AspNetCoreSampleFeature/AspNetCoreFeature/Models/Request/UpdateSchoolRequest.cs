using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

public class UpdateSchoolRequest
{
    /// <summary>
    /// 學校名稱
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(10, ErrorMessage = "學校名稱最長為10")]
    public string Name { get; set; }
}