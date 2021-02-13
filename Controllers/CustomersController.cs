using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webshop_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {   private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public CustomersController(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            List<Customer> products = await _context.Customers.Include(c => c.Orders).ToListAsync();

            List <CustomerDTO> customerDTOs = _mapper.Map<List<CustomerDTO>>(products);

            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(int id){
            var Customer = await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

            if (Customer == null){
                return NotFound();
            }
           return Ok(_mapper.Map<CustomerDTO>(Customer));
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerDTO newCustomerDTO) {
            Customer newCustomer = _mapper.Map<Customer>(newCustomerDTO);

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateCustomer", newCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id){
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer == null){
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}