using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.DeliveryModeData;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.DeliveryModeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.DeliveryModeData
{
    public interface IDeliveryModeService : IGenericService<DeliveryModeDTO, DeliveryModeEntity>
    {
        PagedList<DeliveryModeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<DeliveryModeDTO> GetAllActiveData(PagedParameters pagedParameters);

       // List<DeliveryModeDTO> GetFilteredData(DeliveryModeFilterDTO Fillter);


    }
}