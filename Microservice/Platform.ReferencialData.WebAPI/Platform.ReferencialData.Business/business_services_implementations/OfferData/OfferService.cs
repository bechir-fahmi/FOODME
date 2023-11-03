using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.OfferData;
using Platform.ReferencialData.BusinessModel.OfferData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.OfferData;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.OfferData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.OfferData
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork<OfferEntity> _offerRepository;
        private readonly IUnitOfWork<TagOfferEntity> _tagOfferRepository;
        private readonly IUnitOfWork<VendorEntity> _vendorRepository;

        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly IUserService _userService;
        private readonly string _cacheKey = CacheKey.OfferCacheKey;

        public OfferService(IUnitOfWork<OfferEntity> offerRepository, IUnitOfWork<VendorEntity> vendorRepository,
            IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService, IUnitOfWork<TagOfferEntity> tagOfferRepository, IUserService userService)
        {
            _offerRepository = offerRepository;
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
            _tagOfferRepository = tagOfferRepository;
            _userService = userService;
        }

        public ResponseDTO Add(OfferDTO offerDTO, bool updateCache = true)
        {
     
                var vendorExist = _vendorRepository.Repository.Get(x => x.VendorId.ToString() == offerDTO.VendorId);
                if (vendorExist == null)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        Error = "Vendor not found",
                        Message = $"Vendor with id {offerDTO.VendorId} was not found"
                    };
                }

           
            if (offerDTO.StartDateTime > offerDTO.EndDateTime)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = "wrong input: start date or end date",
                    Message = $"Start date can't be after the end date"
                };
            }
            Offer OfferBM = _mapper.Map<Offer>(offerDTO);
            OfferEntity Offer = _mapper.Map<OfferEntity>(OfferBM);
            _offerRepository.Repository.Insert(Offer);
             _offerRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);

            return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status201Created,
                    Error = "",
                    Message = "Offer Created successfully"
                };
           
            
        }

        public OfferDTO Get(Guid id)
        {
            var OfferDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return OfferDTO;
        }
        public PagedList<OfferDTO> Get(string tag, PagedParameters pagedParameters)
        {
            var mOfferDTOByTag = GetAll().Where(x => x.Tags.Any(y => y.value == tag)).ToList();
            return PagedList<OfferDTO>.ToGenericPagedList(mOfferDTOByTag, pagedParameters);
        }
        public OfferDTO Get(Expression<Func<OfferEntity, bool>> expression)
        {
            Expression<Func<OfferDTO, bool>> exp = _mapper.Map<Expression<Func<OfferDTO, bool>>>(expression);
            List<OfferDTO> OfferList = GetAll();
            OfferDTO Offer = null;
            if (OfferList != null && OfferList.Count > 0)
            {
                Offer = ((IQueryable<OfferDTO>)OfferList).FirstOrDefault(exp);
            }

            return Offer;
        }

        public PagedList<OfferDTO> GetAll(PagedParameters pagedParameters)
        {
            var OfferDtoList = GetAll();

            return PagedList<OfferDTO>.ToGenericPagedList(OfferDtoList, pagedParameters);
        }
        public PagedList<OfferDTO> GetAll(PagedParameters pagedParameters, string userId)
        {
            var userEntity = _userService.GetUser(userId);
            var OfferDtoList = GetAll();
            if (userEntity.UserType == "aggregator" || userEntity.UserType == "brand")
            {
                if (userEntity.AssignedTo != null)
                {
                    var OfferDtoListFiltredByVendor = OfferDtoList.Where(x => x.VendorId == userEntity.AssignedTo).ToList();
                    return PagedList<OfferDTO>.ToGenericPagedList(OfferDtoListFiltredByVendor, pagedParameters);
                }
                else
                {
                    return PagedList<OfferDTO>.ToGenericPagedList(new List<OfferDTO> { }, pagedParameters);
                }


            }

            return PagedList<OfferDTO>.ToGenericPagedList(OfferDtoList, pagedParameters);
        }
        public PagedList<OfferDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var OfferDtoList = GetAll().Where(x=>x.Status==Shared.Enum.Status.isActive).ToList();   

            return PagedList<OfferDTO>.ToGenericPagedList(OfferDtoList, pagedParameters);
        }
        public List<OfferDTO> GetAll()
        {
                        var cachedData = _cache.GetData<List<OfferDTO>>(_cacheKey);
                        if (cachedData != null)
                        {
                            return cachedData;
                        }
                        else
                        {
            var OfferList = _offerRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource",  "Tags"});
                var offerBMList = _mapper.Map<IList<Offer>>(OfferList);
            foreach ( var offerBM in offerBMList )
            {
                offerBM.VendorName = _vendorRepository.Repository.Get(x => x.VendorId.ToString() == offerBM.VendorId).Name;
            }
            var OfferDtoList = _mapper.Map<IList<OfferDTO>>(offerBMList);
            _cache.SetData(_cacheKey, OfferDtoList, DateTimeOffset.UtcNow.AddDays(1));
            return (List<OfferDTO>)OfferDtoList;
            }
        }

        public ResponseDTO Remove(Guid id, bool updateCache = true)
        {
            OfferDTO offerDTO = Get(id);
            if (offerDTO == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "Offer Not Found",
                    Message = $"Offer id : {id}"
                };
            }
            Offer OfferBM = _mapper.Map<Offer>(offerDTO);
            OfferEntity Offer = _mapper.Map<OfferEntity>(OfferBM);
            Offer.Status = Shared.Enum.Status.isDeleted;
            _offerRepository.Repository.Update(Offer);
            _offerRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status204NoContent,
                Error = "",
                Message = $"Offer {id} Removed successfully"
            };
            

        }
        public void deleteOldTags(Guid idOffer, List<TagOfferEntity> tags)
        {
            var oldTags = _tagOfferRepository.Repository.GetAll(x => x.OfferId == idOffer);
            if (oldTags.Count >= 1)
            {
                _tagOfferRepository.Repository.DeleteRange(oldTags);
                _tagOfferRepository.Save();
            }
        }
        public ResponseDTO Update(OfferDTO offerDTO, bool updateCache = true)
        {
            OfferDTO offerExist = Get(offerDTO.Id);
            if (offerExist != null) 
            {
                foreach (var LanguageRessource in offerDTO.LanguageResourceSet?.LanguageRessource)
                {
                    if(LanguageRessource.Image != offerExist.LanguageResourceSet?.LanguageRessource[0].Image)
                    {
                        var image = LanguageRessource.Image;
                        if (image != null)
                        {
                            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                            LanguageRessource.Image = imageURL;
                        }
                    }
                    
                }
                var offerImage = offerDTO.Image;
                if (offerImage != offerExist.Image)
                {
                    var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, offerImage);
                    offerDTO.Image = imageURL;
                }

                Offer OfferBM = _mapper.Map<Offer>(offerDTO);
                OfferEntity Offer = _mapper.Map<OfferEntity>(OfferBM);
                if (Offer.LanguageResourceSet != null)
                    _languageResourceService.deleteOldLanguageResources(Offer.LanguageResourceSet.LanguageResourceSetId, Offer.LanguageResourceSet.LanguageRessource);
                deleteOldTags(Offer.Id, Offer.Tags);
                _offerRepository.Repository.Update(Offer);
                _offerRepository.Save();
                if (updateCache)
                    _cache.RemoveData(_cacheKey);
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Error = "",
                    Message = "Offer updated successfully"
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status404NotFound,
                Error = "",
                Message = "Offer not found"
            };
        }

        public List<OfferDTO> GetFilteredData(OfferFilterDTO filter)
        {

            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
            && (string.IsNullOrEmpty(filter.Name) || (x.LanguageResourceSet.LanguageRessource != null && x.LanguageResourceSet.LanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
            return refDataDtoList;
        }
    }
}
