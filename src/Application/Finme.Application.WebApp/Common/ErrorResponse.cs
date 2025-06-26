using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Common
{
    public class ErrorResponse(string title, int status, string detail = null)
    {
        public string Title { get; set; } = title;
        public int Status { get; set; } = status;
        public string Detail { get; set; } = detail;
        public List<ErrorDetails> Errors { get; set; } = [];

        public void AddError(string propertyName, string errorMessage)
        {
            Errors.Add(new ErrorDetails(propertyName, errorMessage));
        }
    }
}
