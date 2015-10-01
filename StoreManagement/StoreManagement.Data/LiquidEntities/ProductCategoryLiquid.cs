﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;

namespace StoreManagement.Data.LiquidEntities
{
    public class ProductCategoryLiquid : BaseDrop
    {
        public ProductCategory ProductCategory { get; set; }
        public PageDesign PageDesign { get; set; }



        public ProductCategoryLiquid(ProductCategory productCategory, PageDesign pageDesign)
        {
            this.ProductCategory = productCategory;
            this.PageDesign = pageDesign;
        }


        public String DetailLink
        {
            get
            {
                return LinkHelper.GetCategoryLink(this.ProductCategory);
            }
        }
        public int Count { get; set; }
    }
}
