using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webbanhang.Context;

namespace Webbanhang.Models
{
    public class ProSearch
    {
        trung_webEntities objtrung_webEntities = new trung_webEntities();
        public List<Product> SearchByKey(string key)
        {
            return objtrung_webEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
    }
}