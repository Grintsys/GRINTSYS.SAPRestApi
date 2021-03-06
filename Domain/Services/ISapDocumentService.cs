﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Inputs;
using SAPbobsCOM;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public interface ISapDocumentService
    {
        Company Connect(SapSettingsInput input);
        Task<TaskResponse> Execute(ISapDocumentInput input);
    }
}
