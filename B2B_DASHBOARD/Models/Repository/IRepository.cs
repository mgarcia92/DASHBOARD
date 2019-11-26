using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models.Repository
{
    public interface IRepository<T>
    {
        int Add(T Object);
        T Find<T>(string id, string IdName) where T : class, new();
        int Delete(string id, string IdName);
        int Update(T Object);
        List<T> List<T>() where T : class, new();
    }
}