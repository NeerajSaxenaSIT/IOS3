using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IOS.Configuration.ReportManager
{
    [DefaultPropertyAttribute("Worksheet Properties")]
    public class WorksheetProperties
    {
        private string _wsTitle;
        private int _wsOrdinal;
        private string _styleOwner = Environment.UserName;
        [ReadOnly(true), DisplayName("Style Owner"), Description("Selected Object's Style Owner name")]
        public String StyleOwner
        {
            get { return _styleOwner; }
            set { _styleOwner = value; }
        }
        [ReadOnly(true), Category("Worksheet"), DescriptionAttribute("Set Worksheet Ordinal")]
        public int WorksheetOrdinal
        {
            get { return _wsOrdinal; }
            set { _wsOrdinal = value; }
        }
        [Category("Worksheet"), Description("Set Slide Title")]
        public String WorksheetTitle
        {
            get { return _wsTitle; }
            set { _wsTitle = value; }
        }
    }
}