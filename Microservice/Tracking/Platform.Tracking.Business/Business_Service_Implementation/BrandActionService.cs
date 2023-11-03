using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.Response;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class BrandActionService : IBrandActionService
    {
        private readonly IUnitOfWork<BrandActionEntity> _brandActionRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.BrandActionCacheKey;

        public BrandActionService(IUnitOfWork<BrandActionEntity> brandActionRepository,
            IMapper mapper
            ,
            ICacheService cache
            )
        {
            _brandActionRepository = brandActionRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public List<BrandActionDTO> GetAllBrandActions()
        {
            var cachedData = _cache.GetData<List<BrandActionDTO>>(_cacheKey);
            if (cachedData != null && cachedData.Count != 0)
            {
                return cachedData;
            }
            else
                {
                IList<BrandActionEntity> brandActionEntities = _brandActionRepository.Repository.GetAll();
                IList<BrandAction> brandActions = _mapper.Map<IList<BrandAction>>(brandActionEntities);
                IList<BrandActionDTO> brandActionDTOs = _mapper.Map<IList<BrandActionDTO>>(brandActions);
                _cache.SetData(_cacheKey, brandActionDTOs, DateTimeOffset.UtcNow.AddDays(1));
                return (List<BrandActionDTO>)brandActionDTOs;
            }
        }
        public BrandActionDTO AddBrandAction(BrandActionDTO brandActionDTO, TypeOfAction typeOfAction, bool updateCache = true)
        {
            BrandAction brandAction = _mapper.Map<BrandAction>(brandActionDTO);
            brandAction.TypeOfAction = typeOfAction;
            brandAction.TimeOfAction = DateTime.Now;
            BrandActionEntity brandActionEntity = _mapper.Map<BrandActionEntity>(brandAction);
            _brandActionRepository.Repository.Insert(brandActionEntity);
            _brandActionRepository.Save();
            brandActionDTO.Id = brandActionEntity.Id;
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }

            return brandActionDTO;
        }

        public PagedList<BrandActionDTO> GetAll(PagedParameters pagedParameters)
        {
            var brandActionDTOList = GetAllBrandActions();
            return PagedList<BrandActionDTO>.ToGenericPagedList(brandActionDTOList, pagedParameters);
        }


        public Dictionary<string, int> GetAllActionsByBrand(TypeOfAction typeOfAction)
        {
            var brandActionDTOs = GetAllBrandActions()
                .Where(brandAction => brandAction.TypeOfAction == typeOfAction)
                .GroupBy(brandAction => brandAction.BrandName.ToLower())
                .ToDictionary(group => group.Key, group => group.ToList().Count);


            return brandActionDTOs;
        }

        /******************************************************************************************************************/

        public Dictionary<Guid, int> GetAllActionsByBrandId(TypeOfAction typeOfAction)
        {
            var brandActionDTOs = GetAllBrandActions()
                .Where(brandAction => brandAction.TypeOfAction == typeOfAction)
                .GroupBy(brandAction => brandAction.BrandModelId)
                .ToDictionary(group => group.Key, group => group.ToList().Count);


            return brandActionDTOs;
        }

        /*****************************************************************************************************************/


        public Dictionary<string, int> GetAllActionsByPeriodOfTime(TypeOfAction typeOfAction, DateTime startTime, DateTime endTime)
        {

            var brandActionDTOs = GetAllBrandActions()
                .Where(brandAction => brandAction.TypeOfAction == typeOfAction && brandAction.TimeOfAction >= startTime && brandAction.TimeOfAction <= endTime)
                .GroupBy(brandAction => brandAction.BrandName)
                .ToDictionary(group => group.Key, group => group.ToList().Count);


            return brandActionDTOs;
        }

        public Dictionary<string, int> GetAllBrandActionsByPeriodOfTime(Guid BrandModelId, TypeOfAction typeOfAction, DateTime startTime, DateTime endTime)
        {

            var brandActionDTOs = GetAllBrandActions()
                .Where(brandAction => brandAction.TypeOfAction == typeOfAction && brandAction.BrandModelId == BrandModelId && brandAction.TimeOfAction >= startTime && brandAction.TimeOfAction <= endTime)
                .GroupBy(brandAction => brandAction.BrandName)
                .ToDictionary(group => group.Key, group => group.ToList().Count);


            return brandActionDTOs;
        }

      
    }
}
