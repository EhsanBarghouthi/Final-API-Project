using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.BrandServices;
using RDP_NTier_Task.BL.userServices;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CustomerBrandsController : ControllerBase
    {
        private readonly IBrandServices brandServices;

        public CustomerBrandsController(IBrandServices brandServices)
        {
            this.brandServices = brandServices;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<BrandResponse>>> GetAll()
        {
            List<BrandResponse> brandResponses = await brandServices.GetAll();
            return Ok(brandResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            BrandResponse brand = await brandServices.GetById(id);
            if (brand != null)
                return Ok(brand);
            return BadRequest("Not Exist !!! ");
        }



    }
}

