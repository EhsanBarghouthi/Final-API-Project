using Mapster;
using RDP_NTier_Task.BL.General_Services;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.BrandServices
{
    public class BrandServices : IBrandServices
    {
        private readonly IGenericRepository<Brand> repository;
        private readonly IFileService fileService;
        private readonly string folderPath;

        public BrandServices(IGenericRepository<Brand> repository, IFileService fileService)
        {
            this.repository = repository;
            this.fileService = fileService;

            folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BrandImages");
        }

        public async Task<int> Create(BrandRequest requestEntity)
        {
            // Save image and get file name
            string imageName = null;
            if (requestEntity.BrandImage != null)
            {
                imageName = await fileService.SaveFile(requestEntity.BrandImage, folderPath);
            }

            // Map request DTO to Product entity
            var brandEntity = requestEntity.Adapt<Brand>(); // <-- add parentheses
            brandEntity.BrandImage = imageName;

            // Save to database
            return await repository.Create(brandEntity);
        }

        public async Task<int> Delete(int id)
        {
            Brand brand = await repository.GetById(id);

            if (brand is null)
            {
                return 0;
            }

            // delete image : 
            var result =await fileService.DeleteFile(brand.BrandImage, folderPath);
            if (result == 1)
            {
                return await repository.Delete(id);
            }
            return 0;
        }


        public async Task<List<BrandResponse>> GetAll()
        {
            List<Brand> brands = await repository.GetAll();
            List<BrandResponse> brandResponses = brands.Adapt<List<BrandResponse>>();
            return brandResponses;
        }

        public async Task<BrandResponse> GetById(int id)
        {
            Brand brand = await repository.GetById(id);
            if (brand is not null)
            {
                BrandResponse brandResponse = brand.Adapt<BrandResponse>();
                return brandResponse;
            }
            else
                return null;

        }

        public async Task<int> RemoveAll()
        {
            List<Brand> brands = await repository.GetAll();
            if (brands.Count > 0)
            {
                foreach (Brand brand in brands)
                {
                    await fileService.DeleteFile(brand.BrandImage, folderPath);
                }
                var result = await repository.RemoveAll();

                if (result > 0)
                {
                    return result;
                }
                return 0;
            }
            return 0;
        }

        public async Task<int> Update(BrandUpdateRequest entityRequest, int id)
        {
            var brand = await repository.GetById(id);
            if (brand is null || entityRequest is null)
                return 0;

            // If a new image was uploaded
            if (entityRequest.BrandImage is { Length: > 0 }
                    && entityRequest.BrandImage is not null)
            {
                // delete the old image if exists
                if (!string.IsNullOrWhiteSpace(brand.BrandImage))
                {
                    fileService.DeleteFile(brand.BrandImage, folderPath);
                }

                // save new file
                string fileName = await fileService.SaveFile(entityRequest.BrandImage, folderPath);
                brand.BrandImage = fileName;
            }

            // update name only if provided
            if (!string.IsNullOrWhiteSpace(entityRequest.BrandName))
                brand.BrandName = entityRequest.BrandName;

            // make sure id stays consistent
            brand.Id = id;

            return await repository.Update(brand);
        }

        public async Task<int> Update(BrandRequest entityRequest, int id)
        {
            throw new NotImplementedException();
        }
    }
}
