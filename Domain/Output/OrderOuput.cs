using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Output
{
    public class OrderOuput
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string RemoteId { get; set; }

        public int Status { get; set; }

        public long UserId { get; set; }

        public string Comment { get; set; }

        public string LastMessage { get; set; }

        public int Series { get; set; }

        public string DeliveryDate { get; set; }

        public DateTime CreationTime { get; set; }

        public string CardCode { get; set; }
    }
}