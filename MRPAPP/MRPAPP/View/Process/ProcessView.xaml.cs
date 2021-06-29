﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MRPAPP.View.Process
{
    /// <summary>
    /// ProcessView.xaml에 대한 상호 작용 논리
    /// 1. 공정계획에서 오늘의 생산계획 일정 불러옴
    /// 2. 없으면 에러 표시 , 시작버튼 클릭하지 못하게 만듬
    /// 3. 있으면 오늘의 날짜 표시 , 시작 버튼 활성화
    /// 3-1 Mqtt Subscription 연결 factory1/machine1/data 확인
    /// 4. 시작버튼클릭시 새 공정을 생성, DB에 입력
    ///  공정코드 :PRC20210618001 (PRC + yyyy + MM + dd + NNN)
    /// 5. 공정처리 애니메이션 시작
    /// 6. 로드타임  후 애니메이션 중지 6/28까지 했음
    /// 7. 센서싱값 리턴될때까지 중지
    /// 8. 센서링 결과값에 따라서 생산품 색상 변경
    /// 9. 현재 공정의 DB값 업데이트
    /// 10. 결과 레이블 값 수정/표시
    /// </summary>
    public partial class ProcessView : Page
    {
        // 금일 일정 불러오기
        private Model.Schedules currSchedules;
        public ProcessView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                currSchedules = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(Commons.PLANTCODE)).Where(s => s.SchDate.Equals(DateTime.Parse(today))).FirstOrDefault();

                // 금일 일정이 없을 경우 if조건 실행
                if (currSchedules == null)
                {
                    await Commons.ShowMessageAsync("공정", "공정계획이 없습니다. 계획일정을 먼저 입력하세요");
                    // 시작버튼 Disable
                    LblProcessDate.Content = string.Empty;
                    LblSchedulesLoadTime.Content = "None";
                    LblSchAmount.Content = "None";
                    BtnStartProcess.IsEnabled = false; // 버튼비활성화
                    return;
                }
                else
                {
                    // 금일 일정이 있을 경우 공정계획 표시
                    MessageBox.Show($"{today} 공정 시작합니다.");
                    LblProcessDate.Content = currSchedules.SchDate.ToString("yyyy년 MM월 dd일"); // 공정일
                    LblSchedulesLoadTime.Content = $"{currSchedules.SchLoadTime} 초"; // 로드타임
                    LblSchAmount.Content = $"{currSchedules.SchAmount} 개"; // 계획수량
                    BtnStartProcess.IsEnabled = true; // 버튼 활성화

                    InitConnectMqttBroker(); // 공정 시작시 MQTT 브로커에 연결
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ProcessView Loaded : {ex}");
                throw ex;
            }
        }

        MqttClient client;
        Timer timer = new Timer();
        Stopwatch sw = new Stopwatch();

        private void InitConnectMqttBroker()
        {
            var brokerAddress = IPAddress.Parse("210.119.12.90"); // MQTT broker iP
            client = new MqttClient(brokerAddress);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            // 연결
            client.Connect("Monitor");
            client.Subscribe(new string[] { "factory1/machine1/data/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE});
            


            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sw.Elapsed.Seconds >= 2)// 2초 대기후 일처리
            {
                sw.Stop();
                sw.Reset();
                MessageBox.Show(currentData["PRC_MSG"]);
            }
        }

        Dictionary<string, string> currentData = new Dictionary<string, string>();


        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            var currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

            sw.Stop();
            sw.Reset();
            sw.Start();

            StartSensorAnimation();
        }

        private void StartSensorAnimation()
        {
            DoubleAnimation ba = new DoubleAnimation();
            ba.From = 1; // 이미지 보임
            ba.To = 0; // 이미지 안보임
            ba.Duration = TimeSpan.FromSeconds(2);
            ba.AutoReverse = true;
            // ba.RepeatBehavior = RepeatBehavior.Forever;

            Sensor.BeginAnimation(Canvas.OpacityProperty, ba);

        }

        private void BtnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            InsertProcessData();
            StartAnimation(); // HMI 애니메이션 실행
        }

        private bool InsertProcessData()
        {
            var item = new Model.Process();
            item.Schidx = currSchedules.SchIdx;
            item.PrcCD = GetProcessCodeFromDB();
            item.PrcDate = DateTime.Now;
            item.PrcLoadTime = currSchedules.SchLoadTime;
            item.PrcStartTime = currSchedules.SchStartTime;
            item.PrcEndTime = currSchedules.SchEndTime;
            item.PrcFacilityID = Commons.FACILITYID;
            item.PrcResult = true; // 공정성공일단 픽스
            item.RegData = DateTime.Now;
            item.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetProcess(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("공정데이터 입력 실패!");
                    Commons.ShowMessageAsync("오류", "공정시작 오류발생 , 관리자 문의");
                    return false;
                }
                else
                {
                    Commons.LOGGER.Info("공정데이터 입력!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
                Commons.ShowMessageAsync("오류", "공정시작 오류발생 , 관리자 문의");
                return false;
            }

        }

        private string GetProcessCodeFromDB()
        {
            var prefix = "PRC";
            var prePrcCode = prefix + DateTime.Now.ToString("yyyyMMdd"); // PRC20210629
            var resultCode = string.Empty;

            // 이전까지 공정이 없으면 (PRC20210629...) NULL이 넘어오고
            // PRC20210629001, 002, 003, 004 --> PRC20210629004
            var maxPrc = Logic.DataAccess.GetProcess().Where(p => p.PrcCD.Contains(prePrcCode))
                .OrderByDescending(p => p.PrcCD).FirstOrDefault();
            if (maxPrc == null)
            {
                resultCode = prePrcCode + "001";
            }
            else
            {
                var maxPrcCd = maxPrc.PrcCD; // PRC20210629004
                var maxVal = int.Parse(maxPrcCd.Substring(11)) + 1;

                resultCode = prePrcCode + maxVal.ToString("000"); // 최대공정코드 + 1값
            }
            return resultCode;

        }
        private void StartAnimation()
        {
            // Gear 애니메이션 속성
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime); // 일정 계획로드타임
            //da.RepeatBehavior = RepeatBehavior.Forever; // 무한반복

            RotateTransform rt = new RotateTransform();
            Gear1.RenderTransform = rt;
            Gear1.RenderTransformOrigin = new Point(0.5, 0.5);
            Gear2.RenderTransform = rt;
            Gear2.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            // 제품 애니메이션 속성
            DoubleAnimation ma = new DoubleAnimation();
            ma.From = 161; // Canvas.Left="151" Canvas.Top="547" 통해서 알수있음
            ma.To = 507; // 옮겨지는 x값의 최대값 
            ma.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime);
            //ma.AccelerationRatio = 0.5;
            //ma.AutoReverse = true;

            Product.BeginAnimation(Canvas.LeftProperty, ma);
        }
    }
}