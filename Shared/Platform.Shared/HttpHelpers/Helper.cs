using AutoMapper;
using Microsoft.AspNetCore.Http;
using Platform.Shared.SharedClasses.Pagination;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Platform.Shared.HttpHelpers
{
    public class Helper<A, B, C> : IHelper<A, B, C> where A : class
                                                    where B : class
                                                    where C : class
    {
        public IList<C> GetData(IMapper mapper, string endPoint, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                IList<C> viewModelList;
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var stream = response.Content.ReadAsStream();
                        List<A> dtoModelList = JsonSerializer.Deserialize<List<A>>(stream, jsonOptions);
                        IList<B> businessModelList = mapper.Map<IList<B>>(dtoModelList);
                        viewModelList = mapper.Map<IList<C>>(businessModelList);
                        return viewModelList;
                    }

                    return null;
                }
                return null;

            }
        }

        public IList<C> GetData(IMapper mapper, string endPoint, string authorization, ref PaginationData metadata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                IList<C> viewModelList;
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var header = response.Headers.FirstOrDefault(x => x.Key == "X-Pagination");
                        if (header.Value != null)
                            metadata = Newtonsoft.Json.JsonConvert.DeserializeObject<PaginationData>(((string[])header.Value)[0]);

                        var stream = response.Content.ReadAsStream();
                        List<A> dtoModelList = JsonSerializer.Deserialize<List<A>>(stream, jsonOptions);
                        IList<B> businessModelList = mapper.Map<IList<B>>(dtoModelList);
                        viewModelList = mapper.Map<IList<C>>(businessModelList);
                        return viewModelList;
                    }

                    return null;
                }
                return null;

            }
        }

        public C GetObjData(IMapper mapper, string endPoint, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                C viewModel;
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var stream = response.Content.ReadAsStream();
                        A dtoModel = JsonSerializer.Deserialize<A>(stream, jsonOptions);
                        B businessModel = mapper.Map<B>(dtoModel);
                        viewModel = mapper.Map<C>(businessModel);
                        return viewModel;
                    }

                    return null;
                }
                return null;

            }
        }

        public C Get(IMapper mapper, string endPoint, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                C viewModelList;
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        using var stream = response.Content.ReadAsStream();
                        A dtoModelList = JsonSerializer.Deserialize<A>(stream, jsonOptions);
                        B businessModelList = mapper.Map<B>(dtoModelList);
                        viewModelList = mapper.Map<C>(businessModelList);
                        return viewModelList;
                    }

                    return null;
                }
                return null;

            }
        }

        public void Create(IMapper mapper, string endPoint, C element, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    B buisinessElement = mapper.Map<B>(element);
                    A dtoElement = mapper.Map<A>(buisinessElement);

                    var json = JsonSerializer.Serialize(dtoElement);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = httpClient.Send(request);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public HttpResponseMessage Create(string endPoint, C element, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var json = JsonSerializer.Serialize(element);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = httpClient.Send(request);
                    return response;
                }
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                };
            }
        }

        public HttpResponseMessage Create(IMapper mapper, string endPoint, C element)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endPoint))
            {
                HttpClient httpClient = new HttpClient();
                B buisinessElement = mapper.Map<B>(element);
                A dtoElement = mapper.Map<A>(buisinessElement);
                var json = JsonSerializer.Serialize(dtoElement);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.Send(request);
                return response;
            }
        }

        public HttpResponseMessage Create(string endPoint, C element)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endPoint))
            {
                HttpClient httpClient = new HttpClient();

                var json = JsonSerializer.Serialize(element);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = httpClient.Send(request);
                return response;
            }
        }

        public HttpResponseMessage ConsumeAPI(string endPoint, HttpMethod httpMethod)
        {
            using (var request = new HttpRequestMessage(httpMethod, endPoint))
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.Send(request);
                return response;
            }
        }

        public IList<A> GetData(string endPoint)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.Send(request);
                var stream = response.Content.ReadAsStream();
                var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                IList<A> list = JsonSerializer.Deserialize<IList<A>>(stream, jsonOptions);
                return list;
            }
        }

        public A Get(string endPoint)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endPoint))
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.Send(request);
                if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    var stream = response.Content.ReadAsStream();
                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                    A list = JsonSerializer.Deserialize<A>(stream, jsonOptions);
                    return list;
                }
                return null;
            }
        }

        public void UpdatePart(IMapper mapper, string endPoint, C element, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Patch, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;

                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    B businessElement = mapper.Map<B>(element);
                    A dtoElement = mapper.Map<A>(businessElement);

                    var json = JsonSerializer.Serialize(dtoElement);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = httpClient.Send(request);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public void Update(IMapper mapper, string endPoint, C element, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Put, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;

                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    B businessElement = mapper.Map<B>(element);
                    A dtoElement = mapper.Map<A>(businessElement);

                    var json = JsonSerializer.Serialize(dtoElement);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = httpClient.Send(request);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public void Delete(IMapper mapper, string endPoint, C element, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;
                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var response = httpClient.Send(request);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public void Delete(IMapper mapper, string endPoint, string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, endPoint))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;
                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var response = httpClient.Send(request);
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}
