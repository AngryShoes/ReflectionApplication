using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer
{
    public class ReflectionTest
    {
        public ReflectionTest()
        {
            Console.WriteLine("无参数构造函数...");
        }

        public ReflectionTest(string name)
        {
            Console.WriteLine($"参数类型为string 的构造函数,参数为{name}...");
        }

        public ReflectionTest(int id)
        {
            Console.WriteLine($"参数类型为int的构造函数，参数为{id}...");
        }

        public void Show()
        {
            Console.WriteLine("无参数Show...");
        }

        public void Show1(string name)
        {
            Console.WriteLine($"参数类型为string的show，参数为{name}...");
        }

        public void Show2(int id)
        {
            Console.WriteLine($"参数类型为int的Show，参数为{id.ToString()}...");
        }

        //重载方法
        public void Show3(int id, string name)
        {
            Console.WriteLine($"Id = {id}, Name ={name}");
        }

        public void Show3(string name, int id)
        {
            Console.WriteLine($"Name = {name}, Id = {id}");
        }

        //静态方法
        public static void Show4(string name)
        {
            Console.WriteLine($"This is a static method, Name ={name}...");
        }

        //私有方法
        private void Show5(string name)
        {
            Console.WriteLine($"This a private method, Name = {name}");
        }
    }
}