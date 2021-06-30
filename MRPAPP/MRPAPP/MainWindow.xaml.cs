using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Windows;
using MRPAPP.View.Setting;
using MRPAPP.View.Schedule;
using MRPAPP.View.Process;
using System.Configuration;
using MRPAPP.View.Report;

namespace MRPAPP
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {

        }
        // 오른쪽 상단에 "부산공장" 삽입
        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            Commons.PLANTCODE = ConfigurationManager.AppSettings.Get("PlantCode");
            Commons.FACILITYID = ConfigurationManager.AppSettings.Get("FacilityID");

            // 코드에 맞는 "부산공장" 데이터 가져오는 문
            try
            {
                var plantName = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Equals(Commons.PLANTCODE)).FirstOrDefault().CodeName;
                BtnPlantName.Content = plantName;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }
        // 종료이벤트
        private async void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowMessageAsync("종료", "프로그램을 종료하시겠습니까?",
                MessageDialogStyle.AffirmativeAndNegative, null);

            if (result == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();
        }
        // 설정이벤트
        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new SettingList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnStore_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }
        // 공정계획 이벤트
        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new ScheduleList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnStore_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }
        // 모니터링 이벤트
        private void BtnMonitoring_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new ProcessView();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnStore_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }
        // 리포트 이벤트
        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new ReportView();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnStore_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }
    }
}