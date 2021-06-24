using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRPAPP.View.Setting;
using System;
using System.Linq;
using MRPAPP.Logic;

namespace MRPApp.Test
{
    [TestClass]
    public class SettingsTest
    {
        // DB상에 중복된 데이터가 있는 지 검색
        [TestMethod]
        public void IsDuplicateDataTest()
        {
            var inputCode = "PC010001"; // DB상에 있는 값
            var expectVal = true; // 예상값

            var code = MRPAPP.Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;

            Assert.AreEqual(expectVal, realVal);
        }
    }
}
