using DBInterface;
using System;

namespace MySql
{
    public class MySqlHelper : IDBHelper
    {
        public MySqlHelper()
        {
            Console.WriteLine($"{this.GetType().Name} has been constructed...");
        }

        public void Query()
        {
            Console.WriteLine($"{this.GetType().Name}.Query");
        }
    }
}