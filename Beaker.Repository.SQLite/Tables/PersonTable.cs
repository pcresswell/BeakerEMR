﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using SQLite;

namespace Beaker.Repository.SQLite.Tables
{
    [Table("persons")]
    public class PersonTable : Table
    {
        public PersonTable()
        {

        }

        internal void CopyTo(Person person)
        {
            base.CopyTo(person);
        }

        internal void Update(Person persistable)
        {
            base.Update(persistable);
        }
    }
}