using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.ProductServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminProductsController : ControllerBase
    {
        private readonly IProductServices productServices;

        public AdminProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        [HttpPost]
        public async Task<IActionResult> createProduct([FromForm] productRequest productRequestDTO)
        {

            var result = await productServices.Create(productRequestDTO);
            if (result > 0)
            {
                return Ok("Product Added !!! ");
            }
            return BadRequest("Cant Add !!!!!! ");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productServices.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<productResponse>> getById(int id)
        {
            var product = await productServices.GetById(id);
            if (product == null) return BadRequest("The Product Not Exist !!! ");
            return Ok(product);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await productServices.Delete(id);
            if (deleted == 0) return BadRequest("The Product can Not Deleted !!! ");
            return Ok("Deleted Succefully !!! ");
        }


        [HttpDelete("all")]
        public async Task<IActionResult> RemoveAll()
        {
            var deleted = await productServices.RemoveAll();
            if (deleted == 0) return BadRequest("The Product can Not Deleted !!! ");
            return Ok($"Deleted Succefully !!! {deleted}");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> Update([FromForm] ProductUpdateRequest updateRequest, int id)
        {
            var updated = await productServices.Update(updateRequest, id);
            if (updated == 0) return BadRequest("Cant Update !!!! ");
            return Ok("Updated Successfullly !!!! ");
        }

        [HttpGet("allProds")]
        public async Task<IActionResult> GetAllProds()
        {
            //return Ok(await productServices.GetAll());
            return Ok(await productServices.GetAllProducts(Request));
        }



    }
}
