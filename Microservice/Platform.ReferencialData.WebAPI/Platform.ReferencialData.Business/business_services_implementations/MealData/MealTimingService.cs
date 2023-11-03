using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.MealData;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.MealData
{
    public class MealTimingService : IMealTimingService
    {
        private readonly IUnitOfWork<MealTimingEntity> _mealTimingRepository;
        private readonly IUnitOfWork<TagMealTimingEntity> _tagMealTimingRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.MealTimingCaheKey;

        public MealTimingService(IUnitOfWork<MealTimingEntity> mealTimingRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService, IUnitOfWork<TagMealTimingEntity> tagMealTimingRepository)
        {
            _mealTimingRepository = mealTimingRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
            _tagMealTimingRepository = tagMealTimingRepository;
        }

        public void Add(MealTimingDTO refDataDTO, bool updateCache = true)
        {
            MealTiming mealTimingBM = _mapper.Map<MealTiming>(refDataDTO);
            MealTimingEntity mealTiming = _mapper.Map<MealTimingEntity>(mealTimingBM);
            _mealTimingRepository.Repository.Attach(mealTiming);
            _mealTimingRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public MealTimingDTO Get(int id)
        {
            var mealTimingDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return mealTimingDTO;
        }

        public MealTimingDTO Get(Expression<Func<MealTimingEntity, bool>> expression)
        {
            Expression<Func<MealTimingDTO, bool>> exp = _mapper.Map<Expression<Func<MealTimingDTO, bool>>>(expression);
            List<MealTimingDTO> mealTimingList = GetAll();
            MealTimingDTO mealTiming = null;
            if (mealTimingList != null && mealTimingList.Count > 0)
            {
                mealTiming = ((IQueryable<MealTimingDTO>)mealTimingList).FirstOrDefault(exp);
            }

            return mealTiming;
        }

        public PagedList<MealTimingDTO> GetAll(PagedParameters pagedParameters)
        {
            var mealTimingDtoList = GetAll();

            return PagedList<MealTimingDTO>.ToGenericPagedList(mealTimingDtoList, pagedParameters);
        }  
        public PagedList<MealTimingDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var mealTimingDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<MealTimingDTO>.ToGenericPagedList(mealTimingDtoList, pagedParameters);
        }
        public PagedList<MealTimingDTO> Get(string tag, PagedParameters pagedParameters)
        {
            var mealTimingDTOByTag = GetAll().Where(x => x.Tags.Any(y => y.value == tag)).ToList();
            return PagedList<MealTimingDTO>.ToGenericPagedList(mealTimingDTOByTag, pagedParameters);
        }

        public List<MealTimingDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<MealTimingDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var mealTimingList = _mealTimingRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource",  "Tags"}).Where(x => x.Status == Status.isActive);
                var mealTimingBMList = _mapper.Map<IList<MealTiming>>(mealTimingList);
                var mealTimingDtoList = _mapper.Map<IList<MealTimingDTO>>(mealTimingBMList);
                _cache.SetData(_cacheKey, mealTimingDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<MealTimingDTO>)mealTimingDtoList;
            }
        }

        public void Remove(MealTimingDTO refDataDTO, bool updateCache = true)
        {
            MealTiming mealTimingBM = _mapper.Map<MealTiming>(refDataDTO);
            MealTimingEntity mealTiming = _mapper.Map<MealTimingEntity>(mealTimingBM);
            mealTiming.Status = Shared.Enum.Status.isDeleted;
            _mealTimingRepository.Repository.Update(mealTiming);
            _mealTimingRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idMealTiming, List<TagMealTimingEntity> tags)
        {
            var oldTags = _tagMealTimingRepository.Repository.GetAll(x => x.MealTimingId == idMealTiming);
            if (oldTags.Count >= 1)
            {
                _tagMealTimingRepository.Repository.DeleteRange(oldTags);
                _tagMealTimingRepository.Save();
            }
        }
        public void Update(MealTimingDTO refDataDTO, bool updateCache = true)
        {
            MealTimingDTO mealTimeExist = Get(refDataDTO.Id);
            if(mealTimeExist != null)
            {
                foreach (var LanguageRessource in refDataDTO.LanguageResourceSet?.LanguageRessource)
                {
                    if(LanguageRessource.Image != mealTimeExist.LanguageResourceSet?.LanguageRessource[0]?.Image)
                    {
                        var image = LanguageRessource.Image;
                        if (!string.IsNullOrEmpty(image))
                        {
                            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                            LanguageRessource.Image = imageURL;
                        }
                    }
                    

                }
                MealTiming mealTimingBM = _mapper.Map<MealTiming>(refDataDTO);
                MealTimingEntity mealTiming = _mapper.Map<MealTimingEntity>(mealTimingBM);
                _languageResourceService.deleteOldLanguageResources(mealTiming.LanguageResourceSet.LanguageResourceSetId, mealTiming.LanguageResourceSet.LanguageRessource);
                deleteOldTags(mealTiming.Id, mealTiming.Tags);
                _mealTimingRepository.Repository.Update(mealTiming);
                _mealTimingRepository.Save();
                if (updateCache)
                    _cache.RemoveData(_cacheKey);
            }
            
        }

        public List<MealTimingDTO> GetFilteredData(MealTimingFilterDTO filter)
        {

            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
            && (string.IsNullOrEmpty(filter.Name) || (x.LanguageResourceSet.LanguageRessource != null && x.LanguageResourceSet.LanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
            return refDataDtoList;
        }

        public MealTimingDTO Get(string tag)
        {
            throw new NotImplementedException();
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}