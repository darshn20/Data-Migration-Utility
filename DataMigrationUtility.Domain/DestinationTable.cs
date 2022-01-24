using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.Domain
{
    public class DestinationTable
    {
        public int ID { get; set; }
        public int Sum { get; set; }
        
        public int SourceTableID { get; set; }
    }
}
