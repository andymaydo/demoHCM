using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;

namespace Domain.Models
{
    public class CMSProfileModel  : CMSProfileModelSimple
    {

        public List<Contact> profileParticipantsList 
        {
            get
            {
                //return ObjHelper.FromXml<List<Contact>>(profileParticipants, "ContactList");
                try
                {
                    return JsonSerializer.Deserialize<List<Contact>>(profileParticipantsJSON)
                        .Where(x => x.forEscalation == false).ToList();                    
                }
                catch (Exception ex)
                {
                    return new List<Contact>();
                }
               
                
            }
            set 
            {
                this.profileParticipants = value.ObjToXml("ContactList");
            }
        }
        public List<Contact> escalationUsersList
        {
            get
            {
                //return ObjHelper.FromXml<List<Contact>>(escalationUsers, "ContactList");
                try
                {
                    return JsonSerializer.Deserialize<List<Contact>>(profileParticipantsJSON)
                        .Where(x => x.forEscalation == true).ToList();
                }
                catch (Exception ex)
                {
                    return new List<Contact>();
                }
            }
            set
            {
                this.escalationUsers = value.ObjToXml("ContactList");
            }
        }
        public List<CaseRule> escalationRulesList
        {
            get
            {
                return ObjHelper.FromXml<List<CaseRule>>(escalationRules, "CaseRuleList");
            }
            set
            {
                this.escalationRules = value.ObjToXml("CaseRuleList");
            }
        }

        //public string? GetContactRole(int contactId, int caseStatusId)
        //{
        //    string? contactRole = null;

        //    contactRole = profileParticipantsList?.Find(x => x.ContactID == contactId)?.ProfileRole;
        //    //foreach (Contact c in profileParticipantsList)
        //    //{
        //    //    if (c.ContactID == contactId)
        //    //    {
        //    //        contactRole =  c.ProfileRole;
        //    //    }
        //    //}

        //    //Escalated Case
        //    if (caseStatusId == 6)
        //    {
        //        contactRole = escalationUsersList?.Find(x => x.ContactID == contactId)?.ProfileRole;
        //        //foreach (Contact c in escalationUsersList)
        //        //{
        //        //    if (c.ContactID == contactId)
        //        //    {
        //        //        contactRole = c.ProfileRole;
        //        //    }
        //        //}
        //    }

        //    return contactRole;
        //}


    }

    public class CMSProfileModelSimple
    {
        public int profileID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int profileStatusID { get; set; }
        public string profileStatus { get; set; }
        public int profileType { get; set; }

        public string profileName { get; set; }
        public string profilDescr { get; set; }
        public string NotificationLang { get; set; }

        public string profileParticipants { get; set; }
        public string profileParticipantsJSON { get; set; }
        public string escalationUsers { get; set; }
        public string escalationRules { get; set; }
        public bool NotifyAllProfileParticipants { get; set; }

        public string profileURI { get; set; }
        public string WebURL { get; set; }
        public string WebAuthenitcationType { get; set; }
        public int ModifiedBy { get; set; }
        public string ReasonToDelete { get; set; }

        public CMSProfileModelSimple() { }
    }
}
