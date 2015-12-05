using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beaker.Core
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Patient(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
