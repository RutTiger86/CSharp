using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSharp.RestAPI.Logging.Models.Requests
{
    public class LoggingRequest 
    {
        [Required(AllowEmptyStrings = false)]
        [DefaultValue("loggingData")]
        public required string loggingData { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DefaultValue("notLoggingData")]
        public required string notLoggingData { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DefaultValue("secretData")]
        public required string secretData { get; set; }

        public override string ToString()
        {
            return $"loggingData : {loggingData}, notLoggingData : {notLoggingData}, secretData : {secretData}";
        }
    }

}
