﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract
{
   public interface ICategoryService
   {
       Task<IDataResult<CategoryDto>> Get(int categoryId);
       Task<IDataResult<CategoryListDto>> GetAll();

       Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
       Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();

       Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto,string createByName);
       Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
       Task<IResult> Delete(int categoryId,string modifiedByName);
       Task<IResult> HardDelete(int categoryId);
   }
}
