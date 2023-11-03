using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DtoModel.Response
{
    public class ResponseDTO
    {
        [Required]
        public int StatusCodes { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Error { get; set; } = string.Empty;
    }
}
