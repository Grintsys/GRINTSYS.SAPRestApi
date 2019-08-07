using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Output
{
    public class TaskResponse: HttpResponseBase
    {
        public String Message { get; set; }
        public bool Success { get; set; }
    }
}