using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBInterface;
using Model;
using System.Configuration;
using System.Data.SqlClient;

namespace SqlServer
{
    public class SqlServerHelper : IDBHelper
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Customer"].ConnectionString;

        public SqlServerHelper()
        {
            Console.WriteLine($"{GetType().Name} has been constructed...");
        }

        public void Query()
        {
            Console.WriteLine($"{GetType().Name}.Query");
        }

        /// <summary>
        /// select generic object according to the given id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel
        {
            SqlConnection connection;
            Type sourceType = typeof(T);
            T sourceObject = (T)Activator.CreateInstance(sourceType);

            string columnsName = string.Join(",", sourceType.GetProperties().Select(x => $"[{x.Name}]").ToArray());
            string sqlString = $"SELECT {columnsName} FROM [{sourceType.Name}] WHERE Id ={id}";
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string sqlString = @"SELECT [Id]
                //                     ,[Name]
                //                     ,[CreateTime]
                //                     ,[CreatorId]
                //                     ,[LastModifierId]
                //                     ,[LastModifyTime]
                //                    FROM [Customer].[dbo].[Company]
                //                    WHERE [Id]=1";

                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var property in sourceType.GetProperties())
                    {
                        property.SetValue(sourceObject, reader[property.Name.ToString()]);
                    }
                    Console.WriteLine(reader[0]);
                }
            }
            return sourceObject;
        }
    }
}