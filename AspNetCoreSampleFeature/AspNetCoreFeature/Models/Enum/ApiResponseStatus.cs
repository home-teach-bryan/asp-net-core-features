using System.ComponentModel;

namespace AspNetCoreFeature.Models.Enum;

public enum ApiResponseStatus
{
    /// <summary>
    /// 發生錯誤
    /// </summary>
    [Description("發生錯誤")]
    Error = -4,
    
    /// <summary>
    /// 模型驗證錯誤
    /// </summary>
    [Description("模型驗證錯誤")]
    ModelValidError = -3,
    
    /// <summary>
    /// 找不到使用者
    /// </summary>
    [Description("找不到使用者")]
    UserNotFound = -2,
    
    /// <summary>
    /// 失敗
    /// </summary>
    [Description("失敗")]
    Fail = -1,
    
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success = 1
}