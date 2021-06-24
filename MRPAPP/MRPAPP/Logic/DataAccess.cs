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
        // setting 테이블에서 데이터 가져오기
        internal static List<Settings> GetSettings()
        {
            List<Model.Settings> settings;

            using (var ctx = new MRPEntities())
                settings = ctx.Settings.ToList(); // select
            
            return settings;
        }

        internal static int SetSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item); // insert of update
                return ctx.SaveChanges(); // 데이터 커밋
            }
        }

        internal static int DelSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                var obj = ctx.Settings.Find(item.BasicCode); // 검색코드 : 검색한 실제 데이터를 삭제
                ctx.Settings.Remove(obj); // delete
                return ctx.SaveChanges(); // 데이터 커밋
            }
        }
    }
}
