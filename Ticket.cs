
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem
{
    class Ticket
    {
        public string ticketID { get; set; }
        public string priorityLevel { get; set; }
        public string summary { get; set; }
        public string submitter { get; set; }
        public string status { get; set; }
        public List<string> watching { get; set; }
        
        public string assignee { get; set; }
        

        public Ticket()
        {
            watching = new List<string>();
        }

        public string Display()
        {
            return $"Id: { ticketID}\nSummary: {summary}\nStatus: {status}\n" +
                $"Priority: {priorityLevel}\nSubmitter: {submitter}\nAssign To: {assignee}\nWatchers: {string.Join(", ", watching)}\n";
        }

        internal static void Add(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}