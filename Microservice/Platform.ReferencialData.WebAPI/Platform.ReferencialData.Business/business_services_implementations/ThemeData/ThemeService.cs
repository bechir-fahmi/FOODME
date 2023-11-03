using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.ThemeData;
using Platform.ReferencialData.BusinessModel.ThemeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.Theme;
using Platform.ReferentialData.DtoModel.ThemeData;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.ThemeData
{
    public class ThemeService : IThemeService
    {
        private readonly IUnitOfWork<ThemeEntity> _themeRepository;
        private readonly IMapper _mapper;

        public ThemeService(IUnitOfWork<ThemeEntity> themeRepository, IMapper mapper)
        {
            _themeRepository = themeRepository;
            _mapper = mapper;
        }

        public void Add(ThemeDTO refDataDTO, bool updateCache = true)
        {
            Theme ThemeBM = _mapper.Map<Theme>(refDataDTO);
            ThemeEntity theme = _mapper.Map<ThemeEntity>(ThemeBM);
            _themeRepository.Repository.Attach(theme);
            _themeRepository.Save();
        }

        public ThemeDTO Get(int id)
        {
            var DTO = GetAll().FirstOrDefault(x => x.Id == id);
            return DTO;
        }

        public ThemeDTO Get(Expression<Func<ThemeEntity, bool>> expression)
        {
            Expression<Func<ThemeDTO, bool>> exp = _mapper.Map<Expression<Func<ThemeDTO, bool>>>(expression);
            List<ThemeDTO> themeList = GetAll();
            ThemeDTO theme = null;
            if (themeList != null && themeList.Count > 0)
            {
                theme = ((IQueryable<ThemeDTO>)themeList).FirstOrDefault(exp);
            }

            return theme;
        }

        public List<ThemeDTO> GetAll()
        {
            var ThemeList = _themeRepository.Repository.GetAll();
            var Theme = _mapper.Map<IList<Theme>>(ThemeList);
            var ThemeDtoList = _mapper.Map<IList<ThemeDTO>>(Theme);
            return (List<ThemeDTO>)ThemeDtoList;
        }

        public PagedList<ThemeDTO> GetAll(PagedParameters pagedParameters)
        {
            var DtoList = GetAll();
            return PagedList<ThemeDTO>.ToGenericPagedList(DtoList, pagedParameters);
        }

        public void Remove(int id, bool updateCache = true)
        {
            _themeRepository.Repository.Delete(id);
            _themeRepository.Save();
        }

        public void Remove(ThemeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void Update(ThemeDTO refDataDTO, bool updateCache = true)
        {
            Theme ThemeBM = _mapper.Map<Theme>(refDataDTO);
            ThemeEntity Theme = _mapper.Map<ThemeEntity>(ThemeBM);
            _themeRepository.Repository.Update(Theme);
            _themeRepository.Save();
        }
    }
}
