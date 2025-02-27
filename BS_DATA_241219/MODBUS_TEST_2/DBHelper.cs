using System;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MODBUS_TEST_2
{
    class DBHelper
    {
        //반석 DB
        public static string uid = "sa";
        public static string password = "bsadmin";
        public static string database = "SF_BS-BLAST";
        public static string server = "211.197.91.176,1433"; 
        

        // hnt 개발 DB
        //public static string uid = "sa";
        //public static string password = "hntadmin";
        //public static string database = "SF_BS-BLAST";
        //public static string server = "118.39.27.73";
        


        SqlConnection conn1 = new SqlConnection($"SERVER={server}; DATABASE={database}; UID={uid}; PASSWORD={password};");
        SqlCommand cmd1 = new SqlCommand();

        public void Connect()
        {
            try
            {
                    conn1.Open();
                
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
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{DateTime.Now} " + ex);
            }
        }
        // 에러 삽입
        public void inesrtError(string errorMsg, string errorCode, string sensorName)
        {

            cmd1.Connection = conn1;
            cmd1.CommandText = "INSERT into SD0006_SENSOR_ERROR_LOG " +
                "\r\nvalues " +
                $"\r\n(GETDATE(),'{errorCode}','{errorMsg}','{sensorName}')";
            try
            {
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} " + ex);
            }
        }

        // 온도, 습도, 압력 데이터 삽입 시작 -----------------------------
        public void Query(float[] perValues, float[] temValues, float[] preValues, float[] eleValues, float[] pre2Values, float[] ele2Values, float[] ele3Values)
        {
            cmd1.Connection = conn1;
            cmd1.CommandText = "INSERT INTO SD0002_SENSOR_DATA (ROOM_NAME,POWER, TEMPER, HUMID, PRESS, STATIC, STATUS, TIME) VALUES " +
                              "(@room1, @ele1, @tem1, @per1, @pre1, NULL, 1, GETDATE())," +
                              "(@room2, @ele2, @tem2, @per2, @pre2, NULL, 1, GETDATE())," +
                              "(@room3, @ele3, @tem3, @per3, @pre3, NULL, 1, GETDATE())," +
                              "(@room4, @ele4, @tem4, @per4, @pre4, NULL, 1, GETDATE())," +
                              "(@room5, @ele5, @tem5, @per5, @pre5, NULL, 1, GETDATE())," +
                              "(@room6, @ele6, @tem6, @per6, @pre6, NULL, 1, GETDATE())," +
                              "(@room7, @ele7, @tem7, @per7, @pre7, NULL, 1, GETDATE())," +
                              "(@room8, @ele8, @tem8, @per8, @pre8, NULL, 1, GETDATE())," +
                              "(@room9, @ele9, @tem9, @per9, @pre9, NULL, 1, GETDATE())," +
                              "(@room10, @ele10, @tem10, @per10, @pre10, NULL, 1, GETDATE())";
        
            cmd1.Parameters.Clear();
            AddParametersMsSql(cmd1, perValues, temValues, preValues, eleValues, pre2Values, ele2Values, ele3Values);
            /*AddParametersMySql(cmd2, perValues, temValues, preValues*//*, eleValues*//*);*/
            try
            {
                Connect();
                cmd1.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }
        }

        private void AddParametersMsSql(SqlCommand cmd, float[] perValues, float[] temValues, float[] preValues, float[] eleValues, float[] pre2Values, float[] ele2Values, float[] ele3Values)
        {
            float[] preFinalValues = preValues.Concat(pre2Values).ToArray();
            float[] eleFinalValues = ele2Values.Concat(eleValues).ToArray().Concat(ele3Values).ToArray();
            for (int i = 0; i < 10; i++)
            {
                perValues[i] = (float)Math.Round(perValues[i], 2);
                temValues[i] = (float)Math.Round(temValues[i], 2);
                preFinalValues[i] = (float)Math.Round(preFinalValues[i], 2);
                eleFinalValues[i] = (float)Math.Round(eleFinalValues[i], 2);

                switch (i)
                {
                    case 8:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩A");
                        break;
                    case 9:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩B");
                        break;
                    default:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"캐비닛{8 - i}");
                        break;
                }
                cmd.Parameters.AddWithValue($"@per{i + 1}", perValues[9-i].ToString("F1"));
                cmd.Parameters.AddWithValue($"@tem{i + 1}", temValues[9-i].ToString("F1"));
                cmd.Parameters.AddWithValue($"@pre{i + 1}", preFinalValues[9 - i].ToString("F2"));
                cmd.Parameters.AddWithValue($"@ele{i + 1}", eleFinalValues[9 - i].ToString("F2"));
            }
        }

        // 온도, 습도, 압력 데이터 삽입 끝 -----------------------------

        // 전력량계 데이터 삽입 시작 -----------------------------
        private void AddParameters_2_MsSql(SqlCommand cmd, float[] eleValues, float[] voltValues, float[] ampeorValues, float[] wattValues)
        {

            for (int i = 0; i < 8; i++)
            {
                int stateValue = 0;

                cmd.Parameters.AddWithValue($"@room{i + 1}", $"캐비닛{8 -i}");
                /*switch (i)
                {
                    case 8:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩A");
                        break;
                    case 9:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩B");
                        break;
                    default:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"캐비닛{i + 1}");
                        break;
                }*/
                cmd.Parameters.AddWithValue($"@ele{i + 1}", eleValues[8-i].ToString("F2"));
                cmd.Parameters.AddWithValue($"@volt{i + 1}", voltValues[8 - i].ToString("F2"));
                cmd.Parameters.AddWithValue($"@amp{i + 1}", ampeorValues[8 - i].ToString("F2"));
                cmd.Parameters.AddWithValue($"@watt{i + 1}", wattValues[8-i].ToString("F2"));
                if (wattValues[i] >= 200)
                {
                    stateValue = 1;
                }
                cmd.Parameters.AddWithValue($"@status{i + 1}", stateValue.ToString());
            }
        }

        public void Query_2(List<float[]> resultList1, List<float[]> resultList2, List<float[]> resultList3)
        {
            float[] finalKwhResultList = resultList2[0].Concat(resultList1[0]).ToArray().Concat(resultList3[0]).ToArray();
            float[] finalVResultList = resultList2[1].Concat(resultList1[1]).ToArray().Concat(resultList3[1]).ToArray();
            float[] finalAResultList = resultList2[2].Concat(resultList1[2]).ToArray().Concat(resultList3[2]).ToArray();
            float[] finalWResultList = resultList2[3].Concat(resultList1[3]).ToArray().Concat(resultList3[3]).ToArray();

            cmd1.Connection = conn1;

            cmd1.CommandText = "INSERT INTO SD0002_SENSOR_DATA_POWER_LOG (ROOM_NAME, KWH, CF_KWH, VOLT, AMPERE, WATT, PF, HZ, STATUS, TIME) VALUES " +
                              "(@room1, @ele1, 0, @volt1, @amp1, @watt1, NULL, NULL, @status1, GETDATE())," +
                              "(@room2, @ele2, 0, @volt2, @amp2, @watt2, NULL, NULL, @status2, GETDATE())," +
                              "(@room3, @ele3, 0, @volt3, @amp3, @watt3, NULL, NULL, @status3, GETDATE())," +
                              "(@room4, @ele4, 0, @volt4, @amp4, @watt4, NULL, NULL, @status4, GETDATE())," +
                              "(@room5, @ele5, 0, @volt5, @amp5, @watt5, NULL, NULL, @status5, GETDATE())," +
                              "(@room6, @ele6, 0, @volt6, @amp6, @watt6, NULL, NULL, @status6, GETDATE())," +
                              "(@room7, @ele7, 0, @volt7, @amp7, @watt7, NULL, NULL, @status7, GETDATE())," +
                              "(@room8, @ele8, 0, @volt8, @amp8, @watt8, NULL, NULL, @status8, GETDATE())" /*+
                              "(@room9, @ele9, 0, @volt9, @amp9, @watt9, NULL, NULL, @status9, GETDATE())," +
                              "(@room10, @ele10, 0, @volt10, @amp10, @watt10, NULL,  NULL, @status10, GETDATE())"*/;

            cmd1.Parameters.Clear();
            AddParameters_2_MsSql(cmd1, finalKwhResultList, finalVResultList, finalAResultList, finalWResultList);
            try
            {
                Connect();
                cmd1.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 삽입되었습니다.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }
        }
        // 전력량계 데이터 삽입 끝 -----------------------------

        // 전력량계 LOG 테이블에 기존 데이터가 존재하는지 확인 시작 -----------------------------
        public bool ExistLogData()
        {
            cmd1.Connection = conn1;
            cmd1.CommandText = "SELECT TOP 1 1 " +
                              "FROM SD0002_SENSOR_DATA_POWER_LOG " +
                              "WHERE ROOM_NAME IN ('캐비닛1', '캐비닛2', '캐비닛3', '캐비닛4', '캐비닛5', '캐비닛6', '캐비닛7', '캐비닛8', '샌딩A', '샌딩B')";
            try
            {
                Connect();
                SqlDataReader reader = cmd1.ExecuteReader();

                bool exists = reader.HasRows;

                reader.Close();

                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 조회 오류: {ex}");
                return false;
            }
            finally
            {
                Disconnect();
            }
        }
        // 전력량계 LOG 테이블에 기존 데이터가 존재하는지 확인 끝 -----------------------------

        // 전력량계 기존 KWH 데이터 가져오기 시작 -----------------------------
        public List<float[]> GetKWHQuery()
        {
            List<float[]> resultValues = new List<float[]>();

            cmd1.Connection = conn1;
            cmd1.CommandText = "SELECT ROOM_NAME, KWH, VOLT, AMPERE  " +
                              "FROM SD0002_SENSOR_DATA_POWER_LOG " +
                              "WHERE TIME = (SELECT MAX(TIME) FROM SD0002_SENSOR_DATA_POWER_LOG AS sub WHERE sub.ROOM_NAME = SD0002_SENSOR_DATA_POWER_LOG.ROOM_NAME) " +
                              "AND KWH != 0 " +
                              "ORDER BY CASE WHEN ROOM_NAME LIKE '캐비닛%' THEN 1 ELSE 2 END, ROOM_NAME";

            try
            {
                Connect();
                SqlDataReader reader = cmd1.ExecuteReader();

                Dictionary<string, int> roomIndexMap = new Dictionary<string, int>
                {
                    { "캐비닛1", 0 },
                    { "캐비닛2", 1 },
                    { "캐비닛3", 2 },
                    { "캐비닛4", 3 },
                    { "캐비닛5", 4 },
                    { "캐비닛6", 5 },
                    { "캐비닛7", 6 },
                    { "캐비닛8", 7 },
                    { "샌딩A", 8 },
                    { "샌딩B", 9 }
                };
                
                float[] previousKWH = new float[10];
                float[] previousVOLT= new float[10];
                float[] previousAMPERE = new float[10];
                while (reader.Read())
                {
                    string roomName = reader["ROOM_NAME"].ToString();
                    if (roomIndexMap.ContainsKey(roomName))
                    {
                        int index = roomIndexMap[roomName];
                        previousKWH[index] = Convert.ToSingle(reader["KWH"]);
                        previousVOLT[index] = Convert.ToSingle(reader["VOLT"]);
                        previousAMPERE[index] = Convert.ToSingle(reader["AMPERE"]);
                    }
                }
                resultValues.Add(previousKWH);
                resultValues.Add(previousVOLT);
                resultValues.Add(previousAMPERE);
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 조회 오류: {ex}");
            }
            finally
            {
                Disconnect();
                
            }
            return resultValues;
        }
        // 전력량계 기존 KWH 데이터 가져오기 끝 -----------------------------

        // 작업시간 MIN-MAX 차이 계산 시작 -----------------------------
        public double[] Query_CalcWorkData(string getDate)
        {
            cmd1.Connection = conn1;
            double[] diff = new double[10];

            try
            {
                Connect();
                string[] roomNames = { "캐비닛1", "캐비닛2", "캐비닛3", "캐비닛4", "캐비닛5", "캐비닛6", "캐비닛7", "캐비닛8", "샌딩A", "샌딩B" };

                for (int i = 0; i < roomNames.Length; i++)
                {
                    // Build query
                    cmd1.CommandText = @"
                        SELECT MIN(TIME) AS MinTime, MAX(TIME) AS MaxTime 
                        FROM SD0002_SENSOR_DATA_POWER_LOG
                        WHERE CONVERT(date, TIME) = @getDate AND WATT > 200
                        AND ROOM_NAME = @roomName";

                    cmd1.Parameters.Clear();
                    cmd1.Parameters.AddWithValue("@getDate", getDate);
                    cmd1.Parameters.AddWithValue("@roomName", roomNames[i]);

                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("MinTime")) && !reader.IsDBNull(reader.GetOrdinal("MaxTime")))
                        {
                            DateTime minTime = reader.GetDateTime(reader.GetOrdinal("MinTime"));
                            DateTime maxTime = reader.GetDateTime(reader.GetOrdinal("MaxTime"));

                            double timeDiff = (maxTime - minTime).TotalSeconds;
                            diff[i] = timeDiff;
                        }
                        else
                        {
                            diff[i] = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 시간차이 데이터 조회 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }

            return diff;
        }

        // 작업시간 MIN-MAX 차이 계산 끝 -----------------------------

        // 작업시간 데이터 업데이트 시작 -----------------------------
        public void Query_UpdateWorkTime(double[] diff, string getDate)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE SD0002_SENSOR_DATA_POWER_WORK SET ");

            queryBuilder.Append("WORK_TIME = CASE ROOM_NAME ");

            for (int i = 0; i < 10; i++)
            {
                // float을 시간으로 변환하는 부분이 필요할 수 있음
                queryBuilder.Append($"WHEN @room{i + 1} THEN DATEADD(SECOND, @workTime{i + 1}, '00:00:00') ");
            }

            queryBuilder.Append("END ");

            queryBuilder.Append("WHERE DATE = @getDate AND ROOM_NAME IN (");
            for (int i = 0; i < 10; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(", ");
                }
                queryBuilder.Append($"@room{i + 1}");
            }
            queryBuilder.Append(")");

            cmd1.CommandText = queryBuilder.ToString();

            cmd1.Parameters.Clear();
            AddParameters_3(cmd1, diff, getDate);

            try
            {
                Connect();
                cmd1.ExecuteNonQuery();
                Console.WriteLine("데이터가 성공적으로 업데이트되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 업데이트 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }
        }

        private void AddParameters_3(SqlCommand cmd, double[] diff, string getDate)
        {
            // DATE 파라미터 추가
            cmd.Parameters.AddWithValue("@getDate", getDate);

            // ROOM_NAME 및 WORK_TIME 파라미터를 추가합니다.
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 8:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", "샌딩A");
                        break;
                    case 9:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", "샌딩B");
                        break;
                    default:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"캐비닛{i + 1}");
                        break;
                }
                cmd.Parameters.AddWithValue($"@workTime{i + 1}", diff[i]);
            }
        }
        // 작업시간 데이터 업데이트 끝 -----------------------------

        // 작업시간 데이터가 존재하는지 체크 시작 -----------------------------
        public bool Query_IsExistWorkData(string getDate)
        {
            bool exists = false;

            try
            {
                cmd1.Connection = conn1;
                cmd1.CommandText = "SELECT COUNT(*) FROM SD0002_SENSOR_DATA_POWER_WORK WHERE DATE = @getDate";
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@getDate", getDate);

                Connect();
                int count = (int)cmd1.ExecuteScalar();
                if (count >= 10)
                {
                    exists = true;
                }
                //Console.WriteLine("작업시간 데이터가 성공적으로 조회되었습니다.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 조회 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }
            return exists;
        }
        // 작업시간 데이터가 존재하는지 체크 끝 -----------------------------

        // 작업시간 기본데이터 넣기 시작 -----------------------------
        private void AddParameters_4(SqlCommand cmd)
        {
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 8:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩A");
                        break;
                    case 9:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"샌딩B");
                        break;
                    default:
                        cmd.Parameters.AddWithValue($"@room{i + 1}", $"캐비닛{i + 1}");
                        break;
                }
            }
        }

        public void Query_WorkDefaultData(string getDate)
        {
            cmd1.Connection = conn1;
            cmd1.CommandText = "INSERT INTO SD0002_SENSOR_DATA_POWER_WORK (DATE, ROOM_NAME, WORK_TIME) VALUES " +
                              "(@getDate, @room1, NULL)," +
                              "(@getDate, @room2, NULL)," +
                              "(@getDate, @room3, NULL)," +
                              "(@getDate, @room4, NULL)," +
                              "(@getDate, @room5, NULL)," +
                              "(@getDate, @room6, NULL)," +
                              "(@getDate, @room7, NULL)," +
                              "(@getDate, @room8, NULL)," +
                              "(@getDate, @room9, NULL)," +
                              "(@getDate, @room10, NULL)";

            cmd1.Parameters.Clear();
            cmd1.Parameters.AddWithValue("@getDate", getDate);
            AddParameters_4(cmd1);

            try
            {
                Connect();
                cmd1.ExecuteNonQuery();
                //Console.WriteLine("기본 데이터가 성공적으로 삽입되었습니다.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 삽입 오류: {ex}");
            }
            finally
            {
                Disconnect();
            }
        }
        // 작업시간 기본데이터 넣기 끝 -----------------------------

        // 이번달 누적 kwh 계산 시작 -----------------------------
        public float[] Query_CalcCfKwh()
        {
            float[] calcCfKwh = new float[7];

            try
            {
                using (SqlConnection conn = new SqlConnection($"SERVER={server}; DATABASE={database}; UID={uid}; PASSWORD={password};"))
                {
                    conn.Open();
                    // 현재 월의 첫째 날 00:00와 다음 월의 첫째 날 00:00를 계산
                    DateTime date = DateTime.Now;
                    DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);

                    string[] roomNames = { "캐비닛7", "캐비닛6", "캐비닛5", "캐비닛4", "캐비닛3", "캐비닛2", "캐비닛1" };
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                        SELECT MIN(KWH) AS minKWH, ROOM_NAME
                        FROM SD0002_SENSOR_DATA_POWER_LOG
                        WHERE TIME >= @startDate AND TIME <= @endDate
                        AND KWH > 0
                        AND ROOM_NAME NOT IN ('캐비닛8','샌딩A','샌딩B')
                        group by ROOM_NAME
                        ORDER BY ROOM_NAME;";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@startDate", firstDayOfMonth);
                        cmd.Parameters.AddWithValue("@endDate", firstDayOfNextMonth);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // 쿼리 결과에서 ROOM_NAME과 TotalKWH를 읽음
                                string roomName = reader["ROOM_NAME"].ToString();
                                double totalKwh = reader.IsDBNull(reader.GetOrdinal("minKWH")) ? 0 : reader.GetDouble(reader.GetOrdinal("minKWH"));

                                // roomNames 배열에서 roomName의 인덱스를 찾음
                                int index = Array.IndexOf(roomNames, roomName);
                                if (index >= 0 && index < calcCfKwh.Length)
                                {
                                    calcCfKwh[index] = (float)totalKwh; // 해당 인덱스에 값을 저장
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} 데이터 조회 오류: {ex}");
            }

            return calcCfKwh;
        }
        // 이번달 누적 kwh 계산 끝 -----------------------------
    }
}
