using System.ComponentModel.DataAnnotations;

namespace Denso.HotSheet.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}