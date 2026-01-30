using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Text;
using System.Drawing;
namespace IOS.Configuration.ReportManager
{
    public class ObjectTextBoxProperties
    {
        private string _objectStyleName;
        private string _objecType;
        private int _objectTopMargin = 15;
        private int _objectLeftMargin = 15;
        private System.Drawing.Color _textBoxBoderColor = Color.Black;
        private string _textBoxBorderSize;
        private string _textBoxText;
        private System.Drawing.Color _textBoxFontColor = Color.Black;
        private int _textBoxFontSize = 12;
        private Boolean _textBoxFontIsBold = false;
        private Boolean _textBoxFontIsItalic = false;
        private Boolean _textBoxFontIsUnderline = false;
        private string _textBoxFontName = "Arial";
        private int _width = 100;
        private int _height = 50;
        private string _styleOwner = Environment.UserName;

        [ReadOnly(true)]
        public String StyleName
        {
            get { return _objectStyleName; }
            set { _objectStyleName = value; }
        }
        [ReadOnly(true), DescriptionAttribute("Selected Object's Style Owner name")]
        public String StyleOwner
        {
            get { return _styleOwner; }
            set { _styleOwner = value; }
        }
        [ReadOnly(true), DefaultValueAttribute("TextBox"), DescriptionAttribute("Selected Object Type")]
        public String ObjectType
        {
            get { return _objecType; }
            set { _objecType = value; }
        }
        [CategoryAttribute("Location"), DescriptionAttribute("Set Object Top Location"), DefaultValueAttribute(15)]
        public int Top
        {
            get { return _objectTopMargin; }
            set { _objectTopMargin = value; }
        }

        [CategoryAttribute("Location"), DescriptionAttribute("Set Object Left Location"), DefaultValueAttribute(15)]
        public int Left
        {
            get { return _objectLeftMargin; }
            set { _objectLeftMargin = value; }
        }

        [CategoryAttribute("Size"), DescriptionAttribute("Set Object Width Location"), DefaultValueAttribute("100")]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        [CategoryAttribute("Size"), DescriptionAttribute("Set Object Height Location"), DefaultValueAttribute("50")]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        [CategoryAttribute("Border"), DescriptionAttribute("Set TextBox Border color"), DefaultValueAttribute("Black")]
        public System.Drawing.Color BoderColor
        {
            get { return _textBoxBoderColor; }
            set { _textBoxBoderColor = value; }
        }
        [CategoryAttribute("Border"), DescriptionAttribute("Set TextBox Border Size"), DefaultValueAttribute(0)]
        public String BorderSize
        {
            get { return _textBoxBorderSize; }
            set { _textBoxBorderSize = value; }
        }

        public String TextBoxText
        {
            get { return _textBoxText; }
            set { _textBoxText = value; }
        }
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Color"), DefaultValueAttribute("Black")]
        public System.Drawing.Color FontColor
        {
            get { return _textBoxFontColor; }
            set { _textBoxFontColor = value; }
        }
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Size"), DefaultValueAttribute("10")]
        public int FontSize
        {
            get { return _textBoxFontSize; }
            set { _textBoxFontSize = value; }
        }
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Bold"), DefaultValueAttribute("False")]
        public Boolean IsBold
        {
            get { return _textBoxFontIsBold; }
            set { _textBoxFontIsBold = value; }
        }
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Italic"), DefaultValueAttribute("False")]
        public Boolean IsItalic
        {
            get { return _textBoxFontIsItalic; }
            set { _textBoxFontIsItalic = value; }
        }
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Underline"), DefaultValueAttribute("False")]
        public Boolean IsUnderline
        {
            get { return _textBoxFontIsUnderline; }
            set { _textBoxFontIsUnderline = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(FontNameConverter)), DefaultValueAttribute("Arial")]
        [CategoryAttribute("Font"), DescriptionAttribute("Set TextBox Font Name")]
        public string FontName
        {
            get
            {
                string sFName = "";
                if (_textBoxFontName != null)
                {
                    sFName = _textBoxFontName;
                }
                else
                {
                    if (objFontName._oFontName.Length > 0)
                    {
                        Array.Sort(objFontName._oFontName);
                        sFName = objFontName._oFontName[0];
                    }
                }
                return sFName;
            }
            set { _textBoxFontName = value; }
        }
    }

    internal class objFontName
    {
        internal static string[] _oFontName = FontFamily.Families.Select(f => f.Name).ToArray();
    }

    public class FontNameConverter : StringConverter
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
            return new StandardValuesCollection(objFontName._oFontName);
        }

    }

}
