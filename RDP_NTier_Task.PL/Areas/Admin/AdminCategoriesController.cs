using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Interfaces;
using RDP_NTier_Task.DAL.DTO.CategoryDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;

namespace RDP_NTier_Task.PL.Areas.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminCategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public AdminCategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryResponse> ResponseCategories =await categoryService.GetAll();

            if (ResponseCategories is not null)
                return Ok(ResponseCategories);
            return BadRequest("There Is No Categories Stored");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryResponse categoryResponse =await categoryService.GetById(id);
            if (categoryResponse is not null) return Ok(categoryResponse);
            return BadRequest("The Category Not Exist");
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryRequest categoryRequest)
        {
            int created = await categoryService.Create(categoryRequest);
            if (created > 0)
                return Ok(new { message = "Elelement Added Success" });

            return BadRequest(new { message = "Cant Add Element" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            int delted = await categoryService.Delete(id);
            if (delted > 0)
            {
                return Ok();
            }
            else return BadRequest("The Element Not Exist");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(CategoryRequest categoryRequest, int id)
        {
            int updated = await categoryService.Update(categoryRequest, id);
            if (updated > 0)
            {
                return Ok("Updated !!!!! ");
            }
            else return BadRequest("The Element Not Exist");

        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            int removed = await categoryService.RemoveAll();
            return Ok(new { message = "The Number Of Elements Removed is :", removed });
        }

    }
}
