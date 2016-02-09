﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcPaging;
using NLog;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Data.LiquidEngineHelpers;
using StoreManagement.Data.LiquidEntities;
using StoreManagement.Data.Paging;
using StoreManagement.Data.RequestModel;
using StoreManagement.Service.Services.IServices;

namespace StoreManagement.Service.Services
{
    public class ProductCategoryService : BaseService, IProductCategoryService 
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public ProductCategoryViewModel GetProductCategory(string id, int page)
        {
            var resultModel = new ProductCategoryViewModel();
            int categoryId = id.Split("-".ToCharArray()).Last().ToInt();
            resultModel.SCategories = ProductCategoryRepository.GetProductCategoriesByStoreId(MyStore.Id, StoreConstants.ProductType);
            resultModel.SStore = MyStore;
            resultModel.SCategory = ProductCategoryRepository.GetProductCategory(categoryId);
            var m = ProductRepository.GetProductsCategoryId(MyStore.Id, categoryId, StoreConstants.ProductType, true, page, 24);
            resultModel.SProducts = new PagedList<Product>(m.items, m.page - 1, m.pageSize, m.totalItemCount);
            resultModel.SNavigations = NavigationRepository.GetStoreActiveNavigations(this.MyStore.Id);
            resultModel.SSettings = this.GetStoreSettings();
            return resultModel;

        }
        public StoreLiquidResult GetCategoriesIndexPage(PageDesign pageDesign, StorePagedList<ProductCategory> categories)
        {



            var result = new StoreLiquidResult();
            result.PageDesingName = pageDesign.Name;

            try
            {

                if (pageDesign == null)
                {
                    throw new Exception("PageDesing is null");
                }

                var cats = new List<ProductCategoryLiquid>();
                foreach (var item in categories.items)
                {
                    cats.Add(new ProductCategoryLiquid(item));
                }

                object anonymousObject = new
                {
                    categories = LiquidAnonymousObject.GetProductCategories(cats)
                };

                var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);


                var dic = new Dictionary<String, String>();
                dic.Add(StoreConstants.PageOutput, indexPageOutput);
                dic.Add(StoreConstants.PageSize, categories.pageSize.ToStr());
                dic.Add(StoreConstants.PageNumber, categories.page.ToStr());
                dic.Add(StoreConstants.TotalItemCount, categories.totalItemCount.ToStr());
                //dic.Add(StoreConstants.IsPagingUp, pageDesign.IsPagingUp ? Boolean.TrueString : Boolean.FalseString);
                //dic.Add(StoreConstants.IsPagingDown, pageDesign.IsPagingDown ? Boolean.TrueString : Boolean.FalseString);

                result.LiquidRenderedResult = dic;

            }
            catch (Exception exception)
            {
                Logger.Error(exception, "GetCategoriesIndexPage : categories and pageDesign", String.Format("Categories Items Count : {0}", categories.items.Count));
            }

            return result;

        }

        public StoreLiquidResult GetProductCategoriesPartial(List<ProductCategory> categories, PageDesign pageDesign)
        {
            var dic = new Dictionary<String, String>();
            dic.Add(StoreConstants.PageOutput, "");
            try
            {


                var cats = new List<ProductCategoryLiquid>();
                foreach (var item in categories)
                {
                    cats.Add(new ProductCategoryLiquid(item));
                }

                object anonymousObject = new
                {
                    categories = LiquidAnonymousObject.GetProductCategories(cats)
                };

                var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);
                dic[StoreConstants.PageOutput] = indexPageOutput;


            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetProductCategoriesPartial");
            }

            var result = new StoreLiquidResult();
            result.LiquidRenderedResult = dic;
            result.PageDesingName = pageDesign.Name;
            return result;
        }

        public StoreLiquidResult GetCategoryPage(PageDesign pageDesign, ProductCategory category)
        {
            var dic = new Dictionary<String, String>();
            dic.Add(StoreConstants.PageOutput, "");
            try
            {

                var productCategories = new ProductCategoryLiquid(category);



                object anonymousObject = new
                {
                    category = LiquidAnonymousObject.GetProductCategory(productCategories)
                };

                var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);
                dic[StoreConstants.PageOutput] = indexPageOutput;



            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetProductCategoriesPartial");
            }

            var result = new StoreLiquidResult();
            result.LiquidRenderedResult = dic;
            result.PageDesingName = pageDesign.Name;
            return result;
        }
    }
}
