using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    public class CT03 : BaseMaster
    {
        private int _ct03_batchseqn;
        private int _ct03_fieldseqn;
        private string _ct03_form_field_id;
        private string _ct03_field_data;
        private string _ct03_field_name;
        private string _ct03_field_screenno;
        private int _ct03_field_minlength;
        private int _ct03_field_maxlength;
        private string _ct03_field_enabled;
        private string _ct03_field_visible;
        private string _ct03_field_readonly;
        private string _ct03_field_type;
        private string _ct03_field_pattern;
        private string _ct03_field_required;
        private string _ct03_field_vflag;
        private string _ct03_field_lookup;
        private string _ct03_field_schema;
        private string _ct03_field_qaflag;
        private string _ct03_field_aoflag;
        private int _ct03_field_ixpos;
        private int _ct03_field_iypos;
        private int _ct03_field_iheight;
        private int _ct03_field_iwidth;
        private int _ct03_field_sxpos;
        private int _ct03_field_sypos;
        private int _ct03_field_sheight;
        private int _ct03_field_swidth;
        private string _ct03_field_reject;
        private string _ct03_field_flag;
        private string _ct03_status1;
        private string _ct03_status2;
        private string _ct03_status3;
        public int CT03_FIELDSEQN
        {
            get
            {
                return _ct03_fieldseqn;
            }
            set
            {
                _ct03_fieldseqn = value;
            }
        }
        public string CT03_FORM_FIELD_ID
        {
            get
            {
                return _ct03_form_field_id;
            }
            set
            {
                _ct03_form_field_id = value;
            }
        }
        public string CT03_FIELD_DATA
        {
            get
            {
                return _ct03_field_data;
            }
            set
            {
                _ct03_field_data = value;
            }
        }
        public string CT03_FIELD_NAME
        {
            get
            {
                return _ct03_field_name;
            }
            set
            {
                _ct03_field_name = value;
            }
        }
        public string CT03_FIELD_SECTIONNO
        {
            get
            {
                return _ct03_field_screenno;
            }
            set
            {
                _ct03_field_screenno = value;
            }
        }
        public int CT03_FIELD_MINLENGTH
        {
            get
            {
                return _ct03_field_minlength;
            }
            set
            {
                _ct03_field_minlength = value;
            }
        }
        public int CT03_FIELD_MAXLENGTH
        {
            get
            {
                return _ct03_field_maxlength;
            }
            set
            {
                _ct03_field_maxlength = value;
            }
        }
        public string CT03_FIELD_ENABLED
        {
            get
            {
                return _ct03_field_enabled;
            }
            set
            {
                _ct03_field_enabled = value;
            }
        }
        public string CT03_FIELD_VISIBLE
        {
            get
            {
                return _ct03_field_visible;
            }
            set
            {
                _ct03_field_visible = value;
            }
        }
        public string CT03_FIELD_READONLY
        {
            get
            {
                return _ct03_field_readonly;
            }
            set
            {
                _ct03_field_readonly = value;
            }
        }
        public string CT03_FIELD_TYPE
        {
            get
            {
                return _ct03_field_type;
            }
            set
            {
                _ct03_field_type = value;
            }
        }
        public string CT03_FIELD_PATTERN
        {
            get
            {
                return _ct03_field_pattern;
            }
            set
            {
                _ct03_field_pattern = value;
            }
        }
        public string CT03_FIELD_REQUIRED
        {
            get
            {
                return _ct03_field_required;
            }
            set
            {
                _ct03_field_required = value;
            }
        }
        public string CT03_FIELD_VFLAG
        {
            get
            {
                return _ct03_field_vflag;
            }
            set
            {
                _ct03_field_vflag = value;
            }
        }
        public string CT03_FIELD_LOOKUP
        {
            get
            {
                return _ct03_field_lookup;
            }
            set
            {
                _ct03_field_lookup = value;
            }
        }
        public string CT03_FIELD_SCHEMA
        {
            get
            {
                return _ct03_field_schema;
            }
            set
            {
                _ct03_field_schema = value;
            }
        }
        public string CT03_FIELD_QAFLAG
        {
            get
            {
                return _ct03_field_qaflag;
            }
            set
            {
                _ct03_field_qaflag = value;
            }
        }
        public string CT03_FIELD_AOFLAG
        {
            get
            {
                return _ct03_field_aoflag;
            }
            set
            {
                _ct03_field_aoflag = value;
            }
        }
        public int CT03_FIELD_IXPOS
        {
            get
            {
                return _ct03_field_ixpos;
            }
            set
            {
                _ct03_field_ixpos = value;
            }
        }
        public int CT03_FIELD_IYPOS
        {
            get
            {
                return _ct03_field_iypos;
            }
            set
            {
                _ct03_field_iypos = value;
            }
        }
        public int CT03_FIELD_IHEIGHT
        {
            get
            {
                return _ct03_field_iheight;
            }
            set
            {
                _ct03_field_iheight = value;
            }
        }
        public int CT03_FIELD_IWIDTH
        {
            get
            {
                return _ct03_field_iwidth;
            }
            set
            {
                _ct03_field_iwidth = value;
            }
        }
        public int CT03_FIELD_SXPOS
        {
            get
            {
                return _ct03_field_sxpos;
            }
            set
            {
                _ct03_field_sxpos = value;
            }
        }
        public int CT03_FIELD_SYPOS
        {
            get
            {
                return _ct03_field_sypos;
            }
            set
            {
                _ct03_field_sypos = value;
            }
        }
        public int CT03_FIELD_SHEIGHT
        {
            get
            {
                return _ct03_field_sheight;
            }
            set
            {
                _ct03_field_sheight = value;
            }
        }
        public int CT03_FIELD_SWIDTH
        {
            get
            {
                return _ct03_field_swidth;
            }
            set
            {
                _ct03_field_swidth = value;
            }
        }
        public string CT03_FIELD_REJECT
        {
            get
            {
                return _ct03_field_reject;
            }
            set
            {
                _ct03_field_reject = value;
            }
        }
        public string CT03_FIELD_FLAG
        {
            get
            {
                return _ct03_field_flag;
            }
            set
            {
                _ct03_field_flag = value;
            }
        }
        
        public string CT03_STATUS1
        {
            get
            {
                return _ct03_status1;
            }
            set
            {
                _ct03_status1 = value;
            }
        }
        public string CT03_STATUS2
        {
            get
            {
                return _ct03_status2;
            }
            set
            {
                _ct03_status2 = value;
            }
        }
        public string CT03_STATUS3
        {
            get
            {
                return _ct03_status3;
            }
            set
            {
                _ct03_status3 = value;
            }
        }
        public int CT03_BATCHSEQN
        {
            get
            {
                return _ct03_batchseqn;
            }
            set
            {
                _ct03_batchseqn = value;
            }
        }
    }
}
