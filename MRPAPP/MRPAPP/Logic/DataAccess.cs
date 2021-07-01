using MRPAPP.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPAPP.Logic
{
    public class DataAccess
    {
        /// <summary>
        /// setting
        /// </summary>
        /// <returns></returns>
        
        // setting 테이블에서 데이터 가져오기
        // 단위테스트할때는 public으로 수정
        public static List<Settings> GetSettings()
        {
            List<Model.Settings> list;

            using (var ctx = new MRPEntities())
                list = ctx.Settings.ToList(); // select

            return list;
        }

        public static int SetSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item); // insert of update
                return ctx.SaveChanges(); // 데이터 커밋
            }
        }

        public static int DelSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                var obj = ctx.Settings.Find(item.BasicCode); // 검색코드 : 검색한 실제 데이터를 삭제
                ctx.Settings.Remove(obj); // delete
                return ctx.SaveChanges(); // 데이터 커밋
            }
        }

        /// <summary>
        /// Schdule
        /// </summary>
        /// <returns></returns>
        // Schedule 테이블에서 데이터 가져오기
        public static List<Schedules> GetSchedules()
        {
            List<Model.Schedules> list;

            using (var ctx = new MRPEntities())
                list = ctx.Schedules.ToList(); // select

            return list;
        }

        public static object SetSchedule(Schedules item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item); // insert of update
                return ctx.SaveChanges(); // 데이터 커밋
            }
        }

        internal static List<Process> GetProcess()
        {
            List<Model.Process> list;
            using (var ctx = new MRPEntities())
            {
                list = ctx.Process.ToList();
            }
            return list;
        }

        internal static int SetProcess(Process item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Process.AddOrUpdate(item);
                return ctx.SaveChanges();
            }
        }

        internal static List<Report> GetReportDatas(string startDate, string endDate, string plantCode)
        {
            // MRPConnString 존재하지않기때문에 App.config에 삽입
            var connString = ConfigurationManager.ConnectionStrings["MRPConnString"].ToString();
            var list = new List<Report>();

            // 가상 Database 데이터 가져오기 
            using (var conn = new SqlConnection(connString))
            {
                conn.Open(); // Open을 해야 실행가능 중요!
                // 가상테이블 생성 SQL문
                var sqlQuery = $@"SELECT  sch.SchIdx ,sch.PlantCode ,sch.SchAmount , prc.PrcDate , prc.PrcOKAmount , prc.PrcFAILAmount
                          FROM Schedules AS sch
                          INNER JOIN (
			                          SELECT smr.Schidx , smr.PrcDate , 
				                           SUM(smr.PrcOK) AS PrcOKAmount , SUM(smr.PrcFail) AS PrcFAILAmount
			                            FROM( 
				                          SELECT p.Schidx , p.PrcDate , 
					                          CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
					                          CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
				                          FROM Process AS p
					                  ) AS smr
			                        GROUP BY smr.Schidx, smr.PrcDate
	                        ) AS prc
	                        ON sch.SchIdx = prc.Schidx
                        WHERE sch.PlantCode = '{plantCode}'
                          AND prc.PrcDate BETWEEN '{startDate}' AND '{endDate}'";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader(); /*cmd.ExecuteScalar();*/

                while (reader.Read())
                {
                    var tmp = new Report
                    {
                        // crtl + space 를 통해서 값 확인하면서 코딩
                        SchIdx = (int)reader["SchIdx"],
                        PlantCode = reader["PlantCode"].ToString(),
                        PrcDate = DateTime.Parse(reader["PrcDate"].ToString()),
                        SchAmount = (int)reader["SchAmount"],
                        PrcOKAmount = (int)reader["PrcOKAmount"],
                        PrcFAILAmount = (int)reader["PrcFAILAmount"]
                    };
                    list.Add(tmp);
                }
            }
            return list;
        }
    }
}
