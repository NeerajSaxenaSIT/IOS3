using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOS.Configuration.EntityModel
{
    public class IOSForm
    {
        public IOSForm()
        {
            Categories = new List<Category>();
            Controls = new List<Control>();
        }
        public string Name { get; set; }
        public List<IOS.Configuration.EntityModel.Category> Categories { set; get; }
        public List<IOS.Configuration.EntityModel.Control> Controls { set; get; }
        public Control FindControlByName(string Name)
        {
            try
            {
                return Controls.SingleOrDefault(w => w.ControlId.ToLower() == Name.ToLower());
            }
            catch
            {
                return null;
            }
            
        }
        public Category FindCategoriesByName(string Name)
        {
            try
            {
                return Categories.SingleOrDefault(w => w.Name.ToLower() == Name.ToLower());
            }
            catch
            {
                return null;
            }
        }
    }
}
