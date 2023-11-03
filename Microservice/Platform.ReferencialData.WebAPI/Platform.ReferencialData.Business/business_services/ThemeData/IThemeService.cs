using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.Theme;
using Platform.ReferentialData.DtoModel.ThemeData;

namespace Platform.ReferencialData.Business.business_services.ThemeData
{
    public interface IThemeService: IGenericService<ThemeDTO, ThemeEntity>
    {
        PagedList<ThemeDTO> GetAll(PagedParameters pagedParameters);

    }
}
