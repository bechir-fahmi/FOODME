using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.Tracking.BusinessModel.GetDeals;
using Platform.Tracking.DataModel.GetDeals;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.GetDeals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class DealActionService : IDealAction
    {
        private readonly IUnitOfWork<DealActionEntity> _dealActionRepository;
        private readonly IMapper _mapper;
        private ICacheService _cache;
        private readonly string _cacheKey = CacheKey.DealsCacheKey;

        public DealActionService (IUnitOfWork<DealActionEntity> dealActionRepository , IMapper mapper,ICacheService cache)
        {
            _dealActionRepository= dealActionRepository;
            _mapper=mapper;
            _cache=cache;
        }

        public List<DealsActionDTO> GetDealsActions()
        {
            IList<DealActionEntity> dealActionEntities=_dealActionRepository.Repository.GetAll(includes: new List<string> { "Aggregator" });
            IList<DealAction> dealActions = _mapper.Map<IList<DealAction>>(dealActionEntities);
            IList<DealsActionDTO> dealActionDTOs = _mapper.Map<IList<DealsActionDTO>>(dealActions);
            _cache.SetData(_cacheKey, dealActionDTOs,DateTimeOffset.UtcNow.AddDays(1));
            return (List<DealsActionDTO>)dealActionDTOs;
        }

        public DealsActionDTO AddDealAction(DealsActionDTO dealsActionDTO, bool updateCache = true)
        {
            DealAction dealAction = _mapper.Map<DealAction>(dealsActionDTO);
            dealAction.TimeOfAction = DateTime.Now;
            DealActionEntity dealActionEntity = _mapper.Map<DealActionEntity>(dealAction);
            _dealActionRepository.Repository.Insert(dealActionEntity);
            _dealActionRepository.Save();
            dealsActionDTO.Id=dealActionEntity.Id;
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
            return dealsActionDTO;


        }

        public PagedList<DealsActionDTO> GetDealsActionPagedList(PagedParameters pagedParameters)
        {
            var dealActionDTOList = GetDealsActions(); 
            return PagedList<DealsActionDTO>.ToGenericPagedList(dealActionDTOList, pagedParameters);
        }
    }
}
