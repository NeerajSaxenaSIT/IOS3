using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IOS.Configuration.ChartAdjust
{
    public class ChartAdjustProperties
    {
        //private string _axisLabelText;
        //private System.Drawing.Font _axisLabelFont;
        private bool _scaleAuto = true;
        //private double _scaleMinimum;
        //private double _scaleMaximum;
        //private int _Interval;
        //private int _NumberPrecision;
        // private string _fontName = "Arial";

        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Axis Label Text"), DefaultValueAttribute("")]
        public string AxisLabelText { get; set; }

        //[CategoryAttribute("Chart Axis"), DescriptionAttribute("Axis Label Font"), DefaultValueAttribute("Black")]
        //public System.Drawing.Font AxisLabelFont { get; set; }
        private double maxValue;
        private double minValue;


        [Browsable(false)]
        public bool IsLeft { get; set; }

        [BindableAttribute(true)]
        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Scale Auto"), DefaultValueAttribute(true)]
        [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public bool ScaleAuto
        {

            get { return _scaleAuto; }
            set
            {
                _scaleAuto = value;
                bool newValue = value;
                PropertyDescriptor scaleMin = TypeDescriptor.GetProperties(this.GetType())["ScaleMinimum"];
                ReadOnlyAttribute attribMin = (ReadOnlyAttribute)scaleMin.Attributes[typeof(ReadOnlyAttribute)];
                FieldInfo isReadOnlyMin = attribMin.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
                isReadOnlyMin.SetValue(attribMin, newValue);


                PropertyDescriptor scaleMax = TypeDescriptor.GetProperties(this.GetType())["ScaleMaximum"];
                ReadOnlyAttribute attribMax = (ReadOnlyAttribute)scaleMax.Attributes[typeof(ReadOnlyAttribute)];
                FieldInfo isReadOnlyMax = attribMax.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
                isReadOnlyMax.SetValue(attribMax, newValue);

            }
        
        }


        [BindableAttribute(true), ReadOnly(true)]
        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Enter a Minimum scale value"), DefaultValueAttribute(0)]
        public double ScaleMinimum
        {
            get { return this.minValue; }
            set { this.minValue = value; }
        }

        [ReadOnly(true), BindableAttribute(true)]
        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Enter a Maximum scale value"), DefaultValueAttribute(0)]
        public double ScaleMaximum
        {
            get { return this.maxValue; }
            set { this.maxValue = value;
            }
        }

        [BindableAttribute(true)]
        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Enter the interval"), DefaultValueAttribute(0)]
        public int Interval { get; set; }


        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Enter a Precision value"), DefaultValueAttribute(0)]
        public int NumberPrecision { get; set; }

        [CategoryAttribute("Chart Axis"), DescriptionAttribute("Set Font Color"), DefaultValueAttribute("Arial")]
        // [TypeConverter(typeof(FontNameConverter)), DefaultValueAttribute("Arial")]
        public System.Drawing.Font AxisLabelFont { get; set; }


        //    [CategoryAttribute("Font"), DescriptionAttribute("Set Font Size"), DefaultValueAttribute("10")]
        //    public int FontSize { get; set; }

        //    [CategoryAttribute("Font"), DescriptionAttribute("Set Font Bold"), DefaultValueAttribute("False")]
        //    public Boolean IsBold { get; set; }

        //    [CategoryAttribute("Font"), DescriptionAttribute("Set Font Italic"), DefaultValueAttribute("False")]
        //    public Boolean IsItalic { get; set; }

        //    [CategoryAttribute("Font"), DescriptionAttribute("Set Font Underline"), DefaultValueAttribute("False")]
        //    public Boolean IsUnderline { get; set; }

        //    [Browsable(true)]
        //    [TypeConverter(typeof(FontNameConverter)), DefaultValueAttribute("Arial")]
        //    [CategoryAttribute("Font"), DescriptionAttribute("Set Font Name")]
        //    public string AxisLabelFont
        //    {
        //        get
        //        {
        //            string sFName = "";
        //            if (_fontName != null)
        //            {
        //                sFName = _fontName;
        //            }
        //            else
        //            {
        //                if (objFontName._oFontName.Length > 0)
        //                {
        //                    Array.Sort(objFontName._oFontName);
        //                    sFName = objFontName._oFontName[0];
        //                }
        //            }
        //            return sFName;
        //        }
        //        set { _fontName = value; }
        //    }
    }

    //internal class objFontName
    //{
    //    internal static string[] _oFontName = FontFamily.Families.Select(f => f.Name).ToArray();
    //}

    //public class FontNameConverter : StringConverter
    //{

    //    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    //    {
    //        //true means show a combobox
    //        return true;
    //    }

    //    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    //    {
    //        //true will limit to list. false will show the list, but allow free-form entry
    //        return true;
    //    }
    //    public override
    //        System.ComponentModel.TypeConverter.StandardValuesCollection
    //        GetStandardValues(ITypeDescriptorContext context)
    //    {
    //        return new StandardValuesCollection(objFontName._oFontName);
    //    }

    //}

}
