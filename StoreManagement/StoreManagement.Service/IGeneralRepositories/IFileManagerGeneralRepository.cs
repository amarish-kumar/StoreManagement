﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data.Entities;
using StoreManagement.Data.Paging;

namespace StoreManagement.Service.IGeneralRepositories
{
    public interface IFileManagerGeneralRepository : IGeneralRepository
    {
 
        List<FileManager> GetFilesByStoreId(int storeId);

        FileManager GetFilesByGoogleImageId(String googleImageId);
        List<FileManager> GetFilesByGoogleImageIdArray(String[] googleImageId);
        FileManager GetFilesById(int id);
        List<FileManager> GetStoreCarousels(int storeId);
        StorePagedList<FileManager> GetImagesByStoreId(int storeId, int page, int pageSize);
        Task<List<FileManager>> GetImagesByStoreIdAsync(int storeId, bool? isActive);
        List<FileManager> GetImagesByStoreId(int storeId, bool? isActive);
        Task<List<FileManager>> GetStoreCarouselsAsync(int storeId, int ? take);
        Task<StorePagedList<FileManager>> GetImagesByFileSizeAsync(int storeId, String imageSourceType, String fileSizes, int page, int pageSize);
    }
}
