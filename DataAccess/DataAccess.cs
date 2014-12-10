using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ClaimxWeb.BusinessObject;
using System.IO;
using System.Data.OracleClient;
using System.Data;
using WFAPI;
using System.Web;
using System.Xml.Serialization;

namespace ClaimxWeb.DataAccess
{
    public class DataAccess
    {
        public SQL DB = null;

       public DataAccess()
        {
          
           //sourik
            if (HttpContext.Current.Session["DBName"] != null)
                DB = new SQL(HttpContext.Current.Session["DBName"].ToString());
           else
                DB = new SQL(System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        }

        public DataAccess(string DBName)
       {
           DB = new SQL(DBName);
       }
       
        public string UserID = "";

        //Connection to database
        protected void ConnectDB()
        {
            if (DB.Connected() == false)
            {
                DB.Open_Connection(true);
            }
        }

        //Load the data from claimx_ct01
        public CT01 LoadBatchData(string JobNo, int stage, string userid, int nextStage)
        {
            //ConnectDB();
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_code", stage);
                param[2] = new OracleParameter("mstr_user", userid);
                param[3] = new OracleParameter("mstr_nextstage", nextStage);
                param[4] = new OracleParameter();
                param[4].ParameterName = "batchCT01";
                param[4].OracleType = OracleType.Cursor;
                param[4].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct01_batchselect", param);

                if (dt.Rows.Count > 0)
                {

                    //Creating object for ct01
                    CT01 obj_ct01 = new CT01();
                    obj_ct01.JOBNUMBER = dt.Rows[0]["ct01_jobnumber"].ToString();
                    obj_ct01.BATCHNAME = dt.Rows[0]["ct01_batchname"].ToString();
                    obj_ct01.CT01_BSTEPID = Convert.ToInt32(dt.Rows[0]["ct01_bstepid"].ToString());
                    obj_ct01.CT01_BSTATID = dt.Rows[0]["ct01_bstatid"].ToString();
                    obj_ct01.CT01_BATCHHOME = dt.Rows[0]["ct01_batchhome"].ToString();
                    obj_ct01.CT01_CDATE = Convert.ToDateTime(dt.Rows[0]["ct01_cdate"].ToString());
                    obj_ct01.CT01_RDATE = Convert.ToDateTime(dt.Rows[0]["ct01_rdate"].ToString());
                    obj_ct01.CT01_CHECKOUTBY = dt.Rows[0]["ct01_checkoutby"].ToString();
                    obj_ct01.CT01_EICN = dt.Rows[0]["ct01_eicn"].ToString();
                    obj_ct01.CT01_SICN = dt.Rows[0]["ct01_sicn"].ToString();
                    obj_ct01.CT01_NUMCLAIMS = Convert.ToInt32(dt.Rows[0]["ct01_numclaims"].ToString());
                    obj_ct01.CT01_NUMIMAGES = Convert.ToInt32(dt.Rows[0]["ct01_numimages"].ToString());
                    obj_ct01.CT01_NUMREJECTS = Convert.ToInt32(dt.Rows[0]["ct01_numrejects"].ToString());
                    obj_ct01.CT01_PROIRITY = Convert.ToInt32(dt.Rows[0]["ct01_proirity"].ToString());
                    obj_ct01.CT01_OVERLAY = dt.Rows[0]["ct01_overlay"].ToString();
                    obj_ct01.CT01_TRANSTYPE = dt.Rows[0]["ct01_transtype"].ToString();
                    obj_ct01.Delmark = dt.Rows[0]["ct01_delmark"].ToString(); ;

                    return obj_ct01;
                }
                else
                {
                    throw new IOException("No batch found");
                }

            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

        //Load the data from claimx_ct02
        public CT02 LoadImageDataSeq(string JobNo, string BatchNo, int Seq)
        {

            //ConnectDB();
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_batch", BatchNo);
                param[2] = new OracleParameter("mstr_seq", Seq);
                param[3] = new OracleParameter();
                param[3].ParameterName = "batchCT02Seq";
                param[3].OracleType = OracleType.Cursor;
                param[3].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct02_batchselectSeq", param);

                if (dt.Rows.Count > 0)
                {
                    //Creating object for ct01
                    CT02 obj_ct02 = new CT02();
                    obj_ct02.JOBNUMBER = dt.Rows[0]["ct02_jobnumber"].ToString();
                    obj_ct02.BATCHNAME = dt.Rows[0]["CT02_BATCHNAME"].ToString();
                    obj_ct02.CT02_BATCHSEQN = Convert.ToInt32(dt.Rows[0]["CT02_BATCHSEQN"].ToString());
                    obj_ct02.CT02_ICN = dt.Rows[0]["CT02_ICN"].ToString();
                    obj_ct02.CT02_FORMTYPE = dt.Rows[0]["CT02_FORMTYPE"].ToString();
                    obj_ct02.CT02_IMAGEPATH = dt.Rows[0]["CT02_IMAGEPATH"].ToString();
                    obj_ct02.CT02_CLAIMSTAT = dt.Rows[0]["CT02_CLAIMSTAT"].ToString();
                    obj_ct02.Delmark = dt.Rows[0]["CT02_DELMARK"].ToString();

                    return obj_ct02;
                }
                else
                {
                    throw new IOException("No Image found");
                }
            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

        public List<CT02> LoadImageData(string JobNo, string BatchNo, int Stage)
        {

            //ConnectDB();
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_batch", BatchNo);
                param[2] = new OracleParameter("mstr_step", Stage);
                param[3] = new OracleParameter();
                param[3].ParameterName = "batchCT02";
                param[3].OracleType = OracleType.Cursor;
                param[3].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct02_batchselect", param);

                List<CT02> objlist_CT02 = new List<CT02>();

                if (dt.Rows.Count > 0)
                {
                    //Creating object for ct01
                    for (int c = 0; c < dt.Rows.Count; c++)
                    {
                        CT02 obj_ct02 = new CT02();
                        obj_ct02.JOBNUMBER = dt.Rows[c]["ct02_jobnumber"].ToString();
                        obj_ct02.BATCHNAME = dt.Rows[c]["CT02_BATCHNAME"].ToString();
                        obj_ct02.CT02_BATCHSEQN = Convert.ToInt32(dt.Rows[c]["CT02_BATCHSEQN"].ToString());
                        obj_ct02.CT02_ICN = dt.Rows[c]["CT02_ICN"].ToString();
                        obj_ct02.CT02_FORMTYPE = dt.Rows[c]["CT02_FORMTYPE"].ToString();
                        obj_ct02.CT02_IMAGEPATH = dt.Rows[c]["CT02_IMAGEPATH"].ToString();
                        obj_ct02.CT02_CLAIMSTAT = dt.Rows[c]["CT02_CLAIMSTAT"].ToString();
                        obj_ct02.Delmark = dt.Rows[c]["CT02_DELMARK"].ToString();
                        objlist_CT02.Add(obj_ct02);
                    }
                    return objlist_CT02;


                }
                else
                {
                    throw new IOException("No Image found");
                }
            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }
        //Load the data from claimx_ct03
        public List<CT03> LoadFieldData(string JobNo, string batchName, int batchseqn)
        {
            //ConnectDB();

            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_batch", batchName);
                param[2] = new OracleParameter("mstr_seq", batchseqn);
                param[3] = new OracleParameter();
                param[3].ParameterName = "batchCT03";
                param[3].OracleType = OracleType.Cursor;
                param[3].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct03_batchselect", param);
                List<CT03> objList_CT03 = new List<CT03>();

                if (dt.Rows.Count > 0)
                {

                    for (int c = 0; c < dt.Rows.Count; c++)
                    {
                        //reating object for ct03
                        CT03 obj_ct03 = new CT03();
                        obj_ct03.JOBNUMBER = dt.Rows[c]["ct03_jobnumber"].ToString();
                        obj_ct03.BATCHNAME = dt.Rows[c]["ct03_batchname"].ToString();
                        obj_ct03.CT03_BATCHSEQN = Convert.ToInt32(dt.Rows[c]["ct03_batchseqn"].ToString());
                        obj_ct03.CT03_FIELDSEQN = Convert.ToInt32(dt.Rows[c]["CT03_FIELDSEQN"].ToString());
                        obj_ct03.CT03_FORM_FIELD_ID = dt.Rows[c]["CT03_FORM_FIELD_ID"].ToString();
                        obj_ct03.CT03_FIELD_DATA = dt.Rows[c]["ct03_field_data"].ToString();
                        obj_ct03.CT03_FIELD_NAME = dt.Rows[c]["CT03_FIELD_NAME"].ToString();
                        obj_ct03.CT03_FIELD_SECTIONNO = dt.Rows[c]["CT03_FIELD_SCREENNO"].ToString();
                        obj_ct03.CT03_FIELD_MINLENGTH = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_MINLENGTH"].ToString());
                        obj_ct03.CT03_FIELD_MAXLENGTH = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_MAXLENGTH"].ToString());
                        obj_ct03.CT03_FIELD_ENABLED = dt.Rows[c]["CT03_FIELD_ENABLED"].ToString();
                        obj_ct03.CT03_FIELD_VISIBLE = dt.Rows[c]["CT03_FIELD_VISIBLE"].ToString();
                        obj_ct03.CT03_FIELD_READONLY = dt.Rows[c]["CT03_FIELD_READONLY"].ToString();
                        obj_ct03.CT03_FIELD_TYPE = dt.Rows[c]["CT03_FIELD_TYPE"].ToString();
                        obj_ct03.CT03_FIELD_PATTERN = dt.Rows[c]["CT03_FIELD_PATTERN"].ToString();
                        obj_ct03.CT03_FIELD_REQUIRED = dt.Rows[c]["CT03_FIELD_REQUIRED"].ToString();
                        obj_ct03.CT03_FIELD_VFLAG = dt.Rows[c]["CT03_FIELD_VFLAG"].ToString();
                        obj_ct03.CT03_FIELD_LOOKUP = dt.Rows[c]["CT03_FIELD_LOOKUP"].ToString();
                        obj_ct03.CT03_FIELD_SCHEMA = dt.Rows[c]["CT03_FIELD_SCHEMA"].ToString();
                        obj_ct03.CT03_FIELD_QAFLAG = dt.Rows[c]["CT03_FIELD_QAFLAG"].ToString();
                        obj_ct03.CT03_FIELD_AOFLAG = dt.Rows[c]["CT03_FIELD_AOFLAG"].ToString();
                        obj_ct03.CT03_FIELD_IXPOS = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_IXPOS"].ToString());
                        obj_ct03.CT03_FIELD_IYPOS = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_IYPOS"].ToString());
                        obj_ct03.CT03_FIELD_IHEIGHT = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_IHEIGHT"].ToString());
                        obj_ct03.CT03_FIELD_IWIDTH = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_IWIDTH"].ToString());
                        obj_ct03.CT03_FIELD_SXPOS = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_SXPOS"].ToString());
                        obj_ct03.CT03_FIELD_SYPOS = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_SYPOS"].ToString());
                        obj_ct03.CT03_FIELD_SHEIGHT = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_SHEIGHT"].ToString());
                        obj_ct03.CT03_FIELD_SWIDTH = Convert.ToInt32(dt.Rows[c]["CT03_FIELD_SWIDTH"].ToString());
                        obj_ct03.CT03_FIELD_REJECT = dt.Rows[c]["CT03_FIELD_REJECT"].ToString();
                        obj_ct03.CT03_FIELD_FLAG = dt.Rows[c]["CT03_FIELD_FLAG"].ToString();
                        obj_ct03.Delmark = dt.Rows[c]["CT03_DELMARK"].ToString();
                        objList_CT03.Add(obj_ct03);
                    }


                    return objList_CT03;
                }
                else
                {
                    throw new IOException("No field Info found");
                }

            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

        //Load the data from claimx_ct04
        public List<CT04> LoadScreenData(string JobNo, string batchName, int batchseqn)
        {

            //ConnectDB();
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_batch", batchName);
                param[2] = new OracleParameter("mstr_seq", batchseqn);
                param[3] = new OracleParameter();
                param[3].ParameterName = "batchCT04";
                param[3].OracleType = OracleType.Cursor;
                param[3].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct04_batchselect", param);
                List<CT04> dtobj_CT04 = new List<CT04>();

                if (dt.Rows.Count > 0)
                {

                    for (int c = 0; c < dt.Rows.Count; c++)
                    {
                        //Creating object for ct03
                        CT04 obj_ct04 = new CT04();
                        obj_ct04.JOBNUMBER = dt.Rows[c]["ct04_jobnumber"].ToString();
                        obj_ct04.BATCHNAME = dt.Rows[c]["ct04_batchname"].ToString();
                        obj_ct04.CT04_BATCHSEQN = Convert.ToInt32(dt.Rows[c]["ct04_batchseqn"].ToString());
                        obj_ct04.CT04_SCREEN_NO = dt.Rows[c]["CT04_SCREEN_NO"].ToString();
                        obj_ct04.CT04_SCREEN_NAME = dt.Rows[c]["CT04_SCREEN_NAME"].ToString();
                        obj_ct04.CT04_SCREEN_ORDER = Convert.ToInt32(dt.Rows[c]["CT04_SCREEN_ORDER"].ToString());
                        obj_ct04.CT04_WINDOW_X = Convert.ToInt32(dt.Rows[c]["CT04_WINDOW_X"].ToString());
                        obj_ct04.CT04_WINDOW_Y = Convert.ToInt32(dt.Rows[c]["CT04_WINDOW_Y"].ToString());
                        obj_ct04.CT04_WINDOW_HEIGHT = Convert.ToInt32(dt.Rows[c]["CT04_WINDOW_H"].ToString());
                        obj_ct04.CT04_WINDOW_WIDTH = Convert.ToInt32(dt.Rows[c]["CT04_WINDOW_W"].ToString());
                        obj_ct04.CT04_DATA_X = Convert.ToInt32(dt.Rows[c]["CT04_DCP_X"].ToString());
                        obj_ct04.CT04_DATA_Y = Convert.ToInt32(dt.Rows[c]["CT04_DCP_Y"].ToString());
                        obj_ct04.CT04_SCREEN_FLAG = dt.Rows[c]["CT04_SCREEN_FLAG"].ToString();
                        obj_ct04.CT04_DATA_HEIGHT = Convert.ToInt32(dt.Rows[c]["CT04_DCP_H"].ToString());
                        obj_ct04.CT04_DATA_WIDTH = Convert.ToInt32(dt.Rows[c]["CT04_DCP_W"].ToString());
                        obj_ct04.CT04_IMAGE_X = Convert.ToInt32(dt.Rows[c]["CT04_IVP_X"].ToString());
                        obj_ct04.CT04_IMAGE_Y = Convert.ToInt32(dt.Rows[c]["CT04_IVP_Y"].ToString());
                        obj_ct04.CT04_IMAGE_HEIGHT = Convert.ToInt32(dt.Rows[c]["CT04_IVP_H"].ToString());
                        obj_ct04.CT04_IMAGE_WIDTH = Convert.ToInt32(dt.Rows[c]["CT04_IVP_W"].ToString());
                        obj_ct04.Delmark = dt.Rows[c]["CT04_DELMARK"].ToString(); ;
                        dtobj_CT04.Add(obj_ct04);
                    }


                    return dtobj_CT04;
                }
                else
                {
                    throw new IOException("No Screen information found");
                }

            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

        // Added By SOURIK
        //****************************************************
        public bool WriteXMLCT03(IList<CT03> Data)
        {
            string BatchName = Data[0].BATCHNAME;
            int Seq = Data[0].CT03_BATCHSEQN;

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName));

            string flname = BatchName + "_CT03_" + Seq.ToString() + ".XML";
            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName + "\\" + flname), Serealize<CT03>(ref Data));
            return true;
        }
        
        public bool WriteXMLCT02(IList<CT02> Data)
        {
            string BatchName = Data[0].BATCHNAME;
            

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName)))
                return false;

