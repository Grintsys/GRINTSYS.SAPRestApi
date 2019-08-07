using System;

namespace GRINTSYS.SAPRestApi.Inputs
{
    public class SapSettingsInput
    {
        public String Server { get; set; }
        public String Companydb { get; set; }
        public String DbUserName { get; set; }
        public String DbPassword { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String LicenseServer { get; set; }
    }
}