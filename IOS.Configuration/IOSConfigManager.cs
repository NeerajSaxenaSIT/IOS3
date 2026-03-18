using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IOS.Configuration
{
    public class IOSConfigManager
    {
        EntityModel.IOSUser _User = new EntityModel.IOSUser();
        List<EntityModel.IOSForm> _IOSForms = new List<EntityModel.IOSForm>();
        public List<EntityModel.IOSForm> IOSForms 
        {
            get
            {
                return _IOSForms;
            }
        }

        public void SetConfiguration(ref DataTable data)
        {
            if (data != null)
            {
                DataView view = new DataView(data, "", "", DataViewRowState.CurrentRows);
                DataTable forms = view.ToTable(true, "FormName");
                foreach (DataRow f in forms.Rows)
                {
                    EntityModel.IOSForm form = new EntityModel.IOSForm 
                    {
                        Name = Convert.ToString(f["FormName"])
                    };
                    DataTable categories = new DataView(data, "FormName='" + form.Name + "'", "", DataViewRowState.CurrentRows).ToTable(true, "CategoryName");
                    foreach (DataRow c in categories.Rows)
                    {
                        EntityModel.Category Category = new EntityModel.Category
                        {
                            Name = Convert.ToString(c["CategoryName"])
                        };
                        DataTable controls = new DataView(data, "CategoryName='" + Category.Name + "'", "", DataViewRowState.CurrentRows).ToTable();
                        foreach (DataRow cltr in controls.Rows)
                        {
                            EntityModel.Control control = new EntityModel.Control
                            {
                                ControlId = Convert.ToString(cltr["ControlName"]),
                                Id = Convert.ToString(cltr["TemplateDetailId"]),
                                DefaultEnable = Convert.ToBoolean(cltr["IsEnabled"]),
                                DefaultVisible = Convert.ToBoolean(cltr["IsVisible"]),
                                DisplayName = "", // Convert.ToString(cltr["DisplayName"]),
                                ParentId = "",    //Convert.ToString(cltr["ParentId"]),
                                Type=null
                            };
                            if (!control.DefaultVisible)
                            {
                                control.ConfigType = EntityModel.ConfigType.Hidden;
                            }
                            else if (control.DefaultEnable)
                            {
                                control.ConfigType = EntityModel.ConfigType.Enable;
                            }
                            else
                            {
                                control.ConfigType=EntityModel.ConfigType.Disable;
                            }
                            Category.Controls.Add(control);
                            form.Controls.Add(control);
                        }
                        form.Categories.Add(Category);
                    }
                    _IOSForms.Add(form);
                }
            }
            else
            {
                throw new Exception("Data is null");
            }
        }
        
        public void SetUserInfo(ref DataTable data)
        {
            if (data.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[0][0])))
                {
                    _User.IsValidUser = true;
                    _User.LicenseCompany = Convert.ToString(data.Rows[0]["CompanyName"]);
                    _User.ExpirationDate = Convert.ToDateTime(data.Rows[0]["ExpirationDate"]);
                }
                else
                {
                    _User.IsValidUser = false;
                }
            }
        }
        public EntityModel.IOSForm FindFormByName(string Name)
        {
            return _IOSForms.FirstOrDefault(w => w.Name.ToLower().Trim() == Name.ToLower().Trim());
        }
        public EntityModel.IOSUser User 
        { 
            get 
            { 
                return _User;
            } 
        }
    }
}
