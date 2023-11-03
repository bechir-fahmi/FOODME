using AutoMapper;
using Microsoft.AspNetCore.Http;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.Shared.HttpHelpers
{
    public interface IHelper<A, B, C>
    {
        public IList<C> GetData(IMapper mapper, string endPoint, string authorization);
        public IList<C> GetData(IMapper mapper, string endPoint, string authorization, ref PaginationData metadata);
        public C GetObjData(IMapper mapper, string endPoint, string authorization);
        public IList<A> GetData(string endPoint);
        public C Get(IMapper mapper, string endPoint, string authorization);
        public A Get(string endPoint);
        public void Create(IMapper mapper, string endPoint, C element, string authorization);
        public HttpResponseMessage Create(string endPoint, C element, string authorization);
        public HttpResponseMessage Create(IMapper mapper, string endPoint, C element);
        public HttpResponseMessage Create(string endPoint, C element);
        /// <summary>
        /// Update with PUT
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="endPoint"></param>
        /// <param name="element"></param>
        /// <param name="authorization"></param>
        public void Update(IMapper mapper, string endPoint, C element, string authorization);
        /// <summary>
        /// Update partiel wit PATCH
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="endPoint"></param>
        /// <param name="element"></param>
        /// <param name="authorization"></param>
        public void UpdatePart(IMapper mapper, string endPoint, C element, string authorization);
        public void Delete(IMapper mapper, string endPoint, C element, string authorization);
        public void Delete(IMapper mapper, string endPoint, string authorization);
        public HttpResponseMessage ConsumeAPI(string endPoint, HttpMethod httpMethod);
    }
}
