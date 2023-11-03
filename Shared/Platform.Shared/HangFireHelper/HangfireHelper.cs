namespace Platform.Shared.HangfireHelper
{
    public class HangfireHelper : IHangfireHelper

    {
        public Object Get(string endPoint)
        {
            HttpClient httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            var response = httpClient.Send(request);
            if (response.IsSuccessStatusCode)
            {
                //response.StatusCode = 200;
                //return StatusCode(200);
                // return Ok();
                return response.Content;
            }
            else
            {
                //return BadRequest(response);
                return null;
            }

        }
    }


}