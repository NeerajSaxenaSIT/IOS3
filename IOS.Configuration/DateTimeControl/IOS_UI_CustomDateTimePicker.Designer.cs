namespace IOS.Configuration.DateTimeControl
{
    partial class IOS_UI_CustomDateTimePicker
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_customDateStatus = new System.Windows.Forms.Label();
            this.dtp_customeDateControl = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lbl_customDateStatus
            // 
            this.lbl_customDateStatus.AutoSize = true;
            this.lbl_customDateStatus.Location = new System.Drawing.Point(3, 3);
            this.lbl_customDateStatus.Name = "lbl_customDateStatus";
            this.lbl_customDateStatus.Size = new System.Drawing.Size(62, 13);
            this.lbl_customDateStatus.TabIndex = 0;
            this.lbl_customDateStatus.Text = "From Date :";
            // 
            // dtp_customeDateControl
            // 
            this.dtp_customeDateControl.CausesValidation = false;
            this.dtp_customeDateControl.CustomFormat = "dd/MM/yyyy";
            this.dtp_customeDateControl.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtp_customeDateControl.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_customeDateControl.Location = new System.Drawing.Point(89, 3);
            this.dtp_customeDateControl.Name = "dtp_customeDateControl";
            this.dtp_customeDateControl.Size = new System.Drawing.Size(122, 20);
            this.dtp_customeDateControl.TabIndex = 1;
            this.dtp_customeDateControl.ValueChanged += new System.EventHandler(this.dtp_customeDateControl_ValueChanged);
            this.dtp_customeDateControl.DropDown += new System.EventHandler(this.dtp_customeDateControl_DropDown);
            this.dtp_customeDateControl.CloseUp += new System.EventHandler(this.dtp_customeDateControl_CloseUp);
            // 
            // IOS_UI_CustomDateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtp_customeDateControl);
            this.Controls.Add(this.lbl_customDateStatus);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IOS_UI_CustomDateTimePicker";
            this.Size = new System.Drawing.Size(218, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_customDateStatus;
        private System.Windows.Forms.DateTimePicker dtp_customeDateControl;
        
    }
}
