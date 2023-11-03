using Platform.GenericRepository;
using Platform.Tracking.DtoModel.Offre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service
{
    public interface IOfferActionService
    {
        List<OfferActionDTO> GetAllOfferAction();
        OfferActionDTO AddOfferAction(OfferActionDTO offreActionDTO, bool updateCache = true);
        PagedList<OfferActionDTO> GetAll(PagedParameters pagedParameters);
        Dictionary<DateTime, Dictionary<string, int>> CountClicksByTimeAndSocialMedia(Guid offreId);
    }
}
