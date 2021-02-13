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
    public class SizesController : ControllerBase
    {   private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public SizesController(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            List<Size> sizes = await _context.Sizes.ToListAsync();

            List <SizeDTO> sizeDTOs = _mapper.Map<List<SizeDTO>>(sizes);

            return Ok(sizes);
        }

        /* [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id){
            var Product = await _context.Products.Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null){
                return NotFound();
            }
           return Ok(_mapper.Map<ProductDTO>(Product));
        } */

        [HttpPost]
        public async Task<ActionResult> CreateSize(SizeDTO newSizeDTO) {
            Size newSize = _mapper.Map<Size>(newSizeDTO);

            _context.Sizes.Add(newSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateSize", newSizeDTO);
        }

        /* [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }*/
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSize(int id){
            Size size = await _context.Sizes.FindAsync(id);
            if(size == null){
                return NotFound();
            }
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}