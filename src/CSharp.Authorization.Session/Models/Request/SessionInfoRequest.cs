using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CSharp.Authorization.Session.Models.Request
{
    public class SessionInfoRequest
    {
        [Required(AllowEmptyStrings = false)]
        [DefaultValue("SessionTestId")]
        public required string SessionId { get; set; }

        [AllowNull]
        public int? TimeoutMin { get; set; }
    }
}
