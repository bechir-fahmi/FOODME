using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.FoodTypeData;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.FoodTypeData
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly IUnitOfWork<FoodTypeEntity> _foodRepository;
        private readonly IUnitOfWork<TagFoodTypeEntity> _tagFoodTypeRepository;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.FoodTypeCaheKey;

        public FoodTypeService(IUnitOfWork<FoodTypeEntity> foodRepository, IMapper mapper, ICacheService cache, IUnitOfWork<TagFoodTypeEntity> tagFoodRepository, ILanguageResourceService languageResourceService)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
            _cache = cache;
            _tagFoodTypeRepository = tagFoodRepository;
            _languageResourceService = languageResourceService;
        }

        public void Add(FoodTypeDTO refDataDTO, bool updateCache = true)
        {
            FoodType foodTypeBM = _mapper.Map<FoodType>(refDataDTO);
            FoodTypeEntity foodType = _mapper.Map<FoodTypeEntity>(foodTypeBM);
            _foodRepository.Repository.Attach(foodType);
            _foodRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public FoodTypeDTO Get(int id)
        {
            var foodDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return foodDTO;
        }         
        public PagedList<FoodTypeDTO> Get(string tag, PagedParameters pagedParameters)
        {
            var foodDTOByTag = GetAll().Where(x => x.Tags.Any(y => y.value == tag)).ToList();
            return PagedList<FoodTypeDTO>.ToGenericPagedList(foodDTOByTag, pagedParameters);
        }
        public FoodTypeDTO Get(Expression<Func<FoodTypeEntity, bool>> expression)
        {
            Expression<Func<FoodTypeDTO, bool>> exp = _mapper.Map<Expression<Func<FoodTypeDTO, bool>>>(expression);
            List<FoodTypeDTO> foodTypeList = GetAll();
            FoodTypeDTO foodType = null;
            if (foodTypeList != null && foodTypeList.Count > 0)
            {
                foodType = ((IQueryable<FoodTypeDTO>)foodTypeList).FirstOrDefault(exp);
            }

            return foodType;
        }
        public PagedList<FoodTypeDTO> GetAll(PagedParameters pagedParameters)
        {
            var foodTypeDtoList = GetAll();

            return PagedList<FoodTypeDTO>.ToGenericPagedList(foodTypeDtoList, pagedParameters);
        }
        public PagedList<FoodTypeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var foodTypeDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<FoodTypeDTO>.ToGenericPagedList(foodTypeDtoList, pagedParameters);
        }
        public List<FoodTypeDTO> GetAll()
        {
           var cachedData = _cache.GetData<List<FoodTypeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var FoodTypeList = _foodRepository.Repository.GetAll(includes: new List<string>()
                    { "LanguageResourceSet.LanguageRessource",  "Tags"});
                var foodBMList = _mapper.Map<IList<FoodType>>(FoodTypeList);
                var FoodTypeDtoList = _mapper.Map<IList<FoodTypeDTO>>(foodBMList);
                _cache.SetData(_cacheKey, FoodTypeDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<FoodTypeDTO>)FoodTypeDtoList;
            }
        }
        public void Remove(FoodTypeDTO refDataDTO, bool updateCache = true)
        {
            FoodType foodTypeBM = _mapper.Map<FoodType>(refDataDTO);
            FoodTypeEntity foodType = _mapper.Map<FoodTypeEntity>(foodTypeBM);
            foodType.Status = Shared.Enum.Status.isDeleted;
            _foodRepository.Repository.Update(foodType);
            _foodRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idFoodType, List<TagFoodTypeEntity> tags)
        {
            var oldTags = _tagFoodTypeRepository.Repository.GetAll(x => x.FoodTypeId == idFoodType);
            if (oldTags.Count >= 1)
            {
                _tagFoodTypeRepository.Repository.DeleteRange(oldTags);
                _tagFoodTypeRepository.Save();
            }
        }
        public void Update(FoodTypeDTO refDataDTO, bool updateCache = true)
        {
            FoodTypeDTO foodExist = Get(refDataDTO.Id);
            if (foodExist != null)
            {
                foreach (var LanguageRessource in refDataDTO.LanguageResourceSet.LanguageRessource)
                {
                    if (LanguageRessource.Image != foodExist.LanguageResourceSet?.LanguageRessource[0]?.Image)
                    {
                        var image = LanguageRessource.Image;
                        if (!string.IsNullOrEmpty(image))
                        {
                            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                            LanguageRessource.Image = imageURL;
                        }
                    }
                }
                FoodType foodTypeBM = _mapper.Map<FoodType>(refDataDTO);
                FoodTypeEntity foodType = _mapper.Map<FoodTypeEntity>(foodTypeBM);
                _languageResourceService.deleteOldLanguageResources(foodType.LanguageResourceSet.LanguageResourceSetId, foodType.LanguageResourceSet.LanguageRessource);
                deleteOldTags(foodType.Id, foodType.Tags);
                _foodRepository.Repository.Update(foodType);
                _foodRepository.Save();
                if (updateCache)
                    _cache.RemoveData(_cacheKey);
            }
        }
        public List<FoodTypeDTO> GetFilteredData(FoodTypeFilterDTO filter)
        {

            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
            && (string.IsNullOrEmpty(filter.Name) || (x.LanguageResourceSet.LanguageRessource != null && x.LanguageResourceSet.LanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
            return refDataDtoList;
        }
        public FoodTypeDTO Get(string tag)
        {
            throw new NotImplementedException();
        }
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
