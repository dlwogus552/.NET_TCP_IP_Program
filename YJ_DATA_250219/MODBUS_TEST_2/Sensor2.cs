using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;
using YJ_SENSOR_DATA;
using System.Threading.Tasks;
using static MODBUS_TEST_2.Sensor1;
using Newtonsoft.Json;

namespace MODBUS_TEST_2
{
    public partial class Sensor2 : Form
    {
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenSource _clientCancellationTokenSource;
        private Task _listenTask;
        private bool _isListening;
        private bool _isClientConnected;
        public List<Label> TxBoxList = new List<Label>();
        private System.Threading.Timer _timer;
        private BizContent ampereValue;
        public float[] perValue = { 0, 0 }; // [0] : SO2, [1] : NO2
        private const int timeout = 10000; // 10초
        private TcpClient _client; // 클라이언트 연결을 저장하기 위한 필드

        private DBHelper helper = new DBHelper();

        public Sensor2()
        {
            InitializeComponent();
            StartListening();
            ampereValue = new BizContent();

            TxBoxList.Add(SO_PPM);
            TxBoxList.Add(NO_PPM);
            this.FormClosing += OnFormClosing;
        }
        private void StartListening()
        {
            try
            {
                // 이전 리스너가 있으면 정리
                StopListening();

                // 새로운 리스너 시작
                _listener = new TcpListener(IPAddress.Any, 6067);
                _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _listener.Start();
                _isListening = true;
                _isClientConnected = false;
                Log("서버가 6067 포트에서 시작되었습니다...");

                // CancellationTokenSource 생성
                _cancellationTokenSource = new CancellationTokenSource();

                // 클라이언트 연결 대기 시작
                _listenTask = Task.Run(() => ListenForClients(_cancellationTokenSource.Token));

                // 타이머를 시작합니다.
                _timer = new System.Threading.Timer(CheckForTimeout, null, timeout, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Log("sen 2 StartListening 예외 발생: " + ex.Message);
            }
        }
        private async void ListenForClients(CancellationToken cancellationToken)
        {
            try
            {
                while (_isListening && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // 클라이언트 연결을 비동기로 대기합니다.
                        _client = await _listener.AcceptTcpClientAsync();
                        Log("6067 클라이언트 연결 수락됨");
                        SendMessageToClient("device.state.get", new JArray { "AI1", "AI2" });

                        _clientCancellationTokenSource?.Cancel();
                        _clientCancellationTokenSource = new CancellationTokenSource();
                        // 클라이언트가 연결되면 타이머를 멈추고 상태를 갱신합니다.
                        _timer.Change(Timeout.Infinite, Timeout.Infinite);
                        _isClientConnected = true;

                        // 클라이언트와 데이터를 주고받는 작업을 처리합니다.
                        await HandleClient(_client);

                        // 클라이언트 연결이 해제되면 클라이언트 객체를 해제하고 타이머를 다시 시작합니다.
                        _isClientConnected = false;
                        _client = null;
                        _timer.Change(timeout, Timeout.Infinite);
                    }
                    catch (ObjectDisposedException)
                    {
                        Log("sen 2 TcpListener가 중지되었습니다.");
                    }
                    catch (Exception ex)
                    {
                        Log("sen 2 ListenForClients 예외 발생: " + ex.Message);
                    }
                }
            }
            finally
            {
                // 리스닝 작업이 종료될 때 클라이언트 연결 해제
                _client?.Close();
                Log("sen 2 리스닝 작업이 종료되었습니다.");
            }
        }
        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;
            var keepAliveTimeout = TimeSpan.FromSeconds(30);
            var lastKeepAlive = DateTime.UtcNow;

