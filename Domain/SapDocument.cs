﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Inputs;
using SAPbobsCOM;

namespace GRINTSYS.SAPRestApi.BussinessLogic
{
    public class SapDocument: ISapDocument
    {
        private Company _company;
        public SapDocument()
        {
            _company = new Company();
        }

        public Company Connect(SapSettingsInput input)
        {
            _company.Server = String.IsNullOrEmpty(input.Server) ? ConfigurationManager.AppSettings["server"] : input.Server;
            _company.CompanyDB = String.IsNullOrEmpty(input.Companydb) ? ConfigurationManager.AppSettings["companydb"] : input.Companydb;
            _company.DbServerType = BoDataServerTypes.dst_MSSQL2012;
            _company.DbUserName = String.IsNullOrEmpty(input.DbUserName) ? ConfigurationManager.AppSettings["dbuser"]: input.DbUserName;
            _company.DbPassword = String.IsNullOrEmpty(input.DbPassword) ?  ConfigurationManager.AppSettings["dbpassword"] : input.DbPassword;
            _company.UserName = String.IsNullOrEmpty(input.UserName) ? ConfigurationManager.AppSettings["user"] : input.UserName;
            _company.Password = String.IsNullOrEmpty(input.Password) ? ConfigurationManager.AppSettings["password"] : input.Password;
            _company.language = BoSuppLangs.ln_English_Gb;
            _company.UseTrusted = false;
            _company.LicenseServer = String.IsNullOrEmpty(input.LicenseServer) ? ConfigurationManager.AppSettings["licenseServer"] : input.LicenseServer;
            var connectionResult = _company.Connect();

            if (connectionResult != 0)
            {
                int errorCode = 0;
                String errorMessage = "";

                _company.GetLastError(out errorCode, out errorMessage);

                throw new Exception(errorMessage);
            }

            return _company;
        }

        public virtual Task Execute(ISapDocumentInput input)
        {
            throw new NotImplementedException();
        }
    }
}