            string flname = BatchName + "_CT02" + ".XML";
            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath(UserID + "\\" + BatchName + "\\" + flname), Serealize<CT02>(ref Data));
            return true;
        }

        public bool WriteXMLCT01(IList<CT01> Data)
        {
            string BatchName = Data[0].BATCHNAME;

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName)))
                return false;


            string flname = BatchName + "_CT01" + ".XML";
            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("KEY\\" + UserID + "\\" + BatchName + "\\" + flname), Serealize<CT01>(ref Data));
            return true;
        }

        private string Serealize<T>(ref IList<T> items)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(List<T>), new Type[] { typeof(T) });
            serializer.Serialize(stringwriter, items);
            return stringwriter.ToString();
        }

        public List<T> DeSerealize<T>(string FullFileName)
        {
            try
            {
                var stringReader = new System.IO.StringReader(System.IO.File.ReadAllText(FullFileName));
                var serializer = new XmlSerializer(typeof(List<T>), new Type[] { typeof(T) });
                return (List<T>)serializer.Deserialize(stringReader);
            }
            catch
            {
                throw;
            }
        }
        
        public bool SaveCT01(IList<CT01> CT01Data)
        {
            foreach (CT01 SingleRecord in CT01Data)
            {
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("mstr_job", SingleRecord.JOBNUMBER);
                param[1] = new OracleParameter("mstr_batch", SingleRecord.BATCHNAME);
                param[2] = new OracleParameter("mstr_bstepid", SingleRecord.CT01_BSTEPID);
                param[3] = new OracleParameter("mstr_CDate", SingleRecord.CT01_CDATE);
                param[4] = new OracleParameter("mstr_checkout", SingleRecord.CT01_CHECKOUTBY);
                
                param[5] = new OracleParameter();

                param[5].ParameterName = "batchCT01";
                param[5].OracleType = OracleType.Cursor;
                param[5].Direction = ParameterDirection.Output;

                int res = DB.ExeSpNonQuery("ct01_DataSave", param);
                if (res != 0)
                    return false;
            }
            return false;
        }
        public bool SaveCT02(IList<CT02> CT02Data)
        {
            foreach (CT02 SingleRecord in CT02Data)
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("mstr_job", SingleRecord.JOBNUMBER);
                param[1] = new OracleParameter("mstr_batch", SingleRecord.BATCHNAME);
                param[2] = new OracleParameter("mstr_seq", SingleRecord.CT02_BATCHSEQN);
                param[3] = new OracleParameter("mstr_fld_seq", SingleRecord.CT02_CLAIMSTAT);
               

                param[4] = new OracleParameter();

                param[4].ParameterName = "batchCT02";
                param[4].OracleType = OracleType.Cursor;
                param[4].Direction = ParameterDirection.Output;

                int res = DB.ExeSpNonQuery("ct02_DataSave", param);
                if (res != 0)
                    return false;
            }
            return false;
        }
        public bool SaveCT03(IList<CT03> CT03Data)
        {
            foreach (CT03 SingleRecord in CT03Data)
            {
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("mstr_job", SingleRecord.JOBNUMBER);
                param[1] = new OracleParameter("mstr_batch", SingleRecord.BATCHNAME);
                param[2] = new OracleParameter("mstr_seq", SingleRecord.CT03_BATCHSEQN);
                param[3] = new OracleParameter("mstr_frm_fld_id", SingleRecord.CT03_FORM_FIELD_ID);
                param[4] = new OracleParameter("mstr_fld_data", SingleRecord.CT03_FIELD_DATA);
                param[5] = new OracleParameter("mstr_fld_rej", SingleRecord.CT03_FIELD_REJECT);
                
                param[6] = new OracleParameter();

                param[6].ParameterName = "batchCT03";
                param[6].OracleType = OracleType.Cursor;
                param[6].Direction = ParameterDirection.Output;

                int res = DB.ExeSpNonQuery("ct03_DataSave", param);
                if (res != 0)
                    return false;
            }
            return false;
        }


        public IList<CT01> LoadALLBatchData(string JobNo, int stage, string userid, int nextStage)
        {
            //ConnectDB();

            IList<CT01> CT01List = new List<CT01>();
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("mstr_job", JobNo);
                param[1] = new OracleParameter("mstr_code", stage);
                param[2] = new OracleParameter("mstr_user", userid);
                param[3] = new OracleParameter("mstr_nextstage", nextStage);
                param[4] = new OracleParameter();
                param[4].ParameterName = "batchCT01";
                param[4].OracleType = OracleType.Cursor;
                param[4].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("ct01_batchselect", param);

                for(int i=0;i<dt.Rows.Count;i++)
                {

                    //Creating object for ct01
                    CT01 obj_ct01 = new CT01();
                    obj_ct01.JOBNUMBER = dt.Rows[i]["ct01_jobnumber"].ToString();
                    obj_ct01.BATCHNAME = dt.Rows[i]["ct01_batchname"].ToString();
                    obj_ct01.CT01_BSTEPID = Convert.ToInt32(dt.Rows[i]["ct01_bstepid"].ToString());
                    obj_ct01.CT01_BSTATID = dt.Rows[i]["ct01_bstatid"].ToString();
                    obj_ct01.CT01_BATCHHOME = dt.Rows[i]["ct01_batchhome"].ToString();
                    obj_ct01.CT01_CDATE = Convert.ToDateTime(dt.Rows[i]["ct01_cdate"].ToString());
                    obj_ct01.CT01_RDATE = Convert.ToDateTime(dt.Rows[i]["ct01_rdate"].ToString());
                    obj_ct01.CT01_CHECKOUTBY = dt.Rows[i]["ct01_checkoutby"].ToString();
                    obj_ct01.CT01_EICN = dt.Rows[i]["ct01_eicn"].ToString();
                    obj_ct01.CT01_SICN = dt.Rows[i]["ct01_sicn"].ToString();
                    obj_ct01.CT01_NUMCLAIMS = Convert.ToInt32(dt.Rows[i]["ct01_numclaims"].ToString());
                    obj_ct01.CT01_NUMIMAGES = Convert.ToInt32(dt.Rows[i]["ct01_numimages"].ToString());
                    obj_ct01.CT01_NUMREJECTS = Convert.ToInt32(dt.Rows[i]["ct01_numrejects"].ToString());
                    obj_ct01.CT01_PROIRITY = Convert.ToInt32(dt.Rows[i]["ct01_proirity"].ToString());
                    obj_ct01.CT01_OVERLAY = dt.Rows[i]["ct01_overlay"].ToString();
                    obj_ct01.CT01_TRANSTYPE = dt.Rows[i]["ct01_transtype"].ToString();
                    obj_ct01.Delmark = "N";

                    CT01List.Add(obj_ct01);
                    
                }
                return CT01List;

            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

        public CL07 DoLogin(string uid, string pwd)
        {
            CL07 UserData = null;
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[3];
                param[0] = new OracleParameter("USERID", uid);
                param[1] = new OracleParameter("PWD", pwd);
               
                param[2] = new OracleParameter();
                param[2].ParameterName = "RES";
                param[2].OracleType = OracleType.Cursor;
                param[2].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("DBS_LOGIN", param);

                if (dt.Rows.Count > 0)
                {
                    UserData = new CL07();
                    UserData.CL07_USER_ID = dt.Rows[0]["CL07_USER_ID"].ToString();
                    UserData.CL07_USER_NAME = dt.Rows[0]["CL07_USER_NAME"].ToString();
                    
                }
            }
            catch(Exception ex)
            {
            }
            return UserData;
        }

        public IList<CL01> GetJobListByUser(string uid)
        {
            IList<CL01> JobData = null;
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("USERID", uid);
               

                param[1] = new OracleParameter();
                param[1].ParameterName = "RES";
                param[1].OracleType = OracleType.Cursor;
                param[1].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("DBS_GET_JOB_BY_USER", param);
                if(dt.Rows.Count>0)
                {
                    JobData = new List<CL01>();
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CL01 Job = new CL01();

                    Job.CL01_JOB_NO = dt.Rows[i]["CL01_JOB_NO"].ToString();
                    Job.CL01_JOB_DESC = dt.Rows[i]["CL01_JOB_DESC"].ToString();
                    Job.CL01_CLIENT_JOB_NO = dt.Rows[i]["CL01_CLIENT_JOB_NO"].ToString();
                    Job.CL01_CLIENT_ID = dt.Rows[i]["CL01_CLIENT_ID"].ToString();
                    Job.CL01_STAT = dt.Rows[i]["CL01_STAT"].ToString();
                    Job.CL01_DATABASE = dt.Rows[i]["CL01_DATABASE"].ToString();

                    JobData.Add(Job);

                }
            }
            catch (Exception ex)
            {
            }
            return JobData;
        }

        public string GetDB(string jobnumber)
        {
            string DBName = "";
            try
            {
                //Calling stored procedure to load batch attribute list
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("JOBID", jobnumber);

                param[1] = new OracleParameter();
                param[1].ParameterName = "RES";
                param[1].OracleType = OracleType.Cursor;
                param[1].Direction = ParameterDirection.Output;
                DataTable dt = DB.ExeSpSelect("DBS_GETDB", param);
                if (dt.Rows.Count > 0)
                {
                    DBName = dt.Rows[0]["CL04_DATABASE"].ToString();
                }
            }
            catch(Exception x)
            { }
            return DBName;
        }

        //****************************************************
    }
}
