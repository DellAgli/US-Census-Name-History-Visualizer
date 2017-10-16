using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.App_Start.Database;

namespace WebApplication.App_Start.Database
{
    public class NamesProjectDatabaseConnection : DatabaseConnection
    {
        public NamesProjectDatabaseConnection(string strConnectionString) : base(strConnectionString)
        {
        }

        public DataClassCollection GetTable()
        {
            return this.ExecuteSP("GetNamesTable", new string[0], new DataClass());
        }

        public DataClassCollection GetNames(String strSearch = "", int intMethod = 0)
        {
            DataClass dcSearch = new DataClass();
            String strProc = intMethod == 1 ? "spGetNamesListByPronounciation" : "spGetNamesList";
            dcSearch["search"] = strSearch;
            return this.ExecuteSP(strProc, new string[1] { "search"}, dcSearch);
        }
        public DataClassCollection GetYears()
        {
            return this.ExecuteSP("spGetYearsList", new string[0], new DataClass());
        }
        public DataClassCollection GetStates()
        {
            return this.ExecuteSP("spGetStatesList", new string[0], new DataClass());
        }

        public DataClass GetGraphData(String strStatesQuery, String strNamesQuery)
        {
            DataClass dcSearch = new DataClass();
            dcSearch["namesarray"] = strNamesQuery;
            dcSearch["statesarray"] = strStatesQuery;
            DataClassCollection dccolData = this.ExecuteSP("spSearchNames", new string[2] { "namesarray", "statesarray" }, dcSearch);

            DataClass dcReturn = new DataClass();
            int[] aintYears = new int[dccolData.Count];
            int[] aintFemale = new int[dccolData.Count];
            int[] aintMale = new int[dccolData.Count];
            int[] aintPercentYears = new int[dccolData.Count];
            double[] adblPercentM = new double[dccolData.Count];
            int intPercentCount = 0;

            for (int i = 0; i < dccolData.Count;i++)
            {
                int intYear = Int32.Parse(dccolData[i]["Year"].ToString());
                int intMale = Int32.Parse(dccolData[i]["Males"].ToString());
                int intFemale = Int32.Parse(dccolData[i]["Females"].ToString());
                aintYears[i] = intYear;
                aintMale[i] = intMale;
                aintFemale[i] = intFemale;

                if(intFemale+intMale > 0)
                {
                    aintPercentYears[intPercentCount] = intYear;
                    adblPercentM[intPercentCount] = ((double)intMale) / ((double)(intFemale+intMale)) * 100.0;
                    intPercentCount++;
                }
            }

            Array.Resize(ref aintPercentYears, intPercentCount);
            Array.Resize(ref adblPercentM, intPercentCount);

            dcReturn["Years"] = aintYears;
            dcReturn["Males"] = aintMale;
            dcReturn["Females"] = aintFemale;
            dcReturn["YearsPercentage"] = aintPercentYears;
            dcReturn["MalesPercentage"] = adblPercentM;
            return dcReturn;
            }

        public void LoadNamesTable()
        {
            const string strProcName = "spPutName";
            string[] astrParams = new string[5] { "State", "Gender", "Year", "Name", "Number" };
            using (TextFieldParser parser = new TextFieldParser(@"d:\my files\documents\visual studio 2017\Projects\NamesProject\WebApplication1\App_Start\Database\Data\raw.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadLine();
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    DataClass dcParams = new DataClass();
                    dcParams[astrParams[0]] = fields[0];
                    dcParams[astrParams[1]] = fields[1];
                    dcParams[astrParams[2]] = fields[2];
                    dcParams[astrParams[3]] = fields[3];
                    dcParams[astrParams[4]] = Int32.Parse(fields[4]);

                    ExecuteSP(strProcName, astrParams, dcParams);
                }
            }
        }

        public void LoadPronounciationTable()
        {
            const string strProcName = "spPutPronounciation";
            string[] astrParams = new string[2] { "Name", "Pronounciation" };
            Regex rgx = new Regex("[^a-zA-Z]");
            using (TextFieldParser parser = new TextFieldParser(@"d:\my files\documents\visual studio 2017\Projects\NamesProject\WebApplication1\App_Start\Database\Data\cmudict.dict.txt"))
            {
                while (!parser.EndOfData)
                {
                    try
                    {
                        //Process row
                        string line = parser.ReadLine();
                        string name = rgx.Replace(line.Split(' ')[0], "");
                        name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
                        string pronounciation = String.Join(" ", line.Split(' ').Skip(1));
                        DataClass dcParams = new DataClass();
                        dcParams[astrParams[0]] = name;
                        dcParams[astrParams[1]] = pronounciation;

                        ExecuteSP(strProcName, astrParams, dcParams);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }

                }
            }
        }
    }
}