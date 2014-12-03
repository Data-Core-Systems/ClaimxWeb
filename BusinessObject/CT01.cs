using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    public class CT01:BaseMaster
    {
        
        private int _ct01_numclaims;
        private int _ct01_numimages;
        private int _ct01_numrejects;
        private string _ct01_sicn;
        private string _ct01_eicn;
        private int _ct01_proirity;
        private DateTime _ct01_rdate;
        private DateTime _ct01_cdate;
        private string _ct01_checkoutby;
        private string _ct01_bstatid;
        private int _ct01_bstepid;
        private string _ct01_batchhome;
        private string _ct01_overlay;
        private string _ct01_transtype; 
        private string _ct01_status1;
        private string _ct01_status2;
        private string _ct01_status3;
       

        
        public int CT01_NUMCLAIMS
        {
            get
            {
                return _ct01_numclaims;
            }
            set
            {
                _ct01_numclaims = value;
            }
        }
        public int CT01_NUMIMAGES
        {
            get
            {
                return _ct01_numimages;
            }
            set
            {
                _ct01_numimages = value;
            }
        }
        public int CT01_NUMREJECTS
        {
            get
            {
                return _ct01_numrejects;
            }
            set
            {
                _ct01_numrejects = value;
            }
        }
        public string CT01_SICN
        {
            get
            {
                return _ct01_sicn;
            }
            set
            {
                _ct01_sicn = value;
            }
        }
        public string CT01_EICN
        {
            get
            {
                return _ct01_eicn;
            }
            set
            {
                _ct01_eicn = value;
            }
        }
        public int CT01_PROIRITY
        {
            get
            {
                return _ct01_proirity;
            }
            set
            {
                _ct01_proirity = value;
            }
        }
        public DateTime CT01_RDATE
        {
            get
            {
                return _ct01_rdate;
            }
            set
            {
                _ct01_rdate = value;
            }
        }
        public DateTime CT01_CDATE
        {
            get
            {
                return _ct01_cdate;
            }
            set
            {
                _ct01_cdate = value;
            }
        }
        public string CT01_CHECKOUTBY
        {
            get
            {
                return _ct01_checkoutby;
            }
            set
            {
                _ct01_checkoutby = value;
            }
        }
        public string CT01_BSTATID
        {
            get
            {
                return _ct01_bstatid;
            }
            set
            {
                _ct01_bstatid = value;
            }
        }
        public int CT01_BSTEPID
        {
            get
            {
                return _ct01_bstepid;
            }
            set
            {
                _ct01_bstepid = value;
            }
        }
        public string CT01_BATCHHOME
        {
            get
            {
                return _ct01_batchhome;
            }
            set
            {
                _ct01_batchhome = value;
            }
        }
        public string CT01_OVERLAY
        {
            get
            {
                return _ct01_overlay;
            }
            set
            {
                _ct01_overlay = value;
            }
        }
        public string CT01_TRANSTYPE
        {
            get
            {
                return _ct01_transtype;
            }
            set
            {
                _ct01_transtype = value;
            }
        }

        public string CT01_STATUS1
        {
            get
            {
                return _ct01_status1;
            }
            set
            {
                _ct01_status1 = value;
            }
        }

        public string CT01_STATUS2
        {
            get
            {
                return _ct01_status2;
            }
            set
            {
                _ct01_status2 = value;
            }
        }

        public string CT01_STATUS3
        {
            get
            {
                return _ct01_status3;
            }
            set
            {
                _ct01_status3 = value;
            }
        }

       
    }
}
