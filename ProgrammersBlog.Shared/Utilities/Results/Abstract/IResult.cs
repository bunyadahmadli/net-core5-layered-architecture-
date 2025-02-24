﻿using System;
using System.Collections.Generic;
using System.Text;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
   public interface IResult
    {
        public ResultStatus ResultStatus { get;  }
        public string Message { get;  }
        public Exception Exception { get;  }

    }
}
