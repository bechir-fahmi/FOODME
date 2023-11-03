using AutoMapper;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services_implementations
{
    public class UserOTPVerificationService : IUserOTPVerificationService
    {
        private readonly IUnitOfWork<UserOTPVerificationEntity> _UserOTPVerificationRepository;
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;

        public UserOTPVerificationService(IUnitOfWork<UserOTPVerificationEntity> userOTPVerificationRepository,
            IAuthManager authManager, 
            IMapper mapper)
        {
            _UserOTPVerificationRepository = userOTPVerificationRepository;
            _authManager = authManager;
            _mapper = mapper;
        }

        public void Add(UserOTPVerificationDTO userOTPVerification)
        {
            UserOTPVerification userOTP = _mapper.Map<UserOTPVerification>(userOTPVerification);
            UserOTPVerificationEntity userOTPEntity = _mapper.Map<UserOTPVerificationEntity>(userOTP);
            _UserOTPVerificationRepository.Repository.Attach(userOTPEntity);
            _UserOTPVerificationRepository.Save();
        }
        public void Update(UserOTPVerificationDTO userOTPVerification)
        {
            UserOTPVerification userOTP = _mapper.Map<UserOTPVerification>(userOTPVerification);
            UserOTPVerificationEntity userOTPEntity = _mapper.Map<UserOTPVerificationEntity>(userOTP);
            _UserOTPVerificationRepository.Repository.Update(userOTPEntity);
            _UserOTPVerificationRepository.Save();
        }
        public UserOTPVerificationDTO GetOTPVerificationByUserPhoneNumber(string phoneNumber)
        {
            UserOTPVerificationEntity userOTPEntity = _UserOTPVerificationRepository.Repository.Get(x => x.PhoneNumber == phoneNumber);
            UserOTPVerification userOTPVerification = _mapper.Map<UserOTPVerification>(userOTPEntity);
            UserOTPVerificationDTO userOTPVerificationDTO = _mapper.Map<UserOTPVerificationDTO>(userOTPVerification);
            return userOTPVerificationDTO;
        }
    }
}
