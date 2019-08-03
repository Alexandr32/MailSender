using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodePasswordDLL.Tests
{
    [TestClass]
    public class CodePasswordTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        /// <summary>
        /// Проверка правильности кодировки пароля
        /// </summary>
        [TestMethod]
        public void GetCodPassword_abc_bcd()
        {
            // Что подвется на вход
            string strIn = "abc";
            // Что должно возвращать
            string strExpected = "bcd";

            // Возвращаемое значение
            string strActual = CodePassword.GetCodPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }

       
    }
}
