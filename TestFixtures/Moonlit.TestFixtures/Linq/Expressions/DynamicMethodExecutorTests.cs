﻿using Moonlit.Diagnostics;
using Moonlit.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Moonlit.TestFixtures.Linq.Expressions
{


    /// <summary>
    ///这是 DynamicMethodExecutorTest 的测试类，旨在
    ///包含所有 DynamicMethodExecutorTest 单元测试
    ///</summary>
    [TestClass()]
    public class DynamicMethodExecutorTests
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
      

        /// <summary>
        ///Execute 的测试
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            try
            {
                Type mockType = typeof(Mock);
                var mock = new Mock();
                var set = mockType.GetProperty("Sum").GetSetMethod();
                DynamicMethodExecutor executor = new DynamicMethodExecutor(set);
                Action action=() =>
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        executor.Execute(mock, new object[] { i });
                    }
                };

                this.TestContext.WriteLine(action.Elapsed(1, false).ToString());
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(ex.ToString());
                TestContext.WriteLine(ex.GetType().Name);
            }

            try
            {
                Type mockType = typeof(Mock);
                var mock = new Mock();
                var set = mockType.GetProperty("Sum").GetSetMethod();
               Action action=() =>
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        set.Invoke(mock, new object[] { i });
                    }
                };
               this.TestContext.WriteLine(action.Elapsed(1, false).ToString());
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(ex.ToString());
                TestContext.WriteLine(ex.GetType().Name);
            }
        }
        class Mock
        {
            int _sum = 0;

            public int Sum
            {
                get { return _sum; }
                set { _sum = value; }
            }
        }
    }
}
