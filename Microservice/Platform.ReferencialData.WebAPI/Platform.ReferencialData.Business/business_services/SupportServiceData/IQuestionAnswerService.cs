using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.SupportService;

namespace Platform.ReferencialData.Business.business_services.SupportServiceData
{
    public interface IQuestionAnswerService : IGenericService<QuestionAnswerDTO, QuestionAnswerEntity>
    {
        PagedList<QuestionAnswerDTO> GetAll(PagedParameters pagedParameters);

    }
}
