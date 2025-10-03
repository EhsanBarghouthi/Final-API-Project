using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDP_NTier_Task.DAL.Repostry.ProductRepository;
using RDP_NTier_Task.BL.General_Services;
using Microsoft.AspNetCore.Http;

namespace RDP_NTier_Task.BL.ServicesRepository.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository repository;
        private readonly IFileService fileServices;
        private readonly string imageFolder ;

        public ProductServices(IProductRepository repository, IFileService fileServices)
        {
            this.repository = repository;
            this.fileServices = fileServices;

            imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages");

        }


        public async Task<int> Create(productRequest requestEntity)
        {
            // Map request DTO to Product entity
            var product = requestEntity.Adapt<Product>(); // <-- add parentheses

            // Save image and get file name
            if (requestEntity.image != null)
            {
               string imageName =await fileServices.SaveFile(requestEntity.image, imageFolder);
                product.image = imageName;

            }
            // Save image and get file name --> for sub Images
            if (requestEntity.subImages != null)
            {
               List<string> imageName =await fileServices.SaveManyFiles(requestEntity.subImages, imageFolder);

                // this used navigation from product , and store in the productImage , (you can save product first , then add and store productImage in loop direct)
                product.subImages = imageName.Select(img=> new ProductImages { ImageName = img}).ToList();

            }

            // Save to database
            return await repository.Create(product);
        }

        public async Task<int> Delete(int id)
        {
            var product = await repository.GetById(id);
            if (product is null) return 0;

            var result =await fileServices.DeleteFile(product.image,imageFolder);
            if (result > 0) 
            {
                return await repository.Delete(id);
            }
            return 0;
        }

        public async Task<List<productResponse>> GetAll()
        {
            List<Product> products = await repository.GetAll();
            if (products is null) return null;
            List<productResponse> productResponse = products.Adapt<List<productResponse>>();
            return productResponse;
        }


        public async Task<productResponse> GetById(int id)
        {
            Product product =await repository.GetById(id);
            if (product is null) return null;
            productResponse productResponse = product.Adapt<productResponse>();
            return productResponse;
        }

        public async Task<int> RemoveAll()
        {
            List<Product> products =await repository.GetAll();
            if (products.Count == 0) return 0;
            
            foreach(Product product in products)
            {
                if (!string.IsNullOrWhiteSpace(product.image))
                {
                    fileServices.DeleteFile(product.image, imageFolder);
                }
            }
            int result =await repository.RemoveAll();
            return result;
        }


        public async Task<int> Update(ProductUpdateRequest entityRequest, int id)
        {
            Product oldProduct =await repository.GetById(id);
            if (oldProduct is null) return 0;

            string fileName;
            //for Image : 
            if (entityRequest.image is not null)
            {
                if (oldProduct.image != null) fileServices.DeleteFile(oldProduct.image, imageFolder);
                fileName =await fileServices.SaveFile(entityRequest.image, imageFolder);
            }
            else
                fileName = oldProduct.image;

            // This will ignore nulls in the DTO and only update provided fields
            TypeAdapterConfig<ProductUpdateRequest, Product>
                .NewConfig()
                .IgnoreNullValues(true); // ignore null so we don’t overwrite DB values

            entityRequest.Adapt(oldProduct);

            oldProduct.Id = id;
            oldProduct.image = fileName;

            return await repository.Update(oldProduct);
        }



        public async Task<int> Update(productRequest entityRequest, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<productResponse>> GetAllProducts(HttpRequest request, bool onlyActive=false)
        {
            var products = await repository.GetAllProducts();
            if(onlyActive) products = products.Where(p => p.status == Status.Active).ToList();

            var prods = products.Select(p => new productResponse
            {
                brandId = p.brandId,
                categoryId = p.categoryId,
                ImageUrl = $"{request.Scheme}://{request.Host}/Images/ProductImages/{p.image}",
                quantity = p.quantity,
                productCode = p.productCode,
                productName = p.productName,
                productDescription = p.productDescription,
                productPrice = p.productPrice,
                subImages = p.subImages.Select(img => $"{request.Scheme}://{request.Host}/Images/ProductImages/{img.ImageName}").ToList(),

                reviewResponse = p.Reviews.Select(r=> new ReviewResponseDTO
                {
                    Id = r.Id,
                    comment = r.comment,
                    Rate=r.Rate,
                    Ordering=r.Ordering,
                    ReviewDate=r.ReviewDate,
                    userFullName=r.User.FullName
                }).ToList()

            }).ToList();

            return prods;


        }
    }
}
