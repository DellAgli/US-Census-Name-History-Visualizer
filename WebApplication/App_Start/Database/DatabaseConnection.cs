using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebApplication1.App_Start.Database;

public abstract class DatabaseConnection
{
    private String ConnectionString
    {
        get;set;
    }

    public DatabaseConnection(string strConnectionString)
    {
        ConnectionString = strConnectionString;
    }

    protected DataClassCollection ExecuteSP(String strSP, String[] astrParameterNames, DataClass dcParameters)
    {
        List<DataClass> lstdictReturn = new List<DataClass>();
        DataClassCollection dcc = new DataClassCollection();
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string queryStatement = GenerateSPQueryString(strSP, astrParameterNames, dcParameters);

            using (SqlCommand cmd = new SqlCommand(queryStatement, con))
            {
                DataTable table = new DataTable();

                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 300;
                con.Open();
                dap.Fill(table);
                con.Close();

                foreach(DataRow r in table.Rows)
                {
                    DataClass dcRow = new DataClass();
                    foreach(DataColumn c in table.Columns)
                    {
                        dcRow[c.ColumnName] = r[c.ColumnName].ToString().TrimEnd();
                    }
                    dcc.Add(dcRow);
                }

            }
        }


        return dcc;
    }

    protected String GenerateSPQueryString(String strSP, String[] astrParameterNames, DataClass dcParameterDictionary)
    {
        const String strTemplate = "Execute {0} {1}";
        return String.Format(strTemplate,strSP, String.Join(", ", dcParameterDictionary.GenerateSPParams(astrParameterNames)));
    }

}