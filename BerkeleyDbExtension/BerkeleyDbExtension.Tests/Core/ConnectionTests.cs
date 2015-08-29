using System.Collections.Generic;
using BerkeleyDbExtension.Core;
using BerkeleyDbExtension.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerkeleyDbExtension.Tests.Core
{
    [TestClass]
    public class ConnectionTests
    {
        private Connection<string, string> instance;

        [TestMethod]
        public void ConnectionTest()
        {
            if (instance == null)
                instance = new Connection<string, string>(@"c:\test.dbd");

            Assert.IsNotNull(instance);
        }


        [TestMethod]
        public void OpenTest()
        {
            ConnectionTest();
            if (instance != null)
            {
                if (!instance.IsOpen)
                {
                    instance.Open();
                    Assert.IsTrue(instance.IsOpen);
                }
            }
            else Assert.Fail("Invalid connection instance.");
        }

        [TestMethod]
        public void CloseTest()
        {
            ConnectionTest();
            if (instance != null)
            {
                Assert.IsTrue(Utilities.IsSuccess(() => instance.Close()));
            }
            else Assert.Fail("Invalid connection instance.");
        }

        [TestMethod]
        public void AddTest()
        {
            ConnectionTest();
            if (instance != null)
            {
                if (instance.IsOpen)
                {
                    bool success = Utilities.IsSuccess(() => instance.Add("Hello", "World"));

                    Assert.IsTrue(success);
                }
            }
            else Assert.Fail("Invalid connection instance.");
        }

        [TestMethod]
        public void AddTest1()
        {
            ConnectionTest();
            if (instance != null)
            {
                if (instance.IsOpen)
                {
                    bool success =
                        Utilities.IsSuccess(
                            () =>
                                instance.Add(new Dictionary<string, string>
                                {
                                    {"Hello", "World"},
                                    {"Berkeley", "Rocks"}
                                }));

                    Assert.IsTrue(success);
                }
            }
            else Assert.Fail("Invalid connection instance.");
        }
    }
}