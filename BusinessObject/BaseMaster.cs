using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimxWeb.BusinessObject
{
    public enum Delmark
    {
        True = 'Y',
        False = 'N',

    }
    public class BaseMaster
    {
        private string _jobnumber;
        private string _batchname;
        private Delmark _delmark;

        public BaseMaster(string Job, string Batch)
        {
            _jobnumber = Job;
            _batchname = Batch;
            _delmark = Delmark.False;
        }

        public BaseMaster()
        {
            _jobnumber = "";
            _batchname = "";
            _delmark = Delmark.False;
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
        public Delmark Delmark
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
