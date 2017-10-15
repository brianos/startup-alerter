using log4net;
using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace startup_alerter
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main()
        {
            log.Info("app start");

            var startUpAlertData = GetStartUpAlertData();
            var createMessageOptions = GetCreateMessageOptions(startUpAlertData);
            ConfigureRestClient(startUpAlertData);
            SendStartUpAlert(createMessageOptions);

            log.Info("app end");
        }

        private static StartUpAlertData GetStartUpAlertData()
        {
            var accountSid = ConfigurationManager.AppSettings["accountSid"];
            var authToken = ConfigurationManager.AppSettings["authToken"];
            var toNumber = ConfigurationManager.AppSettings["toNumber"];
            var fromNumber = ConfigurationManager.AppSettings["fromNumber"];
            var body = "StartUp: " + DateTime.Now;
            return new StartUpAlertData(accountSid, authToken, fromNumber, toNumber, body);
        }

        private static CreateMessageOptions GetCreateMessageOptions(StartUpAlertData startUpAlertData)
        {
            var fromNumber = new PhoneNumber(startUpAlertData.FromNumber);
            var toNumber = new PhoneNumber(startUpAlertData.ToNumber);

            return new CreateMessageOptions(toNumber)
            {
                From = fromNumber,
                Body = startUpAlertData.Body
            };
        }

        private static void ConfigureRestClient(StartUpAlertData startUpAlertData)
        {
            TwilioClient.Init(startUpAlertData.AccountSid, startUpAlertData.AuthToken);
        }

        private static void SendStartUpAlert(CreateMessageOptions createMessageOptions)
        {
            var messageResource = MessageResource.Create(createMessageOptions);
            if (messageResource.Sid == String.Empty)
            {
                var errorMessage = "Message sending failed.";
                log.Error(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
