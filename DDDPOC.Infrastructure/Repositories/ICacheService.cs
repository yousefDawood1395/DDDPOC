﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Infrastructure.Repositories
{
    public interface ICacheService
    {
       public T GetData<T>(string key);
       public bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        public object RemoveData(string key);
    }
}
