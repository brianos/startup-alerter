using log4net;
using System;


namespace startup_alerter
{
    public class StartUpAlertData
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string AccountSid { get; private set; }
        public string AuthToken { get; private set; }
        public string FromNumber { get; private set; }
        public string ToNumber { get; private set; }
        public string Body { get; private set; }

        public StartUpAlertData(string accountSid, string authToken,
                                string fromNumber, string toNumber,
                                string body)
        {
            GuardInput(accountSid, authToken, fromNumber, toNumber, body);

            AccountSid = accountSid;
            AuthToken = authToken;
            FromNumber = fromNumber;
            ToNumber = toNumber;
            Body = body;
        }

        private void GuardInput(string accountSid, string authToken,
                                string fromNumber, string toNumber,
                                string body)
        {
            GuardStringInput(accountSid, "accountSid");
            GuardStringInput(authToken, "authToken");
            GuardStringInput(fromNumber, "fromNumber");
            GuardStringInput(toNumber, "toNumber");
            GuardStringInput(body, "body");
        }

        private void GuardStringInput(string parameter, string parameterName)
        {
            var errorMessage = parameter + " must be specified.";

            if (String.IsNullOrWhiteSpace(parameter))
            {
                log.Error(parameterName + " IsNullOrWhiteSpace");
                throw new ArgumentException(errorMessage, parameterName);
            }
        }
    }
}
