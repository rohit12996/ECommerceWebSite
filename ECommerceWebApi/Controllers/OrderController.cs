using System.Xml.Linq;
using ECommerceWebApi.Data;
using ECommerceWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {

            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var product = await _context.Orders.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

      
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderRequest addOrderRequest)
        {
            var product = new Order()
            {
                Name = addOrderRequest.Name,
                price = addOrderRequest.price,
                Category = addOrderRequest.Category,
                Quantity = addOrderRequest.Quantity,
                userId = addOrderRequest.userId,
                ModifiedDate = DateTime.Now

            };

            await _context.Orders.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }


        [HttpPut]
        [Route("id")]
        public async Task<IActionResult> UpdateOrder(int id, AddOrderRequest updateOrderRequest)
        {
            var orders = await _context.Orders.FindAsync(id);

            if (orders != null)
            {
                orders.Name = updateOrderRequest.Name;
                orders.price = updateOrderRequest.price;
                orders.Category = updateOrderRequest.Category;
                orders.Quantity = updateOrderRequest.Quantity;
                orders.userId = updateOrderRequest.userId;
                orders.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Remove(orders);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound(id);
        }
    }
}
