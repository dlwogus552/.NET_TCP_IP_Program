using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Sensor_DATA
{
    class DBHelper
    {
        private string uid = "admin";
        private string password = "hntadmin";
        private string database = "cloud_test";
        private string server = "223.130.134.147";
        private string port = "3306";
        private string connectionString;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        public DBHelper() {
            connectionString = $"Server={server};Port={port};Database={database};Uid={uid};Pwd={password};";
            conn = new MySqlConnection(connectionString);
            cmd = new MySqlCommand();
        }
        //프로시저 호출 메서드("프로시저 이름", 프로시저 파라미터)
        public List<Dictionary<String, Object>> ProcedureCall(string procedureName, params MySqlParameter[] parameters)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 명령에 매개변수를 추가합니다.
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 필요에 따라 결과 집합을 처리합니다.
                            while (reader.Read())
                            {
                                // 예시: 결과 집합의 각 열을 출력합니다.
                                Dictionary<String, Object> row = new Dictionary<string, Object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"{DateTime.Now} " + ex);
                }
            }
            return results;
        }
        public void InsertValue(Dictionary<string,float> valueDic, string cust)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        StringBuilder insertValues = new StringBuilder("INSERT INTO monitoring_sensor_log(CHANNEL_COD, COD_CUST, VAL_SIGNAL) VALUES ");
                        StringBuilder stateLogValues = new StringBuilder();
                        StringBuilder consoleText= new StringBuilder();
                        int i = 0;
                        foreach (var value in valueDic)
                        {
                            if (value.Value != -1000)
                            {
                                i++;
                                insertValues.Append($"('{value.Key}','{cust}',{value.Value}), ");
                                consoleText.Append($"{value.Key} : {value.Value}, ");
                            }
                        }

                        string sqlCommandText = insertValues.ToString().TrimEnd(',', ' ');
                        if (i >= 1)
                        {
                            cmd.CommandText = sqlCommandText;
                            cmd.ExecuteNonQuery();
                            string consoleTx = consoleText.ToString().TrimEnd(',', ' ');
                            Console.WriteLine($"{DateTime.Now} : {consoleText} 삽입");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex.Message}");
                }
            }
        }
        public void InsertState(string channelCode, string errorCod, string errorText)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $"INSERT INTO sensor_state_log(CHANNEL_COD,ERROR_COD,ERROR_TEXT ,EVENT_TIME) VALUES('{channelCode}','{errorCod}','{errorText}', NOW());";

                        // 쿼리 실행
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now} 상태 로그 삽입 오류: {ex.Message}");
                }
            }
        }
    }
}
