using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Denso.HotSheet.Email
{
    public static class EmailHelper
    {
        public static AlternateView CreateHtmlMessageWithLogo(string message, string contentRootPath)
        {
            string logoPath = Path.Combine(contentRootPath, "Assets/Images/denso-logo.png");

            string messageHtml = "<html><body>" + message + "<br/><img src=cid:DensoLogo id='imgDensoLogo' alt='' /></body></html>";

            var inline = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg);
            inline.ContentId = "DensoLogo";

            var alternateView = AlternateView.CreateAlternateViewFromString(
                                    messageHtml,
                                    Encoding.UTF8,
                                    MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);

            return alternateView;
        }

        public static AlternateView CreateHtmlMessageWithImages(string message, string contentRootPath, string clientURL, string webAppLink = null, string webAppLinkText = null)
        {
            string logoPath = Path.Combine(contentRootPath, "assets/images/denso-logo.png");
            string facebookPath = Path.Combine(contentRootPath, "assets/images/denso-facebook.png");
            string youtubePath = Path.Combine(contentRootPath, "assets/images/denso-youtube.png");
            string linkedinPath = Path.Combine(contentRootPath, "assets/images/denso-linkedin.png");
            string truckPath = Path.Combine(contentRootPath, "assets/images/denso-truck.png");
            string emailBaseTemplatePath = Path.Combine(contentRootPath, "assets/templates/email-base-template.html");
            
            string messageHtml = File.ReadAllText(emailBaseTemplatePath);

            var inlineDenso = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg);
            inlineDenso.ContentId = "DensoLogo";

            var inlineFacebook = new LinkedResource(facebookPath, MediaTypeNames.Image.Jpeg);
            inlineFacebook.ContentId = "DensoFacebook";

            var inlineYoutube = new LinkedResource(youtubePath, MediaTypeNames.Image.Jpeg);
            inlineYoutube.ContentId = "DensoYoutube";

            var inlineLinkedin = new LinkedResource(linkedinPath, MediaTypeNames.Image.Jpeg);
            inlineLinkedin.ContentId = "DensoLinkedin";

            var inlineTruck = new LinkedResource(truckPath, MediaTypeNames.Image.Jpeg);
            inlineTruck.ContentId = "DensoTruck";

            messageHtml = messageHtml
                .Replace("{bodyMsg}", message)
                .Replace("{webAppLink}", string.IsNullOrEmpty(webAppLink) ? clientURL : webAppLink)
                .Replace("{webAppLinkText}", string.IsNullOrEmpty(webAppLinkText) ? "Denso - Hot Sheet System" : webAppLinkText);

            var alternateView = AlternateView.CreateAlternateViewFromString(messageHtml, Encoding.UTF8, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inlineDenso);
            alternateView.LinkedResources.Add(inlineFacebook);
            alternateView.LinkedResources.Add(inlineYoutube);
            alternateView.LinkedResources.Add(inlineLinkedin);
            alternateView.LinkedResources.Add(inlineTruck);

            return alternateView;
        }
    }
}
