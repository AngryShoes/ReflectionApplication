using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer
{
    public class Singleton
    {
        private static Singleton _singleton;

        /**实例构造器是私有的不允许外界实例化 在静态构造器中CLR只调用一次，保证
        在各个线程中该类型只被实例化一次**/

        private Singleton()
        {
            Console.WriteLine("Singleton被构造...");
        }

        static Singleton()
        {
            _singleton = new Singleton();
        }

        public static Singleton CreateInstance()
        {
            return _singleton;
        }
    }
}