using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;

namespace Platform.ReferencialData.Business.business_services.KitchenTypeData
{
    public interface IKitchenTypeService:IGenericService<KitchenTypeDTO,KitchenTypeEntity>
    {
        PagedList<KitchenTypeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<KitchenTypeDTO> GetAllActiveData(PagedParameters pagedParameters);

        PagedList<KitchenTypeDTO> Get(string tag, PagedParameters pagedParameters);
        List<KitchenTypeDTO> GetFilteredData(KitchenTypeFillter Fillter);


    }
}
