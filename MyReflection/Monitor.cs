using DBInterface;
using MySql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    public class Monitor
    {
        public static void ShowPerformance()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                MySqlHelper mysqlHelper = new MySqlHelper();
                //mysqlHelper.Query();
            }
            stopWatch.Stop();
            Console.WriteLine($"==============No refelection:{stopWatch.ElapsedMilliseconds}==============");

            Stopwatch stopWatch4Refelection = new Stopwatch();
            stopWatch4Refelection.Start();
            Assembly assembly = Assembly.Load("MySql");//1.加载dll
            Type type = assembly.GetType("MySql.MySqlHelper");//2.获取dll中的类型
            for (int i = 0; i < 1000000; i++)
            {
                Object dbHelperObject = Activator.CreateInstance(type);//3.创建类型的实例
                IDBHelper dbHelperFromReflection = dbHelperObject as IDBHelper; //4.类型转换，用抽象不用具体（里氏）
                dbHelperFromReflection.Query();
            }
            stopWatch4Refelection.Stop();
            Console.WriteLine($"================Refelection:{stopWatch4Refelection.ElapsedMilliseconds}==============");
        }
    }
}