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
    public class MealTypeService : IMealTypeService
    {
        private readonly IUnitOfWork<MealTypeEntity> _mealRepository;
        private readonly IUnitOfWork<TagMealTypeEntity> _tagMealTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.MealTypeCaheKey;

        public MealTypeService(IUnitOfWork<MealTypeEntity> mealRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService, IUnitOfWork<TagMealTypeEntity> tagmealTypeRepository)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
            _tagMealTypeRepository = tagmealTypeRepository;
        }

        public void Add(MealTypeDTO refDataDTO, bool updateCache = true)
        {
            MealType mealTypeBM = _mapper.Map<MealType>(refDataDTO);
            MealTypeEntity mealType = _mapper.Map<MealTypeEntity>(mealTypeBM);
            _mealRepository.Repository.Attach(mealType);
            _mealRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public MealTypeDTO Get(int id)
        {
            var mealTypeDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return mealTypeDTO;
        }
        public PagedList<MealTypeDTO> Get(string tag, PagedParameters pagedParameters)
        {
            var mealTpeDTOByTag = GetAll().Where(x => x.Tags.Any(y => y.value == tag)).ToList();
            return PagedList<MealTypeDTO>.ToGenericPagedList(mealTpeDTOByTag, pagedParameters);
        }
        public MealTypeDTO Get(Expression<Func<MealTypeEntity, bool>> expression)
        {
            Expression<Func<MealTypeDTO, bool>> exp = _mapper.Map<Expression<Func<MealTypeDTO, bool>>>(expression);
            List<MealTypeDTO> mealTypeList = GetAll();
            MealTypeDTO mealType = null;
            if (mealTypeList != null && mealTypeList.Count > 0)
            {
                mealType = ((IQueryable<MealTypeDTO>)mealTypeList).FirstOrDefault(exp);
            }

            return mealType;
        }

        public PagedList<MealTypeDTO> GetAll(PagedParameters pagedParameters)
        {
            var mealTypeDtoList = GetAll();

            return PagedList<MealTypeDTO>.ToGenericPagedList(mealTypeDtoList, pagedParameters);
        }
        public PagedList<MealTypeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var mealTypeDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<MealTypeDTO>.ToGenericPagedList(mealTypeDtoList, pagedParameters);
        }

        public List<MealTypeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<MealTypeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
            var mealTypeList = _mealRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource",  "Tags"}).Where(x => x.Status == Status.isActive);
                var mealBMList = _mapper.Map<IList<MealType>>(mealTypeList);
            var mealTypeDtoList = _mapper.Map<IList<MealTypeDTO>>(mealBMList);
                _cache.SetData(_cacheKey, mealTypeDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<MealTypeDTO>)mealTypeDtoList;
            }
        }

        public void Remove(MealTypeDTO refDataDTO, bool updateCache = true)
        {
            MealType mealTypeBM = _mapper.Map<MealType>(refDataDTO);
            MealTypeEntity mealType = _mapper.Map<MealTypeEntity>(mealTypeBM);
            mealType.Status = Shared.Enum.Status.isDeleted;
            _mealRepository.Repository.Update(mealType);
            _mealRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idMealType, List<TagMealTypeEntity> tags)
        {
            var oldTags = _tagMealTypeRepository.Repository.GetAll(x => x.MealTypeId == idMealType);
            if (oldTags.Count >= 1)
            {
                _tagMealTypeRepository.Repository.DeleteRange(oldTags);
                _tagMealTypeRepository.Save();
            }
        }
        public void Update(MealTypeDTO refDataDTO, bool updateCache = true)
        {
            MealTypeDTO mealTypeExist = Get(refDataDTO.Id);
            if(mealTypeExist != null)
            {
                foreach (var LanguageRessource in refDataDTO.LanguageResourceSet?.LanguageRessource)
                {
                    if(LanguageRessource.Image != mealTypeExist.LanguageResourceSet?.LanguageRessource[0]?.Image)
                    {
                        var image = LanguageRessource.Image;
                        if (!string.IsNullOrEmpty(image))
                        {
                            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                            LanguageRessource.Image = imageURL;
                        }
                    }
                    
                }

                MealType mealTypeBM = _mapper.Map<MealType>(refDataDTO);
                MealTypeEntity mealType = _mapper.Map<MealTypeEntity>(mealTypeBM);
                _languageResourceService.deleteOldLanguageResources(mealType.LanguageResourceSet.LanguageResourceSetId, mealType.LanguageResourceSet.LanguageRessource);
                deleteOldTags(mealType.Id, mealType.Tags);
                _mealRepository.Repository.Update(mealType);
                _mealRepository.Save();
                if (updateCache)
                    _cache.RemoveData(_cacheKey);
            }
            
        }

        public List<MealTypeDTO> GetFilteredData(MealTypeFilterDTO filter)
        {

            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
            && (string.IsNullOrEmpty(filter.Name) || (x.LanguageResourceSet.LanguageRessource != null && x.LanguageResourceSet.LanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
            return refDataDtoList;
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
