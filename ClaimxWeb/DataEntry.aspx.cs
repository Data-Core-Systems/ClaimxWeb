using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimxWeb.DataAccess;
using ClaimxWeb.BusinessObject;
using System.IO;
using System.Xml.Serialization;

namespace ClaimxWeb
{
    public partial class DataEntry : System.Web.UI.Page
    {
        #region Variables
        int mi_code = 0;
        string mstr_status = "";
        string mstr_process_type = "K";//later take this in session
        DateTime mdt_starttime;
        string mstr_userid = "202769";
        string mstr_jobno = "4352";
        string mstr_batchname = "";
        int mi_batchseqn = 0;
        string mstr_icn = "";
        string mstr_FTPIP = "";
        DataAccess.DataAccess obj_dataaccess = new DataAccess.DataAccess();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBatchInformation();
          
        }
        //Deleting and creating directory
        private void Deletefolder()
        {
            if (Directory.Exists(Server.MapPath(mstr_userid)))
            {
                Directory.Delete(Server.MapPath(mstr_userid), true);
            }
            Directory.CreateDirectory(Server.MapPath(mstr_userid));
        } 
        private void LoadBatchInformation()
        {
            try
            {
                mdt_starttime = System.DateTime.Now;

                

                if (mstr_process_type == "K")
                {
                    mi_code = 20;
		            mstr_status = "K";

                }
                int li_nextcode = 25;

                //Load Batch Information
                obj_dataaccess = new DataAccess.DataAccess();
                
                obj_dataaccess.UserID = mstr_userid;

                CT01 obj_ct01 = obj_dataaccess.LoadBatchData(mstr_jobno, mi_code, mstr_userid, li_nextcode);
                mstr_batchname = obj_ct01.BATCHNAME;

                //Load Information for ct02
                List<CT02> obj_li_ct02 = obj_dataaccess.LoadImageData(mstr_jobno, mstr_batchname, mi_code);
                //Determining the current seqn
                mi_batchseqn = obj_li_ct02[0].CT02_BATCHSEQN;
                mstr_icn = obj_li_ct02[0].CT02_ICN;

                //Writing file for image Loading  
                Deletefolder();
                Directory.CreateDirectory(Server.MapPath(mstr_userid+"\\"+mstr_batchname));
            }
            catch (Exception e)
            {
                throw new IOException(e.Message + " " + e.Source);
            }
        }

       

       
    }
}