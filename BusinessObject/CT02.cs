using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    public class CT02 : BaseMaster
    {
        private int _ct02_batchseqn;
        private string _ct02_icn;
        private string _ct02_formtype;
        private string _ct02_imagepath;
        private string _ct02_claimstat;
        private string _ct02_status1;
        private string _ct02_status2;
        private string _ct02_status3;
       

        public int CT02_BATCHSEQN
        {
            get
            {
                return _ct02_batchseqn;
            }
            set
            {
                _ct02_batchseqn = value;
            }
        }
        public string CT02_ICN
        {
            get
            {
                return _ct02_icn;
            }
            set
            {
                _ct02_icn = value;
            }
        }
        public string CT02_FORMTYPE
        {
            get
            {
                return _ct02_formtype;
            }
            set
            {
                _ct02_formtype = value;
            }
        }
        public string CT02_IMAGEPATH
        {
            get
            {
                return _ct02_imagepath;
            }
            set
            {
                _ct02_imagepath = value;
            }
        }
        public string CT02_CLAIMSTAT
        {
            get
            {
                return _ct02_claimstat;
            }
            set
            {
                _ct02_claimstat = value;
            }
        }
        public string CT02_STATUS1
        {
            get
            {
                return _ct02_status1;
            }
            set
            {
                _ct02_status1 = value;
            }
        }
        public string CT02_STATUS2
        {
            get
            {
                return _ct02_status2;
            }
            set
            {
                _ct02_status2 = value;
            }
        }

        public string CT02_STATUS3
        {
            get
            {
                return _ct02_status3;
            }
            set
            {
                _ct02_status3 = value;
            }
        }

        
    }
}
