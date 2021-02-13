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
    public class OrderDetailsController : ControllerBase
    {   private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public OrderDetailsController(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(){
            List<OrderDetails> orderDetails = await _context.OrderDetails.Include(o => o.Order).ToListAsync();
            
            List <OrderDetailsDTO> orderDetailsDTOs = _mapper.Map<List<OrderDetailsDTO>>(orderDetails);
            
            return Ok(orderDetailsDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> Get(int id){
            var OrderDetails = await _context.OrderDetails.Include(o => o.Order).FirstOrDefaultAsync(o => o.Id == id);

            if (OrderDetails == null){
                return NotFound();
            }
           return Ok(_mapper.Map<OrderDetailsDTO>(OrderDetails));
        }
    }
}