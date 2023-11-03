using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.KitchenTypeData;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferentialData.DataModel.TagData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.KitchenTypeData
{
    public class KitchenTypeService : IKitchenTypeService
    {
        private readonly IUnitOfWork<KitchenTypeEntity> _kitchenRepository;
        private readonly IUnitOfWork<TagEntity> _tagRepository;
        private readonly IUnitOfWork<TagKitchenTypeEntity> _tagKitchenTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.kitchenCaheKey;

        public KitchenTypeService(IUnitOfWork<KitchenTypeEntity> kitchenRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService, IUnitOfWork<TagKitchenTypeEntity> tagKitchenTypeRepository, IUnitOfWork<TagEntity> tagRepository)
        {
            _kitchenRepository = kitchenRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
            _tagKitchenTypeRepository = tagKitchenTypeRepository;
            _tagRepository = tagRepository;
        }

        public void Add(KitchenTypeDTO refDataDTO, bool updateCache = true)
        {
            KitchenType kitchenBM = _mapper.Map<KitchenType>(refDataDTO);
            KitchenTypeEntity kitchen = _mapper.Map<KitchenTypeEntity>(kitchenBM);
            _kitchenRepository.Repository.Attach(kitchen);
            _kitchenRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public KitchenTypeDTO Get(int id)
        {
            var kitchenDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return kitchenDTO;
        }
        public PagedList<KitchenTypeDTO> Get(string tag, PagedParameters pagedParameters)
        {
            var kitchenDTOByTag = GetAll().Where(x => x.Tags.Any(y => y.value == tag)).ToList();
            return PagedList<KitchenTypeDTO>.ToGenericPagedList(kitchenDTOByTag, pagedParameters);
        }
        public KitchenTypeDTO Get(Expression<Func<KitchenTypeEntity, bool>> expression)
        {
            Expression<Func<KitchenTypeDTO, bool>> exp = _mapper.Map<Expression<Func<KitchenTypeDTO, bool>>>(expression);
            List<KitchenTypeDTO> kitchenList = GetAll();
            KitchenTypeDTO kitchen = null;
            if (kitchenList != null && kitchenList.Count > 0)
            {
                kitchen = ((IQueryable<KitchenTypeDTO>)kitchenList).FirstOrDefault(exp);
            }
            return kitchen;
        }
        public PagedList<KitchenTypeDTO> GetAll(PagedParameters pagedParameters)
        {
            var kitchenDtoList = GetAll();

            return PagedList<KitchenTypeDTO>.ToGenericPagedList(kitchenDtoList, pagedParameters);
        } 
        public PagedList<KitchenTypeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var kitchenDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<KitchenTypeDTO>.ToGenericPagedList(kitchenDtoList, pagedParameters);
        }
        public List<KitchenTypeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<KitchenTypeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var kitchenList = _kitchenRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource",  "Tags"});
                var kitchenBMList = _mapper.Map<IList<KitchenType>>(kitchenList);
                var kitchenDtoList = _mapper.Map<IList<KitchenTypeDTO>>(kitchenBMList);
                _cache.SetData(_cacheKey, kitchenDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<KitchenTypeDTO>)kitchenDtoList;
            }
        }
        public void Remove(KitchenTypeDTO refDataDTO, bool updateCache = true)
        {
            KitchenType kitchenBM = _mapper.Map<KitchenType>(refDataDTO);
            KitchenTypeEntity kitchen = _mapper.Map<KitchenTypeEntity>(kitchenBM);
            kitchen.Status = Status.isDeleted;
            _kitchenRepository.Repository.Update(kitchen);
            _kitchenRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idKitchenType, List<TagKitchenTypeEntity> tags)
        {
            var oldKitchenTags = _tagKitchenTypeRepository.Repository.GetAll(x => x.KitchenTypeId == idKitchenType);
            if (oldKitchenTags.Count >= 1)
            {
                _tagKitchenTypeRepository.Repository.DeleteRange(oldKitchenTags);
                _tagKitchenTypeRepository.Save();
            }
/*            if (tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    _tagRepository.Repository.Delete(tag.TagId);
                }
                _tagRepository.Save();
            }*/
        }
        public void Update(KitchenTypeDTO refDataDTO, bool updateCache = true)
        {
            KitchenTypeDTO kitchenExist = Get(refDataDTO.Id);
            if(kitchenExist != null)
            {
                foreach (var LanguageRessource in refDataDTO.LanguageResourceSet.LanguageRessource)
                {
                    if(LanguageRessource.Image != kitchenExist.LanguageResourceSet?.LanguageRessource[0]?.Image)
                    {
                        var image = LanguageRessource.Image;
                        if (!string.IsNullOrEmpty(image))
                        {
                            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                            LanguageRessource.Image = imageURL;
                        }
                    }                  
                }
                KitchenType kitchenBM = _mapper.Map<KitchenType>(refDataDTO);
                KitchenTypeEntity kitchen = _mapper.Map<KitchenTypeEntity>(kitchenBM);
                _languageResourceService.deleteOldLanguageResources(kitchen.LanguageResourceSet.LanguageResourceSetId, kitchen.LanguageResourceSet.LanguageRessource);
                deleteOldTags(kitchen.Id, kitchen.Tags);
                _kitchenRepository.Repository.Update(kitchen);
                _kitchenRepository.Save();
                if (updateCache)
                    _cache.RemoveData(_cacheKey);
            }
            
        } 
        public List<KitchenTypeDTO> GetFilteredData(KitchenTypeFillter filter)
        {

            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
            && (string.IsNullOrEmpty(filter.Name) || (x.LanguageResourceSet.LanguageRessource != null && x.LanguageResourceSet.LanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
            return refDataDtoList;
        }
        public KitchenTypeDTO Get(string tag)
        {
            throw new NotImplementedException();
        }
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
