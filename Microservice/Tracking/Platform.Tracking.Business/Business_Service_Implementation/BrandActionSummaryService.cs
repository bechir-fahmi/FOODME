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
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class BrandActionSummaryService: IBrandActionSummaryService
    {
        private readonly IUnitOfWork<BrandActionSummaryEntity> _brandActionSummaryRepository;
        private readonly IUnitOfWork<BrandActionSummaryView> _brandActionSummaryViewRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.BrandActionSummaryCacheKey;

        public BrandActionSummaryService(IUnitOfWork<BrandActionSummaryEntity> brandActionSummaryRepository,
            IUnitOfWork<BrandActionSummaryView> brandActionSummaryViewRepository,
            IMapper mapper,
            ICacheService cache)
        {
            _brandActionSummaryRepository = brandActionSummaryRepository;
            _brandActionSummaryViewRepository = brandActionSummaryViewRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public List<BrandActionSummaryView> getBrandActionSummaryViews()
        {
            var BrandActionSummaryViews = _brandActionSummaryViewRepository.Repository.GetAll().ToList();
            return BrandActionSummaryViews;
        }
        public List<BrandActionSummaryDTO> GetAllBrandActionSummary()
        {
            //var cachedData = _cache.GetData<List<BrandActionSummaryDTO>>(_cacheKey);
            //if (cachedData != null && cachedData.Count != 0)
            //{
            //    return cachedData;
            //}
            //else
            //{
                IList<BrandActionSummaryEntity> brandActionSummaryEntities = _brandActionSummaryRepository.Repository.GetAll();
                IList<BrandActionSummary> brandActionSummaries = _mapper.Map<IList<BrandActionSummary>>(brandActionSummaryEntities);
                IList<BrandActionSummaryDTO> brandActionsummaryDTOs = _mapper.Map<IList<BrandActionSummaryDTO>>(brandActionSummaries);
                /*_cache.SetData(_cacheKey, brandActionsummaryDTOs, DateTimeOffset.UtcNow.AddMinutes(1))*/;
                return (List<BrandActionSummaryDTO>)brandActionsummaryDTOs;
            //}
        }
        public List<BrandActionSummaryTotalUsersDTO> GetAllBrandActionSummaryTotalUsers()
        {
            //var cachedData = _cache.GetData<List<BrandActionSummaryDTO>>(_cacheKey);
            //if (cachedData != null && cachedData.Count != 0)
            //{
            //    return cachedData;
            //}
            //else
            //{
            var  brandActionSummaryEntities = _brandActionSummaryRepository.Repository.GetAll().GroupBy(x=>x.BrandModelId).Select(group => new
            {
                key = group.Key,
                Items = group.ToList()
            }) .ToList();
            var BrandActionSummaryTotalUsersDTOList = new List<BrandActionSummaryTotalUsersDTO> { };
            foreach (var brandActionSummaryEntity in brandActionSummaryEntities)
            {
                var BrandActionSummaryTotalUsersDTO = new BrandActionSummaryTotalUsersDTO
                {
                    BrandModelId= brandActionSummaryEntity.Items[0].BrandModelId,
                    ViewDetailsCount= brandActionSummaryEntity.Items.Sum(x=>x.ViewDetailsCount),
                    GoToAppCount = brandActionSummaryEntity.Items.Sum(x => x.GoToAppCount),
                    TotalUsers = brandActionSummaryEntity.Items.Count(),

                };
                BrandActionSummaryTotalUsersDTOList.Add(BrandActionSummaryTotalUsersDTO);
            }

            /*_cache.SetData(_cacheKey, brandActionsummaryDTOs, DateTimeOffset.UtcNow.AddMinutes(1))*/
            ;
            return (List<BrandActionSummaryTotalUsersDTO>)BrandActionSummaryTotalUsersDTOList;
            //}
        }
        public async Task<ResponseDTO> AddOrUpdateBrandActionSummary(List<BrandActionSummaryView> BrandActionSummaryViews)
        {
            foreach (var BrandActionSummaryView in BrandActionSummaryViews)
            {
                BrandActionSummaryDTO brandActionSummaryDTO = new BrandActionSummaryDTO();
                if (BrandActionSummaryView.TypeOfAction == 0)
                {
                     brandActionSummaryDTO = new BrandActionSummaryDTO()
                    {
                        UserId = BrandActionSummaryView.UserId,
                        BrandModelId = BrandActionSummaryView.BrandModelId,
                        ViewDetailsCount = BrandActionSummaryView.TypeOfActionCount

                    };

                }
                else if (BrandActionSummaryView.TypeOfAction == 1)
                {
                    brandActionSummaryDTO = new BrandActionSummaryDTO()
                    {
                        UserId = BrandActionSummaryView.UserId,
                        BrandModelId = BrandActionSummaryView.BrandModelId,
                        GoToAppCount = BrandActionSummaryView.TypeOfActionCount

                    };

                }
                BrandActionSummary brandActionSummary = _mapper.Map<BrandActionSummary>(brandActionSummaryDTO);
                BrandActionSummaryEntity brandActionSummaryEntity = _mapper.Map<BrandActionSummaryEntity>(brandActionSummary);
                var BrandActionSummaryExist = getBrandActionSummary(BrandActionSummaryView.UserId, BrandActionSummaryView.BrandModelId);
                if (BrandActionSummaryExist != null)
                {
                    brandActionSummaryEntity.Id = BrandActionSummaryExist.Id;
                    if (BrandActionSummaryView.TypeOfAction == 0)
                    {
                        brandActionSummaryEntity.GoToAppCount = BrandActionSummaryExist.GoToAppCount;
                    }
                    else if (BrandActionSummaryView.TypeOfAction == 1)
                    {
                        brandActionSummaryEntity.ViewDetailsCount= BrandActionSummaryExist.ViewDetailsCount;
                    }
                    _brandActionSummaryRepository.Repository.Update(brandActionSummaryEntity);
                    _brandActionSummaryRepository.Save();

                }
                else
                {
                    brandActionSummaryEntity.Id = Guid.NewGuid();
                    _brandActionSummaryRepository.Repository.Insert(brandActionSummaryEntity);
                    _brandActionSummaryRepository.Save();
                }


            }           
                return new ResponseDTO
                {
                    StatusCodes = 200,
                    Error = "",
                    Message = "update Brand action summary is successfull"

                };      

        }

        private BrandActionSummaryEntity? getBrandActionSummary(Guid UserId, Guid BrandModelId)
        {
            return _brandActionSummaryRepository.Repository.Get(x => x.UserId == UserId && x.BrandModelId == BrandModelId);

        }
    }
}
