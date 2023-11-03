using Platform.GenericRepository;
using Platform.ReferentialData.DtoModel;

namespace Platform.ReferencialData.Business.business_services;

public interface IZoneService
{
    PagedList<ZoneDTO> GetAll(PagedParameters pagedParameters);
    PagedList<ZoneDTO> GetAllActiveData(PagedParameters pagedParameters);


    List<ZoneDTO> GetAll();

    ZoneDTO Get(int id);

    void Add(ZoneDTO refDataDTO, bool updateCache = true);

    void Remove(ZoneDTO refDataDTO, bool updateCache = true);

    void Update(ZoneDTO refDataDTO, bool updateCache = true);

}
