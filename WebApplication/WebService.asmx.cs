using System.Configuration;
using System.Web.Services;
using WebApplication.App_Start.Database;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        private static NamesProjectDatabaseConnection DB => new NamesProjectDatabaseConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString);

        [WebMethod]
        public string LoadData()
        {
            //DB.LoadTable();
            return "{'message':'This function has been disabled'}";
        }

        [WebMethod]
        public string GetNames()
        {
            return DB.GetNames().JSONArray;
        }
        [WebMethod]
        public string GetStates()
        {
            return DB.GetStates().JSONArray;
        }
        [WebMethod]
        public string GetYears()
        {
            return DB.GetYears().JSONArray;
        }
        [WebMethod]
        public string SearchNames(string strSearch,int intMethod)
        {
            return DB.GetNames(strSearch,intMethod).JSONArray;
        }
        [WebMethod]
        public string GetGraphData(string strNamesArray, string strStatesArray)
        {
            return DB.GetGraphData(strStatesArray, strNamesArray).JSONObject;
        }

        [WebMethod]
        public string Deploy()
        {
            DB.LoadNamesTable();
            DB.LoadPronounciationTable();
            return "";
        }
    }
}
