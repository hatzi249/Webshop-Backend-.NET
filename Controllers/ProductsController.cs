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
    public class ProductsController : ControllerBase
    {   private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public ProductsController(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            List<Product> products = await _context.Products.Include(p => p.Sizes).ToListAsync();

            List <ProductDTO> productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id){
            var Product = await _context.Products.Include(p => p.Sizes).FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null){
                return NotFound();
            }
           return Ok(_mapper.Map<ProductDTO>(Product));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDTO newProductDTO) {
            Product newProduct = _mapper.Map<Product>(newProductDTO);

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateProduct", newProductDTO);
        }

        [HttpPut("{id}")]
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
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id){
            Product product = await _context.Products.FindAsync(id);
            if(product == null){
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}