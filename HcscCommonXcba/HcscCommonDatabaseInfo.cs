using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Trizetto.Facets.ErCoreIfcExtensionData;

namespace HcscCommonXcba
{
   public class HcscCommonDatabaseInfo
    {
        private const string DB_QUERY = "declare @ldbname varchar(100)  select @ldbname =  db_name()  select @ldbname;";
        private const string CUSTOM = "custom";
        private const string XC = "xc";
        private const string STAGE = "stage";
        public const string STR_DATA_COL_DBNAME = "/FacetsData/Collection/Column[@name='COL1']";
        static XmlDocument objXmlDatabase = new XmlDocument();
        private static string strDatabaseName = string.Empty;
        private static string strFacetsCoreDbName = string.Empty;
        /// <summary>
        /// Get Facets Database Details
        /// </summary>
        /// <param name="objExtData"></param>
        /// <returns></returns>
        public static void GetFacetsDatabaseName(IFaExtensionData objExtData)
        {
            try
            {
                strDatabaseName = objExtData.GetDbRequest(DB_QUERY);
                if (strDatabaseName.Length > 0)
                {
                    objXmlDatabase.LoadXml(strDatabaseName);
                    strFacetsCoreDbName = objXmlDatabase.SelectSingleNode(STR_DATA_COL_DBNAME).InnerText;
                    FacetsCoreDatabaseName = strFacetsCoreDbName;
                    FacetsCustomDatabaseName = "Custom";
                    FacetsXcDatabaseName = strFacetsCoreDbName + XC;
                    FacetsStageDatabaseName = strFacetsCoreDbName + STAGE;
                    FacetsSecCustomDatabaseName = "CustomExtract";
                    FacetsSecStageDatabaseName = "StageExtract";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                strDatabaseName = string.Empty;

            }
        }


        /// <summary>
        /// Get Facets Core Database Name
        /// </summary>
        public static string FacetsCoreDatabaseName { get; set; }
        /// <summary>
        /// Get Facets XC Database Name
        /// </summary>
        public static string FacetsXcDatabaseName { get; set; }
        /// <summary>
        /// Get Facets Stage Database Name
        /// </summary>
        public static string FacetsStageDatabaseName { get; set; }
        /// <summary>
        /// Get Facets Custom Database Name
        /// </summary>
        public static string FacetsCustomDatabaseName { get; set; }
        /// <summary>
        /// Get Secondary Custom Database Name
        /// </summary>
        public static string FacetsSecCustomDatabaseName { get; set; }
        /// <summary>
        /// Get Secondary Stage Database Name
        /// </summary>
        public static string FacetsSecStageDatabaseName { get; set; }
    }
}
