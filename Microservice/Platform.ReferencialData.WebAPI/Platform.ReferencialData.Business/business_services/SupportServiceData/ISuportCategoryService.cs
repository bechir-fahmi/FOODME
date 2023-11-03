using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferentialData.DtoModel.SupportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.SupportServiceData
{
    public interface ISuportCategoryService : IGenericService<SuportCategoryDTO, SuportCategoryEntity>
    {
        PagedList<SuportCategoryDTO> GetAll(PagedParameters pagedParameters);

    }
}
