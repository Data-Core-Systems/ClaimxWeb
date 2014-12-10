using ClaimxWeb.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ClaimxWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string doLogin(string username, string password)
        {
            
            DataAccess.DataAccess da = new DataAccess.DataAccess();
            ClaimxWeb.BusinessObject.CL07 UserData = da.DoLogin(username, password);
           if(UserData!=null)
           {
               IList<CL01> JobDetails = getJobListbyUser(UserData.CL07_USER_ID);
               string resurt= new JavaScriptSerializer().Serialize(JobDetails);
               return resurt;
               
           }
               
            else
                return "fail";
        }

        
        public static IList<CL01> getJobListbyUser(string uid)
        {
            
            DataAccess.DataAccess da = new DataAccess.DataAccess();
            return (da.GetJobListByUser(uid));
            
        }

         [WebMethod]
        public static string JobSelect(string jobid)
        {

            DataAccess.DataAccess da = new DataAccess.DataAccess();
            string DBName = da.GetDB(jobid);
            if (DBName != string.Empty)
            {
                //IList<CL01> JobDetails = getJobListbyUser(UserData.CL07_USER_ID);
                //string resurt = new JavaScriptSerializer().Serialize(JobDetails);
                //return resurt;
                setDB(DBName);
                
                return "success";
            }

            else
                return "fail";
        }

        private static void setDB(string DBName)
         {
             HttpContext.Current.Session["DBName"] = DBName;
         }
    }
}