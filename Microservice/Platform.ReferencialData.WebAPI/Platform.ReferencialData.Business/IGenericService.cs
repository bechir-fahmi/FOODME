using System.Linq.Expressions;

namespace Platform.ReferencialData.Business
{
    public interface IGenericService<DTO, T>
    {
        List<DTO> GetAll();

        DTO Get(int id);

        DTO Get(Expression<Func<T, bool>> expression);

        void Add(DTO refDataDTO, bool updateCache = true);

        void Remove(DTO refDataDTO, bool updateCache = true);
        void Remove(int refData, bool updateCache = true);

        void Update(DTO refDataDTO, bool updateCache = true);

    }
}
