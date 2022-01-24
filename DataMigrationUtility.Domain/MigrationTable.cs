using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.Domain
{
    public class MigrationTable
    {
        public int ID { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Status { get; set; }
    }
}
