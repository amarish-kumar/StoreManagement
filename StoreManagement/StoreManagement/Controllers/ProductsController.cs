﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using NLog;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Data.RequestModel;
using StoreManagement.Service.Interfaces;
using StoreManagement.Service.Services;

namespace StoreManagement.Controllers
{
    [OutputCache(CacheProfile = "Cache1Days")]
    public class ProductsController : BaseController
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            var resultModel = new ProductsViewModel();
            resultModel.SCategories = ProductCategoryService.GetProductCategoriesByStoreIdFromCache(MyStore.Id, StoreConstants.ProductType);
            resultModel.SStore = MyStore;
            resultModel.SNavigations = NavigationService.GetStoreActiveNavigations(this.MyStore.Id);
            resultModel.SSettings = this.GetStoreSettings();
            return View(resultModel);
        }
        //
        // GET: /Products/
        public ActionResult Product(String id)
        {
            var resultModel = new ProductDetailViewModel();
            int productId = id.Split("-".ToCharArray()).Last().ToInt();
            resultModel.SProduct = ProductService.GetProductsById(productId);
            resultModel.SStore = MyStore;
            resultModel.SCategory = ProductCategoryService.GetProductCategory(resultModel.Product.ProductCategoryId);
            resultModel.SCategories = ProductCategoryService.GetProductCategoriesByStoreId(MyStore.Id, StoreConstants.ProductType);
            resultModel.SNavigations = NavigationService.GetStoreActiveNavigations(this.MyStore.Id);
            resultModel.SSettings = this.GetStoreSettings();
            return View(resultModel);
        }
	}
}