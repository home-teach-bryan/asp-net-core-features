using AspNetCoreFeature.Models.Request;
using AspNetCoreSample.Models;

namespace AspNetCoreFeature.Services;

public interface IProductService
{
    public bool AddProduct(AddProductRequest product);

    public bool UpdateProduct(Guid id, UpdateProductRequest product);

    public bool RemoveProduct(Guid id);
    
    public List<Product> GetAllProducts();

    public Product GetProduct(Guid id);
}