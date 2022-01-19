using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Models
{
    public class UiMessages
    {
       
        public List<Message> List { get; set; }
        public UiMessages()
        {
            List = new List<Message>();
        }

        public void AddError(string shortMessage, string detailedMessage = null, bool showDetail = false)
        {
            List.Add(new Message { Short = shortMessage, Detail = detailedMessage, isError=true, showDetail=showDetail });            
        }
        public void AddInfo(string shortMessage, string detailedMessage = null, bool showDetail = false)
        {
            List.Add(new Message { Short = shortMessage, Detail = detailedMessage, isError = false, showDetail = showDetail });
        }
    }
    public class Message
    {
        public string Short { get; set; }
        public string Detail { get; set; }
        public bool isError { get; set; }
        public bool showDetail { get; set; }
    }
}
