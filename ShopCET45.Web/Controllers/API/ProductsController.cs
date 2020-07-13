using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopCET45.Web.Data.Repositories;

namespace ShopCET45.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productRepository.GetAllWithUsers());  // O metodo OK faz a descerilização para json. ja contem a anotação para do [HttpGet]
        }
    }
}
