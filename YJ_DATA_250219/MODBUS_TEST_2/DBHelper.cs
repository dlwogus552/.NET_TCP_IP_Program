using System;
using MySql.Data.MySqlClient;

namespace MODBUS_TEST_2
{
    class DBHelper
    {
        public static string uid = "sa";
        public static string password = "yjadmin";
        public static string database = "sf_yjfab";
        public static string server = "127.0.0.1";
        public static string port = "3306";
        public static string connectionString = $"Server={server};Port={port};Database={database};Uid={uid};Pwd={password};";

        MySqlConnection conn = new MySqlConnection(connectionString);
        MySqlCommand cmd = new MySqlCommand();

        public void Connect()
        {
            try
            {
                conn.Open();
                Console.WriteLine("데이터베이스 연결이 성공적으로 열렸습니다.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{DateTime.Now} " + ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("데이터베이스 연결이 성공적으로 닫혔습니다.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{DateTime.Now} " + ex);
            }
        }
        public float[] selectElect()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT MAX(VAL_SIGNAL1) as VAL_SIGNAL1,MAX(VAL_SIGNAL2) as VAL_SIGNAL2,MAX(VAL_SIGNAL3) as VAL_SIGNAL3,MAX(VAL_SIGNAL4) as VAL_SIGNAL4," +
                              "MAX(VAL_SIGNAL5) as VAL_SIGNAL5,MAX(VAL_SIGNAL6) as VAL_SIGNAL6,MAX(VAL_SIGNAL7) as VAL_SIGNAL7,MAX(VAL_SIGNAL8) as VAL_SIGNAL8" +
                              "  FROM monitoring_sensor_log" +
                              " WHERE COD_DEVICE = 'YJ-MKWH'" +
                              " GROUP BY COD_DEVICE";
            float[] electValue = new float[8];
            try
            {
                Connect();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            electValue[i] = reader.GetFloat($"VAL_SIGNAL{i + 1}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 조회 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
            return electValue;
        }
        public void inesrtError( string errorCode, string errorMsg, string sensorName)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO sensor_error_log" +
                "\r\nVALUES " +
                $"\r\n(NOW(), '{errorCode}','{errorMsg}','{sensorName}')";

            cmd.Parameters.Clear();
            try
            {
                Connect();
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
        public void insertElect(float[] array, string name)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO monitoring_sensor_log (COD_DEVICE,DAY_EVENT,VAL_SIGNAL1,VAL_SIGNAL2,VAL_SIGNAL3,VAL_SIGNAL4,VAL_SIGNAL5,VAL_SIGNAL6,VAL_SIGNAL7,VAL_SIGNAL8)" +
                "\r\nVALUES " +
                $"\r\n('YJ-{name}', NOW(), {array[0]},{array[1]},{array[2]},{array[3]},{array[4]},{array[5]},{array[6]},{array[7]})";

            cmd.Parameters.Clear();
            try
            {
                Connect();
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
        public void insertInverter(float[] array, string name)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO monitoring_sensor_log (COD_DEVICE,DAY_EVENT,VAL_SIGNAL1,VAL_SIGNAL2,VAL_SIGNAL3,VAL_SIGNAL4,VAL_SIGNAL5)" +
                "\r\nVALUES " +
                $"\r\n('YJ-{name}', NOW(), {array[0]},{array[1]},{array[2]},{array[3]},{array[4]})";

            cmd.Parameters.Clear();
            try
            {
                Connect();
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
        public void Query(float[] perValues1, float[] perValues2)
        {
            Random random = new Random();
            int randomAngle1 = random.Next(15, 21); // 200 이상, 230 이하의 랜덤 값 생성
            int randomAngle2 = random.Next(15, 21); // 200 이상, 230 이하의 랜덤 값 생성

            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;
            if (currentMinute >= 20 && currentMinute <= 40 && currentHour != 12 && currentHour<= 17 && currentHour >=8)
            {
                randomAngle1 = random.Next(59, 65); // 200 이상, 230 이하의 랜덤 값 생성
                randomAngle2 = random.Next(67, 71); // 200 이상, 230 이하의 랜덤 값 생성
            }


            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO monitoring_sensor_log (COD_DEVICE,DAY_EVENT,VAL_SIGNAL1,VAL_SIGNAL2)" +
                "\r\nVALUES " +
                $"\r\n('YJ-NO2', NOW(), {perValues1[1]},{perValues2[1]})," +
                $"\r\n('YJ-SO2', NOW(), {perValues2[0]} ,{perValues2[0]})," +
                $"\r\n('YJ-TEM', NOW(), {randomAngle1},{randomAngle2})";

            cmd.Parameters.Clear();

            try
            {
                Connect();
                cmd.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
