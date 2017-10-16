using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataClass
/// </summary>
public class DataClass
{
    private Dictionary<string, object> dictData;
    private Dictionary<string, object> Data
    {
        get
        {
            if (dictData == null)
                dictData = new Dictionary<string, object>();
            return dictData;
        }
    }

    public object this[String key]
    {
        get
        {
            if(!dictData.ContainsKey(key))
                return null;
            return dictData[key];
        }
        set
        {
            Data[key] = value;
        }
    }

    public String JSONObject
    {
        get
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Data);
        }
        
    }

    public Dictionary<string,object> Dictionary
    {
        get { return this.dictData; }
    }

    public String GenerateSPParams(IEnumerable<string> estrParams)
    {
        const String strParamMapping = "@{0} = {1} ";
        List<String> lstParamMapping = new List<string>();
        foreach (String strParam in estrParams)
        {
            lstParamMapping.Add(String.Format(strParamMapping, strParam, FormatParameter(strParam)));
        }

        return String.Join(", ",lstParamMapping);
    }

    private String FormatParameter(String strParam)
    {
        if (!Data.ContainsKey(strParam))
            return "NULL";
        object objValue = Data[strParam];
        switch (Type.GetTypeCode(objValue.GetType()))
        {
            case TypeCode.String:
            case TypeCode.Char:
                return "'" + objValue.ToString() + "'";
            case TypeCode.Boolean:
                return Convert.ToBoolean(objValue) ? "1" : "0";
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Byte:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return objValue.ToString();
            case TypeCode.DateTime:
                return Convert.ToDateTime(objValue).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            default:
                return "NULL";
        }
    }

    
}