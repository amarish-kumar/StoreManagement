﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data.Entities;
using StoreManagement.Service.IGeneralRepositories;

namespace StoreManagement.Service.Repositories.Interfaces
{

    public interface IActivityRepository : IBaseRepository<Activity, int>, IActivityGeneralRepository, IDisposable 
    {
        List<Activity> GetActivitiesByStoreId(int storeId, string search);
    }
}
