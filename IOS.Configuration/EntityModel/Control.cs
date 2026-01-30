using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOS.Configuration.EntityModel
{
    public class Control
    {
       public string Id { get; set; }
       public Type Type { get; set; }
       public string ControlId { get; set; }
       public string DisplayName { get; set; }
       public bool DefaultEnable { get; set; }
       public bool DefaultVisible { get; set; }
       public string ParentId { get; set; }
       public ConfigType ConfigType { set; get; }
    }
}
