using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBInterface;

namespace Oracle
{
    public class OracleHelper : IDBHelper
    {
        public OracleHelper()
        {
            Console.WriteLine($"{this.GetType().Name} has been constructed...");
        }

        public void Query()
        {
            Console.WriteLine($"{this.GetType().Name}.Query");
        }
    }
}