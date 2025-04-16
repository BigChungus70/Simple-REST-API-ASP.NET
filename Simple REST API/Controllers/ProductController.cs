using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Simple_REST_API.Models;
using Simple_REST_API.Service;

namespace Simple_REST_API.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _service;
    public ProductController(ILogger<ProductController> logger, ProductService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        List<ProductDTO> AllProducts = await _service.GetAllProducts();
        if (AllProducts.Count == 0)
            return NoContent();

        return Ok(AllProducts);

    }
    [HttpGet]
    public async Task<IActionResult> GetProductById([FromQuery] int Id)
    {
        ProductDTO? Product = await _service.GetProductById(Id);

        if (Product == null)
            return NotFound("No Such Product");

        return Ok(Product);
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO Product)
    {
        ProductDTO? NewProduct;
        try
        {
             NewProduct = await _service.AddProduct(Product);
            if (NewProduct == null)
            {
                return BadRequest("Product could not be created due to invalid input.");
            }
        }
        catch
        {
            return Error();
        }
        return Ok(NewProduct);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromQuery] int Id,[FromBody] ProductDTO Product)
    {
        ProductDTO? product = await _service.UpdateProduct(Id, Product);

        if (product == null)
            return NotFound();
        return Ok(product);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] int Id)
    {
        bool result = await _service.DeleteProduct(Id);
        if (!result)
            return  NotFound("No Such Product");
        return NoContent();
    }
}
