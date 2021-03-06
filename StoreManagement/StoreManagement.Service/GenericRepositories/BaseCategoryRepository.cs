﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.EntityFramework.Enums;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Service.Repositories.Interfaces;

namespace StoreManagement.Service.GenericRepositories
{
    public class BaseCategoryRepository : GenericBaseRepository
    {
        public static List<T> GetBaseCategoriesSearchList<T>(IBaseRepository<T, int> repository, int storeId, string search, String type) where T : BaseCategory
        {
            try
            {

                var cats = repository.FindBy(r => r.StoreId == storeId &&
                                      r.CategoryType.Equals(type, StringComparison.InvariantCultureIgnoreCase));

                if (!String.IsNullOrEmpty(search.ToStr()))
                {
                    cats = cats.Where(r => r.Name.ToLower().Contains(search.ToLower().Trim()));
                }

                return cats.OrderBy(r => r.Ordering).ThenByDescending(r => r.Id).ToList();

            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                return null;
            }
        }

        public static List<T> GetCategoriesByStoreId<T>(IBaseRepository<T, int> repository, int storeId, String type, bool? isActive) where T : BaseCategory
        {
            try
            {

                var items = repository.FindBy(r => r.StoreId == storeId &&
                         r.CategoryType.Equals(type, StringComparison.InvariantCultureIgnoreCase));

                if (isActive.HasValue)
                {
                    items = items.Where(r => r.State == isActive.Value);
                }

                return items.OrderBy(r => r.Ordering).ToList();
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                return null;
            }
        }
        public static async Task<List<T>> GetCategoriesByStoreIdAsync<T>(IBaseRepository<T, int> repository, int storeId, String type, bool? isActive, int? take, int? skip = null) where T : BaseCategory
        {
            try
            {
                Expression<Func<T, bool>> match = r2 => r2.StoreId == storeId
                    && !String.IsNullOrEmpty(type) ?  r2.CategoryType.Equals(type, StringComparison.InvariantCultureIgnoreCase) : true 
                    && r2.State == (isActive.HasValue ? isActive.Value : r2.State);
                var items = repository.FindAllAsync(match, t => t.Ordering, OrderByType.Descending, take, skip);
                var itemsResult = items;
                return await itemsResult;
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                return null;
            }
        }

    }
}
