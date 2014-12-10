using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    
    public class BaseMaster
    {
        private string _jobnumber;
        private string _batchname;
        private string _delmark;

        public BaseMaster(string Job, string Batch)
        {
            _jobnumber = Job;
            _batchname = Batch;
            _delmark = "N"; 
        }

        public BaseMaster()
        {
            _jobnumber = "";
            _batchname = "";
            _delmark = "N";
        }
        public string JOBNUMBER
        {
            get
            {
                return _jobnumber;
            }
            set
            {
                _jobnumber = value;
            }
        }
        public string BATCHNAME
        {
            get
            {
                return _batchname;
            }
            set
            {
                _batchname = value;
            }
        }
        public string Delmark
        {
            get
            {
                return _delmark;
            }
            set
            {
                _delmark = value;
            }
        }
    }
}
