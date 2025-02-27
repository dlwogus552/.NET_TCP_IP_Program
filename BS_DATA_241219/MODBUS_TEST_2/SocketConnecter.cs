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
        public AsyncObject obj = new AsyncObject(99999);   // 소켓 크기 설정

        private string sensorName;
        private string serverIp;
        private int serverPort;

        private bool connectState = false;
        public readonly object lockObject = new object();
        private int reconnectAttempts = 0;
        private const int reconnectDelay = 5000; // 5초 지연 후 재연결 시도
        public event Action OnConnected;
        public event Action DisConnected;
        private DBHelper dbHelper = new DBHelper();

        public SocketConnecter(string serverIp, int serverPort, string sensorName)
        {
            SocketSet();
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            ConnectServer();
            this.sensorName = sensorName;
        }

        private void SocketSet()
        {
            sensorSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private void ConnectServer()
        {
            try
            {
                if (sensorSocket != null && sensorSocket.Connected)
                {
                    Log("shutdown");
                    sensorSocket.Shutdown(SocketShutdown.Both);
                    sensorSocket.Close();
                }

                IPAddress serverAddr = IPAddress.Parse($"{serverIp}");
                IPEndPoint clientEP = new IPEndPoint(serverAddr, serverPort);
                sensorSocket.BeginConnect(clientEP, new AsyncCallback(ConnectCallback), sensorSocket);
                Log($"{sensorName} 서버 {serverIp}:{serverPort}에 연결 시도 중");
            }
            catch (Exception ex)
            {
                dbHelper.inesrtError(ex.GetType().ToString(), "ConnectServer 에러 - " + ex.Message, $"{sensorName}");
                Log($"{sensorName} 연결 오류: {ex}");
                DisConnect();
                Reconnect();
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                if (client.Connected)
                {
                    obj.WorkingSocket = client;
                    Log($"{sensorName} 서버에 연결됨.");
                    connectState = true;
                    reconnectAttempts = 0;
                    // 연결 성공 이벤트 발생
                    OnConnected?.Invoke();
                }
                else
                {
                    Log($"{sensorName} 서버에 연결 실패.");
                    DisConnect();
                    Reconnect();
                }
            }
            catch (SocketException ex)
            {
                Log($"{sensorName} ConnectCallback 오류: {ex}");
                DisConnect();
                Reconnect();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Log($"{sensorName} ConnectCallback 오류: {ex}");
            }
            catch (ObjectDisposedException ex)
            {
                Log($"{sensorName} ConnectCallback 오류: {ex}");
                DisConnect();
                Reconnect();
            }
            catch (Exception ex)
            {
                Log($"{sensorName} 예기치 않은 ConnectCallback 오류: {ex}");
                dbHelper.inesrtError(ex.GetType().ToString(), "ConnectCallback 에러 - " + ex.Message, $"{sensorName}");
                DisConnect();
                Reconnect();
            }
        }
        public void DisConnect()
        {
            DisConnected?.Invoke();

            if (obj.WorkingSocket != null)
            {
                if (obj.WorkingSocket.Connected)
                {
                    sensorSocket.Shutdown(SocketShutdown.Both);
                    Log("소켓 연결 종료 완료.");
                }

                obj.WorkingSocket.Close();
                obj.WorkingSocket.Dispose();
                obj.WorkingSocket = null;
            }
            connectState = false;
        }
        public void Reconnect()
        {
            lock (lockObject)
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
                    SocketSet();

                    ConnectServer();
                    Thread.Sleep(reconnectDelay); // 재연결 전 지연 추가
                }
                catch (Exception ex)
                {
                    Log($"{sensorName} 재연결 오류: {ex}");
                }
            }
        }
        public bool CheckSocketState()
        {
            if (obj.WorkingSocket == null || !obj.WorkingSocket.Connected)
            {
                Log($"{sensorName} 소켓이 연결되지 않았습니다.");
                DisConnect();
                Reconnect();
                return false;
            }
            return true;
        }
        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
    public class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;

        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, BufferSize);
        }
    }

}
