namespace Denso.HotSheet.Email
{
    public interface IEmailManager
    {
        void Send(string emailAddress, string subject, string body, string webAppLink = null, string webAppLinkText = null);
    }
}
