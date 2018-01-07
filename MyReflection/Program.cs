using DBInterface;
using Model;
using MySql;
using SqlServer;
using System;
using System.Reflection;

namespace MyReflection
{
    internal class Program
    {
        /// <summary>
        /// 反射
        /// 优点：动态
        /// 缺点：避开编译器检查
        ///       性能比普通创建对象耗时
        ///       耗时的绝对值比较小 通常不会明显的影响性能
        /// </summary>
        private static void Main(string[] args)
        {
            //Monitor.ShowPerformance();

            #region Use reflection to create instance

            Console.WriteLine("===============Start process=============");
            IDBHelper dbHelper = new MySqlHelper();
            dbHelper.Query();
            try
            {
                IDBHelper dbHelperFromReflection = DBFactory.CreateDBHelper();
                dbHelperFromReflection.Query();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("===============End process=============");

            #endregion Use reflection to create instance

            #region Reflection

            //1.遍历bin目录下的assembly，2.反射根据地址栏中的controller名找到类型，然后根据方法创建方法的触发
            ///由上面的可扩展可以知道要扩展的功能必须是要有共同的接口约束
            ///依赖接口完成可配置
            ///对于有接口的类反射创建实例后，可通过类型转换成相应的接口再调用方法
            ///对于没有实现接口的类可以用MethodInfo来获取方法
            //黑：反射可以调用私有方法，可以破坏单例
            Assembly assembly = Assembly.Load("SqlServer");
            Type type = assembly.GetType("SqlServer.ReflectionTest");

            object object1 = Activator.CreateInstance(type);
            object object2 = Activator.CreateInstance(type, new object[] { "123" });//创建有参构造器
            object objecy3 = Activator.CreateInstance(type, new object[] { 123 });//创建有参构造器

            MethodInfo methodInfo = type.GetMethod(nameof(SqlServer.ReflectionTest.Show));/**获取一个方法**/
            methodInfo.Invoke(object1, null);

            MethodInfo methodInfo1 = type.GetMethod(nameof(SqlServer.ReflectionTest.Show2));
            methodInfo1.Invoke(object1, new object[] { 123 });

            MethodInfo methodInfo2 = type.GetMethod(nameof(SqlServer.ReflectionTest.Show1));
            methodInfo2.Invoke(object1, new object[] { "Lucy" });

            #region Used for Overload method

            MethodInfo methodInfo3 = type.GetMethod(nameof(SqlServer.ReflectionTest.Show3), new Type[] { typeof(int), typeof(string) });
            methodInfo3.Invoke(object1, new object[] { 999, "Wubai" });

            MethodInfo methodInfo4 = type.GetMethod(nameof(SqlServer.ReflectionTest.Show3), new Type[] { typeof(string), typeof(int) });
            methodInfo4.Invoke(object1, new object[] { "Wubai", 999 });

            #endregion Used for Overload method

            #region Used for static method

            MethodInfo methodInfo5 = type.GetMethod(nameof(SqlServer.ReflectionTest.Show4));
            methodInfo5.Invoke(object1, new object[] { "Jerry" });
            methodInfo5.Invoke(null, new object[] { "Jerry" });

            #endregion Used for static method

            #region Used for private method

            MethodInfo methodInfo6 = type.GetMethod("Show5", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo6.Invoke(object1, new object[] { "Mike" });

            #endregion Used for private method

            #region Used for property and field

            Type peopleType = typeof(People);
            People people = (People)Activator.CreateInstance(peopleType);
            foreach (var property in peopleType.GetProperties())
            {
                Console.WriteLine($"property is {property.Name}, value is {property.GetValue(people)}");
                if (property.Name.Equals("Id"))
                {
                    property.SetValue(people, 125);
                }
                else if (property.Name.Equals("Name"))
                {
                    property.SetValue(people, "Barnett");
                }
                Console.WriteLine($"property is {property.Name}, value is {property.GetValue(people)}");
            }
            foreach (var field in peopleType.GetFields())
            {
                Console.WriteLine($"property is {field.Name}, value is {field.GetValue(people)}");
                if (field.Name.Equals("Description"))
                {
                    field.SetValue(people, "From China");
                }
                Console.WriteLine($"property is {field.Name}, value is {field.GetValue(people)}");
            }

            #endregion Used for property and field

            #region Used for infrenging singleton

            for (int i = 0; i < 5; i++)
            {
                new Action(() =>
                {
                    Singleton.CreateInstance();
                }).BeginInvoke(null, null);
            }
            //only create one instance in the five threads

            //infringes singleton
            Type singletonType = typeof(Singleton);
            object singleton1 = Activator.CreateInstance(singletonType, true);
            object singleton2 = Activator.CreateInstance(singletonType, true);
            object singleton3 = Activator.CreateInstance(singletonType, true);
            object singleton4 = Activator.CreateInstance(singletonType, true);
            object singleton5 = Activator.CreateInstance(singletonType, true);

            #endregion Used for infrenging singleton

            #endregion Reflection

            #region Used for create a simple ORM

            Console.WriteLine("***********************sql helper***************************");
            SqlServerHelper sqlserverHelper = new SqlServerHelper();
            Company company = sqlserverHelper.Find<Company>(1);
            User user = sqlserverHelper.Find<User>(1);

            #endregion Used for create a simple ORM

            Console.Read();
        }
    }
}