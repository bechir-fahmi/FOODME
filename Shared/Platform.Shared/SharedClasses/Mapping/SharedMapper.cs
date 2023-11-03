using AutoMapper;
using Platform.Shared.SharedClasses.WeekDayModel;

namespace Platform.Shared.SharedClasses.Mapping
{
    public class SharedMapper : Profile
    {
        public SharedMapper() 
        {
            #region WeekDay
            CreateMap<WeekDayDTO, WeekDay>().ReverseMap();
            CreateMap<WeekDay, WeekDayEntity>().ReverseMap();
            CreateMap<WeekDayVM, WeekDay>().ReverseMap();
            #endregion
        }
    }
}
