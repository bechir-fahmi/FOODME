using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Platform.NotificationSystems.DtoModel.SMS;
using Platform.NotificationSystems.ViewModels.SMS._4jawaly;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Net;
using System.Text;

namespace Platform.NotificationSystems.Business.business_services.SMS._4jawaly
{
    public class _4jawalyService : I4jawalyService
    {
        private readonly ILogger<ResponseDTO> _logger;

        public _4jawalyService(ILogger<ResponseDTO> logger) 
        {
            _logger = logger;
        }
        public async Task<ResponseDTO> Send4jawaly(SMSProviderDTO sMSProvider, List<message> messages)
        {
            ResponseDTO response = new ();
            var senddata = new SendData
            {
                messages = messages,
                globals = new Globals()
                {
                    number_iso = "SA",
                    sender = sMSProvider.Sender
                }
            };

            string credentials = sMSProvider.AppKey + ":" + sMSProvider.AppSecret;

            var httpRequest = (HttpWebRequest)WebRequest.Create(sMSProvider.Endpoint.UrlTemplate);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.UserAgent = sMSProvider.UserAgent;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));


            var data = await Task.Run(() => JsonConvert.SerializeObject(senddata));

            try
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string result = "";
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }

                _4jawalyRoot myDeserializedClass = JsonConvert.DeserializeObject<_4jawalyRoot>(result);

                response = new ResponseDTO
                {
                    StatusCodes = myDeserializedClass.code,
                    Message = myDeserializedClass.message,
                };

            }
            catch (Exception e)
            {
                _logger.LogWarning("Exception in Send4jawaly : " + e.Message);
                response = new()
                {
                    StatusCodes = 500,
                    Message = "Exception in Send4jawaly : " + e.Message
                };
            }

            return response;
        }
    }
}
