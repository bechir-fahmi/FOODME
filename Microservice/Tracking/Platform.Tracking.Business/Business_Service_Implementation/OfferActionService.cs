using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.BusinessModel.Offre;
using Platform.Tracking.DataModel.Offre;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.Offre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class OfferActionService : IOfferActionService
    {
        private readonly IUnitOfWork<OfferActionEntity> _offreActionRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.OfferActionCacheKey;
        public OfferActionService(IUnitOfWork<OfferActionEntity> offreActionRepository, IMapper mapper, ICacheService cache)
        {
            _offreActionRepository= offreActionRepository;
            _mapper= mapper;
            _cache= cache;
        }
        public List<OfferActionDTO> GetAllOfferAction() {
            var cachedData = _cache.GetData<List<OfferActionDTO>>(_cacheKey);
            if (cachedData != null && cachedData.Count != 0)
            {
                return cachedData;
            }
            else
            {
                IList<OfferActionEntity> offreActionEntities= _offreActionRepository.Repository.GetAll();
                IList<OfferAction> offerActions = _mapper.Map<IList<OfferAction>>(offreActionEntities);
                IList<OfferActionDTO>offerActionDTOs=_mapper.Map<IList<OfferActionDTO>>(offerActions);
                return (List<OfferActionDTO>)offerActionDTOs;
            }
        }
        public OfferActionDTO AddOfferAction(OfferActionDTO offerActionDTO ,bool updateCache =true) {
            OfferAction offerAction=_mapper.Map<OfferAction>(offerActionDTO);
            offerAction.TimeOfAction= DateTime.Now;
            OfferActionEntity offerActionEntity=_mapper.Map<OfferActionEntity>(offerAction);
            _offreActionRepository.Repository.Insert(offerActionEntity);
            _offreActionRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
            return offerActionDTO;
            
        }

        public PagedList<OfferActionDTO> GetAll(PagedParameters pagedParameters)
        {
            var offerActionDTOList = GetAllOfferAction();
            return PagedList<OfferActionDTO>.ToGenericPagedList(offerActionDTOList, pagedParameters);
        }

        public Dictionary<DateTime, Dictionary<string, int>> CountClicksByTimeAndSocialMedia(Guid offreId)
        {
            var offerActions = _offreActionRepository.Repository.GetAll()
                .Where(action => action.OffreID == offreId && !string.IsNullOrEmpty(action.SocialMedia))
                .ToList();

            if (offerActions == null || offerActions.Count == 0)
            {
                return null; // Return early if no valid data found.
            }

            var result = new Dictionary<DateTime, Dictionary<string, int>>();

            foreach (var action in offerActions)
            {
                var timestamp = action.TimeOfAction;

                if (!result.TryGetValue(timestamp, out var socialMediaCounts))
                {
                    socialMediaCounts = new Dictionary<string, int>();
                    result[timestamp] = socialMediaCounts;
                }

                if (!socialMediaCounts.TryGetValue(action.SocialMedia, out var count))
                {
                    socialMediaCounts[action.SocialMedia] = 1;
                }
                else
                {
                    socialMediaCounts[action.SocialMedia]++;
                }
            }

            return result;
        }



    }
}
