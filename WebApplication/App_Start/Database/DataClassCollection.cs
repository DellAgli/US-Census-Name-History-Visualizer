using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Start.Database
{
    public class DataClassCollection : Collection<DataClass>
    {

        public String JSONArray
        {
            get
            {
                List<object> lobjObjects = new List<object>();
                foreach(DataClass dcObject in this)
                {
                    lobjObjects.Add(dcObject.Dictionary);
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(lobjObjects.ToArray());
            }
        }
    }
}