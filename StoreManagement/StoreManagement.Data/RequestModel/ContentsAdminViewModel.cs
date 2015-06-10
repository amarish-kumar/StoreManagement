﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data.Entities;

namespace StoreManagement.Data.RequestModel
{
    public class ContentsAdminViewModel
    {
        
        public List<Category> Categories { get; set; }
        public List<Content> Contents { get; set; }

    }
}