using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Data;

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
        private string _dashboardTabPages;
        private string _selectedPages;

        public bool SetEditable;
        public string TabPages;
        public string SelectPages;

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

        [Browsable(true)]
        [TypeConverter(typeof(CheckedListConverter))]
        [ReadOnly(false)]
        [Category("Slide Dashboard"), Description("Select multiple pages"), Editor(typeof(CheckedListEditor), typeof(UITypeEditor))]
        public string DashboardTabPages
        {
            get { return _dashboardTabPages; }
            set { _dashboardTabPages = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(CheckedListConverter))]
        [ReadOnly(true)]
        [Category("Slide Dashboard"), Description("Dashboard pages to print"), Editor(typeof(CheckedListEditor), typeof(UITypeEditor))]
        public string SelectedPages
        {
            get { return _selectedPages; }
            set { _selectedPages = value; }
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
    public class CheckedListEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context.PropertyDescriptor.IsReadOnly)
            {
                return value;
            }

            var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc == null)
                return value;

            var slide = context.Instance as SlideProperties;
            if (slide == null || string.IsNullOrWhiteSpace(slide.TabPages))
                return value;

            CheckedListBox clb = new CheckedListBox();
            clb.BorderStyle = BorderStyle.None;
            clb.CheckOnClick = true;
                        
            var availableItems = slide.TabPages.Split(',').Select(x => x.Trim()).Where(x => x != "").ToList();

            var selectedItems = value?.ToString().Split(',').Select(x => x.Trim()).ToList();

            if (selectedItems == null)
            {
                foreach (var item in availableItems)
                    clb.Items.Add(item, true);
            }
            else
            {
                foreach (var item in availableItems)
                    clb.Items.Add(item, selectedItems.Contains(item));
            }

            edSvc.DropDownControl(clb);
            var result = clb.CheckedItems.Cast<string>().ToArray();
            return string.Join(",", result);
        }
    }
    public class CheckedListConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return value?.ToString();
        }
    }

}
