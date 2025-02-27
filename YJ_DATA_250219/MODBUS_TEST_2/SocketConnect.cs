using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace MODBUS_TEST_2
{
    class SocketConnecter
    {
        public Socket sensorSocket;
        private string sensorName;
        private string serverIp;
        private int serverPort;

        public bool connectState = false;
        public readonly object lockObject = new object();
        private int reconnectAttempts = 0;
        private const int reconnectDelay = 5000; // 5초 지연 후 재연결 시도
        public event Action OnConnected;
        public event Action DisConnected;
        public event Action<byte[]> OnDataReceived;
        private DBHelper dbHelper = new DBHelper();

        public SocketConnecter(string serverIp, int serverPort, string sensorName) 
        {
            SocketSet();
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            this.sensorName = sensorName;
        }

        private void SocketSet()
        {
            sensorSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public async Task ConnectServerAsync()
        {
            try
            {
                if (sensorSocket.Connected)
                {
                    sensorSocket.Shutdown(SocketShutdown.Both);
                    sensorSocket.Close();
                }

                sensorSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await sensorSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse(serverIp), serverPort));

                connectState = true;
                OnConnected?.Invoke();
                Log($"{sensorName} 서버에 연결됨");

                _ = ReceiveLoopAsync();
            }
            catch (SocketException ex)
            {
                Log($"{sensorName} ConnectCallback 오류: {ex}");
                await DisconnectAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Log($"{sensorName} ConnectCallback 오류: {ex}");
                await DisconnectAsync();
            }
            catch (Exception ex)
            {
                Log($"{sensorName} 예기치 않은 ConnectCallback 오류: {ex}");
                dbHelper.inesrtError(ex.GetType().ToString(), "ConnectCallback 에러 - " + ex.Message, $"{sensorName}");
                await DisconnectAsync();
            }
        }

        public async Task DisconnectAsync()
        {
            if (sensorSocket.Connected)
            {
                sensorSocket.Shutdown(SocketShutdown.Both);
                sensorSocket.Close();
            }
            connectState = false;
            DisConnected?.Invoke();
            Console.WriteLine($"{sensorName} 서버 연결 종료");
            await ReconnectAsync();
        }
        public async Task ReconnectAsync()
        {
            if (connectState)
            {
                Log($"{sensorName}는 이미 연결되었습니다.");
                return;
            }
            reconnectAttempts++;
            Log($"{sensorName} 재연결 중... 시도 #{reconnectAttempts}");

            try
            {
                // 새 소켓 생성
                DisConnected.Invoke();
                SocketSet();
                await ConnectServerAsync();
            }
            catch (Exception ex)
            {
                Log($"{sensorName} 재연결 오류: {ex}");
            }
        }
        public async Task SendAsync(byte[] data)
        {
            if (sensorSocket.Connected)
            {
                try
                {
                    await sensorSocket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
                }
                catch (Exception ex)
                {
                    Log($"{sensorName} 데이터 전송 오류: {ex.Message}");
                }
            }
        }

        private async Task ReceiveLoopAsync()
        {
            byte[] buffer = new byte[4096];
            while (connectState)
            {
                try
                {
                    int receivedBytes = await sensorSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                    if (receivedBytes > 0)
                    {
                        byte[] receivedData = buffer.Take(receivedBytes).ToArray();
                        OnDataReceived?.Invoke(receivedData);
                    }
                    else
                    {
                        await DisconnectAsync();
                    }
                }
                catch (Exception ex)
                {
                    Log($"데이터 수신 오류: {ex}");
                    await DisconnectAsync();
                    break;
                }
            }
        }
        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
