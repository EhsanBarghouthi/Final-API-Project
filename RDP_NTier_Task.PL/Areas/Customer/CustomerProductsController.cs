using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.ProductServices;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class CustomerProductsController : ControllerBase
    {
        private readonly IProductServices productServices;

        public CustomerProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            //return Ok(await productServices.GetAll());
            return Ok(await productServices.GetAllProducts(Request,true));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<productResponse>> getById(int id)
        {
            var product = await productServices.GetById(id);
            if (product == null) return BadRequest("The Product Not Exist !!! ");
            return Ok(product);

        }

    }
}
