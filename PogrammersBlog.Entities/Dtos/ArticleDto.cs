﻿using System;
using System.Collections.Generic;
using System.Text;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Entities.Dtos
{
   public class ArticleDto:DtoGetBase
    {
        public Article  Article { get; set; }
    }
}
