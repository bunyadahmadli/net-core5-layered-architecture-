﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
  public  class CategoryManager:ICategoryService
  {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
      {
          _unitOfWork = unitOfWork;
          _mapper = mapper;
      }

      public async Task<IDataResult<CategoryDto>> Get(int categoryId)
      {
        var category = await _unitOfWork.Categories.GetAsync(c=>c.Id==categoryId,c=>c.Articles);
        if (category != null)
        {
            return new DataResult<CategoryDto>(ResultStatus.Success,new CategoryDto
            {
                Category = category,
                ResultStatus = ResultStatus.Success
            });
        }
        return new DataResult<CategoryDto>(ResultStatus.Error,"Böyle bir kategori bulunamadı.",new CategoryDto
        {
            Category = null,
            ResultStatus = ResultStatus.Error,
            Message = "Böyle bir kategori bulunamadı."

        });

      }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c=>c.Articles);

            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error,"Hiç bir kategori bulunmadı.",new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir kategori bulunmadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunmadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunmadı.", null);
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createByName;
            category.ModifeidByName = createByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
          
             return new DataResult<CategoryDto>(ResultStatus.Success,$"{categoryAddDto.Name} adlı kategori başarıyla eklendi.",new CategoryDto
             {
                 Category = addedCategory,
                 ResultStatus = ResultStatus.Success,
                 Message = $"{categoryAddDto.Name} adlı kategori başarıyla eklendi."
             });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifeidByName = modifiedByName;
             var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir.",new CategoryDto
            {
               Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir."
            });
        }

        public async Task<IResult> Delete(int categoryId,string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifeidByName = modifiedByName;
                category.ModifiedDate=DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }
    }
}
