using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.BrandServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminBrandsController : ControllerBase
    {
        private readonly IBrandServices brandServices;

        public AdminBrandsController(IBrandServices brandServices)
        {
            this.brandServices = brandServices;
        }

        [HttpPost]
        public async Task<IActionResult> createBrand([FromForm] BrandRequest brandRequest)
        {
            var result =await brandServices.Create(brandRequest);
            if (result > 0)
            {
                return Ok("Brand Added Success !!!! ");
            }
            return BadRequest("Cant Add Brand !!! ");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await brandServices.Delete(id);
            if (result > 0)
            {
                return Ok("Deleted Success !!! ");
            }
            return BadRequest("Deleted Failed !!!!! ");

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

        [HttpDelete("all")]
        public async Task<IActionResult> removeAll()
        {
            var result = await brandServices.RemoveAll();
            if (result > 0) return Ok($"All Brands Removed : {result}");
            return BadRequest("Not Remove !! ");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(BrandUpdateRequest brand, int id)
        {
            var res = await brandServices.Update(brand, id);
            if (res > 0) return Ok("Updated !!! ");
            return BadRequest("Not Updated");
        }

    }
}
