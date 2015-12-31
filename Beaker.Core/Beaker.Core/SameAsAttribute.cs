using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Beaker.Core
{
    /// <summary>
    /// Used to mark a property for consideration when making a SameAs comparison.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SameAsAttribute : System.Attribute
    {
        public SameAsAttribute() { }
    }  
}
