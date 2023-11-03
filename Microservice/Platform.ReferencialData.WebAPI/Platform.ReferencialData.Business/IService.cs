using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business
{
    public interface IService<DTO, T>
    {
        List<DTO> GetAll();

        DTO Get(int id);

        DTO Get(Expression<Func<T, bool>> expression);

        void Add(DTO refDataDTO);

        void Remove(int id);

        void Update(DTO refDataDTO);

    }
}
