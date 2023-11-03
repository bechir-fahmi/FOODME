using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.UserAddressData
{
    public interface IUserAddressTypeService : IGenericService<UserAddressTypeDTO, UserAddressTypeEntity>
    {
        PagedList<UserAddressTypeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<UserAddressTypeDTO> GetAllActiveData(PagedParameters pagedParameters);

    }
}
