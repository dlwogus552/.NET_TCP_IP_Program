using System.Net.Sockets;

namespace Cloud_Sensor_DATA
{
    internal class mainThread
    {
        static DBHelper helper = new DBHelper();
        static List<SensorClient> sensorClients = new List<SensorClient>();
        static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {

            List<Dictionary<string, object>> list = helper.ProcedureCall("IPADDR_SELECT");

            foreach(var result in list)
            {
                string PUBLIC_IP = "";
                string codCust = "";
                foreach(var item in result)
                {
                    if (item.Key == "PUBLIC_IP")
                    {
                        PUBLIC_IP = (string)item.Value;
                    }
                    if (item.Key == "COD_CUST")
                    {
                        codCust = (string)item.Value;
                    }
                }
                SensorClient client = new SensorClient(PUBLIC_IP, codCust);
                sensorClients.Add(client);

                Thread clientThread = new Thread(client.Start);
                clientThread.IsBackground = true; // 백그라운드 스레드로 설정
                clientThread.Start();
            }

            // 메인 스레드에서 사용자 입력 대기
            Task.Run(() =>
            {
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (input != null && input.Trim().ToUpper() == "EXIT")
                    {
                        Console.WriteLine("Exiting...");
                        cts.Cancel();  // 모든 클라이언트에 취소 요청
                        break;
                    }
                }
            }).Wait();  // 사용자가 "EXIT" 입력할 때까지 대기
        }
    }
}