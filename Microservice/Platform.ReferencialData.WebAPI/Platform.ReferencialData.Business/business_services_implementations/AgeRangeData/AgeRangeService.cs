using AutoMapper;
using Nest;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.AgeRangeData;
using Platform.ReferencialData.BusinessModel.AgeRangeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.AgeRangeData;
using Platform.ReferentialData.DtoModel.AgeRangeData;
using Platform.Shared.Cache;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.AgeRangeData
{
    public class AgeRangeService : IAgeRangeService
    {
        private readonly IUnitOfWork<AgeRangeEntity> _ageRangeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.AgeRangeCaheKey;

        public AgeRangeService(IUnitOfWork<AgeRangeEntity> ageRangeRepository, IMapper mapper, ICacheService cache)
        {
            _ageRangeRepository = ageRangeRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public bool ValidateAgeRangeWithExistingRanges(AgeRangeDTO ageRangeDTO)
        {
            var ageRanges = GetAll();
            foreach (var existingRange in ageRanges)
            {
                if ((ageRangeDTO.MinAge <= existingRange.MaxAge && ageRangeDTO.MinAge >= existingRange.MinAge)
                    ||(ageRangeDTO.MaxAge <= existingRange.MaxAge && ageRangeDTO.MaxAge >= existingRange.MinAge))
                {
                    return false; 
                }

            }
            return true;

        }
        public bool ValidateNewAgeRange(AgeRangeDTO ageRangeDTO)
        {
            return ageRangeDTO.MaxAge > ageRangeDTO.MinAge ? true : false;
        }
        public void Add(AgeRangeDTO ageRangeDTO, bool updateCache = true)
        {
            AgeRange ageRangeBM = _mapper.Map<AgeRange>(ageRangeDTO);
            AgeRangeEntity ageRange = _mapper.Map<AgeRangeEntity>(ageRangeBM);
            if(ValidateAgeRangeWithExistingRanges(ageRangeDTO) && ValidateNewAgeRange(ageRangeDTO))
            {
                _ageRangeRepository.Repository.Attach(ageRange);
                _ageRangeRepository.Save();
            }
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public AgeRangeDTO Get(int id)
        {
            var ageRange = GetAll().FirstOrDefault(x => x.Id == id);
            return ageRange;
        }
        public AgeRangeDTO Get(Expression<Func<AgeRangeEntity, bool>> expression)
        {
            Expression<Func<AgeRangeDTO, bool>> exp = _mapper.Map<Expression<Func<AgeRangeDTO, bool>>>(expression);
            List<AgeRangeDTO> ageRangeList = GetAll();
            AgeRangeDTO ageRange = null;
            if (ageRangeList != null && ageRangeList.Count > 0)
            {
                ageRange = ((IQueryable<AgeRangeDTO>)ageRangeList).FirstOrDefault(exp);
            }

            return ageRange;
        }
        public PagedList<AgeRangeDTO> GetAll(PagedParameters pagedParameters)
        {
            var ageRangeDtoList = GetAll();

            return PagedList<AgeRangeDTO>.ToGenericPagedList(ageRangeDtoList, pagedParameters);
        }
        public List<AgeRangeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<AgeRangeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var AgeRangeList = _ageRangeRepository.Repository.GetAll();
                var ageRangeBMList = _mapper.Map<IList<AgeRange>>(AgeRangeList);
                var ageRangeDtoList = _mapper.Map<IList<AgeRangeDTO>>(ageRangeBMList);
                _cache.SetData(_cacheKey, ageRangeDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<AgeRangeDTO>)ageRangeDtoList;
            }
        }

        public PagedList<AgeRangeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var ageRangeDtoList = GetAll().Where(x => x.Status == Shared.Enum.Status.isActive).ToList();

            return PagedList<AgeRangeDTO>.ToGenericPagedList(ageRangeDtoList, pagedParameters);
        }
        public void Remove(AgeRangeDTO refDataDTO, bool updateCache = true)
        {
            AgeRange ageRangeBM = _mapper.Map<AgeRange>(refDataDTO);
            AgeRangeEntity ageRage = _mapper.Map<AgeRangeEntity>(ageRangeBM);
            ageRage.Status = Shared.Enum.Status.isDeleted;
            _ageRangeRepository.Repository.Update(ageRage);
            _ageRangeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Update(AgeRangeDTO refDataDTO, bool updateCache = true)
        {
            AgeRange ageRangeBM = _mapper.Map<AgeRange>(refDataDTO);
            AgeRangeEntity ageRage = _mapper.Map<AgeRangeEntity>(ageRangeBM);
            _ageRangeRepository.Repository.Update(ageRage);
            _ageRangeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }


        /* public List<AgeRangeDTO> GetFilteredData(AgeRangeFilterDTO filter)
         {

             var refDataDtoList = GetAll();
             refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
             && (string.IsNullOrEmpty(filter.Name) || (x.nameImageLanguageRessource != null && x.nameImageLanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
             return refDataDtoList;
         }*/
    }
}