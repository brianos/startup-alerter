using log4net;
using System;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Text;
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
            log.Info("alert: " + startUpAlertData.Body);

            var twilioCreateMessageOptions = GetTwilioCreateMessageOptions(startUpAlertData);
            ConfigureTwilioRestClient(startUpAlertData);
            SendStartUpAlert(twilioCreateMessageOptions);

            log.Info("app end");
        }

        private static StartUpAlertData GetStartUpAlertData()
        {
            var accountSid = ConfigurationManager.AppSettings["accountSid"];
            var authToken = ConfigurationManager.AppSettings["authToken"];
            var toNumber = ConfigurationManager.AppSettings["toNumber"];
            var fromNumber = ConfigurationManager.AppSettings["fromNumber"];

            var body = new StringBuilder();
            body.AppendLine("StartUp: " + DateTime.Now);
            body.Append("IP: " + GetIpAddress());

            return new StartUpAlertData(accountSid, authToken, fromNumber, toNumber, body.ToString());
        }

        private static string GetIpAddress()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);
            IPAddress ipAddress = ipHostInfo.AddressList
                                            .FirstOrDefault(
                                                a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            return ipAddress.ToString();
        }

        private static CreateMessageOptions GetTwilioCreateMessageOptions(StartUpAlertData startUpAlertData)
        {
            var fromNumber = new PhoneNumber(startUpAlertData.FromNumber);
            var toNumber = new PhoneNumber(startUpAlertData.ToNumber);

            return new CreateMessageOptions(toNumber)
            {
                From = fromNumber,
                Body = startUpAlertData.Body
            };
        }

        private static void ConfigureTwilioRestClient(StartUpAlertData startUpAlertData)
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
