using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MRPAPP.View.Process
{
    /// <summary>
    /// ProcessView.xaml에 대한 상호 작용 논리
    /// 1. 공정계획에서 오늘의 생산계획 일정 불러옴
    /// 2. 없으면 에러 표시 , 시작버튼 클릭하지 못하게 만듬
    /// 3. 있으면 오늘의 날짜 표시 , 시작 버튼 활성화
    /// 4. 시작버튼클릭시 새 공정을 생성, DB에 입력
    ///  공정코드 :PRC20210618001 (PRC + yyyy + MM + dd + NNN)
    /// 5. 공정처리 애니메이션 시작
    /// 6. 로드타임  후 애니메이션 중지
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
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ProcessView Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnStartProcess_Click(object sender, RoutedEventArgs e)
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
            ma.From = 151; // Canvas.Left="151" Canvas.Top="547" 통해서 알수있음
            ma.To = 507; // 옮겨지는 x값의 최대값 
            ma.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime);
            //ma.AccelerationRatio = 0.5;
            //ma.AutoReverse = true;

            Product.BeginAnimation(Canvas.LeftProperty, ma);
        }
    }
}
