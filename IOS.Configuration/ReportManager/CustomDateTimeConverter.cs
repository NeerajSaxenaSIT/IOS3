using System;
using System.ComponentModel;
using System.Globalization;

public class CustomDateTimeConverter : TypeConverter
{
    private const string DisplayFormat = "yyyy-MM-dd HH:mm";

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(string))
            return true;

        return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is DateTime dt)
        {
            return dt.ToString(DisplayFormat);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;

        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string s && DateTime.TryParse(s, out DateTime dt))
            return dt;

        return base.ConvertFrom(context, culture, value);
    }
}