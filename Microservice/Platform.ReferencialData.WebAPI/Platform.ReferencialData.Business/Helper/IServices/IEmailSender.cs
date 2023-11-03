using Platform.ReferencialData.Business.Helper.Configuration;

namespace Platform.ReferencialData.Business.Helper.IServices
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
