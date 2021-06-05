using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captcha
{
    public class DataBase
    {
        public static string connetionString = "Data Source=K21503N08\\SQLEXPRESS;Initial Catalog=JustDB;Integrated Security=True";
        public SqlConnection sqlConnect; //переменная подключения
        public SqlCommand command; //переменная sql команды
        public SqlDataReader dataReader; //переменная считывания данных
        string sql; //строка с командой

        public void Connection_Open() //метод, открывающий соединение
        {
            sqlConnect = new SqlConnection(connetionString);
            sqlConnect.Open();
        }
        public void Connection_Close() //метод, открывающий соединение
        {
            sqlConnect.Close();
        }

        public bool IsAccessAllowed(string Login, string Password)
        {
            bool Access = false;
            string sql = "Select * from [User] where Login like'" + Login + "'and Password like'" + Password + "'";

            command = new SqlCommand(sql, sqlConnect);
            dataReader = command.ExecuteReader();

            Access = dataReader.HasRows ? true : false;
            dataReader.Close();
            return Access;
        }

        public int GetClientId(string Login)
        {
            int Result = 0;
            string sql = "Select Client_ID from [User] where Login like'" + Login + "'";

            command = new SqlCommand(sql, sqlConnect);
            dataReader = command.ExecuteReader();

            if (dataReader.Read()) Result = dataReader.GetInt32(0);
            dataReader.Close();
            return Result;
        }
    }
}