            try
            {
                while (true)
                {
                    if (stream.DataAvailable)
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        if (message.Contains("}{")) continue;
                        var jsonData = JsonConvert.DeserializeObject<ReceiveData>(message);
                        if (jsonData != null && jsonData.method == "device.state.get.resp")
                        {
                            ampereValue = jsonData.respContent;
                            lastKeepAlive = DateTime.UtcNow;
                            Log("6067 받은 메시지: " + message);
                        }
                        if (jsonData != null && jsonData.method == "device.state.autoUp")
                        {
                            lastKeepAlive = DateTime.UtcNow;
                            ampereValue = jsonData.bizContent;
                            ConvertAmpereToFloat();
                            Log("6067 받은 메시지: " + message);
                        }
                        if (jsonData != null && jsonData.method == "device.report.keepAlive")
                        {
                            lastKeepAlive = DateTime.UtcNow;
                            Log("6067 Alive 메시지 수신");
                        }
                        checkValue();
                        UpdatePerTextBox();
                    }
                    if (DateTime.UtcNow - lastKeepAlive > keepAliveTimeout)
                    {
                        Log("sen 2 클라이언트의 Keep-Alive 타임아웃 발생, 연결 끊어짐");
                        break;
                    }
                }
                Log($"sen 2 클라이언트 연결 끊어짐");
            }
            catch (Exception e)
            {
                Log("sen 2 HandleClient 예외 발생: " + e.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
                Log("sen 2 클라이언트 연결 해제됨");
                // 클라이언트 연결 해제 시 새로운 연결을 대기
                _isClientConnected = false;
                StartListening();
            }
        }
        private void checkValue()
        {
            float max = perValue[0] > perValue[1] ? perValue[0] : perValue[1];
            if (max > 2)
            {
                JObject bizConent = new JObject
                {
                    { "DI1", 1 }
                };
                SendMessageToClient("device.state.set", bizConent);
            }
        }
        private void SendMessageToClient(string method, JToken bizContent)
        {
            if (_client != null && _client.Connected)
            {
                JObject json = new JObject
                {
                    { "msgId", "20240814023914916323" },
                    { "sn", "w221413s004001237cc8" },
                    { "method", method },
                    { "bizContent", bizContent }
                };
                string data = json.ToString();
                NetworkStream stream = _client.GetStream();
                byte[] responseBytes = Encoding.UTF8.GetBytes(data);
                stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                Log("sen 2 클라이언트가 연결되어 있지 않음.");
            }
        }
        private void CheckForTimeout(object state)
        {
            if (!_isClientConnected && _isListening)
            {
                Log("sen 2 타임아웃 발생: 포트를 닫았다가 다시 엽니다.");
                RestartListening();
            }
        }

        private void RestartListening()
        {
            _isListening = false;

            // 현재 작업을 취소하고 종료 대기
            _cancellationTokenSource?.Cancel();
            _listenTask?.Wait();

            StopListening();

            // 새로운 리스닝 시작
            StartListening();
        }

        private void StopListening()
        {
            _isListening = false;
            try
            {
                _listener?.Stop();
                _listener = null;
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                _clientCancellationTokenSource?.Cancel();
                _clientCancellationTokenSource?.Dispose();
                _clientCancellationTokenSource = null;
                Log("sen 2 서버가 중지되었습니다...");
            }
            catch (Exception ex)
            {
                Log("sen 2 StopListening 예외 발생: " + ex.Message);
            }
        }



        private void ConvertAmpereToFloat()
        {
            perValue[0] = (ampereValue.AI1 - 4) / 16 * 20;
            perValue[1] = (ampereValue.AI2 - 4) / 16 * 20;
        }
        private void UpdatePerTextBox()
        {

            Invoke(new Action(() => TxBoxList[0].Text = perValue[0].ToString()+" PPM"));
            Invoke(new Action(() => TxBoxList[1].Text = perValue[1].ToString() + " PPM"));
        }

        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // 폼이 닫힐 때 리소스 정리
            StopListening();
            _timer?.Dispose();
            _clientCancellationTokenSource?.Cancel();
            _clientCancellationTokenSource?.Dispose();
            Log("sen 2 폼이 종료되었습니다.");
        }
        public class ReceiveData
        {
            public string msgId { get; set; }
            public string sn { get; set; }
            public string method { get; set; }
            public BizContent bizContent { get; set; }
            public BizContent respContent { get; set; }

        }

        public class BizContent
        {
            public float AI1 { get; set; }
            public float AI2 { get; set; }

            public BizContent()
            {
                AI1 = 0;
                AI2 = 0;
            }
        }
    }
}
