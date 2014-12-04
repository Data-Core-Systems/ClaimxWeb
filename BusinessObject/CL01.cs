using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    public class CL01
    {
        public CL01() { }

        private string _cl01_job_no;
        public string CL01_JOB_NO { get { return _cl01_job_no; } set { _cl01_job_no = value; } }

        private string _cl01_job_desc;
        public string CL01_JOB_DESC { get { return _cl01_job_desc; } set { _cl01_job_desc = value; } }

        private string _cl01_client_job_no;
        public string CL01_CLIENT_JOB_NO { get { return _cl01_client_job_no; } set { _cl01_client_job_no = value; } }

        private string _cl01_client_id;
        public string CL01_CLIENT_ID { get { return _cl01_client_id; } set { _cl01_client_id = value; } }

        private string _cl01_stat;
        public string CL01_STAT { get { return _cl01_stat; } set { _cl01_stat = value; } }

        private string _cl01_database;
        public string CL01_DATABASE { get { return _cl01_database; } set { _cl01_database = value; } }
    }
}
