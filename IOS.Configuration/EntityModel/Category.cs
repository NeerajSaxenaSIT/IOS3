using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOS.Configuration.EntityModel
{
    public class Category
    {
        public Category()
        {
            Controls = new List<Control>();
        }
        public string Name { get; set; }
        public List<IOS.Configuration.EntityModel.Control> Controls { set; get; }
        public Control FindByName(string Name)
        {
            return Controls.SingleOrDefault(w => w.ControlId.ToLower() == Name.ToLower());
        }
    }
}
