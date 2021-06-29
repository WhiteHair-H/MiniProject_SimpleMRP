using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DiviceSubApp
{
    public partial class FrmMain : Form
    {
        MqttClient client;
        string connectionString; // DB연결문자열 | MQTT broker address
        ulong lineCount;
        delegate void UpdateTextCallback(string message); // 스레드상 윈폼 RichTextbox 텍스트출력 필요
        Stopwatch sw = new Stopwatch();

        public FrmMain()
        {
            InitializeComponent();
            InitializeAllData();
        }

        private void InitializeAllData()
        {
            connectionString = "Data Source=" + TxtConnectionString.Text + ";" +
                                " Initial Catalog=MRP;" +
                                " Persist Security Info=True;" +
                                " User ID=sa;" +
                                " password=mssql_p@ssw0rd!;";
            lineCount = 0;
            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;
            IPAddress brokerAddress;

            try
            {
                brokerAddress = IPAddress.Parse(TxtConnectionString.Text);
                client = new MqttClient(brokerAddress);
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Timer.Enabled = true;
            Timer.Interval = 1000; // 1000ms -0=> 1sec
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblResult.Text = sw.Elapsed.Seconds.ToString();
            if (sw.Elapsed.Seconds >= 2) // 2초 대기 후
            {
                sw.Stop();
                sw.Reset();
                // 실제 처리 프로세스 실행
                //UpdateText("처리!!");
                PrcCorrentDataToDB();
            }
        }

        // 여러 데이터중 최종 데이터만 DB에 입력처리 메서드
        private void PrcCorrentDataToDB()
        {
            if (iotData.Count > 0)
            {
                var correctData = iotData[iotData.Count - 1];
                // DB에 입력
                // UpdateText("DB처리");
                using (var conn = new SqlConnection(connectionString))// using있을시 자동으로 종료됨
                {
                    var prcResult = correctData["PRC_MSG"] == "OK" ? 1 : 0;
                    string strUpQry = $"UPDATE Process " +
                                      $"    SET PrcResult = '{prcResult}' " +
                                      $"      , ModDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                      $"      , ModID = '{"SYS"}' " +
                                      $"  WHERE PrcIdx = " +
                                      $" (SELECT TOP 1 PrcIdx FROM Process ORDER BY PrcIdx DESC)";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(strUpQry, conn);
                        if (cmd.ExecuteNonQuery() == 1)
                            UpdateText("[DB] 센싱값 Update 성공");
                        else
                            UpdateText("[DB] 센싱값 Update 실패");
                    }
                    catch (Exception ex)
                    {
                        UpdateText($">>>>> DB ERROR : {ex.Message} ");
                    }
                }
            }
            iotData.Clear();// 데이터 모두 삭제
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Message);
                UpdateText($">>>> 받은 메시지 : {message}");
                // message (json)  > C# 
                var currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message); // 역직렬화

                PrcInputDataToList(currentData);

                sw.Stop();
                sw.Reset();
                sw.Start();
            }
            catch (Exception ex)
            {
                UpdateText($">>>>> ERROR : {ex.Message} ");
            }
        }


        List<Dictionary<string, string>> iotData = new List<Dictionary<string, string>>();

        // 라즈베리에서 들어온 메시지를 전역리스트에 입력하는 메서드
        private void PrcInputDataToList(Dictionary<string, string> currentData)
        {
            if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
            {
                iotData.Add(currentData);
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(TxtClientID.Text); // SUBSCR01
            UpdateText(">>>>> Client Connected");
            client.Subscribe(new string[] { TxtSubscriptionTopic.Text },
                new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            UpdateText(">>>>> Subscribing to : " + TxtSubscriptionTopic.Text);

            BtnConnect.Enabled = false;
            BtnDisconnect.Enabled = true;
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            UpdateText(">>>>> Client disconneceted!!");

            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;

        }

        private void UpdateText(string message)
        {
            if (RtbSubscr.InvokeRequired)
            {
                UpdateTextCallback callback = new UpdateTextCallback(UpdateText);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                lineCount++;
                RtbSubscr.AppendText($"{lineCount} : {message}\n");
                RtbSubscr.ScrollToCaret();
            }

        }

    }
}
