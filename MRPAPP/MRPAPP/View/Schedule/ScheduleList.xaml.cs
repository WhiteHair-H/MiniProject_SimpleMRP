using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace MRPAPP.View.Schedule
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleList : Page
    {
        public ScheduleList()
        {
            InitializeComponent();
        }
        // 메인페이지
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadControlData(); // 콤보박스 데이터 로딩(Setting 데이터 가져오기)
                LoadGridData(); // 테이블 데이터 그리드 표시
                InitErrorMessages();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        // setting테이블에 코드 가져오기
        private void LoadControlData()
        {
            // where절을 이용하여 setting에 있는 PC01만 출력
            var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();
            CboPlantCode.ItemsSource = plantCodes;
            CboGridPlantCode.ItemsSource = plantCodes;

            // where절을 이용하여 setting에 있는 FA01만 출력
            var facilityIds = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("FAC1")).ToList();
            CboSchFacilityID.ItemsSource = facilityIds;
        }

        // 코드 , 코드명, 설명 에러메시지 숨김이벤트
        private void InitErrorMessages()
        {
            LblPlantCode.Visibility = LblSchAmount.Visibility = LblSchDate.Visibility = LblSchEndTime.Visibility = LblSchFacilityID.Visibility = LblSchLoadTime.Visibility
                = LblSchStartTime.Visibility = Visibility.Hidden;
        }

        // 데이터 가져오기
        private void LoadGridData()
        {
            List<Model.Schedules> schedules = Logic.DataAccess.GetSchedules();
            this.DataContext = schedules;
        }

        // 초기화 신규 이벤트 = clear
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        // 데이터 추가 이벤트
        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            var item = new Model.Schedules();
            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text); // int형 변환

            if (TmpSchStartTime.SelectedDateTime != null)
                item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if (TmpSchEndTime.SelectedDateTime != null)
                item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;

            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;

            item.RegData = DateTime.Now;
            item.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedule(item);
                if ((int)result == 0) //실패했을 경우
                {
                    Commons.LOGGER.Error("데이터 입력시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 입력실패");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 입력 성공 : {item.SchIdx}"); // 로그
                    // 수정된 데이터 삽입
                    ClearInputs();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex.Message}");
            }
        }

        // 입력데이터 검증 메서드
        public bool IsValidInputs()
        {
            var isvalid = true;
            InitErrorMessages();

            // 공장
            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isvalid = false;
            }
            // 공정일
            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isvalid = false;
            }
            // 공장별로 공정일이 DB값이 있으면 입력되면 안됨
            // PC010001(수원) 2021-06-24

            if (CboPlantCode.SelectedValue != null && string.IsNullOrEmpty(DtpSchDate.Text))
            {
                var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString())).Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
                if (result > 0)
                {
                    LblSchDate.Visibility = Visibility.Visible;
                    LblSchDate.Text = "해당공정 공정일에 계획이 이미있습니다";
                    isvalid = false;
                }
            }

            //// 공정시작시간
            //if (string.IsNullOrEmpty(LblSchStartTime.Text))
            //{
            //    LblSchStartTime.Visibility = Visibility.Visible;
            //    LblSchStartTime.Text = "공정시작시간을 선택하세요";
            //    isvalid = false;
            //}
            //// 공정종료시간
            //if (string.IsNullOrEmpty(LblSchEndTime.Text))
            //{
            //    LblSchEndTime.Visibility = Visibility.Visible;
            //    LblSchEndTime.Text = "공정종료시간을 선택하세요";
            //    isvalid = false;
            //}
            // 로드타임
            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isvalid = false;
            }
            // 공정설비
            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공정설비를 선택하세요";
                isvalid = false;
            }
            // 계획수량
            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "계획수량은 0개 이상입니다.";
                isvalid = false;
            }

            return isvalid;
        }

        // 수정데이터 검증 메서드
        public bool IsValidUpdate()
        {
            var isvalid = true;
            InitErrorMessages();

            // 공장
            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isvalid = false;
            }
            // 공정일
            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isvalid = false;
            }
            //if (CboPlantCode.SelectedValue != null && string.IsNullOrEmpty(DtpSchDate.Text))
            //{
            //    var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString())).Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
            //    if (result > 0)
            //    {
            //        LblSchDate.Visibility = Visibility.Visible;
            //        LblSchDate.Text = "해당공정 공정일에 계획이 이미있습니다";
            //        isvalid = false;
            //    }
            //}
            // 로드타임
            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isvalid = false;
            }
            // 공정설비
            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공정설비를 선택하세요";
                isvalid = false;
            }
            // 계획수량
            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "계획수량은 0개 이상입니다.";
                isvalid = false;
            }

            return isvalid;
        }

        // 수정 이벤트
        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            var item = GrdData.SelectedValue as Model.Schedules;
            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text); // int형 변환

            if (TmpSchStartTime.SelectedDateTime != null)
                item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if (TmpSchEndTime.SelectedDateTime != null)
                item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;

            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;

            item.ModDate = DateTime.Now;
            item.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedule(item);
                if ((int)result == 0) //실패했을 경우
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {item.SchIdx}"); // 로그
                    // 수정된 데이터 삽입
                    ClearInputs();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex.Message}");
            }
        }

        // 초기화 이벤트
        private void ClearInputs()
        {
            TxtSchIdx.Text = "";
            CboPlantCode.SelectedItem = null;
            DtpSchDate.Text = "";
            TxtSchLoadTime.Text = "";
            TmpSchStartTime.SelectedDateTime = null;
            TmpSchEndTime.SelectedDateTime = null;
            CboSchFacilityID.SelectedItem = null;
            NudSchAmount.Value = 0;
            DtpSearchDate.Text = "";

            CboPlantCode.Focus();
        }

        // 검색이벤트
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var search = DtpSearchDate.Text;
                var list = Logic.DataAccess.GetSchedules().Where(s => s.SchDate.Equals(DateTime.Parse(search))).ToList();
                this.DataContext = list;
            }
            catch (Exception)
            {
                Commons.LOGGER.Error("날짜선택실패");
                await Commons.ShowMessageAsync("오류", "잘못된 날짜를 선택하셨습니다.");
            }

        }
        // 엔터 클릭 이벤트
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnSearch_Click(sender, e); // 엔터 클릭시 검색
        }

        // 데이터 읽어오기 이벤트
        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // 초기화를 안하면 다른값이 중복되어 출력됨
            // 그러므로 초기화 사용
            ClearInputs();

            try
            {
                var item = GrdData.SelectedItem as Model.Schedules;
                TxtSchIdx.Text = item.SchIdx.ToString();
                CboPlantCode.SelectedValue = item.PlantCode;
                DtpSchDate.Text = item.SchDate.ToString();
                TxtSchLoadTime.Text = item.SchLoadTime.ToString();

                if (item.SchStartTime != null)
                    TmpSchStartTime.SelectedDateTime = new DateTime(item.SchStartTime.Value.Ticks);
                if (item.SchEndTime != null)
                    TmpSchEndTime.SelectedDateTime = new DateTime(item.SchEndTime.Value.Ticks);

                CboSchFacilityID.SelectedValue = item.SchFacilityID;
                NudSchAmount.Value = item.SchAmount;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex.Message}");
                ClearInputs();
            }
        }
    }
}