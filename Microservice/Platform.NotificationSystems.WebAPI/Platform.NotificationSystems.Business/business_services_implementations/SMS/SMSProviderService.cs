using AutoMapper;
using Platform.NotificationSystems.Business.business_services.SMS;
using Platform.NotificationSystems.BusinessModel.SMS;
using Platform.NotificationSystems.DataModel.SMS;
using Platform.NotificationSystems.DtoModel.SMS;
using Platform.ReferencialData.GenericRepository;

namespace Platform.NotificationSystems.Business.business_services_implementations.SMS
{
    public class SMSProviderService : ISMSProviderService
    {
        private readonly IUnitOfWork<SMSProviderEntity> _sMSProviderRepository;
        private readonly IUnitOfWork<SMSProviderEndPointEntity> _sMSProviderEndPointRepository;
        private readonly IMapper _mapper;

        public SMSProviderService(IUnitOfWork<SMSProviderEntity> sMSProviderRepository,
            IUnitOfWork<SMSProviderEndPointEntity> sMSProviderEndPointRepository,
            IMapper mapper)
        {
            _sMSProviderRepository = sMSProviderRepository;
            _sMSProviderEndPointRepository = sMSProviderEndPointRepository;
            _mapper = mapper;
        }

        public void Add(SMSProviderDTO smsProviderDTO)
        {
            SMSProvider sMSProvider = _mapper.Map<SMSProvider>(smsProviderDTO);
            SMSProviderEntity sMSProviderEntity = _mapper.Map<SMSProviderEntity>(sMSProvider);
            _sMSProviderRepository.Repository.Insert(sMSProviderEntity);
            _sMSProviderRepository.Save();

        }

        public SMSProviderDTO GetByRank(int rank)
        {
            SMSProviderEntity sMSProviderEntity = _sMSProviderRepository.Repository.Get(x => x.Rank == rank);
            SMSProviderEndPointEntity sMSProviderEndPointEntity = _sMSProviderEndPointRepository.Repository.Get(x => x.Id == sMSProviderEntity.EndpointId);
            sMSProviderEntity.Endpoint = sMSProviderEndPointEntity;
            SMSProvider sMSProvider = _mapper.Map<SMSProvider>(sMSProviderEntity);
            SMSProviderDTO sMSProviderDTO = _mapper.Map < SMSProviderDTO>(sMSProvider);
            return sMSProviderDTO;
        }
    }
}
