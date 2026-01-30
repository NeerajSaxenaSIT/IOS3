using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace IOS.Configuration.ReportManager
{
    public class ObjectChartProperties
    {
        private string _objectStyleName;
        private string _objecType = "Chart";
        private int _objectTopMargin = 15;
        private int _objectLeftMargin = 15;
        private int _width = 1000;
        private int _height = 480;
        private string _objectScale = "1";
        private string _technology;
        private string _targetType = "Select";
        private string _predefinedTime = "Select" ;
        private string _manualStartTime = "";   //DateTime.Now
        private string _manualEndTime = "";     //DateTime.Now
        private string _resolution = "" ;
        private string _objectsSelected = "";
        private string _topXShowObjects = "";
        private string _topXDeltaInterval = "";
        private string _counterType = "";
        private string _styleOwner = Environment.UserName;
        private string _aggregateTo = "";
        private string _tagID = "";
        private string _tagsFilter = "";
        private int _topXRowCount = 20;
        private string _purpose = "";        

        [ReadOnly(true), DescriptionAttribute("Selected Object's Style Owner name")]
        public String StyleOwner
        {
            get { return _styleOwner; }
            set { _styleOwner = value; }
        }
        [ReadOnly(true), DescriptionAttribute("Selected object technology")]
        public String Technology
        {
            get { return _technology; }
            set { _technology = value; }
        }
        [ReadOnly(true), DescriptionAttribute("Slide Name")]
        public String StyleName
        {
            get { return _objectStyleName; }
            set { _objectStyleName = value; }
        }
        [ReadOnly(true), DescriptionAttribute("Object Type"), DefaultValueAttribute("Chart")]
        public String ObjectType
        {
            get { return _objecType; }
            set { _objecType = value; }
        }
        [CategoryAttribute("Location"), DescriptionAttribute("Set Object Top Location"), DefaultValueAttribute("15")]
        public int Top
        {
            get { return _objectTopMargin; }
            set { _objectTopMargin = value; }
        }
        [CategoryAttribute("Location"), DescriptionAttribute("Set Object Left Location"), DefaultValueAttribute("15")]
        public int Left
        {
            get { return _objectLeftMargin; }
            set { _objectLeftMargin = value; }
        }
        [DescriptionAttribute("Set Object Scale Value"), DefaultValueAttribute("1")]
        public String ObjectScale
        {
            get { return _objectScale; }
            set { _objectScale = value; }
        }
        [CategoryAttribute("Size"), DescriptionAttribute("Set Object Width Location"), DefaultValueAttribute("1000")]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        [CategoryAttribute("Size"), DescriptionAttribute("Set Object Height Location"), DefaultValueAttribute("480")]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        // Setting chart property grid modified on 02/Apr/2020.
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Target Type"), DefaultValueAttribute("Select")]
        public string TargetType
        {
            get { return _targetType; }
            set { _targetType = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(PredefinedTimeConverter))]
        [ReadOnly(false), CategoryAttribute("Setting"), DescriptionAttribute("Chart Predefined Time")]
        public string PredefinedTime
        {
            get
            {
                string sFName =  "" ;
                if (_predefinedTime != null)
                {
                    sFName = _predefinedTime;
                }
                else
                {
                    if (objPredefinedTime._oPredefTime.Length > 0)
                    {
                        Array.Sort(objPredefinedTime._oPredefTime);
                        sFName = objPredefinedTime._oPredefTime[0];
                    }
                }
                return sFName;
            }
            set { _predefinedTime = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Manual Start Time"), DefaultValueAttribute("Select")]
        public string ManualStartTime
        {
            get { return _manualStartTime; }
            set { _manualStartTime = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Manual End Time"), DefaultValueAttribute("Select")]
        public string ManualEndTime
        {
            get { return _manualEndTime; }
            set { _manualEndTime = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(ResolutionConverter))]
        [CategoryAttribute("Setting"), DescriptionAttribute("Chart Resolution")]
        public string Resolution
        {
            get
            {
                string sFName = "";
                if (_resolution != null)
                {
                    sFName = _resolution;
                }
                else
                {
                    if (objResolution._oRes.Length > 0)
                    {
                        Array.Sort(objResolution._oRes);
                        sFName = objResolution._oRes[0];
                    }
                }
                return sFName;
            }
            set { _resolution = value; }
        }
        [CategoryAttribute("Setting"), DescriptionAttribute("Chart Selected Objects"), DefaultValueAttribute("")]
        public string ObjectsSelected
        {
            get { return _objectsSelected; }
            set { _objectsSelected = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Counter Type"), DefaultValueAttribute("")]
        public string CounterType
        {
            get { return _counterType; }
            set { _counterType = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart TopX Show Objects"), DefaultValueAttribute("")]
        public string TopXShowObjects
        {
            get { return _topXShowObjects; }
            set { _topXShowObjects = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart TopX Delta Interval"), DefaultValueAttribute("")]
        public string TopXDeltaInterval
        {
            get { return _topXDeltaInterval; }
            set { _topXDeltaInterval = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Aggregate To"), DefaultValueAttribute("")]
        public string AggregateTo
        {
            get { return _aggregateTo; }
            set { _aggregateTo = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Tag ID"), DefaultValueAttribute("")]
        public string TagID
        {
            get { return _tagID; }
            set { _tagID = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Tags Filter"), DefaultValueAttribute("")]
        public string Tags_Filter
        {
            get { return _tagsFilter; }
            set { _tagsFilter = value; }
        }
        [ReadOnly(false), CategoryAttribute("Setting"), DescriptionAttribute("Chart TopX Row Count"), DefaultValueAttribute("")]
        public int TopXRowCount
        {
            get { return _topXRowCount; }
            set { _topXRowCount = value; }
        }
        [ReadOnly(true), CategoryAttribute("Setting"), DescriptionAttribute("Chart Purpose"), DefaultValueAttribute("")]
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }        
    }

    internal class objPredefinedTime
    {
        internal static string[] _oPredefTime = { "Yesterday", "Last Week", "Last Month", "Last 7 days", "Last 30 days", "Last 60 days" , "Last 365 Days", "Current Year" }; 
    }
    public class PredefinedTimeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, but allow free-form entry
            return true;
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(objPredefinedTime._oPredefTime);
        }
    }
    internal class objResolution
    {
        internal static string[] _oRes = { "Hourly", "Raw", "Daily", "DailyBH", "Weekly", "WeeklyBH", "Monthly" };
    }
    public class ResolutionConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, but allow free-form entry
            return true;
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(objResolution._oRes);
        }
    }    
}
