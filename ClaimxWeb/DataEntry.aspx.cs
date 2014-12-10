using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimxWeb.DataAccess;
using ClaimxWeb.BusinessObject;
using System.IO;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web.UI.HtmlControls;
using ClaimxWeb.UserControl;
using System.Text;

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
        int err_code = 0;

         //string s = HttpContext.Current.Session["DBName"].ToString();

        //DataAccess.DataAccess obj_dataaccess = new DataAccess.DataAccess();

        DataAccess.DataAccess obj_dataaccess = new DataAccess.DataAccess("Data Source=claimx26;User ID = claimxuser51;Password =claimxuser51");
        private List<TextBoxUserControl> ml_textbox;
        
        #endregion

        //public DataEntry()
        //{
        //    this.Load += new System.EventHandler(this.Page_Load);
            
        //}
        

       
        protected void Page_Load(object sender, EventArgs e)
        {
          
                if (this.IsPostBack == false)
                {
                    if (Request.Form["ajax"] == "true")
                    {
                        ProcessAJAXCall();
                    }
                    else
                    {
                        LoadBatchInformation();
                    }
                }

            
            
            
          
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
        //Display file from server
        public bool DisplayFileFromServer(string ftpServerIP, List<CT02> obj_li_ct02,string lstr_batchhome)
        {
            int pos = lstr_batchhome.IndexOf("/");
            lstr_batchhome = lstr_batchhome.Substring(pos);

            // The serverUri parameter should start with the ftp:// scheme. 

            // Get the object used to communicate with the server.
            WebClient request = new WebClient();

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("datacore", "c0mpt1a123");
            try
            {
                foreach (var ct02 in obj_li_ct02)
                {
                    string lstr_imagepath = ct02.CT02_IMAGEPATH;
                    string str = string.Format("ftp://{0}//{1}", ftpServerIP, lstr_batchhome+lstr_imagepath);
                    byte[] newFileData = request.DownloadData(str);

                    using (FileStream file = File.Create(Server.MapPath(mstr_userid + "\\" + mstr_batchname+"\\"+lstr_imagepath)))
                    {
                        file.Write(newFileData, 0, newFileData.Length);
                    }

                    string ImageName = lstr_imagepath.Replace("TIF", "jpeg");
                    System.Drawing.Bitmap img = new Bitmap(Server.MapPath(mstr_userid + "\\" + mstr_batchname + "\\" + lstr_imagepath));
                    img.Save(Server.MapPath(mstr_userid + "\\" + mstr_batchname + "\\" + ImageName), ImageFormat.Jpeg);
                    img.Dispose();
                }

               
            }
            catch (WebException e)
            {
                throw new IOException(e.Message + " " + e.Source); 
            }
            return true;
        }

        private void ProcessAJAXCall()
        {
            string action = Request.Form["Action"];

            switch (action)
            {
                case "LoadFirstImage":
                    Response.Write(LoadScreenInformation(mstr_jobno));
                    break;
                case "LoadImage":
                    Response.Write(LoadImageInformation(mstr_jobno));
                    break;
                case "LoadNextImage":
                    string JSON = Request.Form["Value"];
                    IList<CT03> DeserializedJSON_CT03 = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<CT03>>(JSON);

                    foreach (CT03 item in DeserializedJSON_CT03)
                    {
                        item.BATCHNAME = Session["Batch"].ToString();
                        item.CT03_BATCHSEQN = Convert.ToInt32(Session["BatchSeqn"]);
                        item.JOBNUMBER = mstr_jobno;
                    }

                    obj_dataaccess.WriteXMLCT03(DeserializedJSON_CT03);

                    Session["BatchSeqn"] = Convert.ToInt32(Session["BatchSeqn"])+1;
                    Response.Write(LoadScreenInformation(mstr_jobno));
                    break;
                case "LoadPreviousImage":
                    Session["BatchSeqn"] = Convert.ToInt32(Session["BatchSeqn"]) - 1;
                    Response.Write(LoadScreenInformation(mstr_jobno));
                    break;

            }
            Response.End(); 
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
                CT01 obj_ct01 = obj_dataaccess.LoadBatchData(mstr_jobno, mi_code, mstr_userid, li_nextcode);
                mstr_batchname = obj_ct01.BATCHNAME;
                Session["Batch"] = mstr_batchname;
                Session["Claim"] = obj_ct01.CT01_NUMCLAIMS.ToString();
                string lstr_batchhome = obj_ct01.CT01_BATCHHOME;

                //Load Information for ct02
                List<CT02> obj_li_ct02 = obj_dataaccess.LoadImageData(mstr_jobno, mstr_batchname, mi_code);
                //Determining the current seqn
                mi_batchseqn = obj_li_ct02[0].CT02_BATCHSEQN;
                Session["BatchSeqn"] = mi_batchseqn;
                mstr_icn = obj_li_ct02[0].CT02_ICN;

                //Writing file for image Loading  
                Deletefolder();
                Directory.CreateDirectory(Server.MapPath(mstr_userid+"\\"+mstr_batchname));

                DisplayFileFromServer("59.162.53.155",obj_li_ct02,lstr_batchhome);

             
            }
            catch (Exception e)
            {
               // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Exceptionmessage", "alert('"+e.Message+"');", true);
                Session["err_message"] = e.Message;
                Response.Redirect("ErrorPage.aspx");
            }
        }

        //Loading image information
        private string LoadImageInformation(string jobName)
        {
            //Finding the information from claimx_ct02
            //Load Information for ct02
            string batchName = Session["Batch"].ToString();
            int li_batchseqn = Convert.ToInt32(Session["BatchSeqn"]);
            CT02 obj_ct02 = obj_dataaccess.LoadImageDataSeq(jobName, batchName, li_batchseqn);
            string ct02_imagename = obj_ct02.CT02_IMAGEPATH;

            string ct02_imagepath = ct02_imagename.Replace("TIF", "jpeg");
            ct02_imagepath = "../"+mstr_userid+"/"+batchName+"/"+ct02_imagepath;

            return ct02_imagepath;

        }

     private bool SaveThisSequence()
        {

            string batchName = Session["Batch"].ToString();
            int li_batchseqn = Convert.ToInt32(Session["BatchSeqn"]);

           
            return true;

        }
       
        //Loading screen information
        private string LoadScreenInformation(string jobName)
        {
            //Finding the information from claimx_ct04
            //Load Information for ct04
            StringBuilder sb = new StringBuilder();
            string batchName = Session["Batch"].ToString();
            int li_batchseqn = Convert.ToInt32(Session["BatchSeqn"]);

           

            dvMain.Controls.Clear();

            List<CT04> obj_li_ct04 = obj_dataaccess.LoadScreenData(jobName, batchName, li_batchseqn);

            foreach (var ct04 in obj_li_ct04)
            {
                int li_window_x = ct04.CT04_WINDOW_X;
                int li_window_y = ct04.CT04_WINDOW_Y;
                int li_window_h = ct04.CT04_WINDOW_HEIGHT;
                int li_window_w = ct04.CT04_WINDOW_WIDTH;

                int li_datapanel_x = ct04.CT04_DATA_X;
                int li_datapanel_y = ct04.CT04_DATA_Y;
                int li_datapanel_h = ct04.CT04_DATA_HEIGHT;
                int li_datapanel_w = ct04.CT04_DATA_WIDTH;

                int li_imagepanel_x = ct04.CT04_IMAGE_X;
                int li_imagepanel_y = ct04.CT04_IMAGE_Y;
                int li_imagepanel_h = ct04.CT04_IMAGE_HEIGHT;
                int li_imagepanel_w = ct04.CT04_IMAGE_WIDTH;

                

                //Creating Main window

                System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                dynDiv.ID = "dvMainWindow";
                dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#d3d3d3");
                dynDiv.Style.Add(HtmlTextWriterStyle.Height, li_window_h+"px");
                dynDiv.Style.Add(HtmlTextWriterStyle.Width, li_window_w+"px");
                dynDiv.Style.Add(HtmlTextWriterStyle.Top, li_window_y + "px");
                dynDiv.Style.Add(HtmlTextWriterStyle.Left, li_window_x + "px");

                System.Web.UI.HtmlControls.HtmlGenericControl HeaderDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                HeaderDiv.ID = "dvHeader";
                HeaderDiv.InnerHtml = "Job: " + mstr_jobno + " Batch : " + batchName + " Seq: " + li_batchseqn.ToString()+"/"+Session["Claim"]+" Mode: KEY";
                dynDiv.Controls.Add(HeaderDiv);

                System.Web.UI.HtmlControls.HtmlGenericControl ImageDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                ImageDiv.ID = "dvImageWindow";
                ImageDiv.Attributes.Add("style", "overflow:auto;height:"+li_imagepanel_h+"px;width:"+li_imagepanel_w+"px;position:absolute;top:"+li_imagepanel_y+"px;left:"+li_imagepanel_x+"px");

                System.Web.UI.HtmlControls.HtmlGenericControl canvas = new System.Web.UI.HtmlControls.HtmlGenericControl("canvas");
                canvas.ID = "layer1";
                canvas.Attributes.Add("height", "2500px");
                canvas.Attributes.Add("width", "2500px");
                canvas.Attributes.Add("style", "position:absolute;z-index:0;");
                ImageDiv.Controls.Add(canvas);

                System.Web.UI.HtmlControls.HtmlGenericControl canvas1 = new System.Web.UI.HtmlControls.HtmlGenericControl("canvas");
                canvas1.ID = "layer2";
                canvas1.Attributes.Add("height", "2500px");
                canvas1.Attributes.Add("width", "2500px");
                canvas1.Attributes.Add("style", "position:absolute;z-index:1;");
                ImageDiv.Controls.Add(canvas1);

                System.Web.UI.HtmlControls.HtmlGenericControl DataDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                DataDiv.ID = "dvDataWindow";
                DataDiv.Attributes.Add("style", "overflow:auto;height:" + li_datapanel_h + "px;width:" + li_datapanel_w + "px;position:absolute;top:" + li_datapanel_y + "px;left:" + li_datapanel_x + "px");

                List<CT03> obj_li_ct03 = obj_dataaccess.LoadFieldData(jobName, batchName, li_batchseqn);

                ml_textbox = new List<TextBoxUserControl>();
                int i = 1;
                int li_nextline = 0;
                //Creating textbox control
                DataDiv.Controls.Clear();
                foreach (var ct03 in obj_li_ct03)
                {
                    TextBoxUserControl tbx = (TextBoxUserControl)LoadControl("~/UserControl/TextBoxUserControl.ascx");
                    tbx.Name = ct03.CT03_FIELD_NAME;
                    tbx.Data = ct03.CT03_FIELD_DATA;
                    tbx.FieldID = ct03.CT03_FORM_FIELD_ID;
                    tbx.MinLength = ct03.CT03_FIELD_MINLENGTH;
                    tbx.MaxLength = ct03.CT03_FIELD_MAXLENGTH;
                    tbx.ReadOnly = Convert.ToBoolean(ct03.CT03_FIELD_READONLY);
                    tbx.Visibility = Convert.ToBoolean(ct03.CT03_FIELD_VISIBLE);
                    tbx.Enable = Convert.ToBoolean(ct03.CT03_FIELD_ENABLED);
                    tbx.Type = ct03.CT03_FIELD_TYPE;
                    tbx.LocationX = ct03.CT03_FIELD_SXPOS;
                    tbx.LocationY = ct03.CT03_FIELD_SYPOS;
                    tbx.Height = ct03.CT03_FIELD_SHEIGHT;
                    tbx.Width = ct03.CT03_FIELD_SWIDTH;
                    tbx.TabIndex = i;
                    tbx.ZoneX = ct03.CT03_FIELD_IXPOS;
                    tbx.ZoneY = ct03.CT03_FIELD_IYPOS;
                    tbx.ZoneH = ct03.CT03_FIELD_IHEIGHT;
                    tbx.ZoneW = ct03.CT03_FIELD_IWIDTH;
                    tbx.Status = ct03.Delmark;
                    tbx.Essential = ct03.CT03_FIELD_REQUIRED;
                    tbx.Pattern = ct03.CT03_FIELD_PATTERN;
                    tbx.Schema = ct03.CT03_FIELD_SCHEMA;
                    tbx.LookupRequired = ct03.CT03_FIELD_LOOKUP;
                    tbx.Verify = ct03.CT03_FIELD_VFLAG;
                    if (li_nextline != ct03.CT03_FIELD_SYPOS && li_nextline!=0)
                    {  tbx.LineStart = "Y"; }
                    else { tbx.LineStart = "N"; }

                    ml_textbox.Add(tbx);

                    tbx.AddControl();

                    DataDiv.Controls.Add(tbx);
                    li_nextline = ct03.CT03_FIELD_SYPOS;
                    i++;

                }
               
                


                dynDiv.Controls.Add(ImageDiv);
                dynDiv.Controls.Add(DataDiv);
                             
                StringWriter tw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                dynDiv.RenderControl(hw);
                

            }

            return sb.ToString();
        }

        
    }
}