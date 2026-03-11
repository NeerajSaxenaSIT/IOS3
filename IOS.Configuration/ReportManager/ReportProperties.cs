using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;

namespace IOS.Configuration.ReportManager
{
   public class ReportProperties
    {
        private string _reportName;
        private string _reportOwner;
        private Boolean _reportLock;
        private string _reportGroupName;
        private string _isEnabled = "Select";
        private string _email;
        private string _interval = "Select";
        private DateTime _startTime = DateTime.Now;
        private string _reportType = "Select";

        [ReadOnly(true), Category("Report"), DescriptionAttribute("Report Name")]
        public String ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }
        [ReadOnly(true), Category("Report"), DescriptionAttribute("Report Owner")]
        public String ReportOwner
        {
            get { return _reportOwner; }
            set { _reportOwner = value; }
        }
        [ReadOnly(true), Category("Report"), DescriptionAttribute("Report is locked/unlocked")]
        public Boolean ReportLock
        {
            get { return _reportLock; }
            set { _reportLock = value; }
        }
        [ReadOnly(true), Category("Report"), DescriptionAttribute("Report Group Name")]
        public String ReportGroupName
        {
            get { return _reportGroupName; }
            set { _reportGroupName = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(ScheduleEnabledConverter))]
        [ReadOnly(false), Category("Schedule"), Description("Report Enabled/Disabled"), DefaultValue("Select"), Display(Order = 1)]
        public string IsEnabled
        {
            get
            {
                string isEnabled = "";
                if (_isEnabled != null)
                {
                    isEnabled = _isEnabled;
                }
                else
                {
                    if (objScheduleEnabled._oIsEnabled.Length > 0)
                    {
                        Array.Sort(objScheduleEnabled._oIsEnabled);
                        isEnabled = objScheduleEnabled._oIsEnabled[0];
                    }
                }
                return isEnabled;
            }
            set { _isEnabled = value; }
        }
        [ReadOnly(false), Category("Schedule"), DescriptionAttribute("Send Report To Email Address(s)"), Display(Order = 2)]
        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }        
        [Browsable(true)]
        [TypeConverter(typeof(ScheduleIntervalConverter))]
        [ReadOnly(false), Category("Schedule"), Description("Report Generation Scheduled Frequency"), DefaultValue("Select"), Display(Order = 3)]
        public string Interval
        {
            get
            {
                string sFName = "";
                if (_startTime != null)
                {
                    sFName = _interval;
                }
                else
                {
                    if (objScheduleInterval._oInterval.Length > 0)
                    {
                        Array.Sort(objScheduleInterval._oInterval);
                        sFName = objScheduleInterval._oInterval[0];
                    }
                }
                return sFName;
            }
            set { _interval = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(CustomDateTimeConverter)), DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm")]
        [ReadOnly(false), Category("Schedule"), Editor(typeof(DateTimePickerEditor), typeof(UITypeEditor)), Description("Report Scheduled Start Time"), DefaultValue("Select"), Display(Order = 4)]
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(ReportTypeConverter))]
        [ReadOnly(true), Category("Schedule"), Description("Report Type"), DefaultValue("Select"), Display(Order = 5)]
        public string ReportType
        {
            get
            {
                string reportType = "";
                if (_reportType != null)
                {
                    reportType = _reportType;
                }
                else
                {
                    if (objReportType._oReportType.Length > 0)
                    {
                        Array.Sort(objReportType._oReportType);
                        reportType = objReportType._oReportType[0];
                    }
                }
                return reportType;
            }
            set { _reportType = value; }
        }

        internal class objScheduleEnabled
        {
            internal static string[] _oIsEnabled = { "Yes", "No" };
        }
        public class ScheduleEnabledConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(objScheduleEnabled._oIsEnabled);
            }
        }
        internal class objScheduleInterval
        {
            internal static string[] _oInterval = { "Hourly", "Daily", "Weekly", "Monthly" };
        }
        public class ScheduleIntervalConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(objScheduleInterval._oInterval);
            }
        }
        internal class objReportType
        {
            internal static string[] _oReportType = { "PowerPoint", "Excel" };
        }
        public class ReportTypeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(objReportType._oReportType);
            }
        }
    }
}
