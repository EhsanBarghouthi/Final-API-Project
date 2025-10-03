using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Interfaces;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class CustomerCategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CustomerCategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryResponse> ResponseCategories = await categoryService.GetAll();

            if (ResponseCategories is not null)
                return Ok(ResponseCategories);
            return BadRequest("There Is No Categories Stored");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryResponse categoryResponse = await categoryService.GetById(id);
            if (categoryResponse is not null) return Ok(categoryResponse);
            return BadRequest("The Category Not Exist");
        }
    }
}
