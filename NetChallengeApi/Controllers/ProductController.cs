using NetChallenge.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetChallenge.Database.Model;
using NetChallenge.Database.Interfaces;
using AutoMapper;
using NetChallenge.WebApi.Dtos;

namespace NetChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IApiRepository _repository;
        private readonly IMapper _mapper;
        public ProductController(IApiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.GetProductsAsync();
            if (products.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            var uProduct = _mapper.Map<ProductDto>(product);
            return Ok(uProduct);
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> GetByDesciption(string? query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return await Get();
            }
            var products = await _repository.GetProductByDescriptionAsync(query);
            if (products.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (product.Id != Guid.Empty)
            {
                return BadRequest("It's not allowed to send Id in Post action.");
            }
            _repository.Add(product);
            if (!await _repository.SaveAll())
            {
                return BadRequest("Could not create the new product.");
            }
            return Created(string.Empty, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Product product)
        {
            Product uProduct = await _repository.GetProductByIdAsync(id);
            if (uProduct == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (id != product.Id)
            {
                return BadRequest(string.Format("Ids {0} and {1} don't match.", id, product.Id));
            }
            _mapper.Map(product,uProduct);
            if (!await _repository.SaveAll())
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            _repository.Delete(product);
            if (!await _repository.SaveAll())
            {
                return BadRequest(string.Format("Could not delete product with {0}.", id));
            }
            return Ok();
        }
 
    }
}
