using System;

namespace MRPAPP.Model
{
    // Report에 대한 가상테이블 값 정의
    public class Report
    {
        public int SchIdx { get; set; }
        public string PlantCode { get; set; }
        public Nullable<int> SchAmount { get; set; }
        public System.DateTime PrcDate { get; set; }
        // PrcOKAmount PrcFAILAmount
        public Nullable<int> PrcOKAmount { get; set; }
        public Nullable<int> PrcFAILAmount { get; set; }
    }
}
