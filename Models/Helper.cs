using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRINTSYS.SAPRestApi.Models
{
    public enum PaymentStatus
    {
        CreadoEnAplicacion = 1,
        CreadoEnSAP = 2,
        Error = 3,
        CanceladoPorFinanzas = 4,
        Autorizado = 5
    }

    public enum PaymentType
    {
        Efectivo = 1,
        Cheque = 2,
        Transferencia = 3
    }
}