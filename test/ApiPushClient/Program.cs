using Newtonsoft.Json;
using System;

namespace ApiPushClient
{
    class Program
    {
        private static string apiurl = "https://api.trafikinfo.trafikverket.se/v2/data.json";
        private static string authenticationkey = "yourauthenticationkey";
        private static Logger logger;
        private static ApiPushClient client;
        static void Main(string[] args)
        {
            var sseurl = string.Empty;
            try
            {
                logger = new Logger(Console.Out);
                client = new ApiPushClient(logger);
                logger.Log("*** Trafikverket Open API - Push Client ***");
                int trainno;
                while (!client.GetUserInput(out trainno));
                string requestquery = client.CreateRequestQuery(authenticationkey, trainno);
                string responsestring = client.SendRequest(apiurl, requestquery);
                var result = JsonConvert.DeserializeObject<Rootobject>(responsestring);
                client.RenderResponse(result);
                sseurl = client.GetSseUrl(result);
                if (sseurl != string.Empty)
                {
                    client.ConnectToSseEndPoint(sseurl);
                }
            }
            catch (Exception e)
            {
                logger.Log("An error occured: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
