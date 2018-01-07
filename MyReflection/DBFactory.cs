using DBInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    public class DBFactory
    {
        private static string dbHelperConfig = ConfigurationManager.AppSettings["DBHelperConfig"];
        private static string dllName = dbHelperConfig.Split(new char[] { ';' })[0];
        private static string typeName = dbHelperConfig.Split(new char[] { ';' })[1];

        public static IDBHelper CreateDBHelper()
        {
            Assembly assembly = Assembly.Load(dllName);             //1.加载dll
            Type type = assembly.GetType(typeName);                 //2.获取dll中的类型
            Object dbHelperObject = Activator.CreateInstance(type); //3.创建类型的实例
            IDBHelper dbHelperFromReflection = dbHelperObject as IDBHelper; //4.类型转换，用抽象不用具体（里氏）
            return dbHelperFromReflection;
        }
    }
}