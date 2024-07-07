using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

/// <summary>
/// 新增學校Request
/// </summary>
public class AddSchoolRequest
{
    /// <summary>
    /// 學校編號
    /// </summary>
    [Required]
    public string Id { get; set; }
    
    /// <summary>
    /// 學校名稱
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(10, ErrorMessage = "學校名稱最長為10")]
    public string Name { get; set; }
}