using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trizetto.Facets.ErCoreIfcExtensionData;
using System.Xml;
namespace HcscCommonXcba
{
    public class HcscCommonConstants
    {

        public const string RETURN_CODE_ZERO = "0";
        public const string p_DBO = "dbo";
        public const string p_EXEC_SP = "EXEC";

        //Char Constant Values
        public const char p_CH_SPACE = ' ';
        public const char p_CH_DOT = '.';
        public const char p_CH_COMMA = ',';
        public const string p_STR_QUOTEWITHCOMMA = "',";
        public const string p_STR_QUOTEWITHSEMICOLON = "';";
        public const string p_STR_SEMICOLON = ";";
        public const string p_STR_QUOTE = "'";
        public const int p_VALUE_ZERO = 0;
        public const int p_VALUE_ONE = 1;
        public const int p_VALUE_TEN = 10;


        public const string p_SP_TPZP_GL_SELECT_EXTRACT_BILL = "tpzp_gl_select_extract_bill";
        public const string P_SP_SL_GL_COLUMNS_BILL = "tpzp_gl_select_extract_columns_bill";

        public const string DATA_NODE = "/FacetsData/Collection[@name='DATA']";
        public const string ERROR_CODE_NODE = "/FacetsData/Column[@name='SQL_ERROR_CODE']";
        public const string NUM_RESULTS = "/FacetsData/Column[@name='NUM_RESULTS']";
        public const string ZERO = "0";
        public const string ONE = "1";
        public const string p_STR_XAIFM0 = "XAIFMB";
        public const string p_STR_SYS_WARN_MSG = "System Warning Message";
        public const string p_STR_PRG_WARN_MSG = "Program/Extension Warning Message";
        //Table Names
        public const string p_DB_TABLEVALUES = "DBTableValues";
        public const string p_DB_COLUMN = "DBColumn";
        //Xpath
        public const string p_XPATH_COLLECTION = "/FacetsData/Collection";
        public const string p_XPATH_COLUMN = "/FacetsData/Column";
        public const string p_XPATH_KEY_DATA = "//Column[@name='XCBA_KEY_DATA']";
        public const string p_XPATH_PZPZ_ID = "//Column[@name='PZPZ_ID']";
        public const string p_XPATH_PZAP_APP_ID = "//Column[@name='PZAP_APP_ID']";
        public const string p_XPATH_WAR = "//Collection[@name='XWARNINGS']/SubCollection[@name='WAR']";
        public const string p_XPATH_ERR_ID = "./Column[@name='ERR_ID']";
        public const string p_XPATH_ERR_CODE = "./Column[@name='ERR_CODE']";
        public const string p_XPATH_ERR_MSG_USER = "./Column[@name='ERR_MSG_USER']";
        public const string p_XPATH_ERR_MSG_SYS = "./Column[@name='ERR_MSG_SYS']";
        public const string p_XPATH_ERR_MSG_PROG = "./Column[@name='ERR_MSG_PROG']";
        public const string p_XPATH_RCML = "//Collection[@name='XRUN_CONTROLS']/SubCollection[@name='RCML']";
        public const string p_XPATH_SYMD_ID = "./Column[@name='SYMD_ID']";
        public const string p_XPATH_SYMD_MSG_CD = "./Column[@name='SYMD_MSG_CD']";
        public const string p_XPATH_RCML_COUNT = "./Column[@name='RCML_COUNT']";
        public const string p_XPATH_ERR = "//Collection[@name='XERRORS']/SubCollection[@name='ERR']";
        public const string p_XPATH_PROC_NAME = "//Column[@name='PROC_NAME']";
        public const string p_XPATH_PROCBOOK_STEP_NO = "//Column[@name='STEP_NO']";
        //datatype 

        public const string p_SPACE = " ";

        //SP

        public const string P_SP_COMMON_CONFIG = "";
        public const string P_TBL_COMMON_CONFIG = "hcst_xcba_job_config";
        public const string p_STR_SELECT = " SELECT ";
        public const string p_STR_COLUMN_SP = " PROC_NAME ";
        public const string p_STR_FROM = " FROM ";
        public const string p_STR_WHERE = " WHERE ";
        public const string p_STR_JOB_ID = " JOB_ID ";
        public const string p_STR_STEP_NO = " STEP_NO ";
        public const string p_STR_EQUAL = " = ";
        public const string p_STR_AND = " AND ";
        public const string p_STR_PZPZ_ID = " @pPZPZ_ID ";
        public const string p_STR_PZPZ_APP_ID = " @pPZAP_APP_ID ";
        public const string p_STR_XCBA_ENGIN_NO = " @pXCBA_ENGIN_NO ";
        public const string p_STR_SP_STEP_NO = " @pSTEP_NO ";
        public const string p_STR_MAX_RECS_PER_QUEUE = " @pMAX_RECS_PER_QUEUE ";
        public const string p_STR_XCBA_KEY_DATA = " @pXCBA_KEY_DATA";
        //RAW GL Claim
        public const string p_SEQ = "SEQ_NO";
        public const string p_DFLTBILL = "DFLTBILLING";
        //public const string p_GL_FULL_KEY = "GL_FULL_KEY";
    }
}
