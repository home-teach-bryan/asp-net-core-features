namespace AspNetCoreFeature.Models.Response;

public class ApiResponse<T>
{
    /// <summary>
    /// 狀態
    /// </summary>
    public bool Status { get; set; }
    
    /// <summary>
    /// 執行訊息
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// 結果
    /// </summary>
    public T Result { get; set; }
}