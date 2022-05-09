using AliasManger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasManger.Models
{
    public class AmAlias
    {
        public int aliasID { get; set; }
        public string aliasName { get; set; }
        public string aliasAddress { get; set; }
        public int aliasStatusID { get; set; }
        public string AliasStatusName { get; set; }
        public string version { get; set; }
        public string caseUrl { get; set; }
        public string belegNummer { get; set; }
        public int licID { get; set; }
        public string APPID { get; set; }
        public string LicName { get; set; }
        public string hcmProfilId { get; set; }
        public string hcmProfilName { get; set; }

        public string AuthFullName { get; set; }
        public DateTime? AuthDate { get; set; }
        public bool WaitingForAuth { get; set; }

    }
    public class AmAliasProtocol : AmAlias
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int ActionID { get; set; }
        public string ActionName { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int InUserID { get; set; }
        public string InFullName { get; set; }
        public string InUserLogin { get; set; }

        public int AuthUserID { get; set; }
        public string AuthUserLogin { get; set; }
        public string Description { get; set; }
        public string AuthNote { get; set; }
    }

    public class AmAliasReport
    {
        public int aliasID { get; set; }
        public string aliasName { get; set; }
        public string aliasAddress { get; set; }
        public int licID { get; set; }
        public string LicName { get; set; }
        public string hcmProfilId { get; set; }
        public string hcmProfilName { get; set; }
        public int Acepted { get; set; }
        public int Ignored { get; set; }
    }

    public class AmAliasFilter
    {

        public string LicenseId { get; set; }
        public int StatusId { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }

        public bool WaitAuth { get; set; }
        public DateTime Von { get; set; } = DateTime.Today.AddDays(-1);
        public DateTime Bis { get; set; } = DateTime.Today;
        public bool AllTime { get; set; }
    }

    public class AmAliasStatusItem
    {
        public int StatusId;
        public string StatusName;
    }
}
