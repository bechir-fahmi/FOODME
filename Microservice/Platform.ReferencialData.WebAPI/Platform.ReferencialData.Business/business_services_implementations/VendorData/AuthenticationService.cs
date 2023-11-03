using AutoMapper;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DtoModel.BrandData;

namespace Platform.ReferencialData.Business.business_services_implementations;

public class AuthenticationService
{
    private readonly IUnitOfWork<AuthenticationEntity> _authRepository;
    private readonly IMapper _mapper;

    public AuthenticationService(IUnitOfWork<AuthenticationEntity> authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }


    public void AddAuthentication(AuthenticationDTO authDTO, Guid dynamicIntegration, bool updateCache = true)
    {
        AuthenticationBM AuthBM = _mapper.Map<AuthenticationBM>(authDTO);
        AuthenticationEntity AuthEntity = _mapper.Map<AuthenticationEntity>(AuthBM);
        _authRepository.Repository.Insert(AuthEntity);
        _authRepository.Save();
    }
}
