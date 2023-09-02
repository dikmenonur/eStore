using Microsoft.AspNetCore.Mvc;

namespace eStore.Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "get")]
        public async Task Get()
        {
          
        }

        [HttpGet(Name = "GetById")]
        public async Task GetById()
        {

        }

        [HttpGet(Name = "GetAll")]
        public async Task GetAll()
        {

        }

        [HttpPost(Name = "AddProduct")]
        public async Task AddProduct()
        {

        }
    }
}