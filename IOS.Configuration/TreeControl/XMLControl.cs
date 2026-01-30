using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IOS.Configuration.TreeControl
{
    public partial class XMLControl : UserControl
    {
        public XMLControl()
        {
            InitializeComponent();
        }
        public bool IsTextOnly
        {
            set
            {
                if (value)
                {
                    chkVisible.Visible = false;
                    chkEnable.Visible = false;
                    this.Width = 150;
                    tableLayoutPanel1.ColumnStyles[0].Width = 95;
                    tableLayoutPanel1.ColumnStyles[1].Width = 3;
                    tableLayoutPanel1.ColumnStyles[2].Width = 2;
                }
            }
        }
        public bool DefaultVisible
        {
            get
            {

                return chkVisible.Checked;
            }
            set
            {
                chkVisible.Checked = value;
            }
        }
        public bool DefaultEnable
        {
            set
            {
                chkEnable.Checked = value;
            }
            get
            {
                return chkEnable.Checked;
            }
        }
        public string CText { get { return lblName.Text; } set { lblName.Text = value; } }
    }
}
