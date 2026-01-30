using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace IOS.Configuration.ReportManager
{
    [DefaultPropertyAttribute("Slide Properties")]
    public class SlideProperties
    {
        private string _slideTitle;
        private string _slideText;
        private string _slideName;
        private int _slideOrdinal = 1;
        private int _slideHeight = 540;
        private int _slideWidth = 720;
        private string _slideeOrientation;
        private string _styleOwner = Environment.UserName;
        [ReadOnly(true), DescriptionAttribute("Selected Object's Style Owner name")]
        public String StyleOwner
        {
            get { return _styleOwner; }
            set { _styleOwner = value; }
        }
        [CategoryAttribute("Slide Text"), DescriptionAttribute("Set Slide Title")]
        public String SlideTitle
        {
            get { return _slideTitle; }
            set { _slideTitle = value; }
        }
        [CategoryAttribute("Slide Text"), DescriptionAttribute("Set Slide Text")]
        public String SlideText
        {
            get { return _slideText; }
            set { _slideText = value; }
        }
        [ReadOnly(true), CategoryAttribute("Slide Name"), DescriptionAttribute("Set Slide Name")]
        public String SlideName
        {
            get { return _slideName; }
            set { _slideName = value; }
        }
        [ReadOnly(true), DescriptionAttribute("Set Slide Ordinal")]
        public int SlideOrdinal
        {
            get { return _slideOrdinal; }
            set { _slideOrdinal = value; }
        }
        [ReadOnly(true), CategoryAttribute("Slide Size"), DescriptionAttribute("Set Slide Height"), DefaultValueAttribute(300)]
        public int Height
        {
            get { return _slideHeight; }
            set { _slideHeight = value; }
        }
        [ReadOnly(true), CategoryAttribute("Slide Size"), DescriptionAttribute("Set Slide Width"), DefaultValueAttribute(350)]
        public int Width
        {
            get { return _slideWidth; }
            set { _slideWidth = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(OrientationConverter)), DefaultValueAttribute("Horizontal")]
        public string Orientation
        {
            //When first loaded set property with the first item in the rule list.
            get
            {
                string sOrientation = "";
                if (_slideeOrientation != null)
                {
                    sOrientation = _slideeOrientation;
                }
                else
                {
                    if (OrientationType._orientationType.Length > 0)
                    {
                        //Sort the list before displaying it
                        Array.Sort(OrientationType._orientationType);
                        sOrientation = OrientationType._orientationType[0];
                    }
                }

                return sOrientation;
            }
            set { _slideeOrientation = value; }
        }
    }
    internal class OrientationType
    {
        internal static string[] _orientationType = { "Horizontal", "Vertical" };

    }
    public class OrientationConverter : StringConverter
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

        public override
            System.ComponentModel.TypeConverter.StandardValuesCollection
            GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(OrientationType._orientationType);
        }
    }
}
