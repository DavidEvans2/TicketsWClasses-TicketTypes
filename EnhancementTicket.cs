using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem
{
    class EnhancementTicket
    {
        public string ticketID { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priorityLevel { get; set; }
        public string submitter { get; set; }
        public string assignee { get; set; }
        public List<string> watching { get; set; }
        public string software { get; set; }
        public double ticketCost { get; set; }
        public string reason { get; set; }
        public string ticketEstimate { get; set; }

        public EnhancementTicket()
        {
            watching = new List<string>();
        }

        public string Display()
        {
            return $"Id: { ticketID}\nSummary: {summary}\nStatus: {status}\n" +
                $"Priority: {priorityLevel}\nSubmitter: {submitter}\nAssign To: {assignee}\nWatchers: {string.Join(", ", watching)}\nSoftware: {software}\n" +
                $"Cost: {ticketCost}\nReason: {reason}\nEstimate: {ticketEstimate}\n";
        }
    }
}
