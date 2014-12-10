using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ClaimxWeb.UserControl
{
    public partial class TextBoxUserControl : System.Web.UI.UserControl
    {

        public TextBoxUserControl()
        {
            //this.Load += new EventHandler(this.Page_Load);
        }
       // protected override void OnPreRender(EventArgs e)
        public void AddControl()
        {
       
            div1.InnerHtml = _name;
            div1.Attributes.Add("class", "TextBoxClass");
            txtBox.Attributes.Add("value", _data);
            txtBox.Attributes.Add("FieldName", _name);
            txtBox.Attributes.Add("class", "TextBoxClass");
            txtBox.Attributes.Add("FieldID", _filedID); //Non HTML property
            txtBox.Attributes.Add("MinLength", _minlength.ToString());//Non HTML property
            txtBox.Attributes.Add("maxlength", _maxlength.ToString());
            if (_readonly == true)
            {
                txtBox.Attributes.Add("readonly", "readonly");
            }
          
            txtBox.Attributes.Add("Enable", _enable.ToString());//Non HTML property
            txtBox.Attributes.Add("Type", _type);//Non HTML property
            txtBox.Attributes.Add("LocationX", _locationX.ToString());//Non HTML property
            txtBox.Attributes.Add("LocationY", _locationY.ToString());//Non HTML property
            //Setting Height and width
            txtBox.Attributes.Add("style", "width:" + _width + "px;height;" + _height + "px;");
            txtBox.Attributes.Add("TabIndex", _tabindex.ToString());
         //   txtBox.Attributes.Add("onfocus", "DrawRect('" + ReturnTextBoxZone() + "');"); //Zoning property
            txtBox.Attributes.Add("status", _status);//Non HTML property
            txtBox.Attributes.Add("Essential", _required);//Non HTML property
            txtBox.Attributes.Add("Pattern", _pattern);//Non HTML property
            txtBox.Attributes.Add("Schema", _schema);//Non HTML property
            txtBox.Attributes.Add("Lookup", _lookuprequired.ToString());//Non HTML property
            txtBox.Attributes.Add("Verify", _verify.ToString());//Non HTML property
            if(_startLine == "Y")
            {
                if (_visible == true)
                {
                    div1.Attributes.Add("class", "TextBoxClass TextBoxFirstClass");
                }
                else
                {
                    div1.Attributes.Add("class", "TextBoxClass Visible TextBoxFirstClass");
                }
            }
                

            txtBox.Attributes.Add("onkeypress", "return KeyPress(event,'" + _type + "');");
            txtBox.Attributes.Add("onkeyup", "Validate(event);");
          //  txtBox.Attributes.Add("onblur", "DoValidate(event);");


        }

        #region Variable

        private string _name;
        private string _data;
        private string _filedID;
        private int _minlength;
        private int _maxlength;
        private bool _readonly;
        private bool _visible;
        private bool _enable;
        private string _type;
        private int _locationX;
        private int _locationY;
        private int _height;
        private int _width;
        private int _tabindex;
        private int _zoneX;
        private int _zoneY;
        private int _zoneH;
        private int _ZoneW;
        private string _status;
        private string _required;
        private string _pattern;
        private string _schema;
        private string _lookuprequired;
        private string _verify;
        private string _startLine;

        #endregion

        #region Property

        public string Name { get { return _name; } set { _name = value; } }
        public string Data { get { return _data; } set { _data = value; } }
        public string FieldID { get { return _filedID; } set { _filedID = value; } }
        public int MinLength { get { return _minlength; } set { _minlength = value; } }
        public int MaxLength { get { return _maxlength; } set { _maxlength = value; } }
        public bool ReadOnly { get { return _readonly; } set { _readonly = value; } }
        public bool Visibility { get { return _visible; } set { _visible = value; } }
        public bool Enable { get { return _enable; } set { _enable = value; } }
        public string Type { get { return _type; } set { _type = value; } }
        public int Width { get { return _width; } set { _width = value; } }
        public int Height { get { return _height; } set { _height = value; } }
        public int LocationX { get { return _locationX; } set { _locationX = value; } }
        public int LocationY { get { return _locationY; } set { _locationY = value; } }
        public int TabIndex { get { return _tabindex; } set { _tabindex = value; } }
        public int ZoneX { get { return _zoneX; } set { _zoneX = value; } }
        public int ZoneY { get { return _zoneY; } set { _zoneY = value; } }
        public int ZoneH { get { return _zoneH; } set { _zoneH = value; } }
        public int ZoneW { get { return _ZoneW; } set { _ZoneW = value; } }
        public string Status { get { return _status; } set { _status = value; } }
        public string Essential { get { return _required; } set { _required = value; } }
        public string Pattern { get { return _pattern; } set { _pattern = value; } }
        public string Schema { get { return _schema; } set { _schema = value; } }
        public string LookupRequired { get { return _lookuprequired; } set { _lookuprequired = value; } }
        public string Verify { get { return _verify; } set { _verify = value; } }
        public string LineStart { get { return _startLine; } set { _startLine = value; } }
        #endregion

        #region Function
        private string ReturnTextBoxProperty()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{MinLength:" + _minlength + ",Required:\"" + _required + "\"}");
            return sb.ToString();
        }
        private string ReturnTextBoxZone()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ZoneX:" + _zoneX + ",ZoneY:" + _zoneY + ",ZoneH:" + ZoneH + ",ZoneW:" + ZoneW + "}");
            return sb.ToString();
        }
        #endregion


    }
}