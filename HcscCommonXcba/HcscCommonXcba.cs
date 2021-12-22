using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Trizetto.Facets.ErCoreIfcExtensionData;
using System.Xml;
using System.Data;
using System.Diagnostics;

namespace HcscCommonXcba
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class HcscCommonXcba
    {
                private string _facetsData = "<FacetsData>" +
        "<!-- General rule: -->" +
        "<Collection name='XERRORS' type='Group'>" +
        "	<SubCollection name='ERR' type='Row'>" +
        "		<!--ERR_ID -> SYMD_ID=AppSymdId  -->" +
        "		<Column name='ERR_ID'></Column>" +
        "		<!--ERR_CODE -> SYMD_MSG_CD  -->" +
        "		<Column name='ERR_CODE'></Column>" +
        "		<!--ERR_MSG_USER -> SYMD_LONG_DESC  -->" +
        "		<Column name='ERR_MSG_USER'></Column>" +
        "		<!--ERR_MSG_SYS -> SYMD_SHORT_DESC  -->" +
        "		<Column name='ERR_MSG_SYS'></Column>" +
        "		<!--ERR_MSG_PROG -> from the code unrelated to SYMD registration -->" +
        "		<Column name='ERR_MSG_PROG'></Column>" +
        "		<!-- multiple if needed! -->" +
        "		<SubCollection name='ERR_DATA' type='Row'>" +
        "			<Column name='ERR_DATA_ID'></Column>" +
        "			<Column name='ERR_DATA_VALUE'></Column>" +
        "		</SubCollection>" +
        "	</SubCollection>" +
        "</Collection>" +
        "<Collection name='XWARNINGS' type='Group'>" +
        "	<SubCollection name='WAR' type='Row'>" +
        "		<Column name='ERR_ID'></Column>" +
        "		<Column name='ERR_CODE'></Column>" +
        "		<Column name='ERR_MSG_USER'></Column>" +
        "		<Column name='ERR_MSG_SYS'></Column>" +
        "		<Column name='ERR_MSG_PROG'></Column>" +
        "	</SubCollection>" +
        "</Collection>" +
        "<Collection name='XRUN_CONTROLS' type='Group'>" +
        "	<SubCollection name='RCML' type='Row'>" +
        "		<Column name='SYMD_ID'></Column>" +
        "		<Column name='SYMD_MSG_CD'></Column>" +
        "		<Column name='RCML_COUNT'></Column>" +
        "	</SubCollection>" +
        "</Collection>" +
        "</FacetsData>";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pobjExtensionData"></param>
        /// <param name="pstrCompId"></param>
        /// <returns></returns>

        public bool CommonCustomXcba (IFaExtensionData pExtensionData, string pCompID)
        {
            bool returnCode = true;
            string eventlogSource = "HcscCommonXcba";
            string logName = "Application";

            string mstrPzpzId = "";
            string mstrPzapAppId = "";
            string mstrStepNo = "";
            string mstrSPName = "";
            string facetsReturnCode = "";
            string msgCommon = "Common Code pCompID =" + pCompID;
            string sampleGetdata = "";
            // XCBA_KEY_DATA
            string xcbaKeyData = "";
            string mstrXcbaEngine = "";
            string mstrMaxRecsPerQueue = "";
            XmlDocument xcbaKeyDataXml = new XmlDocument();

            // Capture the result of the GetDbRequest()
            string getDbRequestResponse = "";
            string getDbRequestResponseSelect = "";
            XmlDocument getDbRequestResponseXml = new XmlDocument();
            XmlDocument getDbRequestResponseSelectXml = new XmlDocument();
           

            // The XML Document used as a parameter of SetData()
            XmlDocument facetsDataXml = new XmlDocument();
            DataSet dsExtractClaimResult = new DataSet();
            try
            {
                // Get the data from Facet in the XML string, and extract the current XCBA_KEY_DATA
                // Note: The XCBA Prime Work 
                xcbaKeyDataXml.LoadXml(pExtensionData.GetData(""));
                xcbaKeyData = xcbaKeyDataXml.SelectSingleNode(HcscCommonConstants.p_XPATH_KEY_DATA).InnerText; //XCBA_KEY_DATA
                mstrPzpzId = xcbaKeyDataXml.SelectSingleNode(HcscCommonConstants.p_XPATH_PZPZ_ID).InnerText;
                mstrPzapAppId = xcbaKeyDataXml.SelectSingleNode(HcscCommonConstants.p_XPATH_PZAP_APP_ID).InnerText;
                mstrStepNo = xcbaKeyDataXml.SelectSingleNode(HcscCommonConstants.p_XPATH_PROCBOOK_STEP_NO).InnerText;
                sampleGetdata = xcbaKeyDataXml.SelectNodes("/FacetsData/Column").ToString();
                string msgNode = "";
                foreach(XmlNode n in xcbaKeyDataXml.ChildNodes)
                {

                    msgNode = n.Name + "=" + n.Value + Environment.NewLine;
                }
                EventLog.WriteEntry(eventlogSource, msgNode, EventLogEntryType.Error, 256);
                //Get the Facets Database names

                HcscCommonDatabaseInfo.GetFacetsDatabaseName(pExtensionData);
              
              // Get stored procedure name from common config table
                StringBuilder sbTblSql = new StringBuilder();
                sbTblSql.Append(HcscCommonConstants.p_STR_SELECT);
                sbTblSql.Append(HcscCommonConstants.p_STR_COLUMN_SP);
                sbTblSql.Append(HcscCommonConstants.p_STR_FROM);
                sbTblSql.Append(HcscCommonDatabaseInfo.FacetsCustomDatabaseName);
                sbTblSql.Append(HcscCommonConstants.p_CH_DOT);
                sbTblSql.Append(HcscCommonConstants.p_DBO);
                sbTblSql.Append(HcscCommonConstants.p_CH_DOT);
                sbTblSql.Append(HcscCommonConstants.P_TBL_COMMON_CONFIG);
                sbTblSql.Append(HcscCommonConstants.p_STR_WHERE);
                sbTblSql.Append(HcscCommonConstants.p_STR_JOB_ID);
                sbTblSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbTblSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbTblSql.Append(mstrPzapAppId);
                sbTblSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbTblSql.Append(HcscCommonConstants.p_STR_AND);
                sbTblSql.Append(HcscCommonConstants.p_STR_STEP_NO);
                sbTblSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbTblSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbTblSql.Append(mstrStepNo);
                sbTblSql.Append(HcscCommonConstants.p_STR_QUOTEWITHSEMICOLON);

                getDbRequestResponseSelect = pExtensionData.GetDbRequest(sbTblSql.ToString());
                // Evaluate the return code
                getDbRequestResponseSelectXml.LoadXml(getDbRequestResponseSelect);
                mstrSPName = getDbRequestResponseSelectXml.SelectSingleNode(HcscCommonConstants.p_XPATH_PROC_NAME).InnerText;


                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(HcscCommonConstants.p_EXEC_SP);
                sbSql.Append(HcscCommonConstants.p_CH_SPACE);
                sbSql.Append(HcscCommonDatabaseInfo.FacetsCustomDatabaseName);
                sbSql.Append(HcscCommonConstants.p_CH_DOT);
                sbSql.Append(HcscCommonConstants.p_DBO);
                sbSql.Append(HcscCommonConstants.p_CH_DOT);
                sbSql.Append(mstrSPName);
                sbSql.Append(HcscCommonConstants.p_CH_SPACE);
                sbSql.Append(HcscCommonConstants.p_STR_PZPZ_ID);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(mstrPzpzId);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTEWITHCOMMA);
                sbSql.Append(HcscCommonConstants.p_STR_PZPZ_APP_ID);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(mstrPzapAppId);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTEWITHCOMMA);
                sbSql.Append(HcscCommonConstants.p_STR_SP_STEP_NO);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(mstrStepNo);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTEWITHCOMMA);
                sbSql.Append(HcscCommonConstants.p_STR_XCBA_ENGIN_NO);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(mstrXcbaEngine);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTEWITHCOMMA);
                sbSql.Append(HcscCommonConstants.p_STR_MAX_RECS_PER_QUEUE);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(mstrMaxRecsPerQueue);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTEWITHCOMMA);
                sbSql.Append(HcscCommonConstants.p_STR_XCBA_KEY_DATA);
                sbSql.Append(HcscCommonConstants.p_STR_EQUAL);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);
                sbSql.Append(xcbaKeyData);
                sbSql.Append(HcscCommonConstants.p_STR_QUOTE);

                sbSql.Append(HcscCommonConstants.p_STR_SEMICOLON);

                //Execute the stored procedure for cust process in Multi Engine mode by passing seq_no
                  getDbRequestResponse = pExtensionData.GetDbRequest(sbSql.ToString());
              //  getDbRequestResponseXml.LoadXml(getDbRequestResponse);

                //facetsReturnCode = getDbRequestResponseXml.SelectSingleNode("//Column[@name='RETURN_CODE']").InnerText;
                msgCommon = msgCommon + " STEP_NO= " + mstrStepNo + " XCBA_KEY_DATA = " + xcbaKeyData + " PZPZ_ID= " + mstrPzpzId + " PZAP_APP_ID= " + mstrPzapAppId + "  sbsql =" + sbSql.ToString() + " getDbRequestResponse= " + getDbRequestResponse + " sbTblSql= "+ sbTblSql.ToString()+ " mstrSPName= " + mstrSPName+ " sampleGetdata ="+ sampleGetdata;

                EventLog.WriteEntry(eventlogSource, msgCommon, EventLogEntryType.Error, 256);


                // Load the default XML
                facetsDataXml.LoadXml(_facetsData);

                //Check if member record exists
                XmlNode xwarningNode = facetsDataXml.SelectSingleNode("//Collection[@name='XWARNINGS']/SubCollection[@name='WAR']");

                xwarningNode.SelectSingleNode("./Column[@name='ERR_ID']").InnerText = "XBDEMO";
                xwarningNode.SelectSingleNode("./Column[@name='ERR_CODE']").InnerText = "20210921";
                xwarningNode.SelectSingleNode("./Column[@name='ERR_MSG_USER']").InnerText = "User Defined Warning Message";
                xwarningNode.SelectSingleNode("./Column[@name='ERR_MSG_SYS']").InnerText = "System Warning Message";
                xwarningNode.SelectSingleNode("./Column[@name='ERR_MSG_PROG']").InnerText = "Program/Extension Warning Message";

                // Update run control
                XmlNode runControlXmlNode = facetsDataXml.SelectSingleNode("//Collection[@name='XRUN_CONTROLS']/SubCollection[@name='RCML']");
                runControlXmlNode.SelectSingleNode("./Column[@name='SYMD_ID']").InnerText = "XBDEMO";
                runControlXmlNode.SelectSingleNode("./Column[@name='SYMD_MSG_CD']").InnerText = "20210900";
                runControlXmlNode.SelectSingleNode("./Column[@name='RCML_COUNT']").InnerText = "1";

                // No error. Remove ERR node
                XmlNode xerrorsNode = facetsDataXml.SelectSingleNode("//Collection[@name='XERRORS']/SubCollection[@name='ERR']");
                xerrorsNode.ParentNode.RemoveChild(xerrorsNode);
                pExtensionData.SetData("XRUNTIME", facetsDataXml.OuterXml);
            }
            catch (Exception ex)
            {


                if (!EventLog.SourceExists(eventlogSource))
                    EventLog.CreateEventSource(eventlogSource, logName);

                // Report an error
                EventLog.WriteEntry(eventlogSource, ex.Message, EventLogEntryType.Error, 256);
                returnCode = false;
            }

            finally
            {
                //Release the COM object
                Marshal.ReleaseComObject(pExtensionData);
            }


            return returnCode;
        }
    }
}
