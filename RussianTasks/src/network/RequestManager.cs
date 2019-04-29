using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RussianTasks.src.network
{
    using RequestCallback = Action<bool, JObject>;

    public class RequestManager
    {
        private static RequestManager _instance;
        public const uint INVALID_HANDLE = 0;
        private uint _handleCounter = INVALID_HANDLE;

        public static RequestManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new RequestManager();
            }
            return _instance;
        }

        private RequestManager() {}

        private HttpWebRequest buildRequest(string baseUrl, Dictionary<string, object> requestParams, string requestMethod)
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append(baseUrl);
            builder.Append("?");

            var paramsToPlace = requestParams.Count;
            foreach (KeyValuePair<string, object> param in requestParams)
            {
                builder.Append(string.Format("{0}={1}", param.Key, param.Value.ToString()));
                if (--paramsToPlace != 0)
                {
                    builder.Append("&");
                }
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = requestMethod;
            return request;
        }

        private void log(string tag, string message)
        {
#if DEBUG
            Log.d(string.Format("{0}: {1}", tag, message));
#endif
        }

        private uint getNextHandle()
        {
            return ++_handleCounter;
        }

        public async void performGetRequest(string url, Dictionary<string, object> requestParams, RequestCallback requestCallback)
        {
            string responseStr = null;
            var handle = getNextHandle();
            try
            {
                var request = buildRequest(url, requestParams, "GET");
                log(string.Format("Request [{0}]", handle), request.RequestUri.ToString());
                var resp = await request.GetResponseAsync();
                System.IO.StreamReader stream = new System.IO.StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                responseStr = stream.ReadToEnd();
                stream.Close();
            }
            catch (WebException ex)
            {
                log(string.Format("Request [{0}] Error", handle), ex.Message.ToString());
                requestCallback(false, null);
                return;
            }
           
            if (responseStr != null)
            {
                log(string.Format("Response [{0}]", handle), responseStr);
                try
                {
                    var requestObj = JObject.Parse(responseStr);
                    var data = requestObj["data"];
                    if (data != null)
                    {
                        requestCallback(true, data as JObject);
                    }
                    else
                    {
                        log(string.Format("Request [{0}] Error", handle), "Server response in empty");
                        requestCallback(false, null);
                    }
                }
                catch (Exception e)
                {
                    log(string.Format("Request [{0}] parse Error", handle), e.ToString());
                    requestCallback(false, null);
                }
            }
            else
            {
                log(string.Format("Request [{0}] Error", handle), "Server response in empty");
                requestCallback(false, null);
            }
        }
    }
}
