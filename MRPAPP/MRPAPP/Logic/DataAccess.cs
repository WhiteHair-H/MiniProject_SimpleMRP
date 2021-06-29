using MRPAPP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    }
}
