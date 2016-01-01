using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository
{
    /// <summary>
    /// Interface that all data and schema
    /// migrations must implement.
    /// </summary>
    public interface IMigration
    {
        /// <summary>
        /// The unique identifier for this migration.
        /// </summary>
        string ID { get; }
        /// <summary>
        /// The migratable database against which this migration should be applied to.
        /// </summary>
        /// <param name="migratableDatabase"></param>
        void Apply(IMigratable migratableDatabase);
    }
}
