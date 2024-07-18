using AspNetCoreFeature.Models.Request;
using AspNetCoreFeature.Services;
using FluentAssertions;

namespace AspNetCoreFeatureTests;

public class ProductServiceTests
{
    
    private ProductService _productService;
    [SetUp]
    public void Setup()
    {
        _productService = new ProductService();
        _productService.AddProduct(new AddProductRequest
        {
            Name = "測試",
            Price = 100,
            Quantity = 1
        });
    }

    [Test]
    public void ProductService_AddProduct_ReturnFalse()
    {
        // arrange
        var request = new AddProductRequest
        {
            Name = "測試",
            Price = 100,
            Quantity = 1
        };
        
        // actual
        var actual = _productService.AddProduct(request);
        
        // assert
        actual.Should().BeFalse();
    }

    [Test]
    public void ProductService_AddProduct_ReturnTrue()
    {
        // arrange
        var request = new AddProductRequest
        {
            Name = "測試1",
            Price = 100,
            Quantity = 1
        };
        
        // actual
        var actual = _productService.AddProduct(request);
        
        // assert
        actual.Should().BeTrue();
    }

    [Test]
    public void ProductService_UpdateProduct_ReturnFalse()
    {
        // arrange
        var request = new UpdateProductRequest
        {
            Name = "測試",
            Price = 100,
            Quantity = 1
        };
        
        // actual
        var actual = _productService.UpdateProduct(Guid.NewGuid(), request);
        
        // assert
        actual.Should().BeFalse();
    }
    
    [Test]
    public void ProductService_UpdateProduct_ReturnTrue()
    {
        // arrange
        var products = _productService.GetAllProducts();
        var existProduct = products.FirstOrDefault();
        
        var request = new UpdateProductRequest
        {
            Name = "測試1",
            Price = 100,
            Quantity = 1
        };
        
        // actual
        var actual = _productService.UpdateProduct(existProduct.Id, request);
        
        // assert
        actual.Should().BeTrue();
    }
    
    [Test]
    public void ProductService_RemoveProduct_ReturnFalse()
    {
        // arrange
        var productId = Guid.NewGuid();
        
        // actual
        var actual = _productService.RemoveProduct(productId);
        
        // assert
        actual.Should().BeFalse();
    }

    [Test]
    public void ProductService_RemoveProduct_ReturnTrue()
    {
        // arrange
        var products = _productService.GetAllProducts();
        var existProduct = products.FirstOrDefault();
        
        // actual 
        var actual = _productService.RemoveProduct(existProduct.Id);
        
        // assert
        actual.Should().BeTrue();
    }

    [Test]
    public void ProductService_GetAllProducts_Products()
    {
        // actual
        var actual = _productService.GetAllProducts();
        
        // assert
        actual.Should().HaveCount(1);
    }
    
    [Test]
    public void ProductService_GetProduct_ReturnNotNullProduct()
    {
        // actual
        var actual = _productService.GetProduct(Guid.NewGuid());
        
        // assert
        actual.Should().NotBeNull();
    }
    
    
}