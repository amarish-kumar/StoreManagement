﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcPaging;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Data.Paging;
using StoreManagement.Service.IGeneralRepositories;

namespace StoreManagement.Service.ApiRepositories
{
    public class FileManagerApiRepository : BaseApiRepository, IFileManagerGeneralRepository
    {


        protected override string ApiControllerName { get { return "FileManagers"; } }

        public FileManagerApiRepository(string webServiceAddress)
            : base(webServiceAddress)
        {

        }
 
        public List<FileManager> GetFilesByStoreId(int storeId)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetFilesByStoreId?storeId={2}", WebServiceAddress, ApiControllerName, storeId);
            return HttpRequestHelper.GetUrlResults<FileManager>(url);

        }

        public FileManager GetFilesByGoogleImageId(string googleImageId)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetFilesByGoogleImageId?storeId={2}", WebServiceAddress, ApiControllerName, googleImageId);
            return HttpRequestHelper.GetUrlResult<FileManager>(url);

        }

        public List<FileManager> GetFilesByGoogleImageIdArray(string[] googleImageId)
        {
            throw new NotImplementedException();
        }

        public FileManager GetFilesById(int id)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetFilesById?id={2}", WebServiceAddress, ApiControllerName, id);
            return HttpRequestHelper.GetUrlResult<FileManager>(url);

        }

        public List<FileManager> GetStoreCarousels(int storeId)
        {

            string url = string.Format("http://{0}/api/{1}/GetStoreCarousels?storeId={2}", WebServiceAddress, ApiControllerName, storeId);
            return HttpRequestHelper.GetUrlResults<FileManager>(url);

        }

        public StorePagedList<FileManager> GetImagesByStoreId(int storeId, int page, int pageSize)
        {
            SetCache();
            String parameters = string.Format("GetImagesByStoreId?storeId={0}&page={1}&pageSize={2}", storeId, page, pageSize);
            string url = string.Format("http://{0}/api/{1}/{2}", WebServiceAddress, ApiControllerName, parameters);
            return HttpRequestHelper.GetUrlPagedResults<FileManager>(url);

        }

        public Task<List<FileManager>> GetImagesByStoreIdAsync(int storeId, bool? isActive)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetImagesByStoreIdAsync?storeId={2}&isActive={3}", WebServiceAddress, ApiControllerName, storeId, isActive);
            return HttpRequestHelper.GetUrlResultsAsync<FileManager>(url);
        }

        public List<FileManager> GetImagesByStoreId(int storeId, bool? isActive)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetImagesByStoreId?storeId={2}&isActive={3}", WebServiceAddress, ApiControllerName, storeId, isActive);
            return HttpRequestHelper.GetUrlResults<FileManager>(url);
        }

        public Task<List<FileManager>> GetStoreCarouselsAsync(int storeId, int? take)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetStoreCarouselsAsync?storeId={2}&take={3}", WebServiceAddress, ApiControllerName, storeId, take);
            return HttpRequestHelper.GetUrlResultsAsync<FileManager>(url);
        }

        public Task<StorePagedList<FileManager>> GetImagesByFileSizeAsync(int storeId, string imageSourceType, string fileSizes, int page, int pageSize)
        {
            SetCache();
            string url = string.Format("http://{0}/api/{1}/GetImagesByFileSizeAsync?storeId={2}&imageSourceType={3}&fileSizes={4}&page={5}&pageSize={6}",
                WebServiceAddress, 
                ApiControllerName, 
                storeId, imageSourceType, fileSizes, page, pageSize);
            return HttpRequestHelper.GetUrlPagedResultsAsync<FileManager>(url);
        }


        protected override void SetCache()
        {
            HttpRequestHelper.CacheMinute = CacheMinute;
            HttpRequestHelper.IsCacheEnable = IsCacheEnable;
        }
    }
}
