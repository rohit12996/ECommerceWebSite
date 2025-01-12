using ECommerceWebApi.Data;
using ECommerceWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                // ProductId = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price,
                Category = addProductRequest.Category,
                SellerId = addProductRequest.SellerId,
                StockQuantity = addProductRequest.StockQuantity,
                ImagePath = addProductRequest.ImagePath,
                ModifiedDate = DateTime.Now

            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }

        [HttpPut]
        [Route("id")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest updateProductRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.Price = updateProductRequest.Price;
                product.Category = updateProductRequest.Category;
                product.SellerId = updateProductRequest.SellerId;
                product.ImagePath = updateProductRequest.ImagePath;
                product.Name = updateProductRequest.Name;
                product.Description = updateProductRequest.Description;
                product.StockQuantity = updateProductRequest.StockQuantity;
                product.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound(id);
        }

    }
}
