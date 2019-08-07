using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRINTSYS.SAPRestApi.BussinessLogic.Inputs
{
    public class SAPOrderInput: ISapDocumentInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public String RemoteId { get; set; }
        public int Status { get; set; }
        public String CardCode { get; set; }
        public long UserId { get; set; }
        public String Comment { get; set; }
        public String LastMessage { get; set; }
        public Int32 Series { get; set; }
        public String DeliveryDate { get; set; }
        public DateTime CreationTime { get; set; }
    }
}