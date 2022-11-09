using EvtSource;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiPushClient
{
    public class ApiPushClient
    {
        private Logger logger;
        public ApiPushClient(Logger log)
        {
            logger = log;
        }

        internal bool GetUserInput(out int trainno)
        {
            logger.Write("Enter train number: ");
            string input = Console.ReadLine();
            if (!Int32.TryParse(input, out trainno))
            {
                logger.Log($"Could not parse '{input}' as a train number.");
                return false;
            }
            return true;
        }

        internal string CreateRequestQuery(string authenticationkey, int trainno)
        {
            var request = "<REQUEST>" +
                            $"<LOGIN authenticationkey='{authenticationkey}' />" +
                             "<QUERY objecttype='TrainAnnouncement' schemaversion='1.3' orderby='TimeAtLocation' sseurl='true' >" +
                                "<FILTER>" +
                                    $"<EQ name='ScheduledDepartureDateTime' value='{DateTime.Now.Date}' />" +
                                    $"<EQ name='AdvertisedTrainIdent' value='{trainno.ToString()}' />" +
                                     "<EXISTS name='TimeAtLocation' value='true' />" +
                                "</FILTER>" +
                                "<INCLUDE>LocationSignature</INCLUDE>" +
                                "<INCLUDE>ActivityType</INCLUDE>" +
                                "<INCLUDE>TimeAtLocation</INCLUDE>" +
                            "</QUERY>" +
                        "</REQUEST>";
            return request;
        }

        internal void ConnectToSseEndPoint(string sseurl)
        {
            var evt = new EventSourceReader(new Uri(sseurl)).Start();
            logger.Log("Connected to SSE endpoint for push notifications, press any key to abort.");
            Task.Run(() => { Spin(); });
            evt.MessageReceived += (object sender, EventSourceMessageEventArgs ev) => RenderResponse(JsonConvert.DeserializeObject<Rootobject>(ev.Message));
            evt.Disconnected += async (object sender, DisconnectEventArgs e) =>
            {
                logger.Log($"Reconnecting - Error: {e.Exception.Message}");
                await Task.Delay(e.ReconnectDelay);
                evt.Start(); // Reconnect to the same URL
            };
        }

        internal string SendRequest(string url, string requestquery)
        {
            logger.Log($"Fetching data from {url} ...");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync(url, new StringContent(requestquery, Encoding.UTF8, "application/xml")).Result;
                var resultstring = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Server responded with status code " + response.StatusCode);
                }
                return resultstring;
            }
        }

        internal string GetSseUrl(Rootobject result)
        {
            return result?.RESPONSE?.RESULT?[0].INFO?.SSEURL;
        }
        internal void RenderResponse(Rootobject result)
        {
            logger.Log("STATION\tTYPE\tTIME");
            if (result?.RESPONSE?.RESULT?[0].TrainAnnouncement?.Length > 0)
            {
                foreach (var trainannouncement in result.RESPONSE.RESULT[0].TrainAnnouncement)
                {
                    var timeatlocation = trainannouncement.TimeAtLocation == new DateTime() ? "" : trainannouncement.TimeAtLocation.ToShortTimeString();
                    logger.Log($"{trainannouncement.LocationSignature}\t{trainannouncement.ActivityType}\t{timeatlocation}");
                };
            }
            if (result?.RESPONSE?.RESULT?[0].TrainAnnouncement?.Length == 0)
            {
                logger.Log($"(No train activity recorded for this train today.)");
            }
            
        }

        private void Spin()
        {
            char[] spinnerAnimationFrames = new[] { '|', '/', '-', '\\' };
            int currentAnimationFrame = 0;
            Random rnd = new Random();
            while (true)
            {
                logger.Write(string.Format("{0}", spinnerAnimationFrames[currentAnimationFrame]));
                currentAnimationFrame++;
                if (currentAnimationFrame == spinnerAnimationFrames.Length)
                    currentAnimationFrame = 0;
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
