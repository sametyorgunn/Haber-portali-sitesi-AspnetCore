﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newsportal.Areas.Admin.Models
{
    public class EditNewsViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
