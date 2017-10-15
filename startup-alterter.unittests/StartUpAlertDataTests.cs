using NUnit.Framework;
using startup_alerter;
using System;

namespace startup_alterter.unittests
{
    [TestFixture]
    public class StartUpAlertDataTests
    {
        [TestCase("", "authToken", "fromNumber", "toNumber", "body")]
        [TestCase("accountSid", "", "fromNumber", "toNumber", "body")]
        [TestCase("accountSid", "authToken", "", "toNumber", "body")]
        [TestCase("accountSid", "authToken", "fromNumber", "", "body")]
        [TestCase("accountSid", "authToken", "fromNumber", "toNumber", "")]
        public void ValidationShouldPreventEmptyInput(string accountSid, string authToken,
                                                    string fromNumber, string toNumber,
                                                    string body)
        {
            Assert.Throws<ArgumentException>(
                () => new StartUpAlertData(accountSid, authToken, fromNumber, toNumber, body));
        }

        [Test]
        public void ConstructorShouldSetTheAccountSid()
        {
            var startUpAlertData = new StartUpAlertData("accountSid", "authToken",
                                                        "fromNumber", "toNumber",
                                                        "body");

            Assert.AreEqual("accountSid", startUpAlertData.AccountSid);
        }

        [Test]
        public void ConstructorShouldSetTheAuthToken()
        {
            var startUpAlertData = new StartUpAlertData("accountSid", "authToken",
                                                        "fromNumber", "toNumber",
                                                        "body");

            Assert.AreEqual("authToken", startUpAlertData.AuthToken);
        }

        [Test]
        public void ConstructorShouldSetTheFromNumber()
        {
            var startUpAlertData = new StartUpAlertData("accountSid", "authToken",
                                                        "fromNumber", "toNumber",
                                                        "body");

            Assert.AreEqual("fromNumber", startUpAlertData.FromNumber);
        }

        [Test]
        public void ConstructorShouldSetTheToNumber()
        {
            var startUpAlertData = new StartUpAlertData("accountSid", "authToken",
                                                        "fromNumber", "toNumber",
                                                        "body");

            Assert.AreEqual("toNumber", startUpAlertData.ToNumber);
        }

        [Test]
        public void ConstructorShouldSetTheBody()
        {
            var startUpAlertData = new StartUpAlertData("accountSid", "authToken",
                                                        "fromNumber", "toNumber",
                                                        "body");

            Assert.AreEqual("body", startUpAlertData.Body);
        }
    }
}
