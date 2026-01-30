using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IOS.Configuration.DateTimeControl
{
    public partial class IOS_UI_CustomDateTimePicker : UserControl
    {
       public event EventHandler DateTimeOpen;
       public event EventHandler DateTimeClose;
       public event EventHandler MatchDate;
        public IOS_UI_CustomDateTimePicker()
        {
            InitializeComponent();
        }
        public string cTag
        {
            set
            {
                dtp_customeDateControl.Tag = value;
            }
        }
        public string DateLabel
        {
            get
            {
                return lbl_customDateStatus.Text ;
            }
            set
            {
                lbl_customDateStatus.Text = value;
            }
        }
        public DateTime StartAndEndDate
        {
            get
            {
                return dtp_customeDateControl.Value ;
            }
            set
            {   
                dtp_customeDateControl.Value = value;
                dtp_customeDateControl.Text = value.ToString();
            }
        }
        private void dtp_customeDateControl_DropDown(object sender, EventArgs e)
        {
            if (DateTimeOpen != null)
            {
                DateTimeOpen(sender, e);
            }            
        }
        private void dtp_customeDateControl_CloseUp(object sender, EventArgs e)
        {
            if (DateTimeClose != null)
            {
                DateTimeClose(sender, e);
            }
        }
        private void dtp_customeDateControl_ValueChanged(object sender, EventArgs e)
        {
            if (MatchDate != null)
            {
                MatchDate(sender, e);
            }
        }       
    }
}