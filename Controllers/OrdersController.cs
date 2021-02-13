using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webshop_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {   private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public OrdersController(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(){
            List<Order> orders = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
            
            List <OrderDTO> orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            
            return Ok(orderDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id){
            var Order = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null){
                return NotFound();
            }
           return Ok(_mapper.Map<OrderDTO>(Order));
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDTO newOrderDTO) {
            Order newOrder = _mapper.Map<Order>(newOrderDTO);

            _context.Orders.Add(newOrder);
            newOrder.OrderDate = DateTime.Now;
            await _context.SaveChangesAsync();


            return CreatedAtAction("CreateOrder", newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id){
            Order order = await _context.Orders.FindAsync(id);
            if(order == null){
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }        
    }
}
