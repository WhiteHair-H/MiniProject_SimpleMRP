using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MRPAPP.View.Report
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReportView : Page
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitControls();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }

        // 차트 생성 이벤트
        private void DisplayChart(List<Model.Report> list)
        {
            int[] schAmounts = list.Select(a => (int)a.SchAmount).ToArray();
            int[] prcOkAmounts = list.Select(a => (int)a.PrcOKAmount).ToArray();
            int[] prcFailAmounts = list.Select(a => (int)a.PrcFAILAmount).ToArray();

            var series1 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "계획수량",
                Values = new LiveCharts.ChartValues<int>(schAmounts),
                Fill = new SolidColorBrush(Colors.Green),
            };
            var series2 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "성공수량",
                Values = new LiveCharts.ChartValues<int>(prcOkAmounts),
                Fill = new SolidColorBrush(Colors.Blue),

            };
            var series3 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "실패수량",
                Values = new LiveCharts.ChartValues<int>(prcFailAmounts),
                Fill = new SolidColorBrush(Colors.Red),

            };
            //차트 할당
            ChtReport.Series.Clear();
            ChtReport.Series.Add(series1);
            ChtReport.Series.Add(series2);
            ChtReport.Series.Add(series3);

            // x축에 해당하는 날짜 가져오기
            ChtReport.AxisX.First().Labels = list.Select(a => a.PrcDate.ToString("yyyy-MM-dd")).ToList();
        }

        private void InitControls()
        {
            DtpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
            DtpEndDate.SelectedDate = DateTime.Now;
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate(new EditAccount()); // 계정정보 수정 화면으로 변경
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs())
            {
                // Datetime에 null값이 저장될 수 있기때문에 DateTime으로 형변환후 작업
                var startDate = ((DateTime)DtpStartDate.SelectedDate).ToString("yyyy-MM-dd");
                var endDate = ((DateTime)DtpEndDate.SelectedDate).ToString("yyyy-MM-dd");
                var searchResult = Logic.DataAccess.GetReportDatas(startDate, endDate, Commons.PLANTCODE);

                DisplayChart(searchResult);
            }
        }

        // 날짜가 빠져있거나 , StartDate가 EndDate보다 최신이면 검색하면 안되게 하는 이벤트
        private bool IsValidInputs()
        {
            var result = true;

            if (DtpStartDate.SelectedDate == null || DtpEndDate.SelectedDate == null)
            {
                Commons.ShowMessageAsync("검색", "검색할 일정을 선택하세요");
                result = false;
            }
            if (DtpStartDate.SelectedDate > DtpEndDate.SelectedDate)
            {
                Commons.ShowMessageAsync("검색", "시작일자가 종료일자보다 최신일 수 없습니다");
                result = false;
            }


            return result;
        }
    }
}
