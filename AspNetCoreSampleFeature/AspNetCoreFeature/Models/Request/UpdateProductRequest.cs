using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

public class UpdateProductRequest
{
    /// <summary>
    /// 產品名稱
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    [MaxLength(length: 10, ErrorMessage = "Name cannot exceed 10 characters")]
    public string Name { get; set; }
    
    /// <summary>
    /// 產品價格
    /// </summary>
    [Range(1, double.MaxValue, ErrorMessage = "Price must be great than 1")]
    public decimal Price { get; set; }

    /// <summary>
    /// 產品數量
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be great than 1")]
    public int Quantity { get; set; }
}