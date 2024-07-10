using AspNetCoreFeature.Models.Request;
using AspNetCoreSample.Models;

namespace AspNetCoreFeature.Services;

public class ProductService : IProductService
{
    private List<Product> _products = new List<Product>();


    public bool AddProduct(AddProductRequest product)
    {
        if (_products.Any(item => item.Name == product.Name))
        {
            return false;
        }
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
        _products.Add(newProduct);
        return true;
    }

    public bool UpdateProduct(Guid id, UpdateProductRequest product)
    {
        var existProduct = _products.FirstOrDefault(item => item.Id == id);
        if (existProduct == null)
        {
            return false;
        }
        existProduct.Name = product.Name;
        existProduct.Price = product.Price;
        existProduct.Quantity = product.Quantity;
        return true;
    }

    public bool RemoveProduct(Guid id)
    {
        var existProduct = _products.FirstOrDefault(item => item.Id == id);
        if (existProduct == null)
        {
            return false;
        }
        _products.Remove(existProduct);
        return true;
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public Product GetProduct(Guid id)
    {
        var existProduct = _products.FirstOrDefault(item => item.Id == id);
        if (existProduct == null)
        {
            return new Product();
        }
        return existProduct;
    }
}