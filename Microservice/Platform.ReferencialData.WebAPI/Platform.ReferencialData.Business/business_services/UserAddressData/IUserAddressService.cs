using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;

namespace Platform.ReferencialData.Business.business_services.UserAddressData
{
    public interface IUserAddressService : IGenericService<UserAddressDTO, UserAddressEntity>
    {
        PagedList<UserAddressDTO> GetAll(PagedParameters pagedParameters);
        PagedList<UserAddressDTO> GetAllActiveData(PagedParameters pagedParameters);
        List<UserAddressDTO> GetUserAddressByUserId(string userId);

        List<UserAddressDTO> GetUserAddressActiveByUserId(string userId);
    }
}
