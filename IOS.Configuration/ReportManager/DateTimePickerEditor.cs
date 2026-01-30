using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace IOS.Configuration.ReportManager
{
    public class DateTimePickerEditor : UITypeEditor
    {
        IWindowsFormsEditorService editorService;
        DateTimePicker picker = new DateTimePicker();

        public DateTimePickerEditor()
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                this.editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            if (this.editorService != null)
            {
                picker.Value = Convert.ToDateTime(value);
                this.editorService.DropDownControl(picker);
                value = picker.Value;
            }

            return value;
        }
    }
}
