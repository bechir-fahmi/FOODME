using Platform.GenericRepository;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.OfferData;

namespace Platform.ReferencialData.Business.business_services.OfferData
{
    public interface IOfferService
    {
        PagedList<OfferDTO> GetAll(PagedParameters pagedParameters, string userId);
        PagedList<OfferDTO> GetAllActiveData(PagedParameters pagedParameters);

        List<OfferDTO> GetFilteredData(OfferFilterDTO Fillter);
        PagedList<OfferDTO> Get(string tag, PagedParameters pagedParameters);
        OfferDTO Get(Guid id);
        ResponseDTO Remove(Guid id, bool updateCache = true);
        ResponseDTO Update(OfferDTO offerDTO, bool updateCache = true);
        ResponseDTO Add(OfferDTO offerDTO, bool updateCache = true);
    }
}
