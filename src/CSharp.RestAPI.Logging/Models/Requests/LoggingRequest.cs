using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